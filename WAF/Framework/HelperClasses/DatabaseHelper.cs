using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WAF.Framework.HelperClasses
{
    public static class DatabaseHelper
    {

        //Open the connection
        public static SqlConnection DBConnect()
        {
            try
            {
                SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["UserDBConnectionString"].ToString());
                connect.Open();
                return connect;
                //sqlConnection = new SqlConnection(connectionString);
                //sqlConnection.Open();
                //return sqlConnection;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR :: " + e.Message);
            }

            return null;
        }

        //Closing the connection 
        public static void DBClose(this SqlConnection sqlConnection)
        {
            try
            {
                sqlConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR :: " + e.Message);
            }
        }

        //Execution
        public static DataTable ExecuteQuery(this SqlConnection sqlConnection, string queryString)
        {

            DataSet dataset;
            try
            {
                //Checking the state of the connection
                if (sqlConnection == null || ((sqlConnection != null && (sqlConnection.State == ConnectionState.Closed ||
                    sqlConnection.State == ConnectionState.Broken))))
                    sqlConnection.Open();

                SqlDataAdapter dataAdaptor = new SqlDataAdapter();
                dataAdaptor.SelectCommand = new SqlCommand(queryString, sqlConnection);
                dataAdaptor.SelectCommand.CommandType = CommandType.Text;

                dataset = new DataSet();
                dataAdaptor.Fill(dataset, "table");
                sqlConnection.Close();
                return dataset.Tables["table"];
            }
            catch (Exception e)
            {
                dataset = null;
                sqlConnection.Close();
                //LogHelpers.Write("ERROR :: " + e.Message);
                return null;
            }
            finally
            {
                sqlConnection.Close();
                dataset = null;
            }


        }

    }
}
