﻿/*
--------------------------------------------------------------------------------------------------------------------
Web Automation Framework - WAF v2.0.8
Designed and Developd by Davron Aliyev
Copyright (c) 2017 Document Storage Systems, Inc.
All rights reserved
--------------------------------------------------------------------------------------------------------------------
*/

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.PhantomJS;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Reflection;
using WAF.Framework.HelperClasses;

namespace WAF.Framework.BaseClasses
{
    public class Browser : BaseSetup
    {
        internal static IWebDriver Instance { get; set; }
        internal static void Initialize()
        {
            switch (ConfigurationManager.AppSettings["Browser"].ToString())
            {
                case "Firefox":
                    Instance = new FirefoxDriver(GetFirefoxOptions());
                    break;
                case "Chrome":
                    var chromeService = ChromeDriverService.CreateDefaultService();
                    chromeService.HideCommandPromptWindow = true;
                    Instance = new ChromeDriver(chromeService, GetChromeOptions());
                    break;
                case "IE":
                    var IEService = InternetExplorerDriverService.CreateDefaultService();
                    IEService.HideCommandPromptWindow = true;
                    Instance = new InternetExplorerDriver(IEService, GetIEOptions());
                    break;
                case "PhantomJS":
                    Instance = new PhantomJSDriver(GetPhantomJsDriverService());
                    break;
                default:
                    Instance = new FirefoxDriver(GetFirefoxOptions());
                    break;
            }
            // Browser screen size options
            switch (ConfigurationManager.AppSettings["ScreenSize"].ToString())
            {
                case "Maximize":
                    Instance.Manage().Window.Maximize();
                    break;
                case "Phone":
                    Instance.Manage().Window.Size = new Size(360, 640);
                    break;
                case "iPad":
                    Instance.Manage().Window.Size = new Size(768, 1024);
                    break;
                case "1280, 720":
                    Instance.Manage().Window.Size = new Size(1280, 720);
                    break;
                case "1600, 900":
                    Instance.Manage().Window.Size = new Size(1600, 900);
                    break;
            }
        }
        private static FirefoxDriverService GetFirefoxOptions()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var driverPath = Path.GetFullPath(Path.Combine(outPutDirectory));
            var service = FirefoxDriverService.CreateDefaultService(driverPath, "geckodriver.exe");
            service.HideCommandPromptWindow = true;
            return service;
        }
        private static InternetExplorerOptions GetIEOptions()
        {
            InternetExplorerOptions options = new InternetExplorerOptions();
            options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
            options.IgnoreZoomLevel = true;
            //options.EnsureCleanSession = true;
            options.EnablePersistentHover = false;
            options.EnableNativeEvents = false;
            //options.AddAdditionalCapability(CapabilityType.AcceptSslCertificates, true);
            return options;
        }
        private static ChromeOptions GetChromeOptions()
        {
            ChromeOptions option = new ChromeOptions();
            option.AddArgument("start-maximized");
            return option;
        }
        private static PhantomJSDriverService GetPhantomJsDriverService()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var driverPath = Path.GetFullPath(Path.Combine(outPutDirectory));
            PhantomJSDriverService service = PhantomJSDriverService.CreateDefaultService(driverPath);
            service.HideCommandPromptWindow = true;
            //service.IgnoreSslErrors = true;
            return service;
        }
        internal static void NavigateToUrl(string url)
        {
            Instance.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
            Instance.Navigate().GoToUrl(url);
            ReportHelper.PassLog("Successfully navigated to: <b>" + url);
        }
        internal static void Open()
        {
            string url = ConfigurationManager.AppSettings["URL"];
            ReportHelper.InfoLog("<b>Step 1 - </b>Navigate to <b>" + url);
            Instance.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
            Instance.Navigate().GoToUrl(url);
            ReportHelper.PassLog("Successfully navigated to: <b>" + url);
        }
        internal static void Close()
        {
            Instance.Close();
            Instance.Quit();
            //Instance.Dispose();
            TestLog.Log(LogStatus.Pass, "Browser closed.");
        }
        internal static void Maximize()
        {
            Instance.Manage().Window.Maximize();
            TestLog.Log(LogStatus.Pass, "Browser maximized.");
        }
        internal static void GoBack()
        {
            Instance.Navigate().Back();
            TestLog.Log(LogStatus.Pass, "Navigated back.");
        }
        internal static void RefreshPage()
        {
            Instance.Navigate().Refresh();
            TestLog.Log(LogStatus.Pass, "Page refreshed.");
        }
        internal static void SwitchToWindow(int index = 0)
        {
            ReadOnlyCollection<string> windows = Instance.WindowHandles;
            if ((windows.Count - 1) < index)
            {
                throw new NoSuchWindowException("Invalid Browser Window Index" + index);
            }
            Instance.SwitchTo().Window(windows[index]);
            Maximize();
            TestLog.Log(LogStatus.Pass, "Switched to new window");
        }
        internal static void SwitchToParent()
        {
            var windowids = Instance.WindowHandles;
            for (int i = windowids.Count - 1; i > 0;)
            {
                Instance.Close();
                i = i - 1;
                Instance.SwitchTo().Window(windowids[i]);
            }
            Instance.SwitchTo().Window(windowids[0]);
            TestLog.Log(LogStatus.Pass, "Switched back to main window");
        }
        internal static void SwitchToIFrame(By locator)
        {
            Instance.SwitchTo().Frame(Instance.FindElement(locator));
        }
        internal static void CloseNewTab()
        {
            Actions action = new Actions(Instance);
            action.KeyDown(Keys.Control).SendKeys("w").KeyUp(Keys.Control).Perform();
            SwitchToParent();
        }
        internal static void ScrollToElement(By location)
        {
            var _location = Instance.FindElement(location);
            ((IJavaScriptExecutor)Instance).ExecuteScript("arguments[0].scrollIntoView(true);", _location);
            TestLog.Log(LogStatus.Pass, "Scroll to element: <b>" + location);
        }
    }
}
