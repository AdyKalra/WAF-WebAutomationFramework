WAF v2.0.7 
04/07/2017
________________________________________________________________________________________________________________

Page Object Model class example
________________________________________________________________________________________________________________

using OpenQA.Selenium;
using WAF.BaseClasses;
using WAF.Framework.BaseClasses;

namespace WAF.Pages
{
    class PageObjectModel
    {
        #region WebElements
        static internal By element = By.XPath("");

        #endregion

        #region Action
        public static void VerifyPageElements()
        {
            VerifyElement.IsPresent(element);
        }

        #endregion

        #region Navigation
        public static void NavigateToPage()
        {
            Driver.ClickOn(element);
            VerifyElement.AreEqual(element, "");
        }

        #endregion
    }
}


________________________________________________________________________________________________________________

Test Case class example
________________________________________________________________________________________________________________

using NUnit.Framework;
using WAF.Framework.BaseClasses;

namespace WAF.UI_Test
{
    [TestFixture]
    class QuickTest : BaseSetup
    {
        [Test]
        public void QuickTest_01()
        {
            Browser.Open();
        }
    }
}


________________________________________________________________________________________________________________

Action methods
________________________________________________________________________________________________________________

	Driver.ClickOn(locator);
	Driver.PressEnter(locator);
	Driver.InsertText(locator, "");
	Driver.SelectDropdown(locator, "");
	Driver.WaitForElement(locator);
	Driver.Wait(2);

	VerifyElement.AreEqual(locator, "");
	VerifyElement.IsPresent(locator);
	VerifyElement.IsNotPresent(locator);
	VerifyElement.TitleIsPresent("");
	VerifyElement.ButtonIsDisabled(locator);
	VerifyElement.ElementPresentBool(locator);

	Browser.Open();
	Browser.NavigateToUrl(url);
	Browser.Close();
	Browser.Maximize();
	Browser.GoBack();
	Browser.RefreshPage();
	Browser.SwitchToWindow();
	Browser.SwitchToParent();
	Browser.SwtichToIFrame();
	Browser.ScrollToElement(location);

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
