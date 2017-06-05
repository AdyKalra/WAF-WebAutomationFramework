/*
--------------------------------------------------------------------------------------------------------------------
Web Automation Framework - WAF v2.0.8
Designed and Developd by Davron Aliyev
Copyright (c) 2017 Document Storage Systems, Inc.
All rights reserved
--------------------------------------------------------------------------------------------------------------------
*/

using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using WAF.Framework.HelperClasses;

namespace WAF.Framework.BaseClasses
{
    public class Driver : Browser
    {
        private static IWebElement element;
        private static SelectElement select;
        internal static void SendKeys(By locator, string text)
        {
            element = WaitHelper.ElementIsVisible(locator);
            element.Clear();
            element.SendKeys(text);
            ReportHelper.PassLog("Successfully text inserted into: <br>" + text + "<br>" + locator.ToString());
        }
        internal static void ClickOn(By locator)
        {
            // Use this block of code is for IE 11 browser on Click event.
            element = WaitHelper.ElementToBeClickable(locator);
            Actions actions = new Actions(Instance);
            actions.MoveToElement(element).Click().Perform();
            ReportHelper.PassLog("Successfully clicked on: <br>" + locator.ToString());

            //****************************************************************************************
            ////  This block of code can be use for any browser other then IE 11 for Click event.
            //    element = WaitHelper.ElementToBeClickable(locator);
            //    element.Click();
            //    // This block of code need it if first click is not working and need second click on IE browser.
            //    //if (VerifyElement.IsElementPresent(locator))
            //    //{
            //    //    element.Click();
            //    //}
            //    ReportHelper.PassLog("Successfully clicked on: <b>" + locator.ToString());
        }
        internal static void SelectFromDropdown(By locator, string value)
        {
            select = new SelectElement(WaitHelper.ElementToBeClickable(locator));
            ClickOn(locator);
            select.SelectByText(value);
            ReportHelper.PassLog("Successfully dropdown option is selected: <br>" + value + "<br>" + locator.ToString());
        }
        internal static void SelectCheckbox(By locator)
        {
            WaitHelper.ElementExists(locator);
            if (!Instance.FindElement(locator).GetAttribute("aria-checked").Equals("true"))
            {
                Instance.FindElement(locator).Click();
                ReportHelper.PassLog("Successfully checkbox is checked: <br>" + locator);
            }
            else
            {
                ReportHelper.WarningLog("Checkbox is alrady checked: <br>" + locator);
            }
        }
        internal static void UncheckCheckbox(By locator)
        {
            WaitHelper.ElementExists(locator);
            if (Instance.FindElement(locator).GetAttribute("aria-checked").Equals("true"))
            {
                Instance.FindElement(locator).Click();
                ReportHelper.PassLog("Successfully checkbox is unchecked: <br>" + locator);
            }
            else
            {
                ReportHelper.WarningLog("Checkbox is not checked for uncheck: <br>" + locator);
            }
        }
    }
}