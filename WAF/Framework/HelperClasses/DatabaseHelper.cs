using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WAF.Framework.HelperClasses
{
    public static class DatabaseHelper
    {
        // Database connection
        public static SqlConnection DBConnect()
        {
            // Connect to Database
            try
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["UserDBConnectionString"].ToString());
                connection.Open();
                return connection;
            }
            catch (Exception)
            {
                throw;
            }
            return null;
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
            catch (Exception)
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
