using NUnit.Framework;
using Test_StudentRegistry.PageObjects;

namespace Test_StudentRegistry
{
    public class AddStudentsPageTest : BaseTest
    {        
        [Test]
        public void Test_AddStudentsPage_Content()
        {
            var page = new AddStudent(driver);
            page.Open();

            Assert.AreEqual("Add Student", page.GetPageTitle());
            Assert.AreEqual("Register New Student", page.GetPageHeadingText());
            Assert.AreEqual("", page.FieldName.Text);
            Assert.AreEqual("", page.FieldEmail.Text);
            Assert.AreEqual("Add", page.ButtonSubmit.Text);            
        }

        [Test]
        public void Test_AddStudentPage_Links()
        {
            var addStudentsPage = new AddStudent(driver);

            addStudentsPage.Open();
            addStudentsPage.LinkHomePage.Click();
            Assert.IsTrue(new HomePage(driver).IsOpen());

            addStudentsPage.Open();
            addStudentsPage.LinkAddStudentsPage.Click();
            Assert.IsTrue(new AddStudent(driver).IsOpen());


            addStudentsPage.Open();
            addStudentsPage.LinkViewStudentsPage.Click();
            Assert.IsTrue(new ViewStudents(driver).IsOpen());
        }

        [Test]
        public void Test_AddStudentsPage_AddValidStudent()
        {
            var page = new AddStudent(driver);
            page.Open();
            string name = "Mario";
            string email = "mario@gmail.com";
            page.AddStudents(name, email);
            var viewStudents = new ViewStudents(driver);
            Assert.IsTrue(viewStudents.IsOpen());
            var students = viewStudents.GetRegisterStudents();
            string newStudent = name + " (" + email + ")";
            Assert.Contains(newStudent, students);
        }

        [Test]
        public void Test_AddStudentsPage_AddInvalidStudent()
        {
            var page = new AddStudent(driver);
            page.Open();
            string name = "";
            string email = "mario@gmail.com";
            page.AddStudents(name, email);
            Assert.IsTrue(page.IsOpen());
            Assert.IsTrue(page.ErrorMessageElement.Text.Contains("Cannot add student."));
        }
    }
}