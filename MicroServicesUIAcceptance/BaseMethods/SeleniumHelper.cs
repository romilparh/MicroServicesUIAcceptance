using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;
using log4net;

namespace MicroservicesUIAcceptance.BaseMethods
{
    public class SeleniumHelper
    {
        private IWebDriver driver;
        private string appURL, browserName, developerName;
        double waitTime, implicitWaitTime;
        protected static ILog logger = LogManager.GetLogger(typeof(SeleniumHelper));

        public SeleniumHelper()
        {
            try
            {
                this.appURL = TestContext.Parameters["appURL"].ToString();
                this.browserName = TestContext.Parameters["browserName"].ToString().ToLower();
                this.developerName = TestContext.Parameters["developerName"].ToString();
                this.waitTime = 25;
                this.implicitWaitTime = 60;
                if (this.appURL == null)
                {
                    throw new Exception("App URL Null");
                } else if (developerName == null)
                {
                    throw new Exception("Developer Name Null");
                }
                switch (this.browserName)
                {
                    case "firefox":
                        this.driver = new FirefoxDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                        break;
                    default:
                        this.driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                        break;
                }
                this.driver.Manage().Cookies.DeleteAllCookies();
                this.driver.Manage().Window.Maximize();
                this.NavigateToHomeUrl();
                logger.Info("App URL Opened");
            }
            catch (Exception e)
            {
                logger.Error("\nException ---\n{0}" + e.StackTrace);
                Assert.Fail("Failed to Initialise Selenium");
            }
        }

        internal IWebDriver returnDriver()
        {
            return this.driver;
        }

        internal void Wait(By by)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(this.waitTime));
                wait.Until(ExpectedConditions.ElementExists(by));
            }
            catch (Exception e)
            {
                logger.Error("\nWait Method ---\n" + e.StackTrace);
                this.TearItDown();
            }
        }

        internal void ImplicitWait()
        {
            this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(this.implicitWaitTime);
        }

        internal void WaitUntilVisible(By by)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(this.waitTime));
                IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
            }
            catch (Exception e)
            {
                logger.Error("\nImplicit Wait Method ---\n" + e.StackTrace);
                this.TearItDown();
            }
        }

        internal void WaitUntilElementClickable(By by)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(this.waitTime));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
            }
            catch (Exception e)
            {
                logger.Error("\nWait Until Element Clickable Method ---\n" + e.StackTrace);
                this.TearItDown();
            }

        }

        internal IWebElement FindElement(By by)
        {
            this.Wait(by);
            try
            {
                return this.driver.FindElement(by);
            }
            catch (Exception e)
            {
                logger.Error("\nElement can not be found ---\n" + e.StackTrace);
                this.TearItDown();
                return null;
            }

        }

        public void NavigateToUrl(string url)
        {
            this.driver.Navigate().GoToUrl(url);
        }

        public void NavigateToHomeUrl()
        {
            this.NavigateToUrl(this.appURL);
        }

        internal void ClearElement(By by)
        {
            this.WaitUntilElementClickable(by);
            try
            {
                this.FindElement(by).Clear();
            }
            catch (Exception e)
            {
                logger.Error("\nElement can not be cleared ---\n" + e.StackTrace);
                this.TearItDown();
            }

        }

        internal void SendKeys(By by, string valueToType, bool inputValidation = false)
        {
            try
            {
                this.WaitUntilElementClickable(by);
                this.FindElement(by).Clear();
                this.FindElement(by).SendKeys(valueToType);
            }
            catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverTimeoutException)
            {
                logger.Error("\nCan not send keys: No such element/WebDriverTimeoutException ---\n{0}" + ex.StackTrace);
                this.TearItDown();
                Assert.Fail("Keys could not be sent");
            }
            catch (Exception ex) when (ex is StaleElementReferenceException)
            {
                // find element again and retry
                this.WaitUntilElementClickable(by);
                this.ClearElement(by);
                this.FindElement(by).SendKeys(valueToType);
            }
        }

        // Tries to click an element taking into account a predefined timeout
        internal void Click(By by)
        {
            try
            {
                this.Wait(by);
                this.FindElement(by).Click();
            }
            catch (Exception ex) when (ex is WebDriverTimeoutException || ex is NoSuchElementException)
            {
                logger.Error("\nCan not send keys: No such element/WebDriverTimeoutException ---\n{0}" + ex.StackTrace);
                this.TearItDown();
                Assert.Fail("Element Not Clicked/can not be clicked");
            }
        }

        internal void BeEquivalentTo(By by, string message)
        {
            try
            {
                this.Wait(by);
                this.FindElement(by).Text.Should().BeEquivalentTo(message);
            }
            catch (Exception e)
            {
                logger.Error("\nValues not equivalent ---\n" + this.FindElement(by).Text+" != " + message +"\n"+e.StackTrace);
                this.TearItDown();
                Assert.Fail("\nValues not equivalent ---\n" + this.FindElement(by).Text + " != " + message);
            }
        }

        internal void BeEquivalentTo(By by, string message, string attribute)
        {
            try
            {
                this.Wait(by);
                this.FindElement(by).GetAttribute(attribute).Should().BeEquivalentTo(message);
                }
            catch (Exception e)
            {
                logger.Error("\nValues not equivalent ---\n" + this.FindElement(by).GetAttribute(attribute) + " != " + message + "\n" + e.StackTrace);
                this.TearItDown();
                Assert.Fail("\nValues not equivalent ---\n" + this.FindElement(by).GetAttribute(attribute) + " != " + message);
            }
        }
        internal void BeEquivalentLesserEqualTo_Double(double expectedValue, double fetchedValue)
        {
            if (expectedValue <= fetchedValue)
            {
                this.CloseDriver();
                Assert.Pass("Expected Value: " + expectedValue + " = Fetched Value: " + fetchedValue + "\nPASS");
            }
            else
            {
                logger.Error("\nValues not equivalent ---\n{0}" + expectedValue + " != " + fetchedValue + "\n");
                this.TearItDown();
                Assert.Fail("Expected Value: " + expectedValue + " = Fetched Value: " + fetchedValue + "\nFAIL");
            }
        }


        internal void CloseDriver()
        {
            this.driver.Close();
            this.driver.Quit();
            this.driver.Dispose();
        }

        private static string CurrentWorkingDirectory => new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) ??
                throw new InvalidOperationException()).LocalPath;

        [TearDown]
        public void TearItDown()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                var screenshot = ((ITakesScreenshot)this.driver).GetScreenshot();
                var filename = $"{this.developerName}{"_"}{TestContext.CurrentContext.Test.MethodName}{"_screenshot_"}{DateTime.Now.Ticks}{".png"}";
                var path = $"{CurrentWorkingDirectory}{filename}";
                screenshot.SaveAsFile(path, ScreenshotImageFormat.Png);
                TestContext.AddTestAttachment(path);
            }
        }

    }
}
