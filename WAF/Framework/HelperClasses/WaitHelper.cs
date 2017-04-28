using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using WAF.Framework.BaseClasses;

namespace WAF.Framework.HelperClasses
{
    class WaitHelper
    {
        internal static void WaitForAlert()
        {
            int i = 0;
            while (i++ < 5)
            {
                try
                {
                    IAlert alert = Browser.Instance.SwitchTo().Alert();
                    alert.Dismiss();
                    break;
                }
                catch (NoAlertPresentException)
                {
                    Wait(1);
                    continue;
                }

            }
        }
        internal static void WaitForElement(By locator)
        {
            try
            {
                (new WebDriverWait(Browser.Instance, TimeSpan.FromSeconds(20))).Until(ExpectedConditions.ElementIsVisible(locator));
            }
            catch (Exception)
            {
                throw new NoSuchElementException("Element is not found: <b>" + locator.ToString());
            }
        }
        internal static void Wait(int seconds)
        {
            Thread.Sleep(seconds / 100);
        }
    }
}
