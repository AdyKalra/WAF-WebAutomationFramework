using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;

namespace WAF.Database
{
    class GridSetup
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new InternetExplorerDriver();

            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities = DesiredCapabilities.InternetExplorer();
            capabilities.SetCapability(CapabilityType.BrowserName, "internet explorer");
            capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Windows));
            driver = new RemoteWebDriver(new Uri("http://devmiavaqav72:4444/wd/hub"), capabilities);

        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }





    }
}
