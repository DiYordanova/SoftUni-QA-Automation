using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using System;

namespace QA_Appium_Android
{
    public class Summator_Tests
    {
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;
        private const string AppiumURI = "http://127.0.0.1:4723/wd/hub";
        private const string app = @"C:\com.example.androidappsummator.apk";

        [SetUp]
        public void Setup()
        {
            options = new AppiumOptions() { PlatformName = "Android" };
            options.AddAdditionalCapability(MobileCapabilityType.App, app);
            driver = new AndroidDriver<AndroidElement>(new Uri(AppiumURI), options);
        }

        [Test]
        public void Test_ValidData()
        {
            var field1 = driver.FindElementById("com.example.androidappsummator:id/editText1");
            field1.SendKeys("5");
            var field2 = driver.FindElementById("com.example.androidappsummator:id/editText2");
            field2.SendKeys("5");
            var buttonCalc = driver.FindElementById("com.example.androidappsummator:id/buttonCalcSum");
            buttonCalc.Click();
            var result = driver.FindElementById("com.example.androidappsummator:id/editTextSum").Text;

            Assert.AreEqual("10", result);
        }

        [Test]
        public void Test_InvalidData()
        {
            var field1 = driver.FindElementById("com.example.androidappsummator:id/editText1");
            field1.SendKeys("aaa");
            var field2 = driver.FindElementById("com.example.androidappsummator:id/editText2");
            field2.SendKeys("5");
            var buttonCalc = driver.FindElementById("com.example.androidappsummator:id/buttonCalcSum");
            buttonCalc.Click();
            var result = driver.FindElementById("com.example.androidappsummator:id/editTextSum").Text;

            Assert.AreEqual("error", result);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}