using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using SmartPortal.DAL;
using System.Data.SqlClient;

namespace SmartPortal.SEMS
{
   public class RequestForLoan
    {

        public DataTable LoadFileForDocument(string documentcode, string documenttype)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@DOCUMENTCODE";
            p1.Value = documentcode;
            p1.SqlDbType = SqlDbType.Text;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@DOCUMENTTYPE";
            p2.Value = documenttype;
            p2.SqlDbType = SqlDbType.Text;

            iRead = DataAccess.GetFromDataTable("ViewImgDoc", p1,p2);

            return iRead;
        }
    }
}
