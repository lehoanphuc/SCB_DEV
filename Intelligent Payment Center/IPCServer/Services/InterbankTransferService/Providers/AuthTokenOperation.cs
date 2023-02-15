using InterbankTransferService.Models;
using Newtonsoft.Json.Linq;
using Swashbuckle.Examples;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace InterbankTransferService.Providers
{
    public class AuthTokenOperation : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            string authenPath = Path.Combine("/", ConfigurationManager.AppSettings[Common.GetTokenPath].ToString());
            JObject jreq = Transactions.GetInfoFromJson("TokenBodyRequestExample") as JObject;
            JObject jres = Transactions.GetInfoFromJson("TokenResponseExample") as JObject;
            swaggerDoc.paths.Add(authenPath, new PathItem
            {
                post = new Operation
                {
                    operationId = "Get Token",
                    tags = new List<string> { "Authentication Method" },
                    consumes = new List<string>
                    {
                        "application/json"
                    },
                    parameters = new[] {
                        new Parameter
                        {
                            name = "body request",
                            @in = "body",
                            schema = schemaRegistry.GetOrRegister(typeof(CreateTokenBody)),
                            @default = jreq.ToString(),
                            required = true
                        },
                        new Parameter
                        {
                            name = "Authorization",
                            @in = "header",
                            required = true
                        }
                    },
                    responses = new Dictionary<string, Swashbuckle.Swagger.Response>()
                    {
                        { "200", new Swashbuckle.Swagger.Response
                            {
                                schema = schemaRegistry.GetOrRegister(typeof(ResponseAuthentication)),
                                examples = jres
                            }    
                        }
                    }
                },

            }); ;
        }
    }
}