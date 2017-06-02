/*
--------------------------------------------------------------------------------------------------------------------
Web Automation Framework - WAF v2.0.8
Designed and Developd by Davron Aliyev
Copyright (c) 2017 Document Storage Systems, Inc.
All rights reserved
--------------------------------------------------------------------------------------------------------------------
*/

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using WAF.Framework.BaseClasses;
using WAF.Framework.HelperClasses;

namespace WAF.BaseClasses
{
    public class VerifyElement : BaseSetup
    {
        private static IWebElement element;
        internal static void IsTitlePresent(string title)
        {
            try
            {
                (new WebDriverWait(Browser.Instance, TimeSpan.FromSeconds(10))).Until(ExpectedConditions.TitleContains(title));
                Assert.AreEqual(title, Browser.Instance.Title);
                ReportHelper.PassLog("Title Verification Passed: <br>" + title);
            }
            catch (AssertionException e)
            {
                ReportHelper.WarningLog("Title Verification Failed: <br>" + e.Message);
            }
        }
        internal static void AreEqual(By locator, string expectedElement)
        {
            try
            {
                element = WaitHelper.ElementIsVisible(locator);
                Assert.AreEqual(expectedElement, Driver.Instance.FindElement(locator).Text.Trim());
                ReportHelper.PassLog("Expected:<b>" + expectedElement + "</b><br>Actual:<b>" + element.Text + "</b>");
            }
            catch (AssertionException e)
            {
                string actualElement = Browser.Instance.FindElement(locator).Text.Trim();
                ReportHelper.WarningLog("Expected: <b>" + expectedElement + "</b><br>Actual: <b>" + element.Text + "</b>");
            }
        }
        internal static void IsPresent(By locator)
        {
            try
            {
                element = WaitHelper.ElementIsVisible(locator);
                Assert.IsTrue(IsElementPresent(locator));
                ReportHelper.PassLog("Expected element is present: <br>" + locator);
            }
            catch (AssertionException e)
            {
                ReportHelper.WarningLog("Expected element is not present: <br>" + locator.ToString() + "<br>" + e.Message);
            }
        }
        internal static void IsHidden(By locator)
        {
            try
            {
                element = WaitHelper.ElementExists(locator);
                if (Browser.Instance.FindElement(locator).GetAttribute("aria-hidden").Equals("true"))
                {
                    ReportHelper.PassLog("Expected element is hidden: <br>" + locator);
                }
                else ReportHelper.WarningLog("Expected element is not hidden.");
            }
            catch (AssertionException e)
            {
                ReportHelper.WarningLog("Expected element is not hidden: <br>" + locator.ToString() + "<br>" + e.Message);
            }
        }
        internal static void IsNotPresent(By locator)
        {
            if (locator == null)
            {
                ReportHelper.PassLog("Expected element is not present: <br>" + locator.ToString());
            }
            else
            {
                ReportHelper.WarningLog("Expected element is present: <br>" + locator.ToString());
            }
        }
        internal static void ButtonIsActive(By locator)
        {
            try
            {
                WaitHelper.ElementToBeClickable(locator);
                ReportHelper.PassLog("Expected button is active: <br>" + locator.ToString());
            }
            catch (AssertionException e)
            {
                ReportHelper.WarningLog("Expected button is not active: <br>" + locator.ToString() + "<br>" + e.Message);
            }
        }
        internal static void ButtonIsDisabled(By locator)
        {
            try
            {
                Assert.IsNotNull(WaitHelper.ElementIsVisible((locator)).GetAttribute("disabled"));
                ReportHelper.PassLog("Expected button is disabled: <br>" + locator.ToString());
            }
            catch (AssertionException e)
            {
                ReportHelper.WarningLog("Expected button is not disabled: <br>" + locator.ToString() + "<br>" + e.Message);
            }
        }
        internal static bool IsElementPresent(By locator)
        {
            try
            {
                Browser.Instance.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
