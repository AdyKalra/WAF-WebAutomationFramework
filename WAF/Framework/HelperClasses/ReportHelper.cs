/*
--------------------------------------------------------------------------------------------------------------------
Web Automation Framework - WAF v2.0.8
Designed and Developd by Davron Aliyev
Copyright (c) 2017 Document Storage Systems, Inc.
All rights reserved
--------------------------------------------------------------------------------------------------------------------
*/

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
        internal static ExtentReports ReportInstance
        {
            get
            {
                return _instance;
            }
        }
        internal static void WarningLog(string _message)
        {
            string screenName = ScreenshotHelper.TakeScreenshot();
            string screenShotPath = TestLog.AddScreenCapture("screenshots/" + screenName);
            TestLog.Log(LogStatus.Warning, screenShotPath + _message);
        }
        internal static void PassLog(string _message)
        {
            TestLog.Log(LogStatus.Pass, _message);
        }
        internal static void InfoLog(string _message)
        {
            TestLog.Log(LogStatus.Info, _message);
        }
        internal static void FailLog(string _message)
        {
            string screenName = ScreenshotHelper.TakeScreenshot();
            string screenShotPath = TestLog.AddScreenCapture("screenshots/" + screenName);
            TestLog.Log(LogStatus.Fail, screenShotPath + _message);
        }

    }
}
