using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;

namespace QA_Appium_Android.Android
{
    public class SummatorAndroidPOM
    {
        private readonly AndroidDriver<AndroidElement> driver;

        public SummatorAndroidPOM(AndroidDriver<AndroidElement> driver)
        {
            this.driver = driver;
        }

        public IWebElement field1 => this.driver.FindElementById("com.example.androidappsummator:id/editText1");
        public IWebElement field2 => this.driver.FindElementById("com.example.androidappsummator:id/editText2");
        public IWebElement buttonCalc => this.driver.FindElementById("com.example.androidappsummator:id/buttonCalcSum");
        public IWebElement result => this.driver.FindElementById("com.example.androidappsummator:id/editTextSum");

        public string Calculator(string num1, string num2)
        {
            field1.SendKeys(num1);
            field2.SendKeys(num2);
            buttonCalc.Click();

            return result.Text;
        }
    }
}
