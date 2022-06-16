using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using System;
using System.Collections.Generic;

namespace Buzzolls.SpecFlow.Tests.step_definitions.ClientSite
{
    [Binding]
    public class ApplicationLetterPage: BasePage
    {
        private static ReadOnlyCollection<IWebElement> vacanciesButtons;
        


        [Given(@"Я заполняю поле 'Имя'")]
        public void WriteInQuestionnaireFirstNameInput()
        {
            IWebElement firstnameInput = SeleniumDriver.FindElement(By.CssSelector("input[placeholder='Укажите имя']"));
            firstnameInput.SendKeys("Имя");
        }
        
        [Given(@"Я заполняю поле 'Фамилия'")]
        public void WriteInQuestionnaireLastNameInput()
        {
            IWebElement lastnameInput = SeleniumDriver.FindElement(By.CssSelector("input[placeholder='Укажите фамилию']"));
            lastnameInput.SendKeys("Фамилия");
        }
        
        [Given(@"Я заполняю поле 'Ваш телефон'")]
        public void WriteInQuestionnairePhoneNumberInput()
        {
            IWebElement phoneNumberInput = SeleniumDriver.FindElement(By.CssSelector("input[placeholder='Укажите номер телефона']"));
            phoneNumberInput.SendKeys("5555555555");
        }

        [Given(@"Я заполняю поле 'Дата рождения'")]
        public void WriteInQuestionnaireDataBirthInput()
        {
            IWebElement dateBirthInput =
                SeleniumDriver.FindElement(By.ClassName("applicationLetterForm_applicationLetter_form_input_birthday_day__2Nv7v"));
            dateBirthInput.Click();
            IWebElement monthSelectList = SeleniumDriver.FindElement(By.ClassName("react-datepicker__month-select"));
            monthSelectList.Click();
            IWebElement monthValueSelect = SeleniumDriver.FindElement(By.CssSelector("option[value = '0']"));
            monthValueSelect.Click();
            IWebElement yearSelectList = SeleniumDriver.FindElement(By.ClassName("react-datepicker__year-select"));
            yearSelectList.Click();
            IWebElement yearValueSelect = SeleniumDriver.FindElement(By.CssSelector("option[value='1990']"));
            yearValueSelect.Click();
            IWebElement datePicker =
                SeleniumDriver.FindElement(By.CssSelector("div[aria-label='Choose понедельник, 1 января 1990 г.']"));
            datePicker.Click();
        }

        [Given(@"Я отказываюсь от обработки персональных данных")]
        public void RefusalOfProcessPersonalData()
        {
            IWebElement checkboxProcessPersonalData =
                SeleniumDriver.FindElement(By.ClassName("checkBox_checkBox__39Dtz"));
            checkboxProcessPersonalData.Click();
        }

        [Given(@"Я нажимаю на кнопку 'Отправить'")]
        public void ClickSendQuestionnaireButton()
        {
            IWebElement applicationLetterButton = SeleniumDriver
                .FindElement(By.ClassName("applicationLetterForm_applicationLetter_form_btn__1oySM"));
            applicationLetterButton.Click();
        }

        [Given(@"Я вижу сообщение об успешной отправке анкеты")]
        public void CanSeeSuccessMessage()
        {
            IWebElement successMessage = SeleniumDriver.FindElement(By.TagName("small"));
            if (successMessage.Text != $"Спасибо! Ваша заявка принята")
            {
                throw new Exception("Заявка не отправлена");
            }
        }
        
        [Given(@"Я вижу сообщение о необходимости подтверждения согласия на обработку персональных данных")]
        public void CanSeeAgreementMessage()
        {
            IWebElement agreementMessage = SeleniumDriver.FindElement(By.TagName("small"));
            if (agreementMessage.Text != $"Подтвердите согласие")
            {
                throw new Exception("Заявка отправлена без согласия на обработку персональных данных");
            }
        }
    }
}