using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace DockerProject.Pages
{
    public class NewProjectPage : Form
    {
        private ITextBox TestName =>
            ElementFactory.GetTextBox(By.XPath("//table[@class='table']//tr//td[1]"), "Test name");

        public NewProjectPage() : base(By.Id("pie"), "New project page")
        {
        }

        public string GetNewTestName()
        {
            return TestName.GetText();
        }
    }
}