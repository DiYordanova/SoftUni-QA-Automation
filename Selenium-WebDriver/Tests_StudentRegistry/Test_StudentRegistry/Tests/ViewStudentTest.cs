using NUnit.Framework;
using Test_StudentRegistry.PageObjects;

namespace Test_StudentRegistry
{
    public class ViewStudentTest : BaseTest
    {
        
        [Test]
        public void Test_ViewStudentsPage_Content()
        {
            var page = new ViewStudents(driver);
            page.Open();

            Assert.AreEqual("Students", page.GetPageTitle());
            Assert.AreEqual("Registered Students", page.GetPageHeadingText());
            var students = page.GetRegisterStudents();
            foreach (var st in students)
            {
                Assert.IsTrue(st.IndexOf("(") > 0);
                Assert.IsTrue(st.LastIndexOf(")") == st.Length - 1);
            }
            
        }

        [Test]
        public void Test_ViewStudentsPage_Links()
        {
            var viewStudentsPage = new ViewStudents(driver);

            viewStudentsPage.Open();
            viewStudentsPage.LinkHomePage.Click();
            Assert.IsTrue(new HomePage(driver).IsOpen());

            viewStudentsPage.Open();
            viewStudentsPage.LinkAddStudentsPage.Click();
            Assert.IsTrue(new AddStudent(driver).IsOpen());


            viewStudentsPage.Open();
            viewStudentsPage.LinkViewStudentsPage.Click();
            Assert.IsTrue(new ViewStudents(driver).IsOpen());
        }
    }
}