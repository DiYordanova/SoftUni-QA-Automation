using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;

namespace QA_Appium_WindForm.Window
{
    public class WindowSummatorPOM
    {
        private readonly WindowsDriver<WindowsElement> driver;
        public WindowSummatorPOM(WindowsDriver<WindowsElement> driver)
        {
            this.driver = driver;
        }

        public IWebElement field1 => driver.FindElementByAccessibilityId("textBoxFirstNum");
        public IWebElement field2 => driver.FindElementByAccessibilityId("textBoxSecondNum");
        public IWebElement buttonCalc => driver.FindElementByAccessibilityId("buttonCalc");
        public IWebElement result => driver.FindElementByAccessibilityId("textBoxSum");

        public string Calculator(string num1, string num2)
        {
            field1.Clear();
            field1.SendKeys(num1);
            field2.Clear();
            field2.SendKeys(num2);
            buttonCalc.Click();

            return result.Text;
        }
    }
}
