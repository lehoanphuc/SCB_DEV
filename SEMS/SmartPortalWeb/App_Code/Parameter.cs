using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SmartPortal.ExceptionCollection;

/// <summary>
/// Summary description for Parameter
/// </summary>
public class Parameter
{
    private string IPCERRORCODE = string.Empty;
    private string IPCERRORDESC = string.Empty;
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    public Parameter()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _service.GetPara("WAL", string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        switch (row.Field<string>("PAR_NAME"))
                        {
                            case "CONSUMER":
                                SmartPortal.Constant.IPC.PARAMETER.CONSUMER = row.Field<string>("PAR_VALUE");
                                break;
                            case "MERCHANT-AGENT":
                                SmartPortal.Constant.IPC.PARAMETER.MERCHANT_AGENT = row.Field<string>("PAR_VALUE");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
            {
                throw new IPCException(IPCERRORCODE);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}