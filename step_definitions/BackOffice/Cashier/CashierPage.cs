using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TechTalk.SpecFlow;
using Buzzolls.SpecFlow.Tools;

namespace Buzzolls.SpecFlow.Tests.step_definitions.BackOffice
{
    [Binding]
    public class CashierHomePage : BasePage
    {
        // private static ReadOnlyCollection<IWebElement> _menuButtons;
        // private static IWebElement _menu1Button;
        private static IWebElement _sideBar;

        [Given(@"Я ввожу данные в консоль браузера")]
        public void Input_DataInBrowserConsole()
        {
            SeleniumDriver.WaitUntilElementAvailable(By.ClassName("osh-panel-box"));
            Console.WriteLine("checkPrinter.setDebuggingModeOn(false,false)");
        }
        
        [Given(@"Я выбираю кассу")]
        public void Choose_Сashbox()
        {
            ReadOnlyCollection<IWebElement> cashBoxes = SeleniumDriver.FindElements(By.ClassName("cashbox-item"));
            IWebElement cashBox = cashBoxes.FirstOrDefault(cashBox => cashBox.Text.Contains("1"));
            cashBox.Click();
        }
        
    }
}