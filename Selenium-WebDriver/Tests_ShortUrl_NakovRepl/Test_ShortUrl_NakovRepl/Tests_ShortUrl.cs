using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;

namespace Test_ShortUrl_NakovRepl
{
    public class Tests_ShortUrl
    {
        ChromeDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Verify_HomePageTitleAndHeader()
        {
            driver.Url = "https://shorturl.nakov.repl.co";
            var urlShortener = driver.FindElement(By.XPath("//h1[contains(.,'URL Shortener')]")).Text;

            Assert.AreEqual("URL Shortener", driver.Title);
            Assert.AreEqual("URL Shortener", urlShortener);            
        }


        [Test]
        public void Verify_ShortUrlsTitleAndHeader()
        {
            driver.Url = "https://shorturl.nakov.repl.co/urls";
            var shortUrls = driver.FindElement(By.XPath("//h1[contains(.,'Short URLs')]")).Text;

            Assert.AreEqual("Short URLs", driver.Title);
            Assert.AreEqual("Short URLs", shortUrls);
        }

        [Test]
        public void Verify_ShortUrlsTable()
        {
            driver.Url = "https://shorturl.nakov.repl.co/urls";          
            var pageTitle = driver.FindElement(By.CssSelector("main > h1")).Text;
            var nakovComField = driver.FindElement(By.XPath("//a[@href='https://nakov.com']")).Text;
            var shorturlFiels = driver.FindElement(By.XPath("//a[@href='http://shorturl.nakov.repl.co/go/nak']")).Text;

            Assert.AreEqual("Short URLs", pageTitle);
            Assert.AreEqual("https://nakov.com", nakovComField);
            Assert.AreEqual("http://shorturl.nakov.repl.co/go/nak", shorturlFiels);
        }

        [Test]
        public void AddUrl_ValidData()
        {
            //Add new url
            driver.Url = "https://shorturl.nakov.repl.co/add-url";
            var textBoxUrl = driver.FindElement(By.Id("url"));
            var newUrl = "https://newurl" + DateTime.Now.Ticks + ".com";
            textBoxUrl.SendKeys(newUrl);
            var textBoxCode = driver.FindElement(By.CssSelector("#code"));
            var code = "code" + DateTime.Now.Ticks;
            textBoxCode.Clear();
            textBoxCode.SendKeys(code);
            var submitButton = driver.FindElement(By.XPath("//button[@type='submit'][contains(.,'Create')]"));
            submitButton.Click();


            //Assert the new URL succesfully added
            var tableRows = driver.FindElements(By.CssSelector("table tr"));
            var lastRow = tableRows.Last();
            var cells = lastRow.FindElements(By.CssSelector("td"));
            var firstCellText = cells[0].Text;

            Assert.AreEqual(newUrl, firstCellText);

            var secondCellText = cells[1].Text;

            Assert.AreEqual("http://shorturl.nakov.repl.co/go/" + code, secondCellText);
        }

        [Test]
        public void AddUrl_InvalidData()
        {            
            driver.Url = "https://shorturl.nakov.repl.co/add-url";
            var textBoxUrl = driver.FindElement(By.Id("url"));
            var newUrl = "99999";
            textBoxUrl.SendKeys(newUrl);
            var textBoxCode = driver.FindElement(By.CssSelector("#code"));
            var code = "code" + DateTime.Now.Ticks;
            textBoxCode.Clear();
            textBoxCode.SendKeys(code);
            var submitButton = driver.FindElement(By.XPath("//button[@type='submit'][contains(.,'Create')]"));
            submitButton.Click();
            var errorMassage = driver.FindElement(By.CssSelector("body > div"));
            Assert.AreEqual("Invalid URL!", errorMassage.Text);            
        }

        [Test]
        public void AddUrl_EmptyData()
        {
            driver.Url = "https://shorturl.nakov.repl.co/add-url";           
            var submitButton = driver.FindElement(By.XPath("//button[@type='submit'][contains(.,'Create')]"));
            submitButton.Click();
            var errorMassage = driver.FindElement(By.CssSelector("body > div"));
            Assert.AreEqual("URL cannot be empty!", errorMassage.Text);
        }

        [Test]
        public void Chech_VisitorCounterWorkCorrectly()
        {
            driver.Url = "https://shorturl.nakov.repl.co/urls";
            var visitorFieldBeforeClickText = driver.FindElement(By.CssSelector("body td:nth-child(4)")).Text;
            var visitorFieldBeforeClickNumber = int.Parse(visitorFieldBeforeClickText);
            var shortUrlField = driver.FindElement(By.CssSelector("body td:nth-child(2) > a"));
            shortUrlField.Click();
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            var visitorFieldAfterClickText = driver.FindElement(By.CssSelector("body td:nth-child(4)")).Text;
            var visitorFieldAfterClickNumber = int.Parse(visitorFieldAfterClickText);
            var visitorresult = visitorFieldAfterClickNumber + 1;
            Assert.AreEqual(visitorFieldBeforeClickNumber, visitorFieldAfterClickNumber);
        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            driver.Quit();
        }
    }
}