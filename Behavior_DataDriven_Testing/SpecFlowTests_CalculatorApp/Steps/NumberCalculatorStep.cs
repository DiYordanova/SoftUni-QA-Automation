using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using TechTalk.SpecFlow;

namespace SpecFlowTests_CalculatorApp.Steps
{
    [Binding,Scope(Feature = "Number Calculator")]
    public class NumberCalculatorStep
    {
        static ChromeDriver driver;
        static IWebElement textBoxNum1;
        static IWebElement textBoxNum2;
        static SelectElement dropDownOperation;
        static IWebElement buttonCalc;
        static IWebElement buttonReset;
        static IWebElement divResult;

        [BeforeFeature]
        public static void OpenCalculatorApp()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://js-calculator.nakov.repl.co");
            var lincNumberCalc = driver.FindElement(By.XPath("//a[@href='#'][contains(.,'Number Calculator')]"));
            lincNumberCalc.Click();
            textBoxNum1 = driver.FindElement(By.CssSelector("input#number1"));
            textBoxNum2 = driver.FindElement(By.CssSelector("input#number2"));
            dropDownOperation = new SelectElement(driver.FindElement(By.CssSelector("select#operation")));
            buttonCalc = driver.FindElement(By.CssSelector("#screenNumberCalc input[value='Calculate']"));
            buttonReset = driver.FindElement(By.CssSelector("#screenNumberCalc input[value='Reset']"));
            divResult = driver.FindElement(By.CssSelector("#screenNumberCalc div.result"));
        }


        [BeforeScenario]
        public static void ResetCalculatorApp()
        {            
            buttonReset.Click();
        }

        [AfterFeature]
        public static void CloseCalculatorApp()
        {
            driver.Quit();
        }

        [Given("the first number is (.*)")]
        public void GivenTheFirstNumberIs(string firstNumber)
        {
            textBoxNum1.SendKeys(firstNumber);
        }

        [Given("the second number is (.*)")]
        public void GivenTheSecondNumberIs(string secondNumber)
        {
            textBoxNum2.SendKeys(secondNumber);
        }

        [When("the two numbers are (.*)")]
        public void WhenTheTwoNumbersAreAdded(string operation)
        {            
            if (operation == "added")
            {
                dropDownOperation.SelectByValue("+");
            }

            else if (operation == "subtracted")
            {
                dropDownOperation.SelectByValue("-");
            }

            else if (operation == "multiplied")
            {
                dropDownOperation.SelectByValue("*");
            }

            else if (operation == "divided")
            {
                dropDownOperation.SelectByValue("/");
            }

            else
            {
                throw new InvalidOperationException($"Operation {operation} not supporterd by the Number Calculator app!");
            }
            
            buttonCalc.Click();
        }              

        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(string expectedResult)
        {
            var result = divResult.Text;
            result = result.Substring(8);
            result.Should().Be(expectedResult);            
        }
    }
}
