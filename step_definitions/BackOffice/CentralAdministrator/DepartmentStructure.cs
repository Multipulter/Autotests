using System;
using System.Collections.ObjectModel;
using System.Linq;
using Buzzolls.SpecFlow.Tools;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TechTalk.SpecFlow;

namespace Buzzolls.SpecFlow.Tests.step_definitions.BackOffice.CentralAdministrator
{
    [Binding]
    public class DepartmentStructure: BasePage
    {
        private static IWebElement _sideBar;
        
        protected override void Initialize()
        {
           
        }
        [Given(@"Я нажимаю на пункт меню 'Подразделения'")]
        public void ClickOn_Departments()
        {
            IWebElement sideBar = SeleniumDriver.FindElement(By.ClassName("list-unstyled"), 100);
            ReadOnlyCollection<IWebElement> contractorsButtons = sideBar.FindElements(By.TagName("a"));
           
            Actions action = new Actions(SeleniumDriver);
            action.MoveToElement(sideBar).Perform();
           
            IWebElement accountingButton = contractorsButtons.FirstOrDefault(x => x.Text.Contains("Подразделения"));
            accountingButton.Click();
        }
        
        [Given(@"Я нажимаю на кнопку 'Добавить подразделение'")]
        public void ClickOnAddDepartment()
        {
            ReadOnlyCollection<IWebElement> addDepartmentButton = SeleniumDriver.FindElements(By.ClassName("btn-primary"));
            IWebElement addDepartmentButtons = addDepartmentButton.FirstOrDefault(x => x.Text.Contains("Добавить подразделение"));
            addDepartmentButtons.Click();
        }

        [Given(@"Я ввожу данные в поле 'Наименование': '(.*)'")]
        public void InputNameDepartment(String name)
        {
            IWebElement InputName = SeleniumDriver.FindElement(By.Id("name-departmen"), 100);
            InputName.SendKeys(name);
        }
        
        [Given(@"Я ввожу данные в поле 'Тип': '(.*)'")]
        public void InputTypeDepartment(String type)
        {
            ReadOnlyCollection<IWebElement> typeSelections = SeleniumDriver.FindElements(By.TagName("option"));
            IWebElement InputType = typeSelections.FirstOrDefault(x=>x.Text==type);
            InputType.Click();
        }
        
        [Given(@"Я ввожу данные в поле 'Статус': '(.*)'")]
        public void InputStatusDepartment(String status)
        {
            ReadOnlyCollection<IWebElement> statusSelections = SeleniumDriver.FindElements(By.TagName("option"));
            IWebElement InputStatus = statusSelections.FirstOrDefault(x=>x.Text==status);
            InputStatus.Click();
        }
        
        [Given(@"Я ввожу данные в поле 'Валюта': '(.*)'")]
        public void InputCurrencyDepartment(String currency)
        {
            ReadOnlyCollection<IWebElement> currencySelections = SeleniumDriver.FindElements(By.TagName("option"));
            IWebElement InputCurrency = currencySelections.FirstOrDefault(x=>x.Text==currency);
            InputCurrency.Click();
        }
      
        [Given(@"Я ввожу данные в поле 'Организация': '(.*)'")]
        public void InputOrganizationDepartment(String organization)
        {
            ReadOnlyCollection<IWebElement> organizationSelections = SeleniumDriver.FindElements(By.TagName("option"));
            IWebElement InputOrganization = organizationSelections.FirstOrDefault(x=>x.Text==organization);
            InputOrganization.Click();
        }
        
        [Given(@"Я ввожу данные в поле 'Максимальная сумма заказа без подтверждения': '(.*)'")]
        public void InputLimitDepartment(String money)
        {
            IWebElement InputLimit = SeleniumDriver.FindElement(By.Id("maxOrderAmount-department"), 100);
            InputLimit.SendKeys(money);
        }
        
        [Given(@"Я ввожу данные в поле 'Логин почтового ящика': '(.*)'")]
        public void InputLoginDepartment(String login)
        {
            IWebElement InputLogin = SeleniumDriver.FindElement(By.Id("emailLogin-department"), 100);
            InputLogin.SendKeys(login);
        }
        
        [Given(@"Я ввожу данные в поле 'Пароль почтового ящика': '(.*)'")]
        public void InputPasswordDepartment(String pass)
        {
            IWebElement InputPassword = SeleniumDriver.FindElement(By.Id("emailPassword-department"), 100);
            InputPassword.SendKeys(pass);
        }
        
        [Given(@"Я ввожу данные в поле 'Часовой пояс': '(.*)'")]
        public void InputTimeZoneDepartment(String hour)
        {
            ReadOnlyCollection<IWebElement> timeZoneSelections = SeleniumDriver.FindElements(By.TagName("option"));
            IWebElement InputTimeZone = timeZoneSelections.FirstOrDefault(x=>x.Text==hour);
            InputTimeZone.Click();
        }

        [Given(@"Я нажимаю кнопку 'Сохранить'")]
        public void SaveDepartment()
        {
            ReadOnlyCollection<IWebElement> buttons = SeleniumDriver.FindElements(By.TagName("button"), 100);
            IWebElement saveButton = buttons.FirstOrDefault(x => x.Text.Contains("Сохранить"));
            saveButton.Click();
        }
        
        [Given(@"Я ввожу данные в поле 'Максимальное время в минутах доставки курьером': '(.*)'")]
        public void InputCourierDepartment(String pass)
        {
            IWebElement InputCourier = SeleniumDriver.FindElement(By.Id("maxDeliveryTimeInMinutes-department"), 100);
            InputCourier.SendKeys(pass);
        }

        [Given(@"Я нажимаю на гамбургер напротив подразделения: '(.*)'")]
        public void DeleteDepartment(String pass)
        {
            ReadOnlyCollection<IWebElement> listDepartment = SeleniumDriver.FindElements(By.ClassName("department-row"), 100);
            IWebElement department = listDepartment.FirstOrDefault(x => x.Text.Contains(pass));
            
            IWebElement buttonsDepartment = department.FindElement(By.ClassName("panel-action"));
            IWebElement button = buttonsDepartment.FindElement(By.TagName("button"));
            button.Click();
        }

        [Given(@"Я нажимаю на кнопку: '(.*)'")]
        public void ClickOn_deleteDepartmentButton(String deleteDepartmentButton)
        {
            IWebElement dropdownmMenu = SeleniumDriver.FindElement(By.ClassName("dropdown-menu"), 100);
            ReadOnlyCollection<IWebElement> deleteDepartmentButtons = dropdownmMenu.FindElements(By.TagName("a"));
            IWebElement deleteButton =
                deleteDepartmentButtons.FirstOrDefault(button => button.Text.Contains(deleteDepartmentButton));
            deleteButton.Click();
        }

        [Given(@"Я нажимаю на кнопку подвтерждения: '(.*)' в модальном окне")]
        public void ClickOn_deleteDepartmentModalButton(String deleteDepartmentModalButton)
        {
            IWebElement modalWindow = SeleniumDriver.FindElement(By.ClassName("modal-content"), 100);       
            SeleniumDriver.WaitUntilElementAvailable(By.ClassName("modal-content"));
            ReadOnlyCollection<IWebElement> modalButtons = modalWindow.FindElements(By.TagName("button"));
            IWebElement deleteModalButton = modalButtons.FirstOrDefault(button => button.Text.Contains(deleteDepartmentModalButton));
            deleteModalButton.Click();
        }

        [Given(@"Я вижу всплывающее сообщение о том, что подразделение '(.*)' успешно удалено")]
        public void CanSee_InfoMessageAlertAboutDepartmentDeleating(String departmentName)
        {
            SeleniumDriver.WaitUntilElementAvailable(By.ClassName("inform-message"));
            IWebElement informMessage = SeleniumDriver.FindElement(By.ClassName("inform-message"), 100);
            String informMessageText = informMessage.Text;
            
            if (informMessageText != "Подразделение " + departmentName + " удалено")
            {
                throw new Exception($"Сообщение об удалении подразделения {departmentName} не найдено!");
            }
        }

        [Given(@"Я закрываю сообщение об успешном создании подразделения")]
        public void ClickOn_InfoMessageCloseButton()
        {
            SeleniumDriver.WaitUntilElementAvailable(By.ClassName("inform-message"));
            IWebElement informMessage = SeleniumDriver.FindElement(By.ClassName("inform-message"), 100);
            IWebElement informMessageCloseButton = informMessage.FindElement(By.TagName("button"));
            informMessageCloseButton.Click();
        }

        [Given(@"Я не вижу удаленного подразделения 'Автотест1' в общем списке")]
        public void CantSee_DeleatedDepartmentInList()
        {
            
        }
    }
}