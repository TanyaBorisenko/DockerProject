using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using DockerProject.Utils;
using OpenQA.Selenium;

namespace DockerProject.Pages
{
   public class AllProjectsPage : Form
    {
        private string _projectNamePattern = "//a[@class='list-group-item'][contains(text(),'{0}')]";

        private ITextBox SuccessMessage => ElementFactory.GetTextBox(
            By.XPath("//div[contains(@class,'alert')and contains(@class,'alert-success')]"), "Success message");

        private ITextBox AddProjectForm =>
            ElementFactory.GetTextBox(By.XPath("//div[@class='modal-open']"), "Add project form");

        private IButton SaveButton =>
            ElementFactory.GetButton(By.XPath("//button[@type='submit']"), "Save project button");

        private ITextBox ProjectName =>
            ElementFactory.GetTextBox(By.XPath("//input[@id='projectName']"), "Enter project name");

        private IButton AddButton =>
            ElementFactory.GetButton(By.XPath("//button[@data-target='#addProject']"), "Add project button");

        private ITextBox FooterText =>
            ElementFactory.GetTextBox(By.XPath("//p[contains(@class,'text-muted')]//span"), "Footer text");

        public AllProjectsPage() : base(
            By.XPath("//div[contains(@class,'container')and contains(@class,'main-container')]"), "Projects page")
        {
        }

        public string GetVariantNumber()
        {
            var versionNumber = FooterText.GetText();
            var split = Parser.StringParse(versionNumber, ": ");

            return split[1];
        }

        public void OpenProjectPage(string projectName)
        {
            var openProjectButton = DriverUtils.GetElementByXPath(_projectNamePattern, projectName);
            openProjectButton.Click();
        }

        public void ClickAddButton()
        {
            AddButton.Click();
        }

        public void EnterProjectName(string name)
        {
            ProjectName.Type(name);
        }

        public void ClickSaveProjectButton()
        {
            SaveButton.Click();
        }

        public bool CheckSuccessMessageIsDisplayed()
        {
            return SuccessMessage.State.IsDisplayed;
        }

        public bool IsAddFormClosed()
        {
            return AddProjectForm.State.WaitForNotDisplayed();
        }

        public bool IsNewProjectDisplayed(string projectName)
        {
            var newProject = DriverUtils.GetElementByXPath(_projectNamePattern, projectName);
            return newProject.Displayed;
        }

        public string GetProjectId(string projectName)
        {
            var stringWithId = DriverUtils.GetElementByXPath(_projectNamePattern, projectName).GetAttribute("href");
            var split = Parser.StringParse(stringWithId, "=");

            return split[1];
        }
    }
}