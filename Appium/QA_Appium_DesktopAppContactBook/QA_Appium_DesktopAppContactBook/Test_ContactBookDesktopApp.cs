using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.UI;
using System;

namespace QA_Appium_DesktopAppContactBook
{
    public class Test_ContactBookDesktopApp
    {
        private const string AppiumServerUrl = "http://[::1]:4723/wd/hub";
        private const string AppiumForTesting = @"C:\Users\35989\source\repos\QA_Appium\QA_Appium_DesktopAppContactBook/ContactBook-DesktopClient.exe";
        private const string ContaktBookApiUrl = "https://contactbook.nakov.repl.co/api";
        private WindowsDriver<WindowsElement> driver;        

        [SetUp]
        public void Setup()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability(MobileCapabilityType.App, AppiumForTesting);
            options.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Windows");
            driver = new WindowsDriver<WindowsElement>(new Uri(AppiumServerUrl), options);     
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test]
        public void Test_SearchSteve()
        {
            var textBoxApiUrl = driver.FindElementByAccessibilityId("textBoxApiUrl");
            textBoxApiUrl.Clear();
            textBoxApiUrl.SendKeys(ContaktBookApiUrl);
            var buttonConnect = driver.FindElementByAccessibilityId("buttonConnect");
            buttonConnect.Click();

            string windowName = driver.WindowHandles[0];
            driver.SwitchTo().Window(windowName);

            var textBoxSearch = driver.FindElementByAccessibilityId("textBoxSearch");
            textBoxSearch.Clear();
            textBoxSearch.SendKeys("steve");
            var buttonSearch = driver.FindElementByAccessibilityId("buttonSearch");                 
            buttonSearch.Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(d => {
                var labelResult = driver.FindElementByAccessibilityId("labelResult");
                return labelResult.Text.StartsWith("Contacts found:");
            });

            var labelResult = driver.FindElementByAccessibilityId("labelResult");

            var tableContacts = driver.FindElementByAccessibilityId("dataGridViewContacts");
            var cellFirstName = tableContacts.FindElementByXPath("//Edit[@Name='FirstName Row 0, Not sorted.']");
            var cellLastName = tableContacts.FindElementByXPath("//Edit[@Name=\"LastName Row 0, Not sorted.\"]");

            Assert.AreEqual("Steve", cellFirstName.Text);
            Assert.AreEqual("Jobs", cellLastName.Text);
        }

        [TearDown]
        public void ShutDown()
        {
            driver.Quit();
        }
    }
}