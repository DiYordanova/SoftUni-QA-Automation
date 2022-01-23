using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Test_Wikipedia
{
    public class WikipediaTest
    {
        ChromeDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void LoadsCorrectlyData_WhenSearch()
        {           
            driver.Url = "https://wikipedia.org";
            driver.FindElement(By.CssSelector("#searchInput")).SendKeys("QA");
            driver.FindElement(By.CssSelector("#searchInput")).SendKeys(Keys.Enter);

            Assert.AreEqual("https://en.wikipedia.org/wiki/QA", driver.Url);
        }

        [Test]
        public void DisplaysCorrectTitle_WhenEnterValidData()
        {   
            driver.Url = "https://wikipedia.org";
            driver.FindElement(By.CssSelector("#searchInput")).SendKeys("QA");
            driver.FindElement(By.CssSelector("#searchInput")).SendKeys(Keys.Enter);
            var titleBox = driver.FindElement(By.CssSelector("#firstHeading")).Text;            

            Assert.AreEqual("QA", titleBox);            
        }


        [Test]
        public void DisplaysEmptyField_WhenDataNotEntered()
        {
            driver.Url = "https://wikipedia.org";
            driver.FindElement(By.CssSelector("#search-form > fieldset > button > i")).Click();
            var searchTitle = driver.FindElement(By.CssSelector("#firstHeading")).Text;
            var searchBox = driver.FindElement(By.XPath("//input[contains(@id,'ooui-php-1')]")).Text;
            var searchVector = driver.FindElement(By.CssSelector("#searchInput")).Text;

            Assert.AreEqual("Search", searchTitle);

            Assert.AreEqual("", searchBox);

            Assert.AreEqual("", searchVector);
            
        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            driver.Quit();
        }
    }
}