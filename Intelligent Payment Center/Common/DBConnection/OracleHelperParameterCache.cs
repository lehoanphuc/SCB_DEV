using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.OracleClient;
using System.Data;

namespace DBConnection
{
    //*********************************************************************
    //
    // OracleHelperParameterCache provides functions to leverage a static cache of procedure parameters, and the
    // ability to discover parameters for stored procedures at run-time.
    //
    //*********************************************************************
    public sealed class OracleHelperParameterCache
    {
        //*********************************************************************
        //
        // Since this class provides only static methods, make the default constructor private to prevent 
        // instances from being created with "new SqlHelperParameterCache()".
        //
        //*********************************************************************

        public OracleHelperParameterCache() { }

        private Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

        //*********************************************************************
        //
        // resolve at run time the appropriate set of SqlParameters for a stored procedure
        // 
        // param name="connectionString" a valid connection string for a SqlConnection 
        // param name="spName" the name of the stored procedure 
        // param name="includeReturnValueParameter" whether or not to include their return value parameter 
        //
        //*********************************************************************

        private OracleParameter[] DiscoverSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
        {
            OracleConnection cn = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand(spName, cn);
            OracleParameter[] discoveredParameters;

            try
            {
                cn.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                OracleCommandBuilder.DeriveParameters(cmd);

                //if (!includeReturnValueParameter)
                //{
                //    cmd.Parameters.RemoveAt(0);
                //}

                discoveredParameters = new OracleParameter[cmd.Parameters.Count];

                cmd.Parameters.CopyTo(discoveredParameters, 0);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                cn.Close();
                cmd.Dispose();
            }

            return discoveredParameters;
        }

        private OracleParameter[] CloneParameters(OracleParameter[] originalParameters)
        {
            //deep copy of cached SqlParameter array
            OracleParameter[] clonedParameters = new OracleParameter[originalParameters.Length];

            for (int i = 0, j = originalParameters.Length; i < j; i++)
            {
                clonedParameters[i] = (OracleParameter)((ICloneable)originalParameters[i]).Clone();
            }

            return clonedParameters;
        }

        //*********************************************************************
        //
        // add parameter array to the cache
        //
        // param name="connectionString" a valid connection string for a SqlConnection 
        // param name="commandText" the stored procedure name or T-SQL command 
        // param name="commandParameters" an array of SqlParamters to be cached 
        //
        //*********************************************************************

        public void CacheParameterSet(string connectionString, string commandText, params OracleParameter[] commandParameters)
        {
            string hashKey = connectionString + ":" + commandText;

            paramCache[hashKey] = commandParameters;
        }

        //*********************************************************************
        //
        // Retrieve a parameter array from the cache
        // 
        // param name="connectionString" a valid connection string for a SqlConnection 
        // param name="commandText" the stored procedure name or T-SQL command 
        // returns an array of SqlParamters
        //
        //*********************************************************************

        public OracleParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            string hashKey = connectionString + ":" + commandText;

            OracleParameter[] cachedParameters = (OracleParameter[])paramCache[hashKey];

            if (cachedParameters == null)
            {
                return null;
            }
            else
            {
                return CloneParameters(cachedParameters);
            }
        }

        //*********************************************************************
        //
        // Retrieves the set of SqlParameters appropriate for the stored procedure
        // 
        // This method will query the database for this information, and then store it in a cache for future requests.
        // 
        // param name="connectionString" a valid connection string for a SqlConnection 
        // param name="spName" the name of the stored procedure 
        // returns an array of SqlParameters
        //
        //*********************************************************************

        public OracleParameter[] GetSpParameterSet(string connectionString, string spName)
        {
            return GetSpParameterSet(connectionString, spName, false);
        }

        //*********************************************************************
        //
        // Retrieves the set of SqlParameters appropriate for the stored procedure
        // 
        // This method will query the database for this information, and then store it in a cache for future requests.
        // 
        // param name="connectionString" a valid connection string for a SqlConnection 
        // param name="spName" the name of the stored procedure 
        // param name="includeReturnValueParameter" a bool value indicating whether the return value parameter should be included in the results 
        // returns an array of SqlParameters
        //
        //*********************************************************************

        public OracleParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
        {
            //string hashKey = connectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");
            string hashKey = connectionString + ":" + spName + ":include ReturnValue Parameter";

            OracleParameter[] cachedParameters;

            cachedParameters = (OracleParameter[])paramCache[hashKey];

            if (cachedParameters == null)
            {
                cachedParameters = (OracleParameter[])(paramCache[hashKey] = DiscoverSpParameterSet(connectionString, spName, includeReturnValueParameter));
            }

            return CloneParameters(cachedParameters);
        }
    }
}
