using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using System;

namespace QA_Appium_Android
{
    public class TestAndroid
    {
        private AndroidDriver<AndroidElement> driver;


        [OneTimeSetUp]
        public void Setup()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability(MobileCapabilityType.App, @"C:\Users\35989\source\repos\QA_Appium\QA_Appium_Android\com.example.androidappsummator.apk");
            options.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            driver = new AndroidDriver<AndroidElement>(new Uri("http://[::1]:4723/wd/hub"), options);
        }

        [Test]
        public void Test_AndroidSumator_ValidData()
        {
            var text1 = driver.FindElement(By.Id("com.example.androidappsummator:id/editText1"));
            text1.Clear();
            text1.SendKeys("10");

            var text2 = driver.FindElement(By.Id("com.example.androidappsummator:id/editText2"));
            text2.Clear();
            text2.SendKeys("20");

            var btnCalcSum = driver.FindElement(By.Id("com.example.androidappsummator:id/buttonCalcSum"));
            btnCalcSum.Click();
            var txtSum = driver.FindElement(By.Id("com.example.androidappsummator:id/editTextSum"));
            Assert.AreEqual("30", txtSum.Text);
        }

        [Test]
        public void Test_AndroidSumator_InvalidData()
        {
            var text1 = driver.FindElement(By.Id("com.example.androidappsummator:id/editText1"));
            text1.Clear();
            text1.SendKeys("15");

            var text2 = driver.FindElement(By.Id("com.example.androidappsummator:id/editText2"));
            text2.Clear();            

            var btnCalcSum = driver.FindElement(By.Id("com.example.androidappsummator:id/buttonCalcSum"));
            btnCalcSum.Click();
            var txtSum = driver.FindElement(By.Id("com.example.androidappsummator:id/editTextSum"));
            Assert.AreEqual("30", txtSum.Text);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}