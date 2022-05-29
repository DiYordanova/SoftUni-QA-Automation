using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Windows;
using QA_Appium_WindForm.Window;
using System;

namespace QA_Appium_WindForm.Tests
{
    public class WindowSummatorTestsPOM
    {
        private WindowsDriver<WindowsElement> driver;
        private AppiumOptions options;
        private string AppPatch = @"D:\SoftUni\SoftUni-QA-Automation\Appium\QA_Appium_WindSumator\WindowsFormsApp.exe";
        private const string AppimURI = "http://127.0.0.1:4723/wd/hub";

        [OneTimeSetUp]
        public void Setup()
        {
            options = new AppiumOptions();
            options.AddAdditionalCapability(MobileCapabilityType.App, AppPatch);
            options.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Windows");
            options.AddAdditionalCapability(MobileCapabilityType.DeviceName, "WindowsPC");
            driver = new WindowsDriver<WindowsElement>(new Uri(AppimURI), options);
        }

        [Test]
        public void Test_ValidData()
        {
            var window = new WindowSummatorPOM(driver);
            string result = window.Calculator("20", "30");

            Assert.AreEqual("50", result);
        }

        [Test]
        public void Test_InvalidData()
        {
            var window = new WindowSummatorPOM(driver);
            string result = window.Calculator("alabala", "30");

            Assert.AreEqual("error", result);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
