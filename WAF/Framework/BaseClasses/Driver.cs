using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
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
        internal static void InsertText(By locator, string text)
        {
            element = GetVisibleElement(locator);
            element.Clear();
            element.SendKeys(text);
            ReportHelper.PassLog("Successfully <b>' " + text + "' </b> text inserted into: <b>" + locator.ToString());
        }
        // Use this block of code is for IE browser on Click event.
        internal static void ClickOn(By locator)
        {
            element = GetClickableElement(locator);
            Actions actions = new Actions(Instance);
            actions.MoveToElement(element).Click().Perform();
            ReportHelper.PassLog("Successfully Clicked on: <b>" + locator.ToString());
        }
        // Use this block of code can be use for other browsers other then IE for Click event.
        //public static void ClickOn(By locator)
        //{
        //    element = GetClickableElement(locator);
        //    element.Click();
        //    // This block of code need it if first click is not working and need second click on IE browser.
        //    //if (VerifyElement.IsElementPresent(locator))
        //    //{
        //    //    element.Click();
        //    //}
        //    ReportHelper.PassLog("Successfully clicked on: <b>" + locator.ToString());
        //}
        internal static void PressEnter(By locator)
        {
            element = GetClickableElement(locator);
            element.SendKeys(Keys.Enter);
            ReportHelper.PassLog("Successfully Pressed Enter on: <b>" + locator);
        }
        internal static void SelectFromDropdown(By locator, string value)
        {
            select = new SelectElement(GetClickableElement(locator));
            select.SelectByText(value);
            ReportHelper.PassLog("Successfully <b>' " + value + "' </b> dropdown option selected from: <b>" + locator.ToString());
        }
        internal static void WaitForElement(By locator)
        {
            try
            {
                (new WebDriverWait(Instance, TimeSpan.FromSeconds(20))).Until(ExpectedConditions.ElementIsVisible(locator));
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
        internal static IWebElement GetVisibleElement(By locator)
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
                throw new NoSuchElementException("Element is not found: <b>" + locator.ToString());
            }
        }
        internal static IWebElement GetClickableElement(By locator)
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
                throw new NoSuchElementException("Can not click the element: <b>" + locator.ToString());
            }
        }
        internal static void SelectCheckbox(By locator)
        {


            element = GetClickableElement(By.XPath("html/body/div[6]/div/div/div[3]/div[1]/div[2]/button[1]"));
            Actions actions = new Actions(Instance);
            actions.MoveToElement(element).SendKeys(Keys.Enter).Perform();



            element = Instance.FindElement(locator);


            if (!element.Selected)
            {
                element.Click();
                //((IJavaScriptExecutor)Instance).ExecuteScript("arguments[0].click();", element);
            }

            GetClickableElement(locator);
            Instance.FindElement(locator).Click();

            //ReportHelper.PassLog("Successfully clicked on checkbox: <b>" + locator.ToString());

            element = GetClickableElement(By.XPath("(//input[@type='checkbox'])[8]"));
            element.Click();

        }

    }
}
