using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace InterbankTransferService.Providers
{
    public sealed class CustomBodyMiddleware : OwinMiddleware
    {
        OwinMiddleware _next;
        public CustomBodyMiddleware(OwinMiddleware next) : base(next)
        {
            
        }

        public override async Task Invoke(IOwinContext context)
        {
            try
            {
                string urlPath = context.Request.Path.Value;

                if (Transactions.CheckURLPath(urlPath))
                {
                    string[] arrPath = urlPath.Split('/');
                    string exceptionMsg = string.Empty;
                    string responseBody = string.Empty;

                    HttpResponse httpResponse = HttpContext.Current.Response;
                    StreamHelper outputCapture = new StreamHelper(httpResponse.Filter);
                    httpResponse.Filter = outputCapture;
                    IOwinResponse owinResponse = context.Response;
                    Stream owinResponseStream = owinResponse.Body;
                    owinResponse.Body = new MemoryStream();

                    if (Transactions.IsAuthenAPI(urlPath))
                    {
                        try
                        {
                            await ReplaceJsonBodyWithUrlEncodedBody(context);
                        }
                        catch (Exception ex)
                        {
                            Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                        }
                        try
                        {
                            await Next.Invoke(context);
                        }
                        catch (Exception ex)
                        {
                            exceptionMsg = Common.SystemError;
                            Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                        }

                        responseBody = await Transactions.GetBodyString(outputCapture, owinResponse, owinResponseStream);

                        if (!string.IsNullOrEmpty(responseBody))
                        {
                            try
                            {
                                JObject joldRes = JObject.Parse(responseBody);
                                if (joldRes != null)
                                {
                                    string errMsg = string.Empty;
                                    try
                                    {
                                        errMsg = joldRes.SelectToken("error").Value<string>();
                                        if (!string.IsNullOrEmpty(errMsg))
                                        {
                                            Transactions.ResponseAuthenFailed(HttpContext.Current.Response, errMsg);
                                        }
                                        else
                                        {
                                            Transactions.ResponseAuthenFailed(HttpContext.Current.Response, Common.SystemError);
                                        }
                                    }
                                    catch { }
                                }
                            }
                            catch
                            {
                                Transactions.ResponseAuthenFailed(HttpContext.Current.Response, Common.SystemError);
                                exceptionMsg = Common.SystemError;
                            }
                            return;
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        try
                        {
                            await Next.Invoke(context);
                        }
                        catch (Exception ex)
                        {
                            Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                            exceptionMsg = Common.SystemError;
                        }
                    }

                    if (!string.IsNullOrEmpty(exceptionMsg))
                    {
                        Transactions.ReturnErrDefault(exceptionMsg);
                        return;
                    }

                    responseBody = await Transactions.GetBodyString(outputCapture, owinResponse, owinResponseStream);

                    if (string.IsNullOrEmpty(responseBody))
                    {
                        Transactions.ReturnErrDefault(Common.SystemError, (int)HttpStatusCode.NotFound);
                        return;
                    }
                }
                else
                {
                    await Next.Invoke(context);
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private async Task ReplaceJsonBodyWithUrlEncodedBody(IOwinContext context)
        {
            var requestParams = await GetFormCollectionFromJsonBody(context);
            var urlEncodedParams = string.Join("&", requestParams.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            var decryptedContent = new StringContent(urlEncodedParams, Encoding.UTF8, Common.urlencodedtype);
            var requestStream = await decryptedContent.ReadAsStreamAsync();
            context.Request.Body = requestStream;
        }

        private async Task<Dictionary<string, string>> GetFormCollectionFromJsonBody(IOwinContext context)
        {
            context.Request.Body.Position = 0;
            var jsonString = await new StreamReader(context.Request.Body).ReadToEndAsync();
            var requestParams = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
            return requestParams;
        }
    }

    public class StreamHelper : Stream
    {
        private Stream InnerStream;
        public MemoryStream CapturedData { get; private set; }

        public StreamHelper(Stream inner)
        {
            InnerStream = inner;
            CapturedData = new MemoryStream();
        }

        public override bool CanRead
        {
            get { return InnerStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return InnerStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return InnerStream.CanWrite; }
        }

        public override void Flush()
        {
            InnerStream.Flush();
        }

        public override long Length
        {
            get { return InnerStream.Length; }
        }

        public override long Position
        {
            get { return InnerStream.Position; }
            set { CapturedData.Position = InnerStream.Position = value; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return InnerStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            CapturedData.Seek(offset, origin);
            return InnerStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            CapturedData.SetLength(value);
            InnerStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            CapturedData.Write(buffer, offset, count);
            InnerStream.Write(buffer, offset, count);
        }
    }
}