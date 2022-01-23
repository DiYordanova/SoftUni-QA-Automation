using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace QA_Selenium_Grid
{
    public class ParallelBrowser_Test
    {
        [ThreadStatic]
        private IWebDriver driver;

        [OneTimeSetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), options);          
        }

        [Test]
        [Parallelizable]
        public void Test_NakovCom_Title()
        {
            driver.Url = "https://nakov.com";
            string pageTitle = driver.Title;
            Assert.That(pageTitle.Contains("Svetlin Nakov"));
        }

        [Test]
        [Parallelizable]
        public void Test_NakovCom_OpenAboutPage()
        {
            driver.Url = "https://nakov.com";
            driver.FindElement(By.CssSelector("body > section > div.about-author > div > a > img")).Click();
            Assert.AreEqual("https://nakov.com/about/", driver.Url);
        }

        [Test]
        [Parallelizable]
        public void Test_NakovCom_SearchQA()
        {
            driver.Navigate().GoToUrl("https://nakov.com/");
            Thread.Sleep(3000);
            driver.FindElement(By.CssSelector("#sh > .smoothScroll")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("s")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("s")).SendKeys("QA");
            driver.FindElement(By.Id("searchsubmit")).Click();
            driver.FindElement(By.CssSelector(".entry-title")).Click();
            Assert.That(driver.FindElement(By.CssSelector(".entry-title")).Text, Is.EqualTo("Search Results for – \"QA\""));
        }

        [Test]
        [Parallelizable]
        public void Test_NakovCom_ContactsPage()
        {
            driver.Navigate().GoToUrl("https://nakov.com/");
            driver.Manage().Window.Size = new System.Drawing.Size(1552, 840);
            Thread.Sleep(1000);
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