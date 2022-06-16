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
    public class AdministratorHomePage : BasePage
    {
        // private static ReadOnlyCollection<IWebElement> _menuButtons;
        // private static IWebElement _menu1Button;
        private static IWebElement _sideBar;

        protected override void Initialize()
        {
            //_sideBar = SeleniumDriver.FindElement(By.ClassName("list-unstyled"));
            // _menuButtons = SeleniumDriver.FindElements(By.Id("username"));
            // _menu1Button = SeleniumDriver.FindElement(By.Id("fa-truck"));
        }

        [Given(@"Я нажимаю на пункт меню 'Контрагенты'")]
        public void ClickOnContractors()
        {
            IWebElement sideBar = SeleniumDriver.FindElement(By.ClassName("list-unstyled"), 100);
            ReadOnlyCollection<IWebElement> contractorsButtons = sideBar.FindElements(By.TagName("a"));
           
            Actions action = new Actions(SeleniumDriver);
            action.MoveToElement(sideBar).Perform();
           
            IWebElement accountingButton = contractorsButtons.FirstOrDefault(x => x.Text.Contains("Учет"));
            accountingButton.Click();
            
            IWebElement contractorsButton = contractorsButtons.FirstOrDefault(x => x.Text.Contains("Контрагенты"));
            contractorsButton.Click();
        }

        [Given(@"Я нажимаю на кнопку 'Добавить контрагента'")]
        public void ClickOnAddContractor()
        {
            IWebElement addContractorButton = SeleniumDriver.FindElement(By.Id("addContractorButton"), 100);
            addContractorButton.Click();
        }

        [Given(@"Я ввожу название контрагента: '(.*)'")]
        public void InputName(String contractorName)
        {
            ReadOnlyCollection<IWebElement> inputBoxes = SeleniumDriver.FindElements(By.ClassName("form-group"), 100);
            IWebElement inputNameBox = inputBoxes.FirstOrDefault(x => x.Text.Contains("Наименование"));
            IWebElement nameInputField = inputNameBox.FindElement(By.TagName("input"));
            nameInputField.SendKeys(contractorName);
        }

        [Given(@"Я ввожу данные в поле 'Полное наименование'")]
        public void InputFullName()
        {
            ReadOnlyCollection<IWebElement> inputBoxes = SeleniumDriver.FindElements(By.ClassName("form-group"), 100);
            IWebElement inputFullNameBox = inputBoxes.FirstOrDefault(x => x.Text.Contains("Полное наименование"));
            IWebElement inputFullNameField = inputFullNameBox.FindElement(By.TagName("textarea"));
            inputFullNameField.SendKeys("Ромашка");
        }

        [Given(@"Я нажимаю на кнопку 'Сохранить'")]
        public void ClickOnSaveButton()
        {
            ReadOnlyCollection<IWebElement> buttons = SeleniumDriver.FindElements(By.TagName("button"), 100);
            IWebElement saveButton = buttons.FirstOrDefault(x => x.Text.Contains("Сохранить"));
            saveButton.Click();
        }

        [Given(@"Я вижу созданного контрагента: '(.*)'")]
        public void CanSeeNewContractor(String contractorName)
        {
            SeleniumDriver.WaitUntilElementClose(By.ClassName("block-ui-active"));
            
            ReadOnlyCollection<IWebElement> contractors = SeleniumDriver.FindElements(By.ClassName("ng-binding"), 100);
            IWebElement madeContractor = contractors.FirstOrDefault(contractor => contractor.Text == contractorName);
            
            if (madeContractor == null)
            {
                IWebElement paginationField = SeleniumDriver.FindElement(By.ClassName("pagination-next"), 100);
                IWebElement paginationPointer = paginationField.FindElement(By.TagName("a"));
                
                ReadOnlyCollection<IWebElement> linkBlock = paginationField.FindElements(By.CssSelector("a[disabled=\"disabled\"]"));
                
                if (linkBlock.Count == 0)
                {
                    paginationPointer.Click();
                    CanSeeNewContractor(contractorName);
                }
                
                else
                {
                    throw new Exception("Созданный Контрагент не найден");
                }
            }
        }

        [Given(@"Я удаляю созданного контрагента: '(.*)'")]
        public void DeleteNewContractor(String contractorName)
        {
            ReadOnlyCollection<IWebElement> contratorsList = SeleniumDriver.FindElements(By.Name("contractorsList"), 100);
            IWebElement contractor = contratorsList.FirstOrDefault(contractor => contractor.Text.Contains(contractorName));
            IWebElement deleteContractorButton = contractor.FindElement(By.Name("deleteContractorButton"));
            deleteContractorButton.Click();
            
            SeleniumDriver.WaitUntilElementAvailable(By.ClassName("modal-dialog"));

            IWebElement modalWindow = SeleniumDriver.FindElement(By.ClassName("modal-dialog"), 100);
            ReadOnlyCollection<IWebElement> modalWindowButtons = modalWindow.FindElements(By.TagName("button"));
            IWebElement okModalWindowButton = modalWindowButtons.FirstOrDefault(button => button.Text.Contains("Да"));
            okModalWindowButton.Click();
        }
    }
}