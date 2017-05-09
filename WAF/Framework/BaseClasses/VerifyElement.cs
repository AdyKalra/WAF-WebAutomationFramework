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
                ReportHelper.PassLog("Page Title Verification Passed: <b>" + title);
            }
            catch (AssertionException e)
            {
                ReportHelper.WarningLog("Page Title Verification Failed: <b>" + e.Message);
            }
        }
        internal static void AreEqual(By locator, string expectedElement)
        {
            try
            {
                element = WaitHelper.ElementIsVisible(locator);
                Assert.AreEqual(expectedElement, Driver.Instance.FindElement(locator).Text.Trim());
                ReportHelper.PassLog("Expected element <b>' " + expectedElement + " '</b> is equal to actual element<b> ' " + element.Text + " '");
            }
            catch (AssertionException e)
            {
                string actualElement = Browser.Instance.FindElement(locator).Text.Trim();
                ReportHelper.WarningLog("Expected:  <b>" + expectedElement + "</b><br>But was:  <b>" + actualElement + "</b><br>" + e.Message);
            }
        }
        internal static void IsPresent(By locator)
        {
            try
            {
                element = WaitHelper.ElementIsVisible(locator);
                Assert.IsTrue(IsElementPresent(locator));
                ReportHelper.PassLog("Element is present: <b>" + locator.ToString());
            }
            catch (AssertionException e)
            {
                ReportHelper.WarningLog("Element is not present: <b>" + locator.ToString() + "</b><br>" + e.Message);
            }
        }
        internal static void IsNotPresent(By locator)
        {
            if (locator == null)
            {
                ReportHelper.PassLog("Element is not present: <b>" + locator.ToString());
            }
            else
            {
                ReportHelper.WarningLog("Element is present: <b>" + locator.ToString());
            }
        }
        internal static void ButtonIsActive(By locator)
        {
            try
            {
                WaitHelper.ElementToBeClickable(locator);
                ReportHelper.PassLog("Expected Button is active: <b>" + locator.ToString());
            }
            catch (AssertionException e)
            {
                ReportHelper.WarningLog("Button is not active: <b>" + locator.ToString() + "</b><br>" + e.Message);
            }
        }
        internal static void ButtonIsDisabled(By locator)
        {
            try
            {
                Assert.IsNotNull(WaitHelper.ElementIsVisible((locator)).GetAttribute("disabled"));
                ReportHelper.PassLog("Button is disabled: <b>" + locator.ToString());
            }
            catch (AssertionException e)
            {
                ReportHelper.WarningLog("Button is not disabled: <b>" + locator.ToString() + "</b><br>" + e.Message);
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
