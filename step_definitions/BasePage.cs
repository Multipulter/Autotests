using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace Buzzolls.SpecFlow.Tests.step_definitions
{
	public abstract class BasePage
	{
		private const Int16 AwaitTime = 5;

		protected static readonly IWebDriver SeleniumDriver = new ChromeDriver();

		protected BasePage()
		{
			SeleniumDriver
				.Manage()
				.Window
				.Maximize();
			OpenPage();
			SeleniumDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(AwaitTime);
			Initialize();
		}

		/// <summary>
		/// Инициализация компонентов страницы
		/// </summary>
		protected virtual void Initialize()
		{
		}

		/// <summary>
		/// Переход на страницу
		/// </summary>
		protected virtual void OpenPage()
		{
		}

		~BasePage()
		{
			//SeleniumDriver
			//    .Manage()
			//    .Cookies
			//    .DeleteAllCookies();

			//SeleniumDriver.Close();
			//SeleniumDriver.Dispose();

			//SeleniumDriver.Quit();
		}
	}
}