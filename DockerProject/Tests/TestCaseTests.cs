using System.Linq;
using Aquality.Selenium.Core.Logging;
using DockerProject.Models;
using DockerProject.Pages;
using DockerProject.Services;
using DockerProject.Utils;
using NUnit.Framework;

namespace DockerProject.Tests
{
    public class TestCaseTests : BaseTest
    {
        private readonly string _variantNumber = Configurator.TestData["VariantNumber"];
        private readonly string _nexageProject = "Nexage";
        private readonly string _footerText = "2";
        private readonly AllProjectsPage _allProjectsPage;
        private readonly ProjectPage _projectPage;
        private readonly string _newProjectName = "NewProject";
        private readonly string _testName = "new test";
        private readonly string _methodName = "new method";
        private readonly string _sid = "dockerTask";
        private readonly string _env = "DESKTOP-3GT4DL5";
        private readonly string _contentType = "image/png";
        private readonly string _logs = "some logs";
        private readonly NewProjectPage _newProjectPage;
        private readonly string _cookieName = "token";

        public TestCaseTests()
        {
            _allProjectsPage = new AllProjectsPage();
            _projectPage = new ProjectPage();
            _newProjectPage = new NewProjectPage();
        }

        [Test]
        public void TestCase()
        {
            Logger.Instance.Info("Get token");
            
            var getTokenResponse = ApplicationApi.GetToken(_variantNumber);
            var token = getTokenResponse.Content;
            
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(token, "Token should be not null");
                Assert.IsTrue(_allProjectsPage.State.WaitForDisplayed(), "Projects page should open");
            });
            
            Logger.Instance.Info("Get project id");
            var projectId = _allProjectsPage.GetProjectId(_nexageProject);
            
            Logger.Instance.Info("Transfer the token and refresh page");
            DriverUtils.TransferToken(_cookieName, token);
            DriverUtils.RefreshPage();
            
            Assert.AreEqual(_allProjectsPage.GetVariantNumber(), _footerText, "Texts should be the same");
            
            Logger.Instance.Info("Open Nexage project page");
            _allProjectsPage.OpenProjectPage(_nexageProject);
            
            Logger.Instance.Info("Get all test in Json format");
            var testsResponse = ApplicationApi.GetAllTests(projectId);
            var tests = testsResponse.Data;
            var getTestsNames = tests.Select(s => s.Name);
            
            Logger.Instance.Info("Get tests on project page");
            var orderTestsByDate = _projectPage.GetTestsDate();
            var testsNames = _projectPage.GetTestsNames();
            
            Assert.Multiple(() =>
            {
                Assert.That(orderTestsByDate, Is.Ordered.Descending, "Tests dates should be ordered by descending");
                CollectionAssert.AreEquivalent(getTestsNames, testsNames, "Collections should be equivalent");
            });
            
            Logger.Instance.Info("Return to project page");
            DriverUtils.BackToProjectPage();
            
            Logger.Instance.Info("Add new project");
            _allProjectsPage.ClickAddButton();
            _allProjectsPage.EnterProjectName(_newProjectName);
            _allProjectsPage.ClickSaveProjectButton();
            
            Assert.IsTrue(_allProjectsPage.CheckSuccessMessageIsDisplayed(), "Message should displayed");
            
            Logger.Instance.Info("Close popup window, refresh page");
            DriverUtils.ClosePopUpWindow();
            Assert.IsTrue(_allProjectsPage.IsAddFormClosed(), "Form should not be opened");
            
            DriverUtils.RefreshPage();
            Assert.IsTrue(_allProjectsPage.IsNewProjectDisplayed(_newProjectName),
                "New project should be displayed on page");
            
            Logger.Instance.Info("Open new project page, add new test");
            _allProjectsPage.OpenProjectPage(_newProjectName);

            var newTestModel = new NewTestModel()
            {
                ProjectName = _newProjectName,
                TestName = _testName,
                MethodName = _methodName,
                Sid = _sid,
                Env = _env
            };

            var getTestId = ApplicationApi.AddNewTest(newTestModel);
            var testId = getTestId.Content;

            Logger.Instance.Info("Add logs and screenshot");
            ApplicationApi.AddLog(testId, _logs);
            var screenshot = DriverUtils.TakeScreenshot();
            ApplicationApi.AddAttachment(testId, screenshot, _contentType);

            Assert.AreEqual(_newProjectPage.GetNewTestName(), _testName, "Names should be the same");
        }
    }
}