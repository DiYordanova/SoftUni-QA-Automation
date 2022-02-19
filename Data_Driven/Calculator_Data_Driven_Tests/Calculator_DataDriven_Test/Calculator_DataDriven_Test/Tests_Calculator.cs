using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Calculator_DataDriven_Test
{
    public class Tests
    {
        ChromeDriver driver;
        IWebElement textBoxNum1;
        IWebElement selectBoxOperation;
        IWebElement textBoxNum2;
        IWebElement buttonCalculate;
        IWebElement buttonReset;
        IWebElement divResult;

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://number-calculator.nakov.repl.co ");
            textBoxNum1 = driver.FindElement(By.Id("number1"));
            selectBoxOperation = driver.FindElement(By.Id("operation"));
            textBoxNum2 = driver.FindElement(By.Id("number2"));
            buttonCalculate = driver.FindElement(By.Id("calcButton"));
            buttonReset = driver.FindElement(By.Id("resetButton"));
            divResult = driver.FindElement(By.Id("result"));
        }

        [TestCase("5", "+", "3", "Result: 8")]
        [TestCase("5", "-", "3", "Result: 2")]
        [TestCase("10", "*", "2", "Result: 20")]
        [TestCase("10", "/", "2", "Result: 5")]

        [TestCase("5.4", "+", "3.4", "Result: 8.8")]
        [TestCase("5.8", "-", "3.2", "Result: 2.6")]
        [TestCase("5.89", "*", "3.85", "Result: 22.6765")]
        [TestCase("3.3", "/", "3", "Result: 1.1")]

        [TestCase("", "+", "8", "Result: invalid input")]
        [TestCase("", "-", "9", "Result: invalid input")]
        [TestCase("","*", "3", "Result: invalid input")]
        [TestCase("", "/", "5", "Result: invalid input")]

        [TestCase("7", "+", "", "Result: invalid input")]
        [TestCase("5", "-", "", "Result: invalid input")]
        [TestCase("4", "*", "", "Result: invalid input")]
        [TestCase("5", "/", "", "Result: invalid input")]

        [TestCase("juy", "/", "5", "Result: invalid input")]
        [TestCase("5", "/", "jykyu", "Result: invalid input")]
        [TestCase("kuyky", "/", "juykyu", "Result: invalid input")]

        [TestCase("7", "@", "2", "Result: invalid operation")]
        [TestCase("4", "", "5", "Result: invalid operation")]
        [TestCase("7", "!", "5", "Result: invalid operation")]

        [TestCase("Infinity", "+", "1", "Result: Infinity")]
        [TestCase("Infinity", "-", "1", "Result: Infinity")]
        [TestCase("Infinity", "*", "1", "Result: Infinity")]
        [TestCase("Infinity", "/", "1", "Result: Infinity")]


        [TestCase("1", "+", "Infinity", "Result: Infinity")]
        [TestCase("2", "-", "Infinity", "Result: -Infinity")]
        [TestCase("3", "*", "Infinity", "Result: Infinity")]
        [TestCase("4", "/", "Infinity", "Result: 0")]

        [TestCase("Infinity", "+", "Infinity", "Result: Infinity")]
        [TestCase("Infinity", "-", "Infinity", "Result: invalid calculation")]
        [TestCase("Infinity", "*", "Infinity", "Result: Infinity")]
        [TestCase("Infinity", "/", "Infinity", "Result: invalid calculation")]

        [TestCase("1.2e28", "*", "2", "Result: 2.4e+28")]
        [TestCase("5.8e89", "/", "2e23", "Result: 2.9e+66")]
        


        public void TestCalculatorWebApp(string num1, string operation, string num2, string expecteResult)
        {
            buttonReset.Click();
            if (num1 != "")
            {
                textBoxNum1.SendKeys(num1);
            }

            if (operation != "")
            {
                selectBoxOperation.SendKeys(operation);
            }

            if (num2 != "")
            {
                textBoxNum2.SendKeys(num2);
            }

            buttonCalculate.Click();
            var actualResult = divResult.Text;

            Assert.AreEqual(expecteResult, actualResult);

        }

        [OneTimeTearDown]
        public void Test1()
        {
            driver.Quit();
        }
    }
}