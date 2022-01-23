using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace Test_SumNumbers_NakovRepl
{
    public class Tests
    {
        ChromeDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void ValidData_WhenAddTwoNumbers()
        {
            driver.Url = "https://sum-numbers.nakov.repl.co";
            driver.FindElement(By.XPath("//input[contains(@id,'number1')]")).SendKeys("10");
            driver.FindElement(By.XPath("//input[contains(@id,'number2')]")).SendKeys("5");
            driver.FindElement(By.XPath("//input[contains(@id,'calcButton')]")).Click();
            var result = driver.FindElement(By.CssSelector("#result > pre")).Text;

            Assert.AreEqual("15", result);
        }

        [Test]
        public void InvalidInput_WhenAddInvalidNumbers()
        {
            driver.Url = "https://sum-numbers.nakov.repl.co";          
            driver.FindElement(By.XPath("//input[contains(@id,'number1')]")).SendKeys("Hello");
            driver.FindElement(By.XPath("//input[contains(@id,'number2')]")).SendKeys("");
            driver.FindElement(By.XPath("//input[contains(@id,'calcButton')]")).Click();
            var result = driver.FindElement(By.CssSelector("#result")).Text;

            Assert.AreEqual("Sum: invalid input", result);
        }

        [Test]
        public void WorksCorrectly_ResetButton()
        {
            driver.Url = "https://sum-numbers.nakov.repl.co";         
            driver.FindElement(By.XPath("//input[contains(@id,'number1')]")).SendKeys("3");
            driver.FindElement(By.XPath("//input[contains(@id,'number2')]")).SendKeys("4");
            driver.FindElement(By.XPath("//input[contains(@id,'calcButton')]")).Click();
            var numberOne = driver.FindElement(By.XPath("//input[contains(@id,'number1')]")).GetAttribute("value");
            Assert.IsNotEmpty(numberOne);
            var numberTwo = driver.FindElement(By.XPath("//input[contains(@id,'number2')]")).GetAttribute("value");
            Assert.IsNotEmpty(numberTwo);
            var result = driver.FindElement(By.CssSelector("#result")).Text;
            Assert.IsNotEmpty(result);

            driver.FindElement(By.XPath("//input[contains(@id,'resetButton')]")).Click();
            numberOne = driver.FindElement(By.XPath("//input[contains(@id,'number1')]")).GetAttribute("value");
            Assert.AreEqual("", numberOne);
            numberTwo = driver.FindElement(By.XPath("//input[contains(@id,'number2')]")).GetAttribute("value");
            Assert.AreEqual("", numberTwo);
            result = driver.FindElement(By.CssSelector("#result")).Text;
            Assert.AreEqual("", result);
        }

        [OneTimeTearDown]
        public void Shutdown()
        {
            driver.Quit();
        }
    }
}