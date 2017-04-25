/*
--------------------------------------------------------------------------------------------------------------------
Web Automation Framework - WAF v2.0.8
Designed and Developd by Davron Aliyev
Copyright (c) 2017 Document Storage Systems, Inc.
All rights reserved
--------------------------------------------------------------------------------------------------------------------
*/

using OpenQA.Selenium;
using System;
using System.Configuration;
using WAF.Framework.BaseClasses;

namespace WAF.Framework.HelperClasses
{
    class ScreenshotHelper
    {
        internal static string TakeScreenshot()
        {
            string screenshotPath = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["ScreenshotPath"];
            string now = DateTime.Now.ToString("MMddyyyhhmmss");
            string screenName = now + "Screenshot.png";
            try
            {
                Screenshot ss = ((ITakesScreenshot)Browser.Instance).GetScreenshot();
                ss.SaveAsFile(screenshotPath + screenName, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return screenName;
        }
    }
}
