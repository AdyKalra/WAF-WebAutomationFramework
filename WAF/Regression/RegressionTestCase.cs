using NUnit.Framework;
using WAF.Framework.BaseClasses;

namespace WAF.Regression
{
    [TestFixture]
    class RegressionTestCase : BaseSetup
    {
        [Test]
        public void RegressionTestCase_01()
        {
            Browser.Open();
        }
    }
}
