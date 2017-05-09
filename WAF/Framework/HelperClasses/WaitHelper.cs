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

        //  An expectation for checking that an element is present on the DOM of a page and visible. Visibility means that the element is not only displayed but also has a height and width that is greater than 0.
        internal static IWebElement ElementIsVisible(By locator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Browser.Instance, TimeSpan.FromSeconds(20));
                wait.PollingInterval = TimeSpan.FromSeconds(2);
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));
                wait.Until(ExpectedConditions.ElementIsVisible(locator));
                return Browser.Instance.FindElement(locator);
            }
            catch (Exception)
            {
                throw new NoSuchElementException("Element is not visible <b>" + locator.ToString());
            }
        }

        //  An expectation for checking an element is visible and enabled such that you can click it.
        internal static IWebElement ElementToBeClickable(By locator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Browser.Instance, TimeSpan.FromSeconds(20));
                wait.PollingInterval = TimeSpan.FromSeconds(2);
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));
                wait.Until(ExpectedConditions.ElementToBeClickable(locator));
                return Browser.Instance.FindElement(locator);
            }
            catch (Exception)
            {
                throw new NoSuchElementException("Element is not clickable: <b>" + locator.ToString());
            }
        }

        //  An expectation for checking that an element is present on the DOM of a page. This does not necessarily mean that the element is visible.
        internal static IWebElement ElementExists(By locator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Browser.Instance, TimeSpan.FromSeconds(20));
                wait.PollingInterval = TimeSpan.FromSeconds(2);
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));
                wait.Until(ExpectedConditions.ElementExists(locator));
                return Browser.Instance.FindElement(locator);
            }
            catch (Exception)
            {
                throw new NoSuchElementException("Element is not exists: <b>" + locator.ToString());
            }
        }
    }
}
