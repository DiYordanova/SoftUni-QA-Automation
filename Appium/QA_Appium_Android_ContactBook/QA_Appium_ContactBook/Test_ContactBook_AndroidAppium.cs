using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using System;

namespace QA_Appium_ContactBook
{
    public class Test_ContactBook_AndroidAppium
    {
        private AndroidDriver<AndroidElement> driver;

        [SetUp]
        public void Setup()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability(MobileCapabilityType.App, @"C:\Users\35989\source\repos\QA_Appium\QA_Appium_ContactBook\contactbook-androidclient.apk");
            options.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            driver = new AndroidDriver<AndroidElement>(new Uri("http://[::1]:4723/wd/hub"), options);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }

        [Test]
        public void Test_ContactsBookSearch()
        {            
            var contactBookUrl = driver.FindElement(By.Id(
                "contactbook.androidclient:id/editTextApiUrl"));
            contactBookUrl.Clear();
            contactBookUrl.SendKeys("https://contactbook.nakov.repl.co/api");

            var buttonConnect = driver.FindElement(By.Id(
                "contactbook.androidclient:id/buttonConnect"));
            buttonConnect.Click();

           var keywordTextBox = driver.FindElement(By.Id(
                "contactbook.androidclient:id/editTextKeyword"));
            keywordTextBox.Clear();
            keywordTextBox.SendKeys("albert");

            var connectSearch = driver.FindElement(By.Id(
                "contactbook.androidclient:id/buttonSearch"));
            connectSearch.Click();      
           
            var firstNameBox = driver.FindElement(By.Id(
                "contactbook.androidclient:id/textViewFirstName"));            

            var lastNameBox = driver.FindElement(By.Id(
                "contactbook.androidclient:id/textViewLastName"));

            Assert.AreEqual("Albert", firstNameBox.Text);
            Assert.AreEqual("Einstein", lastNameBox.Text);
        }

        [TearDown]
        public void ShutDown()
        {
            driver.Quit();          
        }
    }
}