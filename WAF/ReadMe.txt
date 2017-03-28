WAF v2.0.3
02/04/2017
________________________________________________________________________________________________________________

Page Object class setup
________________________________________________________________________________________________________________

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

________________________________________________________________________________________________________________

Test Case class setup
________________________________________________________________________________________________________________

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
________________________________________________________________________________________________________________


XPath samples
________________________________________________________________________________________________________________

//button[normalize-space(text())='Sign In']
//button[contains(., 'Save')]
(//button[@id='dropdownMenu1'])[2]

//input[@value='Save']

//th[contains(., 'Description')]
//th[normalize-space(text())='Description']

//a[@href='/MCCDashboardTest/ReleaseNotes']

//tr[10]/td[2][normalize-space(text())='Discussion Topic']

//tr/td[normalize-space(text())='Labs/Procedures']//following-sibling::td[2][normalize-space(text())='Immediately']

//tbody/tr[2]/td[2][contains(., 'Abc Automation')]//preceding-sibling::td[1]/input
//ul[@class=' ']/li[contains(., ' ')]


________________________________________________________________________________________________________________

Driver action methods
________________________________________________________________________________________________________________

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

