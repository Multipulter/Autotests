using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using System;

namespace Buzzolls.SpecFlow.Tests.step_definitions.ClientSite
{
    [Binding]
    public class DeliveryPage : BasePage
    {
        private static IWebElement _deliveryDetails;

        protected override void Initialize()
        {
            _deliveryDetails = SeleniumDriver.FindElement(By.ClassName("deliveryScreen_delivery_container__mTo79"));
        }

        [Given("Я вижу информацию о доставке")]
        public void CanSeeDeliveryPage()
        {
            if (_deliveryDetails == null)
            {
                throw new Exception("Информация о доставке не найдена =(");
            }
            SeleniumDriver.Dispose();
        }
        
        [Given("Я вижу информацию о часах работы")]
        public void CanSeeWorkingPeriod()
        {
            ReadOnlyCollection<IWebElement> itemsArray = _deliveryDetails.FindElements(By.ClassName("deliveryScreen_delivery_item__bJKU6"));
            ReadOnlyCollection<IWebElement> divsWorkTimeBlock = itemsArray[0].FindElements(By.TagName("div"));
            IWebElement searchingWorkingPeriod = divsWorkTimeBlock[1].FindElement(By.TagName("h2"));
            var asd = searchingWorkingPeriod.Text;
            if (searchingWorkingPeriod.Text != "Часы работы")
            {
                throw new Exception("Информация о часах работы не найдена");
            }
        }

        [Given("Я вижу информацию о времени доставки")]
        public void CanSeeDeliveryTime()
        {
            ReadOnlyCollection<IWebElement> itemsArray = _deliveryDetails.FindElements(By.ClassName("deliveryScreen_delivery_item__bJKU6"));
            ReadOnlyCollection<IWebElement> divsWorkTimeBlock = itemsArray[1].FindElements(By.TagName("div"));
            IWebElement searchingDeliveryTime = divsWorkTimeBlock[1].FindElement(By.TagName("h2"));
            var asd = searchingDeliveryTime.Text;
            if (searchingDeliveryTime.Text != "Доставим быстро")
            {
                throw new Exception("Информация о времени доставки не найдена");
            }
        }

        [Given("Я вижу информацию о стоимости доставки")]
        public void CanSeeDeliveryPrice()
        {
            ReadOnlyCollection<IWebElement> itemsArray = _deliveryDetails.FindElements(By.ClassName("deliveryScreen_delivery_item__bJKU6"));
            ReadOnlyCollection<IWebElement> divsWorkTimeBlock = itemsArray[2].FindElements(By.TagName("div"));
            IWebElement searchingFreeDeliveryHeader = divsWorkTimeBlock[1].FindElement(By.TagName("h2"));
            var asd = searchingFreeDeliveryHeader.Text;
            if (searchingFreeDeliveryHeader.Text != "Бесплатная доставка")
            {
                throw new Exception("Информация о стоимости доставки не найдена");
            }
        }

        [Given("Я вижу кнопку перехода в карту районов")]
        public void CanSeeDeliveryMapButton()
        {
            ReadOnlyCollection<IWebElement> itemsArray = _deliveryDetails.FindElements(By.ClassName("deliveryScreen_delivery__du4Nv"));
            IWebElement searchingdeliveryMapButton =  itemsArray.FirstOrDefault(x => x.FindElement(By.ClassName("deliveryScreen_btn_sector__2yoI1")).Text == "Карта районов доставки");
            if (searchingdeliveryMapButton.Text == "Карта районов доставки")
            {
                throw new Exception("Кнопка для перехода в карту районов не найдена");
            }
        }

        [Given("Я вижу информацию о наиболее часто задаваемых вопросах")]
        public void CanSeeAdditionalInfo()
        {
            ReadOnlyCollection<IWebElement> itemsArray = _deliveryDetails.FindElements(By.ClassName("deliveryScreen_additional_info_item__3EzOt"));
            if (itemsArray == null)
            {
                throw new Exception("Информация о наиболее часто задаваемых вопросах не найдена");
            }
        }
    }
}
        