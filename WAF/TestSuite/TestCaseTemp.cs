using NUnit.Framework;
using WAF.Framework.BaseClasses;
using WAF.Pages;

namespace WAF.TestSuite
{
    [TestFixture]
    class TestCaseTemp : BaseSetup
    {
        [Test]
        public void TC001()
        {
            Browser.Open();
            PagesTemp.NavigateToLoginPage();
            Browser.GoBack();
            PagesTemp.InsertRandomName();
        }
    }
}
