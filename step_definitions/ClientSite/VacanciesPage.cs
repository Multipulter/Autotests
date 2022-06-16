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
    public class VacanciesPage: BasePage
    {
        private static IWebElement _writeInQuestionnaireButton;
        
        protected override void Initialize()
        {
            _writeInQuestionnaireButton = SeleniumDriver.FindElement(By.ClassName("vacanciesScreen_btn__3ZT35"));
        }
        
        [Given(@"Я нажимаю на кнопку 'Заполнить анкету'")]
        public void ClickWriteInQuestionnaireButton()
        {
            _writeInQuestionnaireButton.Click();
        }

        [Given(@"Я нажимаю на кнопку 'Выбрать' в окне 'Кассир'")]
        public void ClickChooseCashierVacancy()
        {
            IWebElement _cashierVacancyWindow = SeleniumDriver.FindElement(By.ClassName("vacancyCard_card_content__2gaNT"));   
            IWebElement cashierVacancyButton = _cashierVacancyWindow.FindElement(By.ClassName("vacanciesScreen_card_btn__323Sc"));
            
            cashierVacancyButton.Click();
        }

        [Given(@"Я вижу название вакансии 'Кассир'")]
        public void CanSeeCashierText()
        {
            IWebElement _cashierBanner = SeleniumDriver.FindElement(By.ClassName("applicationLetterForm_cashierBanner__7vJWi"));
            IWebElement cashierText = _cashierBanner.FindElement(By.ClassName("applicationLetterForm_vacancy_name__1bSFE"));
            if (cashierText.Text != "Кассир")
            {
                throw new Exception("Текст 'Кассир' отсутствует!");
            }
        }
    }
}