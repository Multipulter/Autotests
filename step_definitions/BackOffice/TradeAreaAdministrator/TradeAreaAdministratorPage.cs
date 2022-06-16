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
	public class TradeAreaAdministratorPage : BasePage
	{
		private static IWebElement _sideBar;
		private static Actions _action = new Actions(SeleniumDriver);

		protected override void Initialize()
		{
			_sideBar = SeleniumDriver.FindElement(By.Id("sidebar"), 100);
		}

		[Given(@"Я нажимаю на пункт меню 'Проблемные заказы'")]
		public void ClickOn_ProblemOrdersButton()
		{
			ReadOnlyCollection<IWebElement> sideBarButtons = _sideBar.FindElements(By.TagName("a"));
			Actions action = new Actions(SeleniumDriver);
			action.MoveToElement(_sideBar).Perform();
			
			IWebElement infoChapterButton = sideBarButtons.FirstOrDefault(sideBarButton => sideBarButton.Text == "Информация");
			infoChapterButton!.Click();

			SeleniumDriver.WaitUntilElementAvailable(By.LinkText("Проблемные заказы"));
			IWebElement problemOrdersButton = SeleniumDriver.FindElement(By.LinkText("Проблемные заказы"), 100);
			problemOrdersButton!.Click();
		}

		[Given(@"Я нажимаю на страницу меню 'Меню'")]
		public void ClickOn_MenuPage()
		{
			IWebElement sideBar = SeleniumDriver.FindElement(By.Id("sidebar"), 100);
			ReadOnlyCollection<IWebElement> sideBarButtons = sideBar.FindElements(By.TagName("a"));
			
			Actions action = new Actions(SeleniumDriver);
			action.MoveToElement(sideBar).Perform();
			
			IWebElement menuPageButton = sideBarButtons.FirstOrDefault(sideBarButton => sideBarButton.Text == "Меню");
			menuPageButton!.Click();
		}

		[Given(@"Я открываю раздел 'Бонусные акции'")]
		public void ClickOn_BonusPage()
		{
			IWebElement sideBar = SeleniumDriver.FindElement(By.Id("sidebar"), 100);
			ReadOnlyCollection<IWebElement> sideBarButtons = sideBar.FindElements(By.TagName("a"));
			Actions action = new Actions(SeleniumDriver); 
			action.MoveToElement(sideBar).Perform();
			
			
			IWebElement bonusActionButton = sideBarButtons.FirstOrDefault(sideBarButton => sideBarButton.Text == "Бонусные акции");
			bonusActionButton!.Click();
			
			SeleniumDriver.WaitUntilElementAvailable(By.LinkText("Промо-коды"));
			
			//IWebElement bonusActionMenuList = sideBarButtons.FirstOrDefault(sideBarButton => sideBarButton.GetAttribute("href") == "http://localhost:51275/Promotion/BonusActions/Index");
			IWebElement bonusActionMenuList = sideBarButtons.FirstOrDefault(sideBarButton => sideBarButton.GetAttribute("href") == "https://dev.backoffice.buzzolls.ru/Promotion/BonusActions/Index");
			bonusActionMenuList!.Click();
		}
		
		[Given(@"Я нажимаю на кнопку 'Добавить акцию'")]
		public void ClickOn_AddBonusAction()
		{
			SeleniumDriver.WaitUntilElementAvailable(By.ClassName("container-fluid"));
			
			ReadOnlyCollection<IWebElement> sideBarButtons = SeleniumDriver.FindElements(By.TagName("a"), 100);
			IWebElement addPromocodeButton = sideBarButtons.FirstOrDefault(contractorsButton => contractorsButton.Text == "Добавить акцию");
			addPromocodeButton!.Click();
		}
		
		[Given(@"Я ввожу в поле 'Название' наименование промокода '(.*)'")]
		public void Input_PromocodeName(String promocodeName)
		{

			ReadOnlyCollection<IWebElement> promoInputFields = SeleniumDriver.FindElements(By.TagName("input"), 100);
			IWebElement promocodeNameInput = promoInputFields.FirstOrDefault(promoInputField => promoInputField.GetAttribute("placeholder") == "Введите название акции");
			promocodeNameInput!.SendKeys(promocodeName);
		} 
		
		[Given(@"Выбираю тип акции '(.*)'")]
		public void Select_PromocodeType(String promocodeType)
		{
			
			IWebElement promocodeTypeList = SeleniumDriver.FindElement(By.ClassName("selection"), 100);
			promocodeTypeList.Click();
			
			ReadOnlyCollection<IWebElement> options = SeleniumDriver.FindElements(By.TagName("option"), 100);
			IWebElement promocodeTypeOption = options.FirstOrDefault(option => option.Text == promocodeType);
			promocodeTypeOption!.Click();
		} 
		
		[Given(@"Я добавляю области применения акции '(.*)'")]
		public void Add_AreasOfActionUsage(String area)
		{
			ReadOnlyCollection<IWebElement> selections = SeleniumDriver.FindElements(By.ClassName("selection"), 100);
			IWebElement areaSelection = selections.FirstOrDefault(selection => selection.FindElements(By.TagName("input")).Count() != 0);
			areaSelection!.Click();
			
			if (!area.Contains(","))
			{
				ReadOnlyCollection<IWebElement> options = SeleniumDriver.FindElements(By.TagName("option"), 100);
				IWebElement areaOption = options.FirstOrDefault(option => option.Text == area);
				areaOption!.Click();
			}
			
			else // если строка "Доставка,Ресторан"
			{
				String[] areas = area.Split(",");
				foreach (String currentArea in areas)
				{
					ReadOnlyCollection<IWebElement> options = SeleniumDriver.FindElements(By.TagName("option"), 100);
					IWebElement areaOption = options.FirstOrDefault(option => option.Text == currentArea);
					areaOption!.Click();
				}
			}
		} 
		
		[Given(@"Я добавляю эффект акции")]
		public void Add_ActionEffect()
		{
			ReadOnlyCollection<IWebElement> buttons = SeleniumDriver.FindElements(By.TagName("button"), 100);
			IWebElement buttonAddActionEffect = buttons.FirstOrDefault(button => button.Text == "Добавить эффект");
			buttonAddActionEffect!.Click();
			
			IWebElement modalDialog = SeleniumDriver.FindElement(By.ClassName("modal-dialog"), 100);
			IWebElement section = modalDialog.FindElement(By.ClassName("selection"));
			section.Click();
			
			ReadOnlyCollection<IWebElement> options = SeleniumDriver.FindElements(By.TagName("li"), 100);
			IWebElement currentOption = options.FirstOrDefault(option => option.Text == "Абсолютная скидка на заказ");
			currentOption!.Click();
			
			IWebElement discount = modalDialog.FindElement(By.TagName("input"));
			discount.SendKeys("100");
			
			buttons = modalDialog.FindElements(By.TagName("button"));
			IWebElement saveButton = buttons.FirstOrDefault(button => button.Text == "Ок");
			saveButton!.Click();
		}
		
		[Given(@"Я сохраняю созданную акцию: '(.*)'")]
		public void Save_CreatedAction(String actionName)
		{
			ReadOnlyCollection<IWebElement> buttons = SeleniumDriver.FindElements(By.TagName("button"), 100);
			IWebElement saveButton = buttons.FirstOrDefault(button => button.Text == "Сохранить");
			saveButton!.Click();
			
			SeleniumDriver.WaitUntilElementAvailable(By.LinkText(actionName));
		}
		
		[Given(@"Я нажимаю на 'Гамбургер' напротив созданной акции")]
		public void ClickOn_CreatedActionHamburgerButton()
		{
			ReadOnlyCollection<IWebElement> actionsList = SeleniumDriver.FindElements(By.TagName("a"), 100);
			//var hamburgerButton = actionsList.FirstOrDefault(button => button.GetAttribute("href") == "http://localhost:51275/Promotion/BonusActions/Promocodes");
			var hamburgerButton = actionsList.FirstOrDefault(button => button.GetAttribute("href") == "https://dev.backoffice.buzzolls.ru/Promotion/BonusActions/Promocodes");
			hamburgerButton.Click();
		}
		
		[Given(@"Я удаляю созданную акцию: '(.*)'")]
		public void Delete_CreatedAction(String actionName)
		{
			SeleniumDriver.WaitUntilElementAvailable(By.LinkText(actionName));
			
			ReadOnlyCollection<IWebElement> deleteButtons = SeleniumDriver.FindElements(By.ClassName("fa-trash"), 100);
			deleteButtons[0].Click();
			
			SeleniumDriver.WaitUntilElementAvailable(By.ClassName("modal-content"));
			
			ReadOnlyCollection<IWebElement> pageButtons = SeleniumDriver.FindElements(By.TagName("button"), 100);
			IWebElement modalWindowDeleteButton = pageButtons.FirstOrDefault(modalWindowButton => modalWindowButton.Text == "Удалить");
			modalWindowDeleteButton.Click();
		}
		
		[Given(@"Я нажимаю на кнопку 'Добавить' на странице добавления промокодов")]
		public void ClickOn_AddPromoButton()
		{
			ReadOnlyCollection<IWebElement> addPromoPageButtons = SeleniumDriver.FindElements(By.TagName("a"), 100);
			IWebElement addPromoButton = addPromoPageButtons.FirstOrDefault(promoPageButton => promoPageButton.Text == "Добавить");
			addPromoButton.Click();
		}
		
		[Given(@"Я ввожу значение промокода: '1234'")]
		public void Input_PromoNumbers()
		{
			IWebElement inputPromoNumberField = SeleniumDriver.FindElement(By.Id("code"), 100);
			inputPromoNumberField.SendKeys("1234");
		}
		
		[Given(@"Я сохраняю изменения созданного промокода")]
		public void ClickOn_SavePromoButton()
		{
			ReadOnlyCollection<IWebElement> creatingPromoButtons = SeleniumDriver.FindElements(By.TagName("button"), 100);
			IWebElement savePromoButton = creatingPromoButtons.FirstOrDefault(promoPageButton => promoPageButton.Text == "Сохранить");
			savePromoButton.Click();
		}
		
		[Given(@"Я выбираю торговую точку '(.*)'")]
		public void Choose_TradeObject(String chosenTradeObject)
		{
			IWebElement tradeObjectArea = SeleniumDriver.FindElement(By.Id("tradeObjects"), 100);
			tradeObjectArea.Click();
			
			ReadOnlyCollection<IWebElement> tradeObjects = SeleniumDriver.FindElements(By.ClassName("select_list"), 100);
			IWebElement tradeObject = tradeObjects.FirstOrDefault(tradeObject => tradeObject.Text == chosenTradeObject);
			tradeObject!.Click();
		}
		
		[Given(@"Я нажимаю на кнопку 'Промо'")]
		public void ClickOn_Promo()
		{
			IWebElement tabElement = SeleniumDriver.FindElement(By.ClassName("tab"), 100);
			ReadOnlyCollection<IWebElement> tabElements =  tabElement.FindElements(By.TagName("div"));
			IWebElement currentTab = tabElements.FirstOrDefault(tab => tab.Text == "Промо");
			currentTab!.Click();
		}
		
		[Given(@"Я ввожу цифры промокода '1234' и нажимаю на кнопку 'ОК'")]
		public void Input_PromoNumbers_And_ClickOn_OkButton()
		{
			IWebElement promoLine = SeleniumDriver.FindElement(By.ClassName("promocode-wrapper"), 100);
			IWebElement inputPromoField = promoLine.FindElement(By.TagName("input"));
			inputPromoField.SendKeys("1234");
			
			IWebElement okButton = promoLine.FindElement(By.TagName("button"));
			okButton.Click();
		}
		
		[Given(@"Я нажимаю на кнопку 'Применить' в модальном окне промокода")]
		public void ClickOn_PromoModalWindowUseButton()
		{
			SeleniumDriver.WaitUntilElementAvailable(By.ClassName("modal-dialog"));
			
			IWebElement promoModalWindow = SeleniumDriver.FindElement(By.ClassName("modal-footer"), 100);
			ReadOnlyCollection<IWebElement> promoModalWindowButtons = promoModalWindow.FindElements(By.TagName("button"));
			IWebElement promoModalWindowUseButton = promoModalWindowButtons.FirstOrDefault(promoModalWindowButton => promoModalWindowButton.Text == "Применить");
			promoModalWindowUseButton.Click();
			
			SeleniumDriver.WaitUntilElementClose(By.ClassName("modal-dialog"));
		}
		
		[Given(@"Я вижу созданную акцию 'Авто-тест акция'")]
		public void CanSee_CreatedAction()
		{
			SeleniumDriver.WaitUntilElementAvailable(By.ClassName("bonus-actions"));
			
			IWebElement action =  SeleniumDriver.FindElement(By.ClassName("bonus-actions"), 100);
			ReadOnlyCollection<IWebElement> actionsList = action.FindElements(By.TagName("td"));
			IWebElement createdAction = actionsList.FirstOrDefault(action => action.Text == "Авто-тест акция");
			
			if (createdAction == null)
			{
				throw new Exception("Созданная Авто-тест акция не найдена");
			}
		}
		
		[Given(@"Я выбираю раздел '(.*)' в 'Меню'")]
		public void ClickOn_DeliverySection(String deliveryMenuType)
		{
			ReadOnlyCollection<IWebElement> changeMenuTypeButtons = SeleniumDriver.FindElements(By.Id("changeMenuTypeButtons"), 100);
			IWebElement deliveryMenuTypeButton = changeMenuTypeButtons.FirstOrDefault(changeMenuTypeButton => changeMenuTypeButton.Text == deliveryMenuType);
			deliveryMenuTypeButton!.Click();
			
			SeleniumDriver.WaitUntilElementClose(By.ClassName("block-ui-visible"));
		}

		[Given(@"Я нажимаю на категорию меню '(.*)'")]
		public void ClickOn_CategoryMenuGroup (String categoryMenuGroupName)
		{
			ReadOnlyCollection<IWebElement> categoryMenuGroups = SeleniumDriver.FindElements(By.ClassName("accordion-toggle"), 100);
			IWebElement rollsGroup = categoryMenuGroups.FirstOrDefault(categoryMenuGroup => categoryMenuGroup.Text == categoryMenuGroupName);

			if (rollsGroup == null)
			{
				throw new Exception($"Категория меню: {categoryMenuGroupName} не найдена!");
			}
			
			rollsGroup.Click();
		}

		[Given(@"Я удаляю продукт '(.*)' из меню")]
		public void Delete_ProductFromMenu(String productName)
		{
			IReadOnlyList<IWebElement> menuProducts = SeleniumDriver.FindElements(By.Name("menuProductLine"), 100);
			IWebElement menuProduct = menuProducts.FirstOrDefault(menuProduct => menuProduct.FindElement(By.TagName("span")).Text == productName);
			
			IWebElement button = menuProduct!.FindElement(By.Id("deleteMenuProductButton"));
			button.Click();
		}

		[Given(@"Я нажимаю на кнопку 'Сохранить' в разделе 'Меню'")]
		public void ClickOn_SaveMenuButton()
		{
			IWebElement saveMenuButton = SeleniumDriver.FindElement(By.Id("saveMenuButton"), 100);
			saveMenuButton.Click();
			
			SeleniumDriver.WaitUntilElementClose(By.ClassName("block-ui-visible"));
		}

		[Given(@"Я указываю текущую дату")]
		public void Input_CurrentDate()
		{
			IWebElement dateField = SeleniumDriver.FindElement(By.Id("selected_from_date"), 100);
			DateTime beginDate = DateTime.Today.Date;
			
			_action.DoubleClick(dateField).Perform();
			
			dateField.SendKeys(beginDate.ToString("dd.MM.yyyy"));
		}

		[Given(@"Я нажимаю на кнопку 'Получить данные'")]
		public void ClickOn_GetProblemOrdersDataButton()
		{
			IWebElement getProblemOrdersDataButton = SeleniumDriver.FindElement(By.Id("getProblemOrdersDataButton"), 100);
			getProblemOrdersDataButton.Click();
		}

		[Given(@"Я нажимаю на кнопку 'Редактировать' у проблемы с текстом: '(.*)'")]
		public void ClickOn_FirstEditButton(String problemText)
		{
			SeleniumDriver.WaitUntilElementAvailable(By.Name("problemOrderEditButton"));
			
			ReadOnlyCollection<IWebElement> problemLines = SeleniumDriver.FindElements(By.Name("orderProblemSolution"), 100);
			IWebElement currentProblem = problemLines.FirstOrDefault(currentProblem => currentProblem.Text
				.Contains(problemText));
			IWebElement editButton = currentProblem.FindElement(By.Name("problemOrderEditButton"));
			
			editButton.Click();
		}

		[Given(@"Я ввожу решение проблемы '(.*)'")]
		public void Input_ProblemSolution(String problemSolution)
		{
			IWebElement inputSolutionTextArea = SeleniumDriver.FindElement(By.Id("problemOrderSolutionModalTextArea"), 100);
			inputSolutionTextArea.SendKeys(problemSolution);
		}

		[Given(@"Я нажимаю на чек-бокс 'Проблема решена'")]
		public void ClickOn_SolveProblemCheckBox()
		{
			IWebElement solveProblemCheckBox = SeleniumDriver.FindElement(By.Id("solveProblemCheckBox"), 100);
			solveProblemCheckBox.Click();
		}

		[Given(@"Я нажимаю на кнопку 'Сохранить' в модальном окне проблемного заказа")]
		public void ClickOn_ModalProblemAreaSaveButton()
		{
			IWebElement modalProblemAreaSaveButton = SeleniumDriver.FindElement(By.Id("modalProblemAreaSaveButton"), 100);
			modalProblemAreaSaveButton.Click();
			SeleniumDriver.WaitUntilElementClose(By.ClassName("modal-content"));
		}

		[Given(@"Я вижу статус первой проблемы как 'Решенный' и второй проблемы как 'Нерешенный'")]
		public void CanSee_ProblemOrderStatuses()
		{
			ReadOnlyCollection<IWebElement> solvedProblemOrderStatus = SeleniumDriver.FindElements(By.Id("problemOrderSolvedStatus"), 100);
			String[] solvedProblemOrdersStatusTexts = solvedProblemOrderStatus.Select(solvedProblemOrderStatus => solvedProblemOrderStatus.Text).ToArray();

			if (solvedProblemOrdersStatusTexts.All(problemOrdersStatus => problemOrdersStatus != "Решенный"))
			{
				throw new Exception("Статус первой жалобы не найден!");
			}

			ReadOnlyCollection<IWebElement> unsolvedProblemOrderStatus = SeleniumDriver.FindElements(By.Id("problemOrderUnsolvedStatus"), 100);
			String[] unsolvedProblemOrdersStatusTexts = unsolvedProblemOrderStatus.Select(unsolvedProblemOrderStatus => unsolvedProblemOrderStatus.Text).ToArray();
			
			if (unsolvedProblemOrdersStatusTexts.All(unsolvedProblemOrdersStatus => unsolvedProblemOrdersStatus != "Нерешенный"))
			{
				throw new Exception("Статус второй жалобы не найден!");
			}
		}

		[Given(@"Я вижу тексты решения проблем: '(.*)', '(.*)'")]
		public void CanSee_ProblemOrderSolutionText(String firstProblemSolution, String secondProblemSolution)
		{
			ReadOnlyCollection<IWebElement> problemOrdersSolution = SeleniumDriver.FindElements(By.Id("problemOrderSolutionText"), 100);
			String[] problemOrdersSolutionTexts = problemOrdersSolution.Select(problemOrderSolution => problemOrderSolution.Text).ToArray();

			if (problemOrdersSolutionTexts.All(problemOrderSolutionText => problemOrderSolutionText != firstProblemSolution))
			{
				throw new Exception($"Решение первой проблемы не соответствует {firstProblemSolution}!");
			}

			if (problemOrdersSolutionTexts.All(problemOrderSolutionText => problemOrderSolutionText != secondProblemSolution))
			{
				throw new Exception($"Решение второй проблемы не соответствует {secondProblemSolution}!");
			}
		}

		[Given(@"Я нажимаю на кнопку выхода из учетной записи с помощью сайдбара")]
		public void ClickOn_TradeAreaAdministratorSignOutButton()
		{
			IWebElement sideBar = SeleniumDriver.FindElement(By.Id("sidebar"), 100);
			ReadOnlyCollection<IWebElement> sideBarButtons = sideBar.FindElements(By.TagName("a"));
			
			Actions action = new Actions(SeleniumDriver);
			action.MoveToElement(sideBar).Perform();
			
			IWebElement signOutButton = sideBarButtons.FirstOrDefault(sideBarButton => sideBarButton.Text == "Выход");
			signOutButton.Click();
		}

		[Given(@"Я нажимаю на кноку 'Добавить' в 'Меню'")]
		public void ClickOn_AddMenuItemButton()
		{
			IWebElement addMenuItemButton = SeleniumDriver.FindElement(By.Id("addMenuItemButton"), 100);
			addMenuItemButton.Click();
		}

		[Given(@"Я добавляю удаленный продукт '(.*)' в меню")]
		public void ClickOn_ChooseProductArea(String deletedProductName)
		{
			IWebElement chooseProductArea = SeleniumDriver.FindElement(By.ClassName("select2-selection"), 100);
			chooseProductArea.Click();
			
			IWebElement deletedProductsList = SeleniumDriver.FindElement(By.ClassName("select2-results__options"), 100);
			ReadOnlyCollection<IWebElement> deletedProducts = deletedProductsList.FindElements(By.TagName("li"));
			IWebElement deletedProduct = deletedProducts.FirstOrDefault(deletedProduct => deletedProduct.Text == deletedProductName);
			deletedProduct.Click();
			
			IWebElement submitChangesMenuModalButton = SeleniumDriver.FindElement(By.Id("submitChangesMenuModalButton"), 100);
			SeleniumDriver.WaitUntilElementClickable(submitChangesMenuModalButton);
			submitChangesMenuModalButton.Click();
			
			SeleniumDriver.WaitUntilElementClose(By.Id("editMenuItemForm"));
		}

		[Given("Я вижу тексты решения жалоб: '(.*)', '(.*)'")]
		public void CanSee_ProblemSolutionText(String firstProblemSolution, String secondProblemSolution)
		{
			ReadOnlyCollection<IWebElement> solutionProblemText = SeleniumDriver.FindElements(By.Id("solutionProblemTextArea"), 100);
			String[] problemOrdersSolutionTexts = solutionProblemText.Select(problemOrderSolution => problemOrderSolution.GetAttribute("value")).ToArray();
            
			if (problemOrdersSolutionTexts.All(problemOrderSolutionText => problemOrderSolutionText != firstProblemSolution))
			{
				throw new Exception($"Решение первой проблемы не соответствует {firstProblemSolution}!");
			}
            
			if (problemOrdersSolutionTexts.All(problemOrderSolutionText => problemOrderSolutionText != secondProblemSolution))
			{
				throw new Exception($"Решение второй проблемы не соответствует {secondProblemSolution}!");

			}
		}

		[Given("Я нажимаю на кнопку 'Редактировать' напротив ролла: '(.*)'")]
		public void ClickOn_EditProductButton(String productName)
		{
			ReadOnlyCollection<IWebElement> menuProductLines = SeleniumDriver.FindElements(By.Name("menuProductLine"), 100);
			IWebElement currentProductLine = menuProductLines.FirstOrDefault(productLine => productLine.Text.
				Contains(productName));
			
			IWebElement editProductButton = currentProductLine.FindElement(By.Name("editMenuProductButton"));
			editProductButton.Click();
		}

		[Given("Я нажимаю на чек-бокс 'Быстрый доступ'")]
		public void ClickOn_ProductQuickAccessCheckbox()
		{
			IWebElement productQuickAccessCheckbox = SeleniumDriver.FindElement(By.Id("productQuickAccessCheckbox"), 100);
			productQuickAccessCheckbox.Click();

			IWebElement submitChangesMenuModalButton = SeleniumDriver.FindElement(By.Id("submitChangesMenuModalButton"), 100);
			submitChangesMenuModalButton.Click();
		}
	}
}