using System;
using System.Collections.ObjectModel;
using System.Threading;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Buzzolls.SpecFlow.Tools;
using OpenQA.Selenium.Chrome;

namespace Buzzolls.SpecFlow.Tests.step_definitions.BackOffice.Auth
{
    [Binding]
    public class AuthPage : BasePage
    {
        private static IWebElement _form;
        private static IWebElement _loginInput;
        private static IWebElement _passwordInput;
        private static IWebElement _loginButton;

        protected override void Initialize()
        {
            _loginInput = SeleniumDriver.FindElement(By.Id("username"), 100);
            _passwordInput = SeleniumDriver.FindElement(By.Id("password"), 100);
            _loginButton = SeleniumDriver.FindElement(By.Id("login-submit"), 100);
            // _passwordInput = SeleniumDriver.FindElement(By.Id("password"), 100);
            // _loginButton = SeleniumDriver.FindElement(By.Id("login-submit"), 100);
        }

        protected override void OpenPage()
        {
            SeleniumDriver.Url = Research.BaseBackOfficeAuthUrl;
            SeleniumDriver.Navigate();
            Initialize();
        }

        [Given(@"Я открываю страницу авторизации")]
        public void OpenAuthPage()
        {
            OpenPage();
        }

        [Given(@"Я ввожу логин и пароль учетной записи ревизора")]
        public void InputLoginAndPaswordForAuditor()
        {
            _loginInput.SendKeys("Buhgal");
            _passwordInput.SendKeys("123");
        }

        [Given(@"Я ввожу логин и пароль учетной записи центрального администратора")]
        public void InputLoginAndPaswordForCentralAdmin()
        {
            Initialize();
            _loginInput.SendKeys("admin");
            _passwordInput.SendKeys("123");
        }

        [Given(@"Я ввожу логин и пароль учетной записи оператора 'Call-центра' с логином '(.*)'")]
        public void InputLoginAndPaswordForCallCenter(String operatorsLogin)
        {
            Initialize();
            _loginInput.SendKeys(operatorsLogin);
            _passwordInput.SendKeys("123");
        }

        [Given(@"Я ввожу логин и пароль учетной записи администратора торговой зоны")]
        public void InputLoginAndPasswordForShoppingAreaAdministrator()
        {
            Initialize();
            _loginInput.SendKeys("Admintorg");
            _passwordInput.SendKeys("123");
        }

        [Given(@"Я ввожу логин и пароль учетной записи кассира")]
        public void InputLoginAndPasswordForCashier()
        {
            Initialize();
            _loginInput.SendKeys("Irina");
            _passwordInput.SendKeys("123");
        }

        [Given(@"Я ввожу логин и пароль учетной записи менеджера торговой точки")]
        public void InputLoginManager()
        {
            Initialize();
            _loginInput.SendKeys("OctManager");
            _passwordInput.SendKeys("123");
        }

        [Given(@"Нажимаю кнопку 'ВХОД'")]
        public void PressButton()
        {
            Initialize();
            _loginButton.Click();
        }

        [AfterScenario]
        public void DeleteAllCookiesAfterScenario()
        {
            String homeURL = SeleniumDriver.Url;
            do
            {
                SeleniumDriver.Manage().Cookies.DeleteAllCookies();
                SeleniumDriver.Navigate().GoToUrl(Research.BaseBackOfficeAuthUrl);
                homeURL = SeleniumDriver.Url;
            } while (homeURL != Research.BaseBackOfficeAuthUrl);
        }

        [AfterTestRun]
        public static void CloseBrowserAfterTestsRun()
        {
            SeleniumDriver.Close();
            SeleniumDriver.Dispose();
        }
    }
}
