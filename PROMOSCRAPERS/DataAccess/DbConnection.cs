using System;
using System.Data;
using System.Configuration; 
using System.Data.SqlClient;


namespace DataAccess
{
	/// <summary>
	/// Summary description for DFSConnection.
	/// </summary>
	public class DbConnection
	{		
		private static SqlConnection _FFConn; 
		internal DbConnection()
		{
			//
			// TODO: Add constructor logic here			
			//
		}
        internal static SqlConnection GetConnection() 
		{			
			return GetConnection(GetConnectionString());
		}

		internal static SqlConnection GetConnection(string connectionstring) 
		{
			_FFConn = new SqlConnection();
			_FFConn.ConnectionString = connectionstring; 
			_FFConn.Open ();

			return _FFConn;
		}

		internal static string GetConnectionString()
		{
            string connectionString = ConfigurationManager.ConnectionStrings["DBCON"].ToString();
            return connectionString;
		}

		internal static void CloseConnection(SqlConnection Connnection)
		{			
			if (Connnection != null)
			{                
				if (Connnection.State == ConnectionState.Open)
				{
					Connnection.Close(); 					
				}

                Connnection.Dispose();
                Connnection = null;
			}
		}

		/*
				public string testConnection ()
				{
					SqlConnection testcn = new SqlConnection();
					testcn  = GetConnection();
					return testcn.State.ToString (); 
				}
	
		*/
	}
}
