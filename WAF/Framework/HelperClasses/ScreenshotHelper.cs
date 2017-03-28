using OpenQA.Selenium;
using System;
using System.Configuration;
using WAF.Framework.BaseClasses;

namespace WAF.Framework.HelperClasses
{
    class ScreenshotHelper
    {
        public static string TakeScreenshot()
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
