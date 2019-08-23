using MicroservicesUIAcceptance.BaseMethods;
using MicroservicesUIAcceptance.SaaSTests.AuthenticationMethod;
using MicroServicesUIAcceptance.SaaSTests.DataIndex;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace MicroServicesUIAcceptance.SaaSTests.Tests
{
    class DemoTests
    {
        SeleniumHelper seleniumHelper;
        [Test]
        public void DemoTest()
        {
            this.seleniumHelper = new SeleniumHelper();
            // Demo Call to click for Demo XPath fetched through index values
            this.seleniumHelper.Click(XPathIndex.Instance.Return_demoXPath());
        }

        [TearDown]
        public void TearItDown()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                this.seleniumHelper.CloseDriver();
            }
        }
    }
}
