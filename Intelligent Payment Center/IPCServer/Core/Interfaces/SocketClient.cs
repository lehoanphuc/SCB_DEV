using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Utility;

namespace Interfaces
{
    public class SocketClient: IDisposable
    {
        #region Variable
        const int ReceiveTimeOut = 10000;
        const int BYTE_MSG_LEN = 2;
        const int BYTE_MSG_LEN_INIT = 1;
        IAsyncResult m_result;
        private AsyncCallback m_pfnCallBack;
        private Socket m_clientSocket;
        private class SocketPacket
        {
            public System.Net.Sockets.Socket thisSocket;
            public byte[] dataBuffer = new byte[BYTE_MSG_LEN_INIT];
        }

        private int ReceiveMsgLen = -1;
        private string ReceiveMsg = "";
        private string ErrorCode = Common.ERRORCODE.OK;
        #endregion

        #region Constructor
        public SocketClient()
        {

        }

        void IDisposable.Dispose()
        {
            DisconnectFromServer();
        }
        #endregion

        #region Private Function
        private string ConvertByteArrayToString(byte[] Data, int Length)
        {
            string result = "";
            try
            {
                //Convert To String use ASCII Decoder
                //char[] chars = new char[iRx];
                //System.Text.Decoder d = System.Text.Encoding.ASCII.GetDecoder();
                //int charLen = d.GetChars(socketData.dataBuffer, 0, iRx, chars, 0);
                //System.String szData = new System.String(chars);

                //Convert To String use extension ASCII
                for (int i = 0; i < Length && i < Data.Length; i++)
                {
                    result += Convert.ToChar(Data[i]);
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }

        private bool ConnectToServer(string IP, string Port)
        {
            try
            {
                // Create the socket instance
                m_clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // Cet the remote IP address
                IPAddress ip = IPAddress.Parse(IP);
                int iPortNo = System.Convert.ToInt16(Port);
                // Create the end point 
                IPEndPoint ipEnd = new IPEndPoint(ip, iPortNo);
                // Connect to the remote host
                m_clientSocket.Connect(ipEnd);
                if (m_clientSocket.Connected)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool WaitForData(SocketPacket theSocPkt)
        {
            try
            {
                if (m_pfnCallBack == null)
                {
                    m_pfnCallBack = new AsyncCallback(OnDataReceived);
                }
                theSocPkt.thisSocket = m_clientSocket;
                // Start listening to the data asynchronously
                m_result = m_clientSocket.BeginReceive(theSocPkt.dataBuffer,
                                                        0, theSocPkt.dataBuffer.Length,
                                                        SocketFlags.None,
                                                        m_pfnCallBack,
                                                        theSocPkt);
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }

        private void OnDataReceived(IAsyncResult asyn)
        {
            try
            {
                SocketPacket theSockId = (SocketPacket)asyn.AsyncState;
                int iRx = 0;
                try
                {
                    iRx = theSockId.thisSocket.EndReceive(asyn);
                }
                catch{}
                if (iRx > 0)
                {
                    System.String szData = ConvertByteArrayToString(theSockId.dataBuffer, iRx);
                    if (ReceiveMsgLen < 0)
                    {
                        ReceiveMsg += szData;
                        if (ReceiveMsg.Length >= BYTE_MSG_LEN)
                        {
                            if (ConvertStringToLen(ReceiveMsg.Substring(0, BYTE_MSG_LEN)))
                            {
                                theSockId.dataBuffer = new byte[ReceiveMsgLen];
                                ReceiveMsg = ReceiveMsg.Remove(0, BYTE_MSG_LEN);
                            }
                            else
                            {
                                ReceiveMsgLen = 0;
                                ErrorCode = Common.ERRORCODE.INVALID_MESSAGE_RESPONSE;
                                return;
                            }
                        }
                    }
                    else
                    {
                        ReceiveMsg += szData;
                        if (ReceiveMsg.Length >= ReceiveMsgLen)
                        {
                            ReceiveMsgLen = 0;
                            return;
                        }
                        else
                        {
                            theSockId.dataBuffer = new byte[ReceiveMsgLen - ReceiveMsg.Length];
                        }
                    }
                    WaitForData(theSockId);
                }
                else
                {
                    ReceiveMsgLen = 0;
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private void DisconnectFromServer()
        {
            try
            {
                if (m_clientSocket != null)
                {
                    m_clientSocket.Close();
                    m_clientSocket = null;
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private bool ConvertStringToLen(string strLen)
        {
            try
            {
                string Hex = "";
                for (int i = 0; i < strLen.Length; i++)
                {
                    char Temp = strLen.Substring(i, 1)[0];
                    Hex += String.Format("{0:x2}", (int)Temp);
                }
                ReceiveMsgLen = Convert.ToInt32(Hex, 16);
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }

        private string ConvertLenToString(string Data)
        {
            try
            {
                int Len = Data.Length;
                Data = "";
                string Hex = Convert.ToString(Len, 16).PadLeft(4, '0');
                for (int i = Hex.Length - 2; i >= -1; i = i - 2)
                {
                    string HexTemp;
                    if (i > -1)
                    {
                        HexTemp = Hex.Substring(i, 2);
                    }
                    else
                    {
                        HexTemp = Hex.Substring(0, 1);
                    }
                    Data = Convert.ToChar(Convert.ToInt64(HexTemp, 16)) + Data;
                }
                return Data;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return "\0\0";
            }
        }
        #endregion

        #region Public Function
        public bool SendMessage(TransactionInfo tran, string AutoReceive)
        {
            try
            {
                // Convert To Byte Array use ASCII Encoding
                //byte[] byData = System.Text.Encoding.ASCII.GetBytes(tran.InputData);
                string Data = ConvertLenToString(tran.OutputData) + tran.OutputData;
                // Convert To Byte Array use extension ASCII
                byte[] byData = new byte[Data.Length];
                for (int i = 0; i < Data.Length; i++)
                {
                    byData[i] = Convert.ToByte(Data.Substring(i, 1)[0]);
                }

                string ip = tran.Data[Common.KEYNAME.DESTID].ToString() + "IP";
                ip = Common.SYSVAR[ip].ToString();
                string port = tran.Data[Common.KEYNAME.DESTID].ToString() + "PORT";
                port = Common.SYSVAR[port].ToString();
                if (ConnectToServer(ip, port))
                {
                    m_clientSocket.Send(byData);
                    int timeout = 0;
                    if (AutoReceive == "true")
                    {
                        WaitForData(new SocketPacket());
                        while (ReceiveMsgLen != 0 && timeout < ReceiveTimeOut)
                        {
                            System.Threading.Thread.Sleep(10);
                            timeout += 10;
                        }
                        tran.InputData = ReceiveMsg;
                        if (timeout >= ReceiveTimeOut)
                        {
                            tran.SetErrorInfo(Common.ERRORCODE.MESSAGE_RECEIVE_TIMEOUT, "");
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                DisconnectFromServer();
            }
        }
        #endregion
    }
}
