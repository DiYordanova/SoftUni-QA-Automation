using OpenQA.Selenium;

namespace Test_StudentRegistry.PageObjects
{
    public class AddStudent : BasePage
    {
        public AddStudent(IWebDriver driver) : base(driver)
        {
        }

        public override string PageUrl =>
                 "https://mvc-app-node-express.nakov.repl.co/add-student";

        public IWebElement FieldName =>
            driver.FindElement(By.CssSelector("input#name"));

        public IWebElement FieldEmail =>
            driver.FindElement(By.CssSelector("input#email"));

        public IWebElement ButtonSubmit =>
            driver.FindElement(By.CssSelector("body > form > button[type = 'submit']"));

        public IWebElement ErrorMessageElement =>
           driver.FindElement(By.XPath("//div[contains(@style,'background:red')]"));

        public void AddStudents(string name, string email)
        {
            this.FieldName.SendKeys(name);
            this.FieldEmail.SendKeys(email);
            this.ButtonSubmit.Click();
        }
    }
}
