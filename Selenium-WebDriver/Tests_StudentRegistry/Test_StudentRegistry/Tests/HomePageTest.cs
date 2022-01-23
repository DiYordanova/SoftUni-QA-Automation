using NUnit.Framework;
using Test_StudentRegistry.PageObjects;

namespace Test_StudentRegistry
{
    public class HomePageTest : BaseTest
    {       

        [Test]
        public void Test_HomePage_Content()
        {
            var page = new HomePage(driver);
            page.Open();

            Assert.AreEqual("MVC Example", page.GetPageTitle());
            Assert.AreEqual("Students Registry", page.GetPageHeadingText());
            page.GetStudentsCount();
            Assert.Pass();
        }

        [Test]
        public void Test_HomePage_Links()
        {
            var homePage = new HomePage(driver);

            homePage.Open();
            homePage.LinkHomePage.Click();
            Assert.IsTrue(new HomePage(driver).IsOpen());

            homePage.Open();
            homePage.LinkAddStudentsPage.Click();
            Assert.IsTrue(new AddStudent(driver).IsOpen());


            homePage.Open();
            homePage.LinkViewStudentsPage.Click();
            Assert.IsTrue(new ViewStudents(driver).IsOpen());
        }
    }
}