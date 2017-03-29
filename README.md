# WAF-Web Automation Framework
# About

WAF (Web Automation Framework) is UI test automation framework that can automate testing performance and simplify as much of the testing effort as possible with a minimum set of scripts. Framework is developed under dot net environment using C# language along with Selenium WebDriver 3.3.0, NUnit 3.6.0, ExtentReports 2.41.0 libraries to automate web UI elements. 

WAF designed to perform automation testing using different browser (Chrome, IE, Firefox and PhantomJS) under different environment, different screen resolutions and produce test results into html report document.

Web Automation framework is way to organize the code so that it can be Reusable, Scalable, Maintainable, Understandable, Workable and Reusable.

DSS WAF (Web Automation Framework) is developed that can be used by multiple users across same or multiple projects, not limited to one single project.
Using framework can reduce the effort spent on “X” component (library of code) which is already written or available, and by reusing the code will require less or no testing since the available code is not all new since its already in use.

1.3	Scalable

Framework is scalable, meaning it can be used for small to large projects. Framework is scaled to support multiple technologies and tools that can be web or widows even service based.

1.4	Maintainable

Framework is easily maintainable since it is written in same dot net environment that developers develop DSS Web application. Framework code is segregated as a logical groups of same type (classes) and functionalities (methods).
Framework will have master that will be different entity from working project, so that changes to framework will go to framework projects then changes to test goes to test project.

1.5	Understandable

Framework is usable and understandable by any QA team that can be pluggable to any project even less knowledge automation test engineer will work with code using framework methods.
# Documentation

Include these refrences into your new project

    <Reference Include="nunit.framework, Version=3.6.0.0, Culture=neutral, PublicKeyToken=2638cd05610744eb, processorArchitecture=MSIL">
      <HintPath>..\WAF\WAF-WebAutomationFramework\WAF\packages\NUnit\3.6.0\lib\net45\nunit.framework.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include="WebDriver, Version=3.0.1.0, Culture=neutral, processorArchitecture=MSIL">
      <HintPath>..\WAF\WAF-WebAutomationFramework\WAF\packages\Selenium.WebDriver\3.0.1\lib\net40\WebDriver.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include="WebDriver.Support, Version=3.0.1.0, Culture=neutral, processorArchitecture=MSIL">
      <HintPath>..\WAF\WAF-WebAutomationFramework\WAF\packages\Selenium.Support\3.0.1\lib\net40\WebDriver.Support.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include="Microsoft.Office.Interop.Excel, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c, processorArchitecture=MSIL">
      <EmbedInteropTypes>True</EmbedInteropTypes>
      <HintPath>..\WAF\WAF-WebAutomationFramework\WAF\packages\Microsoft.Office.Interop.Excel\15.0\lib\net20\Microsoft.Office.Interop.Excel.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include="RelevantCodes.ExtentReports, Version=1.4.9.0, Culture=neutral, processorArchitecture=MSIL">
      <HintPath>..\WAF\WAF-WebAutomationFramework\WAF\packages\ExtentReports\2.41.0\lib\RelevantCodes.ExtentReports.dll</HintPath>
      <Private>True</Private>
    </Reference>
    
    
    <Import Project="..\..\WAF\WAF-WebAutomationFramework\WAF\packages\IEDriver\3.0.0.1\build\Selenium.WebDriver.IEDriver.targets" Condition="Exists('..\..\WAF\WAF-WebAutomationFramework\WAF\packages\IEDriver\3.0.0.1\build\Selenium.WebDriver.IEDriver.targets')" /
    <Import Project="..\..\WAF\WAF-WebAutomationFramework\WAF\packages\FFDriver\0.13\build\Selenium.WebDriver.FFDriver.targets" Condition="Exists('..\..\WAF\WAF-WebAutomationFramework\WAF\packages\FFDriver\0.13\build\Selenium.WebDriver.FFDriver.targets')" /
    <Import Project="..\..\WAF\WAF-WebAutomationFramework\WAF\packages\ChromeDriver\2.27.0\build\Selenium.WebDriver.ChromeDriver.targets" Condition="Exists('..\..\WAF\WAF-WebAutomationFramework\WAF\packages\ChromeDriver\2.27.0\build\Selenium.WebDriver.ChromeDriver.targets')" />
    <Import Project="..\..\WAF\WAF-WebAutomationFramework\WAF\packages\PhantomJS\2.1.1\build\Selenium.WebDriver.PhantomJSDriver.targets" Condition="Exists('..\..\WAF\WAF-WebAutomationFramework\WAF\packages\PhantomJS\2.1.1\build\Selenium.WebDriver.PhantomJSDriver.targets')" />

# Page Object class setup

using OpenQA.Selenium;
using WAF.Framework.BaseClasses;
using WAF.Framework.HelperClasses;

namespace WAF.PageObjects
{
    class PageObjectTemp
    {
        #region PageElements
        static internal By searchbox = By.XPath(ExcelReader.From(1, 3, 2));
        static internal By loginLink = By.XPath(ExcelReader.From(1, 3, 3));
        #endregion

        #region PageNavigations
        public static void NavigateToLoginPage()
        {
            Driver.ClickOn(loginLink);
        }
        #endregion

        #region PageActions
        public static void VerifyPageElements()
        {
        }
        #endregion
    }
}

# Test Case class setup

using NUnit.Framework;
using WAF.Framework.BaseClasses;
using WAF.PageObjects;

namespace WAF.TestSuite
{
    [TestFixture]
    class TestCaseTemp : BaseSetup
    {
        [Test]
        public void TestCase_01()
        {
            Browser.Open();
            PageObjectTemp.NavigateToLoginPage();
        }
    }
}

# Driver action methods

	Driver.ClickOn(locator);
	Driver.PressEnter(locator);
	Driver.InsertText(locator, "");
	Driver.SelectDropdown(locator, "");
	Driver.WaitForElement(locator);

	VerifyElement.AreEqual(locator, "");
	VerifyElement.IsPresent(locator);
	VerifyElement.TitleIsPresent("");
	VerifyElement.ButtonIsDisabled(locator);

	Browser.Open();
	Browser.Close();
	Browser.Maximize();
	Browser.GoBack();
	Browser.SwitchToWindow();
	Browser.SwitchToParent();
	Browser.RefreshPage();


