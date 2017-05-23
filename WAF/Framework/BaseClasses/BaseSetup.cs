/*
--------------------------------------------------------------------------------------------------------------------
Web Automation Framework - WAF v2.0.8
Designed and Developd by Davron Aliyev
Copyright (c) 2017 Document Storage Systems, Inc.
All rights reserved
--------------------------------------------------------------------------------------------------------------------
*/

using NUnit.Framework;
using NUnit.Framework.Interfaces;
using RelevantCodes.ExtentReports;
using WAF.Framework.HelperClasses;

namespace WAF.Framework.BaseClasses
{
    public class BaseSetup
    {
        protected ExtentReports ReportLog;
        protected static ExtentTest TestLog;

        [SetUp]
        public void BaseInitialize()
        {
            string testName = (TestContext.CurrentContext.Test.Name);
            Browser.Initialize();
            ReportLog = ReportHelper.ReportInstance;
            TestLog = ReportLog.StartTest(TestContext.CurrentContext.Test.Name);
        }
        [TearDown]
        public void BaseCleanup()
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
            Browser.Close();
            TestLog.Log(logstatus, "Test ended with <b>" + logstatus + stacktrace);
            ReportLog.EndTest(TestLog);
            ReportLog.Flush();
        }
    }
}
