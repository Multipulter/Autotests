using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Events;
using Buzzolls.SpecFlow.Tools;
using OpenQA.Selenium.Support.UI;

namespace Buzzolls.SpecFlow.Tests.step_definitions.BackOffice.ShoppingAreaAdministrator
{
	[Binding]
	public class ManagerPage : BasePage
	{
		private static IWebElement _sideBar;
		private static Actions _action = new Actions(SeleniumDriver);
		private static By _informMessageSelector = By.ClassName("inform-message");

		protected override void Initialize()
		{
			_sideBar = SeleniumDriver.FindElement(By.Id("sidebar"), 100);
		}
		
		[Given(@"Я перехожу в справочник 'Стоп-листы'")]
		public void ClickOn_TradeObjectManagerStopListButton()
		{
			IWebElement sideBar = SeleniumDriver.FindElement(By.Id("sidebar"), 100);
			ReadOnlyCollection<IWebElement> sideBarButtons = sideBar.FindElements(By.TagName("a"));
			Actions action = new Actions(SeleniumDriver); 
			action.MoveToElement(sideBar).Perform();
			
			
			IWebElement stopListMenuButton = sideBarButtons.FirstOrDefault(sideBarButton => sideBarButton.Text == "Стоп-листы");
			stopListMenuButton!.Click();
			
			SeleniumDriver.WaitUntilElementAvailable(By.LinkText("Стоп-листы"));
			
			//IWebElement topListButton = sideBarButtons.FirstOrDefault(sideBarButton => sideBarButton.GetAttribute("href") == "https://dev.backoffice.buzzolls.ru/Managment/StopLists/Index");			
			IWebElement stopListButton = sideBarButtons.FirstOrDefault(sideBarButton => sideBarButton.GetAttribute("href") == "http://localhost:51275/Managment/StopLists/Index");
			stopListButton!.Click();
		}
		
		[Given(@"Я перехожу в справочник 'Стоп-листы продуктов'")]
		public void ClickOn_TradeObjectManagerProductsStopLIstButton()
		{
			IWebElement sideBar = SeleniumDriver.FindElement(By.Id("sidebar"), 100);
			ReadOnlyCollection<IWebElement> sideBarButtons = sideBar.FindElements(By.TagName("a"));
			Actions action = new Actions(SeleniumDriver); 
			action.MoveToElement(sideBar).Perform();
			
			
			IWebElement stopListMenuButton = sideBarButtons.FirstOrDefault(sideBarButton => sideBarButton.Text == "Стоп-листы");
			stopListMenuButton!.Click();
			
			SeleniumDriver.WaitUntilElementAvailable(By.LinkText("Стоп-листы"));
			
			//IWebElement topListButton = sideBarButtons.FirstOrDefault(sideBarButton => sideBarButton.GetAttribute("href") == "https://dev.backoffice.buzzolls.ru/Managment/StopLists/Index");			
			IWebElement tradeObjectManagerProductsStopLIstButton = sideBarButtons.FirstOrDefault(sideBarButton => sideBarButton.GetAttribute("href") == "http://localhost:51275/Managment/ProductsStopList/Index");
			tradeObjectManagerProductsStopLIstButton!.Click();
		}
		
		[Given(@"Я нажимаю на кнопку 'Остановить работу точки'")]
		public void ClickOn_StopTradeObjectWorkButton()
		{
			IWebElement stopTradeObjectWorkButton = SeleniumDriver.FindElement(By.Id("stopTradeObjectWork"), 100);
			stopTradeObjectWorkButton.Click();
		}
		
		[Given(@"Я ввожу ожидаемое окончание стопа торговой точки")]
		public void Input_StopEndingTime()
		{
			IWebElement expectedStopEndingTimeDataField = SeleniumDriver.FindElement(By.Id("expectedEndTime"), 100);
			DateTime dateTime = DateTime.Now + TimeSpan.FromDays(1);
			expectedStopEndingTimeDataField.SendKeys(dateTime.ToString("dd/MM/yyyy 12:00"));
		}
		
		[Given(@"Я выбираю типы заказа для стопа")]
		public void Choose_StopOrderType()
		{
			ReadOnlyCollection<IWebElement> selections = SeleniumDriver.FindElements(By.ClassName("selection"), 100);
			IWebElement areaSelection = selections.FirstOrDefault(selection => selection.FindElements(By.TagName("input")).Count() != 0);
			areaSelection.Click();
			
			ReadOnlyCollection<IWebElement> options = SeleniumDriver.FindElements(By.TagName("option"), 100);
			IWebElement areaOption1 = options.FirstOrDefault(option => option.Text == "Доставка");
			areaOption1.Click();
			
			IWebElement areaOption2 = options.FirstOrDefault(option => option.Text == "Самовывоз");
			areaOption2.Click();
			
			areaSelection.Click();
		}
		
		[Given(@"Я выбираю причину стопа")]
		public void Choose_TradeObjectStopReason()
		{
			IWebElement tradeObjectStopReason = SeleniumDriver.FindElement(By.Id("tradeObjectStopReason"), 100);
			tradeObjectStopReason.Click();
			
			ReadOnlyCollection<IWebElement> options = SeleniumDriver.FindElements(By.TagName("option"), 100);
			IWebElement areaOption = options.FirstOrDefault(option => option.Text == "Нехватка курьеров");
			areaOption.Click();
		}
		
		[Given(@"Я нажимаю на кнопку 'Остановить'")]
		public void ClickOn_StopTradeObjectButton()
		{
			IWebElement stopTradeObjectWorkButton = SeleniumDriver.FindElement(By.Id("stopTradeObjectWorkButton"), 100);
			stopTradeObjectWorkButton.Click();
			
			SeleniumDriver.WaitUntilElementClose(By.ClassName("block-ui-visible"));
			SeleniumDriver.WaitUntilElementAvailable(By.Id("stopTradeObjectWork"));
			SeleniumDriver.WaitUntilElementClose(By.ClassName("block-ui-visible"));
		}
		
		[Given(@"Я нажимаю на кнопку 'Остановить' в меню стопа предзаказов точки")]
		public void ClickOn_StopTradeObjectPreOrdersButton()
		{
			IWebElement stopTradeObjectWorkButton = SeleniumDriver.FindElement(By.Id("stopTradeObjectPreOrdersButton"), 100);
			stopTradeObjectWorkButton.Click();
			
			SeleniumDriver.WaitUntilElementClose(By.ClassName("block-ui-visible"));
			SeleniumDriver.WaitUntilElementAvailable(By.Id("stopTradeObjectWork"));
			SeleniumDriver.WaitUntilElementClose(By.ClassName("block-ui-visible"));
		}
		
		[Given(@"Я нажимаю на кнопку 'Снять стоп'")]
		public void ClickOn_RemoveStopButton()
		{
			IWebElement removeStopButton = SeleniumDriver.FindElement(By.Id("removeTradeObjectStopButton"), 100);
			removeStopButton.Click();
		}
		
		[Given(@"Я нажимаю на кнопку 'Остановить предзаказы точки'")]
		public void ClickOn_StopTradeObjectPreOrders()
		{
			IWebElement stopTradeObjectPreOrdersButton = SeleniumDriver.FindElement(By.Id("stopTradeObjectPreOrdersButton"), 100);
			stopTradeObjectPreOrdersButton.Click();
		}
		
		[Given(@"Я ввожу ожидаемое окончание стопа торговой точки на предзаказ")]
		public void Input_StopPreOrdersEndingTime()
		{
			IWebElement expectedStopPreOrdersEndingTimeDataField = SeleniumDriver.FindElement(By.Id("preordersStopExpectedEndTime"), 100);
			expectedStopPreOrdersEndingTimeDataField.SendKeys(Keys.Control + "a");
			DateTime dateTime = DateTime.Now + TimeSpan.FromDays(1);
			expectedStopPreOrdersEndingTimeDataField.SendKeys(dateTime.ToString("dd/MM/yyyy 12:00"));
		}
		
		[Given(@"Я выбираю причину стопа на предзаказ")]
		public void Choose_StopPreOrdersReason()
		{
			IWebElement tradeObjectStopPreOrdersReason = SeleniumDriver.FindElement(By.Id("tradeObjectStopPreOrdersReason"), 100);
			tradeObjectStopPreOrdersReason.Click();
			
			ReadOnlyCollection<IWebElement> options = SeleniumDriver.FindElements(By.TagName("option"), 100);
			IWebElement areaOption = options.FirstOrDefault(option => option.Text == "Нехватка курьеров");
			areaOption.Click();
		}
		
		[Given(@"Я нажимаю на кнопку отмены стопа на предзаказы")]
		public void ClickOn_CancelPreOrdersStopButton()
		{
			IWebElement cancelPreOrdersStopButton = SeleniumDriver.FindElement(By.Id("cancelPreOrdersStopButton"), 100);
			cancelPreOrdersStopButton.Click();
		}
		
		[Given(@"Я выбираю продукт '(.*)' для стопа")]
		public void Input_productToStopList(String productName)
		{
			ReadOnlyCollection<IWebElement> pageSelectLists = SeleniumDriver.FindElements(By.ClassName("select_list"), 100);
			IWebElement productSelectList = pageSelectLists.FirstOrDefault(productSelectList => productSelectList.Text.Contains("Выберите продукт"));
			productSelectList.Click();
			
			IWebElement inputProductToStop = productSelectList.FindElement(By.TagName("input"));
			inputProductToStop.SendKeys(productName);
			inputProductToStop.SendKeys(Keys.Enter);
		}
		
		[Given(@"Я ввожу причину остановки для стопа: '(.*)'")]
		public void Input_ProductStopReason(String stopReason)
		{
			IWebElement productStopReasonTextArea = SeleniumDriver.FindElement(By.Id("productStopReasonTextArea"), 100);
			productStopReasonTextArea.SendKeys(stopReason);
		}
		
		[Given(@"Я нажимаю на кнопку 'В стоп-лист'")]
		public void ClickOn_AddProductToStopListButton()
		{
			IWebElement addProductToStopListButton = SeleniumDriver.FindElement(By.Id("addProductToStopListButton"), 100);
			addProductToStopListButton.Click();
			
			SeleniumDriver.WaitUntilElementClose(By.ClassName("blocking"));
		}
		
		[Given(@"Я нажимаю на кнопку 'Возобновить продажи' продукта '(.*)'")]
		public void ClickOn_ResumeSalesButton(String productName)
		{
			ReadOnlyCollection<IWebElement> stopProductLines = SeleniumDriver.FindElements(By.TagName("tr"), 100);
			IWebElement currentStopProductLine = stopProductLines
				.FirstOrDefault(currentStopProductLine => currentStopProductLine.Text.Contains(productName));
			
			IWebElement resumeSalesButton = currentStopProductLine.FindElement(By.Name("resumeSalesButton"));
			resumeSalesButton.Click();
			
			SeleniumDriver.WaitUntilElementAvailable(By.ClassName("modal-content"));
			IWebElement yesResumeSalesOfProductModalButton = SeleniumDriver.FindElement(By.Id("yesResumeSalesOfProductModalButton"), 100);
			yesResumeSalesOfProductModalButton.Click();
			
			SeleniumDriver.WaitUntilElementClose(By.ClassName("blocking"));
		}
	}
}