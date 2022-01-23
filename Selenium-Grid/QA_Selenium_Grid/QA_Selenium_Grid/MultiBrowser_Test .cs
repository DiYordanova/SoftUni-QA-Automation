using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;

namespace QA_Selenium_Grid
{
    [TestFixture(typeof(FirefoxOptions))]
    [TestFixture(typeof(ChromeOptions))]

    public class MultiBrowser_Test<TOptions> where TOptions : DriverOptions, new()
    {
        private IWebDriver driver;

        [OneTimeSetUp]
        public void FixtureTearDown()
        {
            var options = new TOptions();
            driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), options);
        }

        [Test]
        public void Test_NakovCom_Title()
        {
            driver.Url = "https://nakov.com";
            string pageTitle = driver.Title;
            Assert.That(pageTitle.Contains("Svetlin Nakov"));
        }

        [Test]
        public void Test_NakovCom_OpenAboutPage()
        {
            driver.Url = "https://nakov.com";
            driver.FindElement(By.CssSelector("body > section > div.about-author > div > a > img")).Click();
            Assert.AreEqual("https://nakov.com/about/", driver.Url);
        }

        [Test]
        public void Test_NakovCom_SearchQA()
        {
            driver.Navigate().GoToUrl("https://nakov.com/");
            driver.FindElement(By.CssSelector("#sh > .smoothScroll")).Click();
            driver.FindElement(By.Id("s")).Click();
            driver.FindElement(By.Id("s")).SendKeys("QA");
            driver.FindElement(By.Id("searchsubmit")).Click();
            driver.FindElement(By.CssSelector(".entry-title")).Click();
            Assert.That(driver.FindElement(By.CssSelector(".entry-title")).Text, Is.EqualTo("Search Results for – \"QA\""));
        }

        [Test]
        public void Test_NakovCom_ContactsPage()
        {
            driver.Navigate().GoToUrl("https://nakov.com/");
            driver.Manage().Window.Size = new System.Drawing.Size(1552, 840);
            driver.FindElement(By.CssSelector(".contacts > a")).Click();
            driver.FindElement(By.CssSelector(".entry-title")).Click();
            Assert.That(driver.FindElement(By.CssSelector(".entry-title")).Text, Is.EqualTo("Svetlin Nakov – Contacts"));
        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            driver.Quit();
        }
    }
}