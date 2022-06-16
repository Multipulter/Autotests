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
    public class HomePage : BasePage
    {
        private static ReadOnlyCollection<IWebElement> _menuButtons;
        private static IWebElement _footerMenu;
        private static IWebElement _storeButton;
        private static IWebElement _filterButton;
        

        protected override void Initialize()
        {
           _menuButtons = SeleniumDriver.FindElements(By.ClassName("navigation_navigationItem__OBbWR"));
           _storeButton = SeleniumDriver.FindElement(By.ClassName("navigation_bottomMenuItemContainer__1Orv5"));
           _footerMenu = SeleniumDriver.FindElement(By.ClassName("footer_menu__2d9Hx"));
           //_filterButton = SeleniumDriver.FindElement(By.ClassName("navigation_logo__ZFnr5"));
        }

        protected override void OpenPage()
        {
            SeleniumDriver.Url = Research.BaseClientSiteUrl;
            
            SeleniumDriver.Navigate();
        }

        [Given(@"Я захожу на клиентский сайт")]
        public void OpenHomePage()
        {
        }

        [Given(@"Я перехожу на вкладку 'Меню'")]
        public void ClickMenuButton()
        {
            IWebElement ClickMenuButton = _menuButtons.FirstOrDefault(x => x.Text == "МЕНЮ");
            ClickMenuButton.Click();
        }

        [Given(@"Я перехожу на вкладку 'Акции'")]
        public void ClickPromoButton()
        {
            IWebElement ClickMenuButton = _menuButtons.FirstOrDefault(x => x.Text == "АКЦИИ");
            ClickMenuButton.Click();
        }

        [Given(@"Я перехожу на вкладку 'Доставка'")]
        public void ClickDeliveryDetailButton()
        {
            IWebElement ClickMenuButton = _menuButtons.FirstOrDefault(x => x.Text == "ДОСТАВКА");
            ClickMenuButton.Click();
       
        }

        [Given(@"Я перехожу на вкладку 'Рестораны'")]
        public void ClickRestaurantsButton()
        {
            IWebElement ClickMenuButton = _menuButtons.FirstOrDefault(x => x.Text == "РЕСТОРАНЫ");
            ClickMenuButton.Click();
          
        }

        [Given(@"Нажимаю кнопку 'Корзина'")]
        public void ClickStoreButton()
        {
            try
            {
                _storeButton.Click();
            }
            catch (Exception e)
            {
                var asd = e.Message;
            }
        }
        
        private static void NewMethod(String category)
        {
            try
            {
                ReadOnlyCollection<IWebElement> _ourMenuItems = SeleniumDriver.FindElements(By.ClassName("homeScreen_menuItem__1sITu"));
                List<IWebElement> ourMenuItemsLinks =
                    _ourMenuItems.Select(item => item.FindElement(By.TagName("a"))).ToList();
                IWebElement searchingItem = ourMenuItemsLinks
                    .FirstOrDefault(item => item.FindElement(By.TagName("span")).Text == category);
                searchingItem.Click();
            }
            catch (Exception e)
            {
                var e1 = e.Message;
            }
        }

        [Given(@"Нажимаю на кнопку Сеты")]
        public void ClickMenu2Button()
        {
            NewMethod("Сеты");
        }

        [Given(@"Нажимаю на кнопку Роллы")]
        public void ClickMenu3Button()
        {
            NewMethod("Роллы");
        }

        [Given(@"Нажмаю на кнопку кнопку Суши и гунканы")]
        public void ClickMenu4Button()
        {
            NewMethod("Суши и гунканы");
        }

        [Given(@"Нажимаю на кнопку Гарниры")]
        public void ClickMenu5Button()
        {
            NewMethod("Гарниры");
        }

        [Given(@"Нажимаю на кнопку Десерты")]
        public void ClickMenu6Button()
        {
            NewMethod("Десерты");
        }

        [Given(@"Нажимаю на кнопку Салаты")]
        public void ClickMenu7Button()
        {
            NewMethod("Салаты");
        }

        [Given(@"Переход на главную")]
        public void ClickFilterButton()
        {
            _filterButton.Click();
        }

        [Given(@"Я перехожу в раздел 'Вакансии'")]
        public void ClickVacancyButton()
        {
            ReadOnlyCollection<IWebElement> _footerMenuButtons = _footerMenu.FindElements(By.TagName("a"));
            IWebElement clickVacancyButton = _footerMenuButtons.FirstOrDefault(x => x.Text == "Вакансии");
            clickVacancyButton.Click();
        }

    }
}

