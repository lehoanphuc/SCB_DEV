using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Collections;
using System.Data;
using System.Configuration;


namespace DBConnection
{
 
    public class OracleHelper 
    {
        #region Public Method
        /// <summary>
        /// Execute Procedure with Input Paramter, Return RecordSet
        /// </summary>
        /// <param name="p_strSPname">Store Name</param>
        /// <param name="p_arrParameter">Input Parameter List</param>
        /// <returns>object</returns>
        [STAThread]
        public object ExecuteScalar(string p_strConnStr, string p_strSPname, params object[] p_arrValue)
        {
            bool includeReturnValueParameter = true;
            if ((p_arrValue != null) && (p_arrValue.Length > 0))
            {
                // Create OracleParameter
                OracleHelperParameterCache objOHPC = new OracleHelperParameterCache();
                OracleParameter[] arrSQLParameter = objOHPC.GetSpParameterSet(p_strConnStr, p_strSPname, includeReturnValueParameter);

                // Assign Parameter Value to OracleParameter
                AssignParameterValues(arrSQLParameter, p_arrValue, includeReturnValueParameter);

                // Execute Override Method
                //return ExecuteScalar(p_strConnStr, p_strSPname, arrSQLParameter);
                DataTable p_dtData = new DataTable();
                return ExecuteScalar(p_strConnStr, p_dtData, p_strSPname, arrSQLParameter);
            }

            else
            {
                return ExecuteScalar(p_strConnStr, p_strSPname, null);
            }
        }
        /// <summary>
        /// Execute Non Query  Procedure
        /// </summary>
        /// <param name="p_strSPname">ProcName</param>
        /// <param name="p_arrParameter">Parameter List</param>
        [STAThread]
        public int ExecuteNonquery(string p_strConnStr, string p_strSPname, params object[] p_arrValue)
        {
            bool includeReturnValueParameter = false;
            if ((p_arrValue != null) && (p_arrValue.Length > 0))
            {
                OracleHelperParameterCache objOHPC = new OracleHelperParameterCache();
                // Tạo danh sách SqlParameter
                OracleParameter[] arrSQLParameter = objOHPC.GetSpParameterSet(
                    p_strConnStr, p_strSPname, includeReturnValueParameter);

                // Gán dữ liệu từ các mãng value vô mảng command parameter
                AssignParameterValues(arrSQLParameter, p_arrValue, includeReturnValueParameter);

                // gọi hàm overload
                return ExecuteNonQuery(p_strConnStr, p_strSPname, arrSQLParameter);
            }

            else
            {
                return ExecuteNonQuery(p_strSPname, null);
            }
        }

        /// <summary>
        /// Execute Non Query  Procedure
        /// </summary>
        /// <param name="p_strSQLCommand">Câu lệnh SQL truyền vào</param>
        [STAThread]
        public int ExecuteNonquerySQL(string p_strConnStr, string p_strSQLCommand)
        {
            //bool includeReturnValueParameter = true;

            OracleConnection conn = new OracleConnection(p_strConnStr);
            OracleCommand cmd = new OracleCommand();
            int result = -5;

            try
            {
                PrepareCommandSQL(cmd, conn, (OracleTransaction)null, p_strSQLCommand);
                result = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
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
            }

            return result;
        }

        /// <summary>
        /// Fill dữ liệu vào dataset dựa trên store name và danh sách parameter truyền vào
        /// </summary>
        /// <param name="p_dsData">DataSet cần truyền vào</param>
        /// <param name="p_strSPname">Store Name</param>
        /// <param name="p_arrParameter">Danh sách các tham số</param>
        [STAThread]
        public void FillDataSet(string p_strConnStr, DataSet p_dsData, string p_strSPname,
            params object[] p_arrValue)
        {
            bool includeReturnValueParameter = true;
            if ((p_arrValue != null) && (p_arrValue.Length > 0))
            {
                OracleHelperParameterCache objOHPC = new OracleHelperParameterCache();
                // Tạo danh sách SqlParameter
                OracleParameter[] arrSQLParameter = objOHPC.GetSpParameterSet(
                    p_strConnStr, p_strSPname, includeReturnValueParameter);

                // Gán dữ liệu từ các mãng value vô mảng command parameter
                AssignParameterValues(arrSQLParameter, p_arrValue, includeReturnValueParameter);

                // gọi hàm overload
                FillDataSet(p_strConnStr, p_dsData, p_strSPname, arrSQLParameter);
            }

            else
            {
                FillDataSet(p_strConnStr, p_dsData, p_strSPname, null);
            }
        }

        /// <summary>
        /// Fill dữ liệu vào dataset dựa trên câu lệnh SQL truyền vào
        /// </summary>
        /// <param name="p_dsData">DataSet cần truyền vào</param>
        /// <param name="p_strSQLCommand">Câu lệnh SQL</param>
        [STAThread]
        public void FillDataSetSQL(string p_strConnStr, DataSet p_dsData, string p_strSQLCommand)
        {
            //bool includeReturnValueParameter = true;

            OracleConnection conn = new OracleConnection(p_strConnStr);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da = new OracleDataAdapter(cmd);

            try
            {
                PrepareCommandSQL(cmd, conn, (OracleTransaction)null, p_strSQLCommand);
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
            }
        }

        /// <summary>
        /// Fill dữ liệu vô datatable dựa trên store name và danh sách parammeter truyền vào
        /// </summary>
        /// <param name="p_dtData">DataTable cần thêm dữ liệu vào</param>
        /// <param name="p_strSPname">Store Name</param>
        /// <param name="p_arrParameter">Danh sách các tham số</param>
        [STAThread]
        public void FillDataTable(string p_strConnStr, DataTable p_dtData, string p_strSPname, params object[] p_arrValue)
        {
            bool includeReturnValueParameter = true;
            if ((p_arrValue != null) && (p_arrValue.Length > 0))
            {
                // Tạo danh sách SqlParameter
                OracleHelperParameterCache objOHPC = new OracleHelperParameterCache();
                OracleParameter[] arrSQLParameter = objOHPC.GetSpParameterSet(
                    p_strConnStr, p_strSPname, includeReturnValueParameter);

                // Gán dữ liệu từ các mãng value vô mảng command parameter
                AssignParameterValues(arrSQLParameter, p_arrValue, includeReturnValueParameter);

                // gọi hàm overload
                FillDataTable(p_strConnStr, p_dtData, p_strSPname, arrSQLParameter);
            }

            else
            {
                FillDataTable(p_strConnStr, p_dtData, p_strSPname, null);
            }
        }

        /// <summary>
        /// Fill dữ liệu vô datatable dựa trên store name và danh sách parammeter truyền vào
        /// </summary>
        /// <param name="p_dtData">DataTable cần thêm dữ liệu vào</param>
        /// <param name="p_strSPname">Store Name</param>
        [STAThread]
        public void FillDataTableSQL(string p_strConnStr, DataTable p_dtData, string p_strSQLCommand)
        {
            //bool includeReturnValueParameter = true;

            OracleConnection conn = new OracleConnection(p_strConnStr);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da = new OracleDataAdapter(cmd);

            try
            {
                PrepareCommandSQL(cmd, conn, (OracleTransaction)null, p_strSQLCommand);
                da.Fill(p_dtData);
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
            }
        }
        #endregion

        #region Private Method
        /// <summary>
        /// Thực thi 1 store ở dạng scalar
        /// </summary>
        /// <param name="p_strStoreName">Store Name</param>
        /// <param name="p_arrSQLParameter">Danh sách tham số</param>
        /// <returns></returns>
        private object ExecuteScalar(string p_strConnStr, string p_strStoreName, params OracleParameter[] p_arrSQLParameter)
        {
            OracleConnection conn = new OracleConnection(p_strConnStr);
            OracleCommand cmd = new OracleCommand();
            object result = null;
            try
            {
                PrepareCommand(cmd, conn, (OracleTransaction)null, p_strStoreName, p_arrSQLParameter);
                // Execute Sql Command
                result = cmd.ExecuteOracleScalar();
                cmd.Parameters.Clear();
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
            }

            return result;
        }

        /// <summary>
        /// Thực thi 1 store ở dạng DataTable nhưng trả về giá trị là 1 dòng 1 cột.
        /// </summary>
        /// <param name="p_strStoreName">Store Name</param>
        /// <param name="p_arrSQLParameter">Danh sách tham số</param>
        /// <returns></returns>
        private object ExecuteScalar(string p_strConnStr, DataTable p_dtData, string p_strStoreName, params OracleParameter[] p_arrSQLParameter)
        {
            OracleConnection conn = new OracleConnection(p_strConnStr);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            object result = null;
            try
            {
                PrepareCommand(cmd, conn, (OracleTransaction)null, p_strStoreName, p_arrSQLParameter);
                da.Fill(p_dtData);
                foreach (DataRow row in p_dtData.Rows)
                {
                    result = row[0];
                }
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
            }

            return result;
        }

        /// <summary>
        /// Execute Procedure Nonquery
        /// </summary>
        /// <param name="p_strStoreName">Procedure Name</param>
        /// <param name="p_arrSQLParameter">Parameter List</param>
        /// <returns></returns>
        private int ExecuteNonQuery(string p_strConnStr, string p_strStoreName, params OracleParameter[] p_arrSQLParameter)
        {
            OracleConnection conn = new OracleConnection(p_strConnStr);
            OracleCommand cmd = new OracleCommand();
            int result = -5;

            try
            {
                PrepareCommand(cmd, conn, (OracleTransaction)null, p_strStoreName, p_arrSQLParameter);

                // Execute Sql Command
                result = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
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
            }

            return result;
        }
        /// <summary>
        /// Đưa dữ liệu vô dataset
        /// </summary>
        /// <param name="p_dsData">DataSet cần đưa dữ liệu vô</param>
        /// <param name="p_strStoreName">Store Name</param>
        /// <param name="p_arrSQLParameter">Danh sách parameyet</param>
        private void FillDataSet(string p_strConnStr, DataSet p_dsData, string p_strStoreName, params OracleParameter[] p_arrSQLParameter)
        {
            OracleConnection conn = new OracleConnection(p_strConnStr);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da = new OracleDataAdapter(cmd);

            try
            {
                PrepareCommand(cmd, conn, (OracleTransaction)null, p_strStoreName, p_arrSQLParameter);
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
            }
        }
        /// <summary>
        /// Đưa dữ liệu vô DataTable
        /// </summary>
        /// <param name="p_dtData">DataTable cần đưa dữ liệu vô</param>
        /// <param name="p_strStoreName">Store Name</param>
        /// <param name="p_arrSQLParameter">Danh sách parameyet</param>
        private void FillDataTable(string p_strConnStr, DataTable p_dtData, string p_strStoreName, params OracleParameter[] p_arrSQLParameter)
        {
            OracleConnection conn = new OracleConnection(p_strConnStr);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da = new OracleDataAdapter(cmd);

            try
            {
                PrepareCommand(cmd, conn, (OracleTransaction)null, p_strStoreName, p_arrSQLParameter);
                da.Fill(p_dtData);
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
            }
        }
        /// <summary>
        /// Assign Value from Object to Oracle Parameter
        /// </summary>
        /// <param name="commandParameters"></param>
        /// <param name="parameterValues"></param>
        private void AssignParameterValues(OracleParameter[] p_arrSQLParameter, object[] p_arrValue, bool includeReturnValueParameter)
        {
            if ((p_arrSQLParameter == null) || (p_arrValue == null))
            {
                return;
            }
            int ProParameterLength = 0;
            //Oracle Parameter have return parameter -> Parameter Input + 1 = Procedure Paramter
            if (includeReturnValueParameter == true)
            {
                ProParameterLength = p_arrValue.Length + 1;
            }
            else
            {
                ProParameterLength = p_arrValue.Length;
            }
            if (p_arrSQLParameter.Length != (ProParameterLength))
            {
                throw new Exception("Parameter count does not match Parameter Value count.");
            }

            for (int i = 0, j = p_arrValue.Length; i < j; i++)
            {
                p_arrSQLParameter[i].Value = p_arrValue[i];
            }
        }

        /// <summary>
        /// Prepare Command for Execute
        /// </summary>
        /// <param name="p_cmd">SqlCommand</param>
        /// <param name="p_conn">Connection</param>
        /// <param name="p_trans">Sql Transaction</param>
        /// <param name="p_strSPName">Store Name</param>
        /// <param name="p_arrSQLParameter">Sql Parameter</param>
        private void PrepareCommand(OracleCommand p_cmd, OracleConnection p_conn,
            OracleTransaction p_trans, string p_strSPName, OracleParameter[] p_arrSQLParameter)
        {
            //if the provided connection is not open, we will open it
            if (p_conn.State != ConnectionState.Open)
            {
                p_conn.Open();
            }

            //associate the connection with the command
            p_cmd.Connection = p_conn;

            //set the command text (stored procedure name or SQL statement)
            p_cmd.CommandText = p_strSPName;

            //if we were provided a transaction, assign it.
            if (p_trans != null)
            {
                p_cmd.Transaction = p_trans;
            }

            //set the command type
            p_cmd.CommandType = CommandType.StoredProcedure;

            //attach the command parameters if they are provided
            if (p_arrSQLParameter != null)
            {
                AttachParameters(p_cmd, p_arrSQLParameter);
            }
        }

        /// <summary>
        /// Prepare Command for Execute
        /// </summary>
        /// <param name="p_cmd">SqlCommand</param>
        /// <param name="p_conn">Connection</param>
        /// <param name="p_trans">Sql Transaction</param>
        /// <param name="p_strSPName">SQL Statement</param>
        private void PrepareCommandSQL(OracleCommand p_cmd, OracleConnection p_conn,
            OracleTransaction p_trans, string p_strSQLCommand)
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

        /// <summary>
        /// Attach các parameter vô OracleCommand
        /// </summary>
        /// <param name="p_cmd">SqlCommand</param>
        /// <param name="p_arrSQLParameter">Danh sách tham số</param>
        private void AttachParameters(OracleCommand p_cmd, OracleParameter[] p_arrSQLParameter)
        {
            foreach (OracleParameter p in p_arrSQLParameter)
            {
                if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                {
                    p.Value = DBNull.Value;
                }

                p_cmd.Parameters.Add(p);
            }
        }
        #endregion

    }
}
