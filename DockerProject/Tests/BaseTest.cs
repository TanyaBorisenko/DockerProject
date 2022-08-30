using Aquality.Selenium.Browsers;
using DockerProject.Utils;
using NUnit.Framework;

namespace DockerProject.Tests
{
    public abstract class BaseTest
    {
        [SetUp]
        public void BeforeTest()
        {
            AqualityServices.Browser.GoTo($"{Configurator.TestData["AuthUrl"]}");
            AqualityServices.Browser.Maximize();
        }

        [TearDown]
        public void AfterTest()
        {
            AqualityServices.Browser.Quit();
        }
    }
}