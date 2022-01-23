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

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void Test_AddUrl()
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
            var submitButton = driver.FindElement(By.XPath("//form/button[@type='submit']"));
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

        [TearDown]
        public void ShutDown()
        {
            driver.Quit();
        }
    }
}