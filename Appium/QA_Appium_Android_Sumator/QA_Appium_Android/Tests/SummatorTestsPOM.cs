using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using QA_Appium_Android.Android;
using System;

namespace QA_Appium_Android.Tests
{
    public class SummatorTestsPOM
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
            var window = new SummatorAndroidPOM(driver);
            string result = window.Calculator("5", "6");

            Assert.AreEqual("11", result);
        }

        [Test]
        public void Test_InvalidData()
        {
            var window = new SummatorAndroidPOM(driver);
            string result = window.Calculator("alabala", "6");

            Assert.AreEqual("error", result);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
