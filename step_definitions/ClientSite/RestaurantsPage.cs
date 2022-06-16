using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using System;

namespace Buzzolls.SpecFlow.Tests.step_definitions.ClientSite
{
    [Binding]
    public class RestaurantsPage : BasePage
    {
        private static ReadOnlyCollection<IWebElement> _restaurantsInformation;

        protected override void Initialize()
        {
            _restaurantsInformation =
                SeleniumDriver.FindElements(By.ClassName("restaurantsScreen_tradeObjectsContainer__L7AFC"));
        }

        [Given("Я вижу информацию о ресторанах")]
        public void CanSeeDeliveryDetails()
        {
            if (!_restaurantsInformation.Any())
            {
                throw new Exception("Информация о ресторанах не найдена");
            }
            SeleniumDriver.Dispose();
        }
    }
}