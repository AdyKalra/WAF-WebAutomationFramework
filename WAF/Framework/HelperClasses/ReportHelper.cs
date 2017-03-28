using RelevantCodes.ExtentReports;
using System;
using System.Configuration;
using WAF.Framework.BaseClasses;

namespace WAF.Framework.HelperClasses
{
    class ReportHelper : BaseSetup
    {
        static string reportPath = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["ReportPath"];
        static string now = DateTime.Now.ToString("MM-dd-yyyy H-mm ");
        static readonly ExtentReports _instance = new ExtentReports(reportPath + now + " Test Report.html", DisplayOrder.NewestFirst);
        public static ExtentReports ReportInstance
        {
            get
            {
                return _instance;
            }
        }
        public static void WarningLog(string _message)
        {
            string screenName = ScreenshotHelper.TakeScreenshot();
            string screenShotPath = TestLog.AddScreenCapture(screenName);
            TestLog.Log(LogStatus.Warning, screenShotPath + _message);
        }
        public static void PassLog(string _message)
        {
            TestLog.Log(LogStatus.Pass, _message);
        }
        public static void FailLog(string _message)
        {
            string screenName = ScreenshotHelper.TakeScreenshot();
            string screenShotPath = TestLog.AddScreenCapture(screenName);
            TestLog.Log(LogStatus.Fail, screenShotPath + _message);
        }
    }
}
