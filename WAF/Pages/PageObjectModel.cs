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
