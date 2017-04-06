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
