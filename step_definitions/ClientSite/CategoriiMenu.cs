using System;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;
using TechTalk.SpecFlow;


namespace Buzzolls.SpecFlow.Tests.step_definitions.ClientSite
{
    [Binding]
    public class CategoriiMenuPage : BasePage
    {
        private static ReadOnlyCollection<IWebElement> _categoryButtons;


        protected override void Initialize()
        {
            _categoryButtons = SeleniumDriver.FindElements(By.ClassName("menuCategoryList_btnCategory__uDelG"));
        }

        [Given(@"Нажимаю на категорию 'Салаты'")]
        public void ClickCategoryButton()
        {
            IWebElement ClickCategory = _categoryButtons.FirstOrDefault(x => x.Text == "Салаты");
            ClickCategory.Click();
        }

        //  [Given(@"Нажимаю на категорию 'Салаты'")]
        //public void ClickAddButton()
        //{
        //  ReadOnlyCollection<IWebElement> itemsArray = _menuItems.FindElements(
        //    By.ClassName("productMenuScreen_menuCategoryList__22XzD"));
        //IWebElement searchingProduct = itemsArray.FirstOrDefault(x =>
        //  x.FindElement(By.ClassName("menuCategoryList_btnCategory__uDelG")).Text == "Салаты");
        //searchingProduct.Click();
        //}

        [Given(@"Вижу надпись 'Салаты'")]
        public void SeeCategoryName()
        {

            IWebElement menuElement =
                SeleniumDriver.FindElement(By.ClassName("productMenuScreen_containerProducts__3Tjlj"));
            IWebElement searchingCategoryName =
                menuElement.FindElement(By.ClassName("productMenuScreen_productGroupTitle__3tPEc"));

            if (searchingCategoryName.Text != "САЛАТЫ")
            {
                throw new Exception("Текст Салаты не найден");
                // [Given(@"Вижу товары из категории 'Салаты'")]
                // public void SeeStoreItem()
                //{
                //  _menuItems = _seleniumDriver.FindElement(By.ClassName("productMenuScreen_productsContainer__2me8d"));

                //ReadOnlyCollection<IWebElement> storeElements =
                //  _seleniumDriver.FindElements(By.ClassName("mproductMenuScreen_containerProducts__3Tjlj"));
                //IWebElement searchingProduct = storeElements.FirstOrDefault(x =>
                //  x.FindElement(By.ClassName("productMenuScreen_productGroupTitle__3tPEc")).Text == "Салаты");
                //if (searchingProduct == null)
                //{
                //  throw new Exception("Не была найдена выбранная категория");
                //}
            }
        }

        [Given(@"Вижу надпись категории '(.*)'")]
        public void SeeCategory4Name(String value)
        {

            IWebElement menuElement =
                SeleniumDriver.FindElement(By.ClassName("productMenuScreen_containerProducts__3Tjlj"));
            IWebElement searchingCategoryName =
                menuElement.FindElement(By.ClassName("productMenuScreen_productGroupTitle__3tPEc"));

            if (searchingCategoryName.Text != value.ToUpper())
            {
                throw new Exception($"Текст {value} не найден");
            }
        }
    }
}