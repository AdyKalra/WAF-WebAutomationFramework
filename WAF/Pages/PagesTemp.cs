using OpenQA.Selenium;
using WAF.BaseClasses;
using WAF.Framework.BaseClasses;

namespace WAF.Pages
{
    public class PagesTemp
    {
        #region PageElements
        static internal By signInButton = By.XPath("//*[@id='gb_70']");
        static internal By header = By.XPath("//h1");
        static internal By searchField = By.XPath("//*[@id='lst-ib']");
        #endregion

        #region PageNavigations
        public static void NavigateToLoginPage()
        {
            Driver.ClickOn(signInButton);
            VerifyElement.AreEqual(header, "One account. All of Google.");
        }
        #endregion

        #region PageActions
        public static void VerifyPageElements()
        {
            VerifyElement.IsPresent(header);
        }
        #endregion
    }
}