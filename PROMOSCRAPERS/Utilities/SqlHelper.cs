using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Collections.Generic;

namespace Utilities
{
    /// <summary>
    /// Summary description for SqlHelper
    /// </summary>
    public static class SqlHelper
    {        
        private static int UseDefaultCommandTimeout = -111; //This will avoid the use of CommandTimeout property


        #region ExecuteReader
        public static SqlDataReader ExecuteReader(string ConnectionString, CommandType Type, string CommandText)
        {
            SqlDataReader dataReader = ExecuteReader(ConnectionString, Type, CommandText, null, UseDefaultCommandTimeout, false);
            return dataReader;
        }

        public static SqlDataReader ExecuteReader(string ConnectionString, CommandType Type, string CommandText, SqlParameter[] parameters)
        {            
            SqlDataReader dataReader = ExecuteReader(ConnectionString, Type, CommandText, parameters, UseDefaultCommandTimeout, false);
            return dataReader;
        }

        public static SqlDataReader ExecuteReader(string ConnectionString, CommandType Type, string CommandText, SqlParameter[] parameters, int CommandTimeout)
        {
            SqlDataReader dataReader = ExecuteReader(ConnectionString, Type, CommandText, parameters, CommandTimeout, false);
            return dataReader;
        }

        

        

        public static SqlDataReader ExecuteReader(string ConnectionString, CommandType Type, string CommandText, SqlParameter[] parameters, bool DoNotPassNullParamtersToProcedure)
        {
            SqlDataReader dataReader = ExecuteReader(ConnectionString, Type, CommandText, parameters, UseDefaultCommandTimeout, DoNotPassNullParamtersToProcedure);
            return dataReader;
        }

        public static SqlDataReader ExecuteReader(string ConnectionString, CommandType Type, string CommandText, SqlParameter[] parameters, int CommandTimeout, bool DoNotPassNullParamtersToProcedure)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dataReader = null;

            try
            {                
                connection = GetConnection(ConnectionString);
                command = GetCommand(connection, Type, CommandText, parameters, CommandTimeout, DoNotPassNullParamtersToProcedure);
                dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (DoNotPassNullParamtersToProcedure) { RefillOutParameters(ref parameters, command.Parameters); }
            }
            finally
            {
                //if (command != null) command.Dispose();
                //CloseConnection(connection);
            }

            return dataReader;
        }
        #endregion


        #region ExecuteDataset

       

        

        public static DataSet ExecuteDataset(string ConnectionString, CommandType Type, string CommandText)
        {
            return ExecuteDataset(ConnectionString, Type, CommandText, null, UseDefaultCommandTimeout, false);
        }
        
        public static DataSet ExecuteDataset(string ConnectionString, CommandType Type, string CommandText, SqlParameter[] parameters)
        {
            return ExecuteDataset(ConnectionString, Type, CommandText, parameters, UseDefaultCommandTimeout, false);
        }

        public static DataSet ExecuteDataset(string ConnectionString, CommandType Type, string CommandText, SqlParameter[] parameters, int CommandTimeout)
        {
            return ExecuteDataset(ConnectionString, Type, CommandText, parameters, CommandTimeout, false);
        }

        public static DataSet ExecuteDataset(string ConnectionString, CommandType Type, string CommandText, SqlParameter[] parameters, bool DoNotPassNullParamtersToProcedure)
        {
            return ExecuteDataset(ConnectionString, Type, CommandText, parameters, UseDefaultCommandTimeout, DoNotPassNullParamtersToProcedure);
        }

        

        public static DataSet ExecuteDataset(string ConnectionString, CommandType Type, string CommandText, SqlParameter[] parameters, int CommandTimeout, bool DoNotPassNullParamtersToProcedure)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataAdapter dataAdapter = null;
            DataSet dataSet = null;

            try
            {             
                connection = GetConnection(ConnectionString);
                command = GetCommand(connection, Type, CommandText, parameters, CommandTimeout, DoNotPassNullParamtersToProcedure);

                dataAdapter = new SqlDataAdapter(command);
                dataSet = new DataSet(); 
                dataAdapter.Fill(dataSet);

                if (DoNotPassNullParamtersToProcedure) { RefillOutParameters(ref parameters, command.Parameters); }
            }
            finally
            {
                if(dataAdapter != null) dataAdapter.Dispose();
                if (command != null) command.Dispose();
                CloseConnection(connection);
            }

            return dataSet;
        }
        #endregion


        #region ExecuteScalar
        

        

        public static object ExecuteScalar(string ConnectionString, CommandType Type, string CommandText)
        {
            object returnValue = ExecuteScalar(ConnectionString, Type, CommandText, null);
            return returnValue;
        }

        public static object ExecuteScalar(string ConnectionString, CommandType Type, string CommandText, SqlParameter[] parameters)
        {
            object returnValue = ExecuteScalar(ConnectionString, Type, CommandText, parameters, UseDefaultCommandTimeout, false);
            return returnValue;
        }

        public static object ExecuteScalar(string ConnectionString, CommandType Type, string CommandText, SqlParameter[] parameters, int CommandTimeout)
        {
            object returnValue = ExecuteScalar(ConnectionString, Type, CommandText, parameters, CommandTimeout, false);
            return returnValue;
        }

        public static object ExecuteScalar(string ConnectionString, CommandType Type, string CommandText, SqlParameter[] parameters, bool DoNotPassNullParamtersToProcedure)
        {
            object returnValue = ExecuteScalar(ConnectionString, Type, CommandText, parameters, UseDefaultCommandTimeout, DoNotPassNullParamtersToProcedure);
            return returnValue;
        }


        public static object ExecuteScalar(string ConnectionString, CommandType Type, string CommandText, SqlParameter[] parameters, int CommandTimeout, bool DoNotPassNullParamtersToProcedure)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            object returnValue = null;

            try
            {
                connection = GetConnection(ConnectionString);
                command = GetCommand(connection, Type, CommandText, parameters, CommandTimeout, DoNotPassNullParamtersToProcedure);
                returnValue = command.ExecuteScalar();

                if (DoNotPassNullParamtersToProcedure) { RefillOutParameters(ref parameters, command.Parameters); }
            }
            finally
            {
                if (command != null) command.Dispose();
                CloseConnection(connection);
            }

            return returnValue;
        }

        public static object ExecuteScalar(SqlTransaction sqlTransaction, CommandType commandType, 
                                                string commandText, SqlParameter[] parameters, 
                                                bool DoNotPassNullParamtersToProcedure)
        {
            SqlCommand command = null;
            object returnValue = null;

            try
            {
                command = GetCommand(sqlTransaction.Connection, commandType, commandText, parameters, UseDefaultCommandTimeout, DoNotPassNullParamtersToProcedure);
                command.Transaction = sqlTransaction;
                returnValue = command.ExecuteScalar();

                if (DoNotPassNullParamtersToProcedure) { RefillOutParameters(ref parameters, command.Parameters); }
            }
            finally
            {
                if (command != null) command.Dispose();
            }

            return returnValue;
        }

        #endregion


        #region ExecuteNonQuery
        public static void ExecuteNonQuery(string ConnectionString, CommandType Type, string CommandText)
        {
            ExecuteNonQuery(ConnectionString, Type, CommandText, null, UseDefaultCommandTimeout, false);
        }

        public static void ExecuteNonQuery(string ConnectionString, CommandType Type, string CommandText, SqlParameter[] parameters)
        {
            ExecuteNonQuery(ConnectionString, Type, CommandText, parameters, UseDefaultCommandTimeout, false);
        }

        /// <summary>
        /// ExecuteNonQuery with exception handling and optional trace capability
        /// </summary>
        /// <param name="traceEnabled">
        /// If set to true, the method will 
        /// write trace information to the listner configured for the calling
        /// applicaiton
        /// </param>
        /// <param name="userName">
        /// used as additional information added to 
        /// any exception before bubbling up
        /// </param>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"> 
        /// </param>
        

        public static void ExecuteNonQuery(string ConnectionString, CommandType Type, string CommandText, SqlParameter[] parameters, int CommandTimeout)
        {
            ExecuteNonQuery(ConnectionString, Type, CommandText, parameters, CommandTimeout, false);
        }

        public static void ExecuteNonQuery(string ConnectionString, CommandType Type, string CommandText, SqlParameter[] parameters, bool DoNotPassNullParamtersToProcedure)
        {
            ExecuteNonQuery(ConnectionString, Type, CommandText, parameters, UseDefaultCommandTimeout, DoNotPassNullParamtersToProcedure);
        }

        public static void ExecuteNonQuery(string ConnectionString, CommandType Type, string CommandText, SqlParameter[] parameters, int CommandTimeout, bool DoNotPassNullParamtersToProcedure)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = GetConnection(ConnectionString);
                command = GetCommand(connection, Type, CommandText, parameters, CommandTimeout, DoNotPassNullParamtersToProcedure);
                command.ExecuteNonQuery();

                if (DoNotPassNullParamtersToProcedure) { RefillOutParameters(ref parameters, command.Parameters); }
            }
            finally
            {
                if (command != null) command.Dispose();
                CloseConnection(connection);
            }
        }

        public static void ExecuteNonQuery(SqlTransaction sqlTransaction, CommandType commandType, string commandText, SqlParameter[] parameters, bool DoNotPassNullParamtersToProcedure)
        {
            SqlCommand command = null;

            try
            {
                command = GetCommand(sqlTransaction.Connection, commandType, commandText, parameters, UseDefaultCommandTimeout, DoNotPassNullParamtersToProcedure);
                command.Transaction = sqlTransaction;
                command.ExecuteNonQuery();

                if (DoNotPassNullParamtersToProcedure) { RefillOutParameters(ref parameters, command.Parameters); }
            }
            finally
            {
                if (command != null) command.Dispose();
            }
        }



        #endregion 
 

        #region command        

        

        private static SqlCommand GetCommand(SqlConnection connection, CommandType Type, string CommandText, SqlParameter[] parameters, int CommandTimeout, bool DoNotPassNullParamtersToProcedure)
        {
            SqlCommand command = null;

            try
            {
                command = new SqlCommand(CommandText, connection);
                command.CommandType = Type;
                if (CommandTimeout > 0) { command.CommandTimeout = CommandTimeout; } //Default is 30 seconds. A value of 0 indicates no limit, and should be avoided in a CommandTimeout because an attempt to execute a command will wait indefinitely.                
                if (parameters != null && parameters.Length > 0) 
                {
                    if (DoNotPassNullParamtersToProcedure)
                    {
                        parameters = FilterNullParamters(parameters);
                    }

                    command.Parameters.AddRange(parameters); 
                }
            }
            finally
            { 
            
            }

            return command;
        }        

        private static SqlParameter[] FilterNullParamters(SqlParameter[] parameters)
        {
            int countNull = 0;
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter == null) { countNull = countNull + 1; continue; }
                if (parameter.Direction == ParameterDirection.Input)
                {                    
                    if (parameter.Value == null) {  countNull = countNull + 1; continue; }
                    if (parameter.Value == DBNull.Value) {  countNull = countNull + 1; continue; }
                }
            }

            if (countNull == 0)
            {
                return parameters;
            }

            SqlParameter[] filterParamters = new SqlParameter[parameters.Length - countNull];
            int index = 0;
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter == null) { continue; }
                if (parameter.Direction == ParameterDirection.Input)
                {                    
                    if (parameter.Value == null) { continue; }
                    if (parameter.Value == DBNull.Value) { continue; }
                }

                filterParamters[index] = new SqlParameter();
                filterParamters[index].ParameterName = parameter.ParameterName;
                filterParamters[index].Direction = parameter.Direction;
                filterParamters[index].SqlDbType = parameter.SqlDbType;
                filterParamters[index].Size = parameter.Size;
                filterParamters[index].Value = parameter.Value;

                index++;
            }

            return filterParamters;
        }

        private static void RefillOutParameters(ref SqlParameter[] OriginalParameters, SqlParameterCollection NotNullParamters)
        {
            foreach (SqlParameter originalParamter in OriginalParameters)
            {
                if (originalParamter != null)
                {
                    if (originalParamter.Direction != ParameterDirection.Input)
                    {
                        foreach (SqlParameter notNullParamter in NotNullParamters)
                        {
                            if (originalParamter.ParameterName == notNullParamter.ParameterName)
                            {
                                originalParamter.Value = notNullParamter.Value;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        

        #region connection
        private static SqlConnection GetConnection(string ConnectionString)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            return connection;
        }

        private static void CloseConnection(SqlConnection Connection)
        {
            if (Connection != null)
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
                Connection.Dispose();
            }
        }
        #endregion
    }
}