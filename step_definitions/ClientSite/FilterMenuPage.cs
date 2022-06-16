using System;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;
using TechTalk.SpecFlow;


namespace Buzzolls.SpecFlow.Tests.step_definitions.ClientSite
{
    [Binding]
    public class FilterMenuPage : BasePage
    {
        private static ReadOnlyCollection<IWebElement> _menuButtons;
        private static IWebElement _storeButton;
        private static IWebElement _filterButton;
        private static IWebElement _pickCategoryButton;
        private static IWebElement _menuItems;

        protected override void Initialize()
        {
            _filterButton = SeleniumDriver.FindElement(By.ClassName("productMenuScreen_selectFilterContainer__okjLl"));

        }

        [Given(@"Нажмаю на фильтр")]
        public void ClickFilterButton()
        {
            _filterButton.Click();
        }

         // [Given(@"Выбираю категориию 'Кальмар' для фильтрации")]
        //public void PickCategory()
        //{
        //  _pickCategoryButton = _seleniumDriver.FindElement(By.Id("react-select-4-option-2"));
        //_pickCategoryButton.Click();
        //}

        
        [Given(@"Выбираю категориию 'Кальмар' для фильтрации")]
        public void ClickAddButton()
        {

            ReadOnlyCollection<IWebElement> itemsArray = _menuItems.FindElements(
                By.ClassName("selectLis__option css-0"));
            IWebElement searchingProduct = itemsArray.FirstOrDefault(x =>
                x.FindElement(By.Id("react-select-3-option-2")).Text == "Кальмар");
            searchingProduct.Click();
        } 
        [Given(@"Вижу товары из категории 'Кальмар'")]
          public void SeeStoreItem()
        {
            _menuItems = SeleniumDriver.FindElement(By.ClassName("productMenuScreen_productsContainer__2me8d"));

            ReadOnlyCollection<IWebElement> storeElements =
                SeleniumDriver.FindElements(By.ClassName("productImage_filters__13NxW"));
            IWebElement searchingProduct = storeElements.FirstOrDefault(x =>
                x.FindElement(By.ClassName("productImage_backgrnd__1ymaI")).Text == "Кальмар");
            if (searchingProduct == null)
            {
                throw new Exception("Не были найдены товары из категории 'Кальмар'");
            }
        }
    }
}

