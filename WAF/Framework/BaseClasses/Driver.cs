using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using WAF.Framework.HelperClasses;

namespace WAF.Framework.BaseClasses
{
    public class Driver : Browser
    {
        private static IWebElement element;
        private static SelectElement select;
        public static void InsertText(By locator, string text)
        {
            element = GetVisibleElement(locator);
            element.Clear();
            element.SendKeys(text);
            ReportHelper.PassLog("Successfully <b>' " + text + "' </b> text inserted into: <b>" + locator.ToString());
        }
        public static void ClickOn(By locator)
        {
            element = GetClickableElement(locator);
            element.Click();
            ReportHelper.PassLog("Successfully clicked on: <b>" + locator.ToString());
        }
        public static void PressEnter(By locator)
        {
            element = GetClickableElement(locator);
            element.SendKeys(Keys.Enter);
            ReportHelper.PassLog("Successfully pressed on: <b>" + locator);
        }
        public static void SelectFromDropdown(By locator, string value)
        {
            select = new SelectElement(GetClickableElement(locator));
            select.SelectByText(value);
            ReportHelper.PassLog("Successfully <b>' " + value + "' </b> dropdown option selected from: <b>" + locator.ToString());
        }
        public static void WaitForElement(By locator)
        {
            try
            {
                (new WebDriverWait(Instance, TimeSpan.FromSeconds(20))).Until(ExpectedConditions.ElementIsVisible(locator));
            }
            catch (Exception)
            {
                throw new NoSuchElementException("Element is not found: <br>" + locator.ToString());
            }
        }
        public static void Wait(int seconds)
        {
            Thread.Sleep(seconds / 100);
        }
        public static IWebElement GetVisibleElement(By locator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Instance, TimeSpan.FromSeconds(20));
                wait.PollingInterval = TimeSpan.FromSeconds(2);
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));
                wait.Until(ExpectedConditions.ElementIsVisible(locator));
                return Instance.FindElement(locator);
            }
            catch (Exception)
            {
                throw new NoSuchElementException("Element is not found: <br>" + locator.ToString());
            }
        }
        public static IWebElement GetClickableElement(By locator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Instance, TimeSpan.FromSeconds(20));
                wait.PollingInterval = TimeSpan.FromSeconds(2);
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));
                wait.Until(ExpectedConditions.ElementToBeClickable(locator));
                return Instance.FindElement(locator);
            }
            catch (Exception)
            {
                throw new NoSuchElementException("<br>Can not click the element: " + locator.ToString());
            }
        }
    }
}