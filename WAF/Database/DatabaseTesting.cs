using NUnit.Framework;
using NUnit.Framework.Interfaces;
using RelevantCodes.ExtentReports;
using System;
using System.Configuration;
using System.Data.SqlClient;
using WAF.Framework.HelperClasses;

namespace WAF.Database
{
    [TestFixture]
    class DatabaseTesting
    {
        // Excution Query
        string selectQuery = "SELECT * FROM TestTable";
        string insertQuery = "INSERT INTO TestTable (column1, column2, column3) VALUES (10, 20, 30), (22, 33, 44), (77, 88, 99);";
        string deleteTableQuery = "DELETE FROM TestTable";

        protected ExtentReports ReportLog;
        protected static ExtentTest TestLog;

        [Test]
        public void DBTest()
        {
            // Connect to Databse
            SqlConnection myDatabase = new SqlConnection(ConfigurationManager.ConnectionStrings["UserDBConnectionString"].ToString());
            myDatabase.Open();
            // Excute SQL command

            SqlCommand cmd = new SqlCommand(selectQuery, myDatabase);
            SqlDataReader dr = cmd.ExecuteReader();
            // Run Test
            if (dr.HasRows)
            {
                int count = 1;
                while (dr.Read())
                {
                    var dbValue = dr.GetValue(0);
                    var exlValue = ExcelReader.ReadFrom(1, 1, count);
                    count = count + 1;
                    try
                    {
                        Assert.AreEqual(dbValue, Convert.ToInt32(exlValue));
                        TestLog.Log(LogStatus.Pass, "Database data <b>'" + dbValue + "'</b> is equal to Excel data <b>'" + exlValue + "'</b>");
                    }
                    catch (Exception)
                    {
                        TestLog.Log(LogStatus.Fail, "Database data <b>'" + dbValue + "'</b> is not equal to Excel data <b>'" + exlValue + "'</b>");
                    }
                }
            }
            // Close Database
            myDatabase.Close();
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
    }
}
