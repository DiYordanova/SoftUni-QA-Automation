using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace QA_Appium_ContactBook
{
    public class Test_ContactBook_AndroidAppium
    {
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;
        private const string AppiumURI = "http://127.0.0.1:4723/wd/hub";
        private const string ApiServiceUrl = "https://contactbook.nakov.repl.co/api";
        private const string appPath = @"C:\Users\Dyliana\Desktop\SoftUni-QA-Automation\Appium\QA_Appium_Android_ContactBook\contactbook-androidclient.apk";
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            options = new AppiumOptions();
            options.AddAdditionalCapability(MobileCapabilityType.App, appPath);
            options.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            driver = new AndroidDriver<AndroidElement>(new Uri(AppiumURI), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));            
        }

        [Test]
        public void Search_ValidData_SingleResult()
        {            
            var contactBookUrl = driver.FindElement(By.Id(
                "contactbook.androidclient:id/editTextApiUrl"));
            contactBookUrl.Clear();
            contactBookUrl.SendKeys(ApiServiceUrl);

            var buttonConnect = driver.FindElement(By.Id(
                "contactbook.androidclient:id/buttonConnect"));
            buttonConnect.Click();          
           
            var keywordTextBox = driver.FindElementById(
                "contactbook.androidclient:id/editTextKeyword");
            keywordTextBox.Clear();
            keywordTextBox.SendKeys("albert");

            var connectSearch = driver.FindElement(By.Id(
                "contactbook.androidclient:id/buttonSearch"));
            connectSearch.Click();      
           
            var firstNameBox = driver.FindElement(By.Id(
                "contactbook.androidclient:id/textViewFirstName"));            

            var lastNameBox = driver.FindElement(By.Id(
                "contactbook.androidclient:id/textViewLastName"));

            var contactCount = driver.FindElementById("contactbook.androidclient:id/textViewSearchResult").Text;
            wait.Until(t => connectSearch.Text != "");

            Assert.AreEqual("Contacts found: 1", contactCount);
            Assert.AreEqual("Albert", firstNameBox.Text);
            Assert.AreEqual("Einstein", lastNameBox.Text);
        }

        [Test]
        public void Search_ValidData_MultipleResults()
        {
            var contactBookUrl = driver.FindElement(By.Id(
                "contactbook.androidclient:id/editTextApiUrl"));
            contactBookUrl.Clear();
            contactBookUrl.SendKeys(ApiServiceUrl);

            var buttonConnect = driver.FindElement(By.Id(
                "contactbook.androidclient:id/buttonConnect"));
            buttonConnect.Click();

            var keywordTextBox = driver.FindElementById(
                "contactbook.androidclient:id/editTextKeyword");
            keywordTextBox.Clear();
            keywordTextBox.SendKeys("e");

            var connectSearch = driver.FindElement(By.Id(
              "contactbook.androidclient:id/buttonSearch"));
            connectSearch.Click();
            var contactCount = driver.FindElementById("contactbook.androidclient:id/textViewSearchResult").Text;

            Assert.AreEqual("Contacts found: 3", contactCount);

            var firstCellName = driver.FindElementByXPath("//android.widget.TableLayout[1]/android.widget.TableRow[3]/android.widget.TextView[2]").Text;

            var secondCellName = driver.FindElementByXPath("//android.widget.TableLayout[2]/android.widget.TableRow[3]/android.widget.TextView[2]").Text;

            var lastCellName = driver.FindElementByXPath("//android.widget.TableLayout[3]/android.widget.TableRow[3]/android.widget.TextView[2]").Text;

            Assert.That(firstCellName.Contains("e"));
            Assert.That(secondCellName.Contains("e"));
            Assert.That(lastCellName.Contains("e"));            
        }

        [Test]
        public void Search_InvalidData()
        {
            var contactBookUrl = driver.FindElement(By.Id(
                "contactbook.androidclient:id/editTextApiUrl"));
            contactBookUrl.Clear();
            contactBookUrl.SendKeys(ApiServiceUrl);

            var buttonConnect = driver.FindElement(By.Id(
                "contactbook.androidclient:id/buttonConnect"));
            buttonConnect.Click();

            var keywordTextBox = driver.FindElementById(
                "contactbook.androidclient:id/editTextKeyword");
            keywordTextBox.Clear();
            keywordTextBox.SendKeys("Aaaaa");

            var connectSearch = driver.FindElement(By.Id(
              "contactbook.androidclient:id/buttonSearch"));
            connectSearch.Click();
            var contactCount = driver.FindElementById("contactbook.androidclient:id/textViewSearchResult").Text;

            Assert.AreEqual("Contacts found: 0", contactCount);
        }
        
        [TearDown]
        public void ShutDown()
        {
            driver.Quit();          
        }
    }
}