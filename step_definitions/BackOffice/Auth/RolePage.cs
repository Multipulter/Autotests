using System;
using System.Collections.ObjectModel;
using System.Linq;
using Buzzolls.SpecFlow.Tools;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Buzzolls.SpecFlow.Tests.step_definitions.BackOffice.Auth
{
    [Binding]
    public class RolePage : BasePage
    {
        [Given(@"Я выбираю роль '(.*)'")]
        public void SelectRole(String roleName)
        {
            SeleniumDriver.WaitUntilElementAvailable(By.ClassName("authenticate-body"));
            
            ReadOnlyCollection<IWebElement> roles = SeleniumDriver.FindElements(By.TagName("button"), 100);
            IWebElement currentRole = roles.FirstOrDefault(role => role.Text == roleName);
            currentRole.Click();
        }
    }
}