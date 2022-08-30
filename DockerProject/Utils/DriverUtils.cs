using System;
using Aquality.Selenium.Browsers;
using OpenQA.Selenium;

namespace DockerProject.Utils
{
    public static class DriverUtils
    {
        public static string TakeScreenshot()
        {
            return Convert.ToBase64String(AqualityServices.Browser.GetScreenshot());
        }

        public static void BackToProjectPage()
        {
            AqualityServices.Browser.GoBack();
        }

        public static void TransferToken(string name, string token)
        {
            AqualityServices.Browser.Driver.Manage().Cookies.AddCookie(new Cookie(name, token));
        }

        public static void RefreshPage()
        {
            AqualityServices.Browser.Refresh();
        }

        public static void ClosePopUpWindow()
        {
            AqualityServices.Browser.ExecuteScript("closePopUp();");
        }

        public static IWebElement GetElementByXPath(string elementPath, string argument)
        {
            return AqualityServices.Browser.Driver.FindElement(By.XPath(string.Format(elementPath, argument)));
        }
    }
}