using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Test_ExCom
{
    public class Tests_ExCom
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void Test_EurBgn_WorkCorrectly()
        {
            driver.Navigate().GoToUrl("https://www.xe.com/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            //If Cookies is on display
            var cookiesAccept = driver.FindElement(By.XPath("//div[3]/section/div[2]/button[2]"));
            cookiesAccept.Click();

            driver.FindElement(By.CssSelector("input#amount")).Click();
            driver.FindElement(By.CssSelector("input#amount")).SendKeys("1");
            driver.FindElement(By.Id("midmarketFromCurrency")).SendKeys("eur");
            driver.FindElement(By.Id("midmarketFromCurrency")).SendKeys(Keys.Enter);
            driver.FindElement(By.Id("midmarketToCurrency")).SendKeys("bgn");
            driver.FindElement(By.Id("midmarketToCurrency")).SendKeys(Keys.Enter);
            driver.FindElement(By.CssSelector(".currency-converter__SubmitZone-zieln1-3 > .button__BaseButton-sc-1qpsalo-0")).Click();
            var rateFound = driver.FindElement(By.XPath("//p[@class='result__BigRate-sc-1bsijpp-1 iGrAod']"));

            Assert.True(rateFound != null);

            Assert.That(driver.FindElement(By.CssSelector(".result__BigRate-sc-1bsijpp-1")).Text, Is.EqualTo("1.95583 Bulgarian Leva"));

        }

        [TearDown]
        public void Shutdown()
        {
            driver.Quit();
        }
    }
}