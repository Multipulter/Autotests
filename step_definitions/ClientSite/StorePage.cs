using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Buzzolls.SpecFlow.Tests.step_definitions.ClientSite
{
    [Binding]
    public class StorePage : BasePage
    {
        private static IWebElement _filterButton;

        protected override void Initialize()
        {
            _filterButton = SeleniumDriver.FindElement(By.ClassName("itemCountProducts_btnCountMinus__DKMSn"));
        }

        [Given(@"Вижу надпись 'Корзина пуста'")]
        public void SeeStoreEmpty()
        {

            IWebElement storeElement = SeleniumDriver.FindElement(By.ClassName("myOrder_orderItems__2LM-E"));
            IWebElement searchingEmpty = storeElement.FindElement(By.ClassName("myOrder_emptyCart__1eky2"));
            var asd = searchingEmpty.Text;
            if (searchingEmpty.Text != "КОРЗИНА ПУСТА")
            {
                throw new Exception("Текст Корзина пуста не найден");

            }
        }

        [Given(@"Удаляю продукт 'Cremette 2.0' из корзины")]
        public void ClickFilterButton()
        {
            _filterButton.Click();
        }
    }
}
    