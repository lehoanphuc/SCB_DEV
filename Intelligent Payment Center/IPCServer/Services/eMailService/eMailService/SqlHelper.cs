using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace eMailService
{
    public class SqlHelper
    {
        public DataSet FillDataSetSQL(string p_strCon, string p_strSQLCommand)
        {
            //bool includeReturnValueParameter = true;
            DataSet p_dsData = new DataSet();
            SqlConnection conn = new SqlConnection(p_strCon);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                PrepareCommandSQL(cmd, conn, (SqlTransaction)null, p_strSQLCommand);
                da.Fill(p_dsData);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                cmd.Dispose();
                da.Dispose();
                p_dsData.Dispose();
            }

            return p_dsData;
        }
        private void PrepareCommandSQL(SqlCommand p_cmd, SqlConnection p_conn,
            SqlTransaction p_trans, string p_strSQLCommand)
        {
            //if the provided connection is not open, we will open it
            if (p_conn.State != ConnectionState.Open)
            {
                p_conn.Open();
            }

            //associate the connection with the command
            p_cmd.Connection = p_conn;

            //set the command text (SQL statement)
            p_cmd.CommandText = p_strSQLCommand;

            //if we were provided a transaction, assign it.
            if (p_trans != null)
            {
                p_cmd.Transaction = p_trans;
            }

            //set the command type
            p_cmd.CommandType = CommandType.Text;
        }
    }
}
