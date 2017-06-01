using NUnit.Framework;
using NUnit.Framework.Interfaces;
using RelevantCodes.ExtentReports;
using System;
using System.Data.SqlClient;
using WAF.Framework.HelperClasses;


namespace WAF.Database
{
    [TestFixture]
    class DatabaseTest
    {
        // Database table and column list
        static internal string table = "TestTable";
        static internal string column1 = "column1";
        static internal string column3 = "columnString";

        // SQL Queries
        internal string selectQuery = "SELECT * FROM " + table;
        string insertQuery = "INSERT INTO " + table + " (" + column1 + ", column2, column3) VALUES (10, 20, 30), (22, 33, 44), (77, 88, 99);";
        string deleteTableQuery = "DELETE FROM " + table;

        // Report Log
        protected ExtentReports ReportLog;
        protected static ExtentTest TestLog;

        //[Test]
        public void DBTestCase()
        {
            // Connect to Database
            var connection = DatabaseHelper.DBConnect();

            // SqlDataReader - which represents a forward-only stream of rows from the database, columns of each row can be accessed by index or name
            SqlCommand cmd = new SqlCommand(selectQuery, connection);
            SqlDataReader dataReader = cmd.ExecuteReader();

            // Run the Test
            if (dataReader.HasRows)
            {
                int count = 1;
                while (dataReader.Read())
                {
                    // First Column 
                    if (dataReader.GetName(0) == column1)
                    {
                        string dbValue = dataReader.GetValue(0).ToString();
                        var exlValue = ExcelReader.ReadFrom(1, 1, count);
                        try
                        {
                            Assert.AreEqual(dbValue, exlValue);
                            TestLog.Log(LogStatus.Pass, "Database data <b>'" + dbValue + "'</b> is equal to Excel data <b>'" + exlValue + "'</b>");
                        }
                        catch (Exception)
                        {
                            TestLog.Log(LogStatus.Fail, "Database data <b>'" + dbValue + "'</b> is not equal to Excel data <b>'" + exlValue + "'</b>");
                        }
                    }
                    else
                    {
                        TestLog.Log(LogStatus.Fail, "Column <b>'" + column1 + "'</b> is not present");
                    }
                    // Last Column
                    if (dataReader.GetName(3) == column3)
                    {
                        string dbValue2 = dataReader.GetValue(3).ToString();
                        string exlValue2 = ExcelReader.ReadFrom(1, 4, count);
                        try
                        {
                            Assert.AreEqual(dbValue2, exlValue2);
                            TestLog.Log(LogStatus.Pass, "Database data <b>'" + dbValue2 + "'</b> is equal to Excel data <b>'" + exlValue2 + "'</b>");
                        }
                        catch (Exception)
                        {
                            TestLog.Log(LogStatus.Fail, "Database data <b>'" + dbValue2 + "'</b> is not equal to Excel data <b>'" + exlValue2 + "'</b>");
                        }
                    }
                    count = count + 1;
                }
            }
            // Close connection
            connection.Close();
        }

        [SetUp]
        public void setup()
        {
            string testName = (TestContext.CurrentContext.Test.Name);
            ReportLog = ReportHelper.ReportInstance;
            TestLog = ReportLog.StartTest(TestContext.CurrentContext.Test.Name);
        }
        [TearDown]
        public void cleanup()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.Message);
            LogStatus logstatus;
            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = LogStatus.Fail;
                    string screenName = ScreenshotHelper.TakeScreenshot();
                    string screenshotPath = TestLog.AddScreenCapture("screenshots//" + screenName);
                    TestLog.Log(LogStatus.Fail, "Screenshot on Fail", screenshotPath);
                    break;
                case TestStatus.Inconclusive:
                    logstatus = LogStatus.Warning;
                    TestLog.Log(LogStatus.Warning, "Warning");
                    break;
                case TestStatus.Skipped:
                    logstatus = LogStatus.Skip;
                    break;
                default:
                    logstatus = LogStatus.Pass;
                    break;
            }
            TestLog.Log(logstatus, "Test ended with <b>" + logstatus + stacktrace);
            ReportLog.EndTest(TestLog);
            ReportLog.Flush();
        }



        // Note

        // ExecuteNonQuery - use this method when you don’t expect a result (perhaps and update statement, or a call to a Stored Procedure that returns no resultset)
        //var command = connection.CreateCommand();
        //command.CommandText = deleteTableQuery;
        //command.ExecuteNonQuery();
        //command.CommandText = insertQuery;
        //command.ExecuteNonQuery();
    }
}
