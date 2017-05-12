using OpenQA.Selenium;

namespace WAF.Framework.HelperClasses
{
    class KeyHelper
    {
        private static IWebElement element;
        internal static void PressEnter(By locator)
        {
            element = WaitHelper.ElementToBeClickable(locator);
            element.SendKeys(Keys.Enter);
            ReportHelper.PassLog("Successfully Pressed Enter on: <b>" + locator);
        }

        internal static void HoldCtrl(By locator, string key)
        {
            element = WaitHelper.ElementToBeClickable(locator);
            element.SendKeys(Keys.Control + key);
            ReportHelper.PassLog("Successfully Pressed Enter on: <b>" + locator);
        }
    }
}
