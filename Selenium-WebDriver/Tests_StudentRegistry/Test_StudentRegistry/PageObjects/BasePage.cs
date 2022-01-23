using OpenQA.Selenium;
using System;

namespace Test_StudentRegistry.PageObjects
{
    public class BasePage
    {
        protected readonly IWebDriver driver;

        public virtual string PageUrl { get; }

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }

        public IWebElement LinkHomePage =>
            driver.FindElement(By.XPath("//a[@href='/'][contains(.,'Home')]"));

        public IWebElement LinkViewStudentsPage =>
           driver.FindElement(By.XPath("//a[@href='/students'][contains(.,'View Students')]"));

        public IWebElement LinkAddStudentsPage =>
           driver.FindElement(By.XPath("//a[@href='/add-student'][contains(.,'Add Student')]"));

        public IWebElement PageHeading => 
            driver.FindElement(By.CssSelector("body > h1"));

        public void Open()
        {
            driver.Navigate().GoToUrl(this.PageUrl);
        }

        public bool IsOpen()
        {
            return driver.Url == this.PageUrl;
        }

        public string GetPageTitle()
        {
            return driver.Title;
        }

        public string GetPageHeadingText()
        {
            return PageHeading.Text;
        }
    }
}


