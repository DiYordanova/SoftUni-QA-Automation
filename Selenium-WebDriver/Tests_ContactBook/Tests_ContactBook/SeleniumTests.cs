using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;

namespace Tests_ContactBook
{
    public class SeleniumTests
    { 
        ChromeDriver driver;
        const string AppBaseUrl = "https://contactbook.nakov.repl.co";

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test]
        public void Test_ListContacts_CheckSteveJobs()
        {
            driver.Url = AppBaseUrl + "/";
            var iconViewContacts = driver.FindElement(By.PartialLinkText("View contacts"));
            iconViewContacts.Click();
            var cellFirtName = driver.FindElement(By.CssSelector("tr.fname > td")).Text;
            var cellLastName = driver.FindElement(By.CssSelector("tr.lname > td")).Text;
            Assert.AreEqual("Steve", cellFirtName);
            Assert.AreEqual("Jobs", cellLastName);
        }


        [Test]
        public void Test_SearchAndCheckAlbert()
        {
            driver.Url = AppBaseUrl + "/";
            var iconSearch = driver.FindElement(By.PartialLinkText("Search contacts"));
            iconSearch.Click();
            var textBoxSearch = driver.FindElement(By.CssSelector("input#keyword"));
            textBoxSearch.SendKeys("albert");
            var buttonSearch  = driver.FindElement(By.CssSelector("button#search"));
            buttonSearch.Click();
            var pageHeading = driver.FindElement(By.CssSelector("main > h1")).Text;
            var cellFirtName = driver.FindElement(By.CssSelector("tr.fname > td")).Text;
            var cellLastName = driver.FindElement(By.CssSelector("tr.lname > td")).Text;

            Assert.AreEqual("Contacts Matching Keyword \"albert\"", pageHeading);
            Assert.AreEqual("Albert", cellFirtName);
            Assert.AreEqual("Einstein", cellLastName);
        }


        [Test]
        public void Test_SearchInvalidData_And_VerifyResultIsEmpty()
        {
            driver.Url = AppBaseUrl + "/";
            var iconSearch = driver.FindElement(By.PartialLinkText("Search contacts"));
            iconSearch.Click();
            var textBoxSearch = driver.FindElement(By.CssSelector("input#keyword"));
            textBoxSearch.SendKeys("invalid2635");
            var buttonSearch = driver.FindElement(By.CssSelector("button#search"));
            buttonSearch.Click();
            var searchResult = driver.FindElement(By.CssSelector("div#searchResult")).Text;

            Assert.AreEqual("No contacts found.", searchResult);
        }

        [Test]
        public void Test_CreatContact_InvalidKeyword()
        {
            driver.Url = AppBaseUrl + "/";
            var iconCreate = driver.FindElement(By.PartialLinkText("Create new contact"));
            iconCreate.Click();
            var buttonCreate = driver.FindElement(By.CssSelector("button#create"));
            buttonCreate.Click();
            var errorMsg = driver.FindElement(By.CssSelector("div.err")).Text;

            Assert.AreEqual("Error: First name cannot be empty!", errorMsg);

        }

        [Test]
        public void Test_CreatContact_ValidData()
        {
            driver.Url = AppBaseUrl + "/";
            var iconCreate = driver.FindElement(By.PartialLinkText("Create new contact"));
            iconCreate.Click();
            var textBoxFirtsName = driver.FindElement(By.CssSelector("input#firstName"));
            var textBoxLastName = driver.FindElement(By.CssSelector("input#lastName"));
            var textBoxPhone = driver.FindElement(By.CssSelector("input#phone"));
            var textBoxEmail = driver.FindElement(By.CssSelector("input#email"));

            string firstName = "First Name" + DateTime.Now.Ticks;
            string lastName = "Last Name" + DateTime.Now.Ticks; ;
            string phone = "+411111" + DateTime.Now.Ticks; ; 
            string email = "name" + DateTime.Now.Ticks + "@gmail.com";

            textBoxFirtsName.SendKeys(firstName);
            textBoxLastName.SendKeys(lastName);
            textBoxPhone.SendKeys(phone);
            textBoxEmail.SendKeys(email);

            var buttonCreate = driver.FindElement(By.CssSelector("button#create"));
            buttonCreate.Click();
          
            var table = driver.FindElements(By.CssSelector("table.contact-entry"));
            var lastContact = table.Last();

            var cellFirtName = lastContact.FindElement(By.CssSelector("tr.fname > td")).Text;
            var cellLastName = lastContact.FindElement(By.CssSelector("tr.lname > td")).Text;
            var cellPhone = lastContact.FindElement(By.CssSelector("tr.phone > td")).Text;
            var cellEmail = lastContact.FindElement(By.CssSelector("tr.email > td")).Text;

            Assert.AreEqual(firstName, cellFirtName);
            Assert.AreEqual(lastName, cellLastName);
            Assert.AreEqual(phone, cellPhone);
            Assert.AreEqual(email, cellEmail);
        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            driver.Quit();
        }
    }
}