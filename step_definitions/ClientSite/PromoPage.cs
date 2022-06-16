using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using System;

namespace Buzzolls.SpecFlow.Tests.step_definitions.ClientSite
{
    [Binding]
    public class PromoPage : BasePage
    {
        private static ReadOnlyCollection<IWebElement> _bonusActions;

        protected override void Initialize()
        {
            _bonusActions = SeleniumDriver.FindElements(By.ClassName("promoScreen_bonus_action__13HkR"));
        }

        [Given(@"Я вижу первую акцию")]
        public void ClickPromoButton()
        {
            if (!_bonusActions.Any())
            {
                throw new Exception("Акции не найдены");
            }
            SeleniumDriver.Dispose();
        }
        
    }
}