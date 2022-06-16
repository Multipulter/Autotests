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
    public class MenuPage : BasePage
    {
        private static ReadOnlyCollection<IWebElement> _menuButtons;

        //  private static IWebElement _storeButton;
        private static IWebElement _menuItems;

        protected override void Initialize()
        {
            _menuButtons = SeleniumDriver.FindElements(By.ClassName("navigation_navigationItem__OBbWR"));
            _menuItems = SeleniumDriver.FindElement(By.ClassName("productMenuScreen_containerProducts__zmzSq"));
            //  _storeButton = _seleniumDriver.FindElement(By.ClassName("navigation_bottomMenuItemContainer__621fl"));
        }

        //    And Нажимаю на кнопку "Добавить" у продукта "Cremette 2.0" в меню
        //   And Нажимаю кнопку "Корзина"
        //  And Вижу в корзине продукт "Cremette 2.0"
        [Given(@"Я нажимаю на кнопку 'Заменить роллы' у продукта 'Cremette 2.0' в меню")]
        public void ClickComboReplacmentsButton()
        {
            ReadOnlyCollection<IWebElement> itemsArray = _menuItems.FindElements(
                By.ClassName("shortProduct_productContainer__2qlN0"));
            IWebElement searchingProduct = itemsArray.FirstOrDefault(x =>
                x.FindElement(By.ClassName("shortProduct_productName__118qG")).Text == "Cremette 2.0");
            IWebElement comboReplacmentsButton =
                searchingProduct.FindElement(By.ClassName("shortProduct_btnComboReplacments__3qFFX"));
            comboReplacmentsButton.Click();
        }

        [Given(@"Я нажимаю на кнопку 'Заменить' у ролла 'Райх'")]
        public void ClickRollReplacmentsButton()
        {
            IWebElement comboItems =
                SeleniumDriver.FindElement(By.ClassName("comboReplacements_comboItemsContainer__2h0Os"));
            ReadOnlyCollection<IWebElement> itemsArray = comboItems.FindElements(
                By.ClassName("comboReplacements_comboItem__3Sc7p"));
            IWebElement searchingProduct = itemsArray.FirstOrDefault(x =>
                x.FindElement(By.ClassName("comboReplacements_comboItemName__2hAje")).Text == "Райх");
            IWebElement comboReplacmentsButton =
                searchingProduct.FindElement(By.ClassName("comboReplacements_btnMinWidth__137bh"));
            comboReplacmentsButton.Click();
        }

        [Given(@"Я выбираю ролл '(.*)'")]
        public void ClickOnProductBlock(String value)
        {
            ReadOnlyCollection<IWebElement> replacementItem =
                SeleniumDriver.FindElements(By.ClassName("carousel_menuItem__1rEwE"));
            IWebElement rollButton = replacementItem
                .FirstOrDefault(x => x.FindElements(By.TagName("div"))[1].Text == value);

            rollButton.Click();
        }

        [Given(@"Я нажимаю на кнопку 'Заменить'")]
        public void RickRollAkasiBugets()
        {
            IWebElement replacementItem =
                SeleniumDriver.FindElement(By.ClassName("selectReplacement_bottomPanel__3_TVl"));
            ReadOnlyCollection<IWebElement> itemsArray = replacementItem.FindElements(
                By.ClassName("btnDefault"));
            IWebElement selectRepleacementButton = itemsArray.FirstOrDefault(x =>
                x.Text == "Заменить");
            selectRepleacementButton.Click();
        }


        [Given(@"Я вижу заменену на ролл '(.*)'")]
        public void CanSeeReplacments(String value)
        {
            IWebElement comboItems =
                SeleniumDriver.FindElement(By.ClassName("comboReplacements_comboItemsContainer__2h0Os"));
            ReadOnlyCollection<IWebElement> itemsArray = comboItems.FindElements(
                By.ClassName("comboReplacements_comboItem__3Sc7p"));
            IWebElement searchingProduct = itemsArray.FirstOrDefault(x =>
                x.FindElement(By.ClassName("comboReplacements_comboItemName__2hAje")).Text == value);
            IWebElement comboReplacmentsButton =
                searchingProduct.FindElement(By.ClassName("btnBackgroundOrange"));
            if (comboReplacmentsButton.Text != "Вернуть")
            {
                throw new Exception("Замена не найдена");
            }
        }

        [Given(@"Нажимаю на кнопку 'Добавить' у продукта 'Cremette 2.0' в меню")]
        public void ClickAddButton()
        {
            ReadOnlyCollection<IWebElement> itemsArray = _menuItems.FindElements(
                By.ClassName("shortProduct_productContainer__1cZBu"));
            IWebElement searchingProduct = itemsArray.FirstOrDefault(x =>
                x.FindElement(By.ClassName("shortProduct_productName__24V9-")).Text == "Cremette 2.0");
            IWebElement addProductButton =
                searchingProduct.FindElement(By.ClassName("btnAddProduct"));
            addProductButton.Click();
        }

        [Given(@"Я вижу стоимость сета (.*)")]
        public void CanSeeComboPrice(String value)
        {
            IWebElement addToCartButton =
                SeleniumDriver.FindElement(By.ClassName("comboReplacements_btnAddCombo__1m_36"));
            if (addToCartButton.Text != $"Добавить за {value} ₽")
            {
                throw new Exception("Стоимость продукта отличается от ожидаемой");
            }
        }

        [Given(@"Я вижу надбавочную стоимость за замену в (.*)")]
        public void CanSeeDifferenceInPrice(String value)
        {
            IWebElement differenceInPrice =
                SeleniumDriver.FindElement(By.ClassName("comboReplacements_differenceInPrice__2ACgo"));
            if (differenceInPrice.Text != $"+ {value} ₽")
            {
                throw new Exception("Надбавочная стоимость отличается от ожидаемой");
            }
        }

        //  [Given(@"Нажимаю кнопку 'Корзина'")]
        //  public void ClickStoreButton()
        // {
        //     try
        //    {
        //        _storeButton.Click();
        //    }
        //    catch (Exception e)
        //   {
        //      var asd = e.Message;
        //   }
        //  }

        [Given(@"Вижу в корзине продукт 'Cremette 2.0'")]
        public void SeeStoreItem()
        {
            ReadOnlyCollection<IWebElement> storeElements =
                SeleniumDriver.FindElements(By.ClassName("cartItem_orderItem__3zWDs"));
            IWebElement searchingProduct = storeElements.FirstOrDefault(x =>
                x.FindElement(By.ClassName("cartItem_orderItemName__19_HM")).Text == "Cremette 2.0");
            if (searchingProduct == null)
            {
                throw new Exception("Не был найден продукт Cremette 2.0");
            }
        }
        
        [Given("Я вижу информацию о меню")]
        public void CanSeeWorkingPeriod()
        {
            if (_menuItems == null)
            {
                throw new Exception("Информация о меню не найдена =(");
            }
            SeleniumDriver.Dispose();
        }

        // [Given(@"Вижу надпись 'Корзина пуста'")]
        // public void SeeStoreEmpty()
        // {
        // IWebElement storeElement = _seleniumDriver.FindElement(By.ClassName("containerCart"));
        // IWebElement searchingEmpty = storeElement.FindElement(By.ClassName("myOrder_emptyCart__1eky2"));
        // if (searchingEmpty.Text!="Корзина пуста")
        // {
        //     throw new Exception("Текст Корзина пуста не найден");

        // }
        // }
    }
}