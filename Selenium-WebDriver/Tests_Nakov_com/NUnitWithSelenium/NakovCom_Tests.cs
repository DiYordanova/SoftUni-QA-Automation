using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace NUnitWithSelenium
{
    public class NakovCom_Tests
    {
        ChromeDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
           // options.AddArguments("--headless");
            driver = new ChromeDriver(options);
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

        [Test]
        public void Test_NakovComSearch()
        {
            driver.Navigate().GoToUrl("https://nakov.com");
            var searchBox = driver.FindElement(By.CssSelector("#s"));
            searchBox.Click();
            searchBox.SendKeys("database");
            searchBox.SendKeys(Keys.Enter);
            var searchResultBox = driver.FindElement(By.CssSelector("body > section > hgroup > h3"));
            var resultText = searchResultBox.Text;

            Assert.AreEqual("Search Results for – \"database\"", resultText);
        }


        [OneTimeTearDown]
        public void ShutDown()
        {
            driver.Quit();
        }
    }
}