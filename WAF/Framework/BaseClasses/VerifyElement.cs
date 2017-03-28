using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RelevantCodes.ExtentReports;
using System;
using WAF.Framework.BaseClasses;
using WAF.Framework.HelperClasses;

namespace WAF.BaseClasses
{
    public class VerifyElement : BaseSetup
    {
        private static IWebElement element;
        public static void AreEqual(By locator, string expectedElement)
        {
            try
            {
                element = Driver.GetVisibleElement(locator);
                Assert.AreEqual(expectedElement, Driver.Instance.FindElement(locator).Text.Trim());
                ReportHelper.PassLog("Expected element <b>' " + expectedElement + " '</b> is equal to actual element<b> ' " + element.Text + " '");
            }
            catch (AssertionException e)
            {
                string actualElement = Browser.Instance.FindElement(locator).Text.Trim();
                ReportHelper.WarningLog("Expected:  <b>" + expectedElement + "</b><br>But was:  <b>" + actualElement + "</b><br>" + e.Message);
            }
        }
        public static void IsPresent(By locator)
        {
            try
            {
                element = Driver.GetVisibleElement(locator);
                Assert.IsTrue(ElementPresentBool(locator));
                ReportHelper.PassLog("Element is present: " + locator.ToString());
            }
            catch (AssertionException e)
            {
                ReportHelper.WarningLog("Element is not present: " + "<br>" + locator.ToString());
            }
        }
        public static void IsNotPresent(By locator)
        {
            if (locator == null)
            {
                ReportHelper.PassLog("Element is not present: " + locator.ToString());
            }
            else
            {
                ReportHelper.WarningLog("Element is present: " + "<br>" + locator.ToString());
            }
        }
        public static void TitleIsPresent(string title)
        {
            try
            {
                (new WebDriverWait(Browser.Instance, TimeSpan.FromSeconds(10))).Until(ExpectedConditions.TitleContains(title));
                Assert.AreEqual(title, Browser.Instance.Title);
                TestLog.Log(LogStatus.Pass, "Title is present: " + title);
            }
            catch (AssertionException e)
            {
                string screenName = ScreenshotHelper.TakeScreenshot();
                string scrrenShotPath = TestLog.AddScreenCapture(screenName);
                TestLog.Log(LogStatus.Warning, scrrenShotPath + "Title verification failed: " + e.Message);
            }
        }
        public static void ButtonIsDisabled(By locator)
        {
            try
            {
                Assert.IsNotNull(Driver.GetClickableElement((locator)).GetAttribute("disabled"));

            }
            catch (AssertionException e)
            {
                string screenName = ScreenshotHelper.TakeScreenshot();
                string scrrenShotPath = TestLog.AddScreenCapture(screenName);
                TestLog.Log(LogStatus.Warning, scrrenShotPath + "Button is not disabled: " + e.Message);
            }
        }
        public static bool ElementPresentBool(By by)
        {
            try
            {
                Driver.Instance.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
