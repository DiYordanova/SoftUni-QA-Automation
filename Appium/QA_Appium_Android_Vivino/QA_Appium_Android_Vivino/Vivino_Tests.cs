using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using System;

namespace QA_Appium_Android_Vivino
{
    public class Vivino_Tests
    {
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;
        private const string AppiumURI = "http://127.0.0.1:4723/wd/hub";
        private const string app = @"D:\SoftUni\SoftUni-QA-Automation\Appium\QA_Appium_Android_Vivino\vivino.web.app_8.18.11-8181199_minAPI19(arm64-v8a,armeabi,armeabi-v7a,mips,x86,x86_64)(nodpi)_apkmirror.com (1).apk";
        private const string VivinoAppPackage = "vivino.web.app";
        private const string VivinoAppStartupActivity =  "com.sphinx_solution.activities.SplashActivity";
        private const string accountEmail = "test_vi@abv.bg";
        private const string accountPassword = "11111";        

        [OneTimeSetUp]
        public void Setup()
        {
            options = new AppiumOptions() { PlatformName = "Android" };
            options.AddAdditionalCapability(MobileCapabilityType.App, app);
            options.AddAdditionalCapability("appPackage", VivinoAppPackage);
            options.AddAdditionalCapability("appActivity", VivinoAppStartupActivity);
            driver = new AndroidDriver<AndroidElement>(new Uri(AppiumURI), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);
        }

        [Test]
        public void Test1()
        {
            var getStartingField = driver.FindElementById("vivino.web.app:id/getstarted_layout");
            getStartingField.Click();
            var emailTextbox = driver.FindElementById("vivino.web.app:id/edtEmail");
            emailTextbox.SendKeys(accountEmail);
            var passwordTextbox = driver.FindElementById("vivino.web.app:id/edtPassword");
            passwordTextbox.SendKeys(accountPassword);
            var nextField = driver.FindElementById("vivino.web.app:id/action_next");
            nextField.Click();
            var search = driver.FindElementById("vivino.web.app:id/wine_explorer_tab");
            search.Click();
            var searchTextbox = driver.FindElementById("vivino.web.app:id/search_header_text");
            searchTextbox.Click();
            var searchField = driver.FindElementById("vivino.web.app:id/editText_input");
            searchField.SendKeys("Katarzyna Reserve Red 2006");
            var firstFieldName = driver.FindElementById("vivino.web.app:id/winename_textView");            
           
            Assert.That(firstFieldName.Text, Does.Contain("Reserve Red"));

            firstFieldName.Click();
            var ratingText = driver.FindElementById("vivino.web.app:id/rating");
            var ratingNumber = double.Parse(ratingText.Text);
            Assert.IsTrue(ratingNumber >= 1 && ratingNumber <= 5);

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}