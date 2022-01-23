using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Linq;

namespace Test_StudentRegistry.PageObjects
{
    public class ViewStudents : BasePage
    {
        public ViewStudents(IWebDriver driver) : base(driver)
        {
        }
        public override string PageUrl =>
                 "https://mvc-app-node-express.nakov.repl.co/students";        

        public ReadOnlyCollection<IWebElement> ListItemsStudents =>
            driver.FindElements(By.CssSelector("Body > ul > li"));

        public string[] GetRegisterStudents()
        {
            var elementsStudents = this.ListItemsStudents.Select(s => s.Text).ToArray();
            return elementsStudents;
        }
    }
}
