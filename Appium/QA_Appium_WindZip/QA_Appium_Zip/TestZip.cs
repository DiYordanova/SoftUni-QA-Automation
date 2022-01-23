using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.IO;
using System.Threading;

namespace QA_Appium_Zip
{
    public class TestZip
    {
        private const string AppiumServerUrl = "http://[::1]:4723/wd/hub";        
        private const string AppiumForTesting = @"C:\Program Files\7-Zip\7zFM.exe";        
        private const string PathZip = @"C:\Program Files\7-Zip\";
        private WindowsDriver<WindowsElement> driver;
        private WindowsDriver<WindowsElement> driverDesktop;
        private string workDir;

        [SetUp]
        public void Setup()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability(MobileCapabilityType.App, AppiumForTesting);
            options.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Windows");
            driver = new WindowsDriver<WindowsElement>(new Uri(AppiumServerUrl), options);


            var optionsDesktop = new AppiumOptions();
            optionsDesktop.AddAdditionalCapability(MobileCapabilityType.App, "Root");
            optionsDesktop.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Windows");
            driverDesktop = new WindowsDriver<WindowsElement>(new Uri(AppiumServerUrl), optionsDesktop);

            workDir = Directory.GetCurrentDirectory() + @"\workDir";
            if (Directory.Exists(workDir))
            {
                Directory.Delete(workDir, true);
            }

            Directory.CreateDirectory(workDir);

            Thread.Sleep(1000);
        }

        [Test]
        public void Test7Zip()
        {
            var textBoxLocation = driver.FindElementByXPath("/Window/Pane/Pane/ComboBox/Edit");
            textBoxLocation.SendKeys(PathZip + Keys.Enter);            
            var listBox = driver.FindElementByClassName("SysListView32");
            listBox.SendKeys(Keys.Control + 'a');
            var buttonAdd = driver.FindElementByName("Add");
            buttonAdd.Click();

            Thread.Sleep(1000);

            var windowCreateArchive = driverDesktop.FindElementByName("Add to Archive");

            var textBoxArchivName = windowCreateArchive.FindElementByXPath("/Window/ComboBox/Edit[@Name='Archive:']");
            string archivName = workDir + @"\archive.7z";
            textBoxArchivName.SendKeys(archivName);

            var textBoxArchivFormat = windowCreateArchive.FindElementByXPath("/Window/ComboBox[@Name='Archive format:']");
            textBoxArchivFormat.SendKeys("7z");

            var textBoxCompressionLevel = windowCreateArchive.FindElementByXPath("/Window/ComboBox[@Name='Compression level:']");
            textBoxCompressionLevel.SendKeys("Ultra");

            var textBoxCompressionMethod = windowCreateArchive.FindElementByXPath("/Window/ComboBox[@Name='Compression method:']");
            textBoxCompressionMethod.SendKeys(Keys.Home);

            var textBoxDictionarySize = windowCreateArchive.FindElementByXPath("/Window/ComboBox[@Name='Dictionary size:']");
            textBoxDictionarySize.SendKeys(Keys.End);

            var textBoxWordSize = windowCreateArchive.FindElementByXPath("/Window/ComboBox[@Name='Word size:']");
            textBoxWordSize.SendKeys(Keys.End);

            var buttonOk = windowCreateArchive.FindElementByXPath("/Window/Button[@Name='OK']");
            buttonOk.Click();

            textBoxLocation.SendKeys(archivName + Keys.Enter);
            var buttonExtract = driver.FindElementByName("Extract");
            buttonExtract.Click();

            var buttonExtractOk = driver.FindElementByName("OK");
            buttonExtractOk.Click();

            Thread.Sleep(1000);

            foreach (string  fileOiginal in Directory.EnumerateFiles(PathZip, "*.*", SearchOption.AllDirectories))
            {
                var fileNameOnly = fileOiginal.Replace(PathZip, "");
                var fileCopy = workDir + @"\" + fileNameOnly;
                FileAssert.AreEqual(fileCopy, fileOiginal);
            }                
        }


        [TearDown]
        public void Shutdown()
        {
            driver.Quit();
            driverDesktop.Quit();
        }
    }
}