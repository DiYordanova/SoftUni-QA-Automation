using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace QA_Appium_WindForm
{
    public class Test_Appium_WinForm
    {        
        private WindowsDriver<WindowsElement> driver;        

        [OneTimeSetUp]
        public void Setup()
        {          
            var options = new AppiumOptions();
            options.AddAdditionalCapability(MobileCapabilityType.App, @"C:\Users\35989\source\repos\QA_Appium\QA_Appium_WindForm\WindowsFormsApp.exe");
            options.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Windows");
            options.AddAdditionalCapability(MobileCapabilityType.DeviceName, "WindowsPC");
            driver = new WindowsDriver<WindowsElement>( new Uri("http://[::1]:4723/wd/hub"), options);         
        }

        [Test]
        public void Test_Sumator_ValidData()
        {
            driver.FindElementByAccessibilityId("textBoxFirstNum").Clear();
            driver.FindElementByAccessibilityId("textBoxFirstNum").SendKeys("20");
            driver.FindElementByAccessibilityId("textBoxSecondNum").Clear();
            driver.FindElementByAccessibilityId("textBoxSecondNum").SendKeys("30");
            driver.FindElementByAccessibilityId("buttonCalc").Click();
            string sum = driver.FindElementByAccessibilityId("textBoxSum").Text;
            Assert.AreEqual("50", sum);
        }

        [Test]
        public void Test_Sumator_InalidData()
        {
            driver.FindElementByAccessibilityId("textBoxFirstNum").Clear();
            driver.FindElementByAccessibilityId("textBoxFirstNum").SendKeys("20");
            driver.FindElementByAccessibilityId("textBoxSecondNum").Clear();
            driver.FindElementByAccessibilityId("textBoxSecondNum").SendKeys("aaa");
            driver.FindElementByAccessibilityId("buttonCalc").Click();
            string sum = driver.FindElementByAccessibilityId("textBoxSum").Text;
            Assert.AreEqual("error", sum);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();            
        }
    }
}