using System;
using System.Collections.Generic;
using System.Linq;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace DockerProject.Pages
{
    public class ProjectPage : Form
    {
        private readonly By _testDate = By.XPath("//table[@class='table']//tr//td[4]");
        private readonly By _testName = By.XPath("//table[@class='table']//tr//td[1]");


        public ProjectPage() : base(By.Id("pie"), "Project page")
        {
        }

        public IEnumerable<DateTime> GetTestsDate()
        {
            return ElementFactory.FindElements<IComboBox>(_testDate).Select(s => DateTime.Parse(s.Text));
        }

        public IEnumerable<string> GetTestsNames()
        {
            return ElementFactory.FindElements<IComboBox>(_testName).Select(s => s.Text);
        }
    }
}