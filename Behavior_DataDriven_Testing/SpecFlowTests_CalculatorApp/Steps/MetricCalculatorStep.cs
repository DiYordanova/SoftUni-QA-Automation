using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace SpecFlowTests_CalculatorApp.Steps
{
    [Binding,Scope(Feature = "Metric Calculator")]
    public class MetricCalculatorStep
    {
        static ChromeDriver driver;
        static IWebElement textBoxInputValue;        
        static SelectElement dropDownInitialMetric;
        static SelectElement dropDownSecondMetric;
        static IWebElement buttonCalc;
        static IWebElement buttonReset;
        static IWebElement divResult;

        [BeforeFeature]
        public static void OpenCalculatorApp()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://js-calculator.nakov.repl.co");
            var lincMetricCalc = driver.FindElement(By.XPath("//a[@href='#'][contains(.,'Metric Calculator')]"));
            lincMetricCalc.Click();
            textBoxInputValue = driver.FindElement(By.CssSelector("input#fromValue"));
            dropDownInitialMetric = new SelectElement(driver.FindElement(By.CssSelector("select#sourceMetric")));
            dropDownSecondMetric = new SelectElement(driver.FindElement(By.CssSelector("select#destMetric")));
            buttonCalc = driver.FindElement(By.CssSelector("#screenMetricCalc input[value='Calculate']"));
            buttonReset = driver.FindElement(By.CssSelector("#screenMetricCalc input[value='Reset']"));
            divResult = driver.FindElement(By.CssSelector("#screenMetricCalc div.result"));
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

        [Given("the input value is (.*)")]
        public void GivenTheInputValueIs(string inputValue)
        {
            textBoxInputValue.SendKeys(inputValue);
        }

        [Given("initial metric is \"(.*)\"")]
        public void GivenInitialMetricIs(string initialMetric)
        {
            dropDownInitialMetric.SelectByValue(initialMetric);
        }

        [Given("to second metric is \"(.*)\"")]
        public void GivenSecondMetricIs(string secondMetric)
        {
            dropDownSecondMetric.SelectByValue(secondMetric);
        }

        [When("the conversion is performed")]
        public void WhenConversionIsPerformed()
        {          
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
