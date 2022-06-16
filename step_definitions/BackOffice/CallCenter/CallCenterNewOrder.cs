using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using Buzzolls.SpecFlow.Tools;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V94.Network;
using OpenQA.Selenium.Interactions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Plugins;

namespace Buzzolls.SpecFlow.Tests.step_definitions.BackOffice.CallCenter
{
    [Binding]
    public class NewOrder : BasePage
    {
        private static IWebElement _orderButton;
        private static Actions _action = new Actions(SeleniumDriver);
        private static By _informMessageSelector = By.ClassName("inform-message");

        protected override void Initialize()
        {
            _orderButton = SeleniumDriver.FindElement(By.Id("orderButton"), 1000);
        }

        [Given(@"Я ввожу '(.*)' в поле 'Телефон'")]
        public void Input_PhoneNumber(String phoneNumber)
        {
            IWebElement phoneNumberInput = SeleniumDriver.FindElement(By.Id("phoneNumber"), 100);
            phoneNumberInput.SendKeys(phoneNumber);
        }

        [Given(@"Я ввожу в поле 'День рождения' сегодняшнее число")]
        public void Input_CurrentDateInBirthdayField()
        {
            IWebElement inputBirthdayDate = SeleniumDriver.FindElement(By.Id("pickerbirthday"), 100);
            DateTime beginDate = DateTime.Today.Date;
            
            inputBirthdayDate!.SendKeys(beginDate.ToString("dd.MM"));
        }

        [Given(@"Я ввожу '(.*)' в поле 'Имя'")]
        public void Input_СlientName(String clientName)
        {
            IWebElement сlientNameInput = SeleniumDriver.FindElement(By.Id("inputClientName"), 100);
            Thread.Sleep(1000);
            сlientNameInput.SendKeys(Keys.Control + "a");
            //сlientNameInput.SendKeys(Keys.Backspace);
            сlientNameInput.SendKeys(clientName);
        }

        [Given(@"Я вижу надпись 'Совпадений не найдено'")]
        public void CanSee_InscriptionNoMatchesFound()
        {
            IWebElement newAdressStreetResult = SeleniumDriver.FindElement(By.Id("select2-newAddressStreets-results"), 100);
            String newAdressStreetResultText = newAdressStreetResult.Text;
            
            if (newAdressStreetResultText != "Совпадений не найдено")
            {
                throw new Exception("Надпись 'Совпадений не найдено' не найдена!");
            }

            IWebElement newAddressStreetsContainer = SeleniumDriver.FindElement(By.Id("select2-newAddressStreets-container"), 100);
            newAddressStreetsContainer.Click();
        }

        [Given(@"Я ввожу данные в поле 'Заказ к'")]
        public void Input_DateTime()
        {
            IWebElement phoneNumber = SeleniumDriver.FindElement(By.Id("executionDate"), 100);
            
            DateTime dateTime = DateTime.Now + TimeSpan.FromHours(1);
            phoneNumber.SendKeys(dateTime.ToString("dd/MM/yyyy HH:mm"));

            ReadOnlyCollection<IWebElement> aboutClientPageInscriptions = SeleniumDriver.FindElements(By.ClassName("control-label"));
            IWebElement aboutClientPageInscription = aboutClientPageInscriptions.FirstOrDefault(inscription => inscription.Text == "Населенный пункт");
            aboutClientPageInscription.Click();
        }

        [Given(@"Я нажимаю на кнопку 'Самовывоз'")]
        public void ClickOn_PickupOption()
        {
            IWebElement pickupOption = SeleniumDriver.FindElement(By.Id("pickupOption"), 100);
            pickupOption.Click();
        }

        [Given("Я выбираю адрес точки самовывоза '(.*)'")]
        public void ClickOn_PickupTradeObject(String pickupTradeObject)
        {
            ReadOnlyCollection<IWebElement> pickupTradeObjects = SeleniumDriver.FindElements(By.Name("tradeObject"), 100);
            IWebElement tradeObject = pickupTradeObjects.FirstOrDefault(tradeObject => tradeObject.Text == pickupTradeObject);
            tradeObject.Click();
        }

        [Given("Я нажимаю на кнопку 'Заказ'")]
        public void ClickOn_OrderButton()
        {
            Initialize();
            SeleniumDriver.WaitUntilElementAvailable(By.Id("orderButton"));
            _orderButton.Click();
        }

        [Given("Я добавляю в корзину продукт '(.*)'")]
        public void ClickOn_AddToCartButton(String productName)
        {
            ReadOnlyCollection<IWebElement> productBoxes = SeleniumDriver.FindElements(By.Name("productBox"), 100);

            String[] products = productBoxes.Select(product => product.FindElement(By.Name("productName")).Text).ToArray();
            IWebElement addToCart = productBoxes.FirstOrDefault(product => product.FindElement(By.Name("productName")).Text.Contains(productName));
            IWebElement addToCartButton = addToCart.FindElement(By.TagName("button"));
            addToCartButton.Click();
        }

        [Given("Я не вижу продукт '(.*)'")]
        public void CantSee_Product(String productName)
        {
            ReadOnlyCollection<IWebElement> productBoxes = SeleniumDriver.FindElements(By.Name("productBox"), 100);
            IWebElement addToCart = productBoxes.FirstOrDefault(productBox => productBox.FindElement(By.Name("productName")).Text.Contains(productName));
            
            if (addToCart != null)
            {
                throw new Exception($"Продукт {productName} найден!");
            }
        }

        [Given("Я оформляю заказ")]
        public void ClickOn_AcceptTheOrderButton()
        {
            IWebElement acceptTheOrderButton = SeleniumDriver.FindElement(By.Id("acceptTheOrder"), 100);
            SeleniumDriver.WaitUntilElementAvailable(By.Id("acceptTheOrder"));
            acceptTheOrderButton.Click();
        }

        [Given("Я нажимаю на кнопку закрытия страницы оформления заказа")]
        public void ClickOn_CancelOrderButton()
        {
            IWebElement cancelOrderButton = SeleniumDriver.FindElement(By.Id("cancelOrderButton"), 100);
            cancelOrderButton.Click();
        }

        [Given("Я не могу оформить заказ")]
        public void CantClickOn_AcceptTheOrderButton()
        {
            IWebElement acceptTheOrderButton = SeleniumDriver.FindElement(By.Id("acceptTheOrder"), 100);
            acceptTheOrderButton?.Click();
        }

        [Given("Я выбираю тип оплаты 'Картой'")]
        public void Pick_PayWithCard()
        {
            IWebElement pickPayWithCardButton = SeleniumDriver.FindElement(By.Id("payWithCardButton"), 100);
            pickPayWithCardButton.Click();
        }

        [Given("Я нажимаю на кнопку 'Копировать' напротив адреса клиента '(.*)'")]
        public void ClickOn_CopyClientDeliveryAddressButton(String clientAddresses)
        {
            SeleniumDriver.WaitUntilElementAvailable(By.ClassName("address-table"));
            
            ReadOnlyCollection<IWebElement> addressesDivs = SeleniumDriver.FindElements(By.ClassName("address-row"), 100);
            IWebElement currentAddressDiv = addressesDivs.FirstOrDefault(cutAddressName => cutAddressName.Text.Replace("\r\nКопировать Скрыть", "") == clientAddresses);
            
            IWebElement copyClientDeliveryAddressButton = currentAddressDiv.FindElement(By.Id("copyClientDeliveryAddressButton"));
            copyClientDeliveryAddressButton.Click();
        }

        [Given("Я нажимаю на кнопку 'Новый адрес'")]
        public void ClickOn_NewAddressButton()
        {
            IWebElement isNewAddress = SeleniumDriver.FindElement(By.Id("isNewAddress"), 100);
            isNewAddress.Click();
        }

        [Given("Я ввожу '(.*)' в поле 'Улица'")]
        public void Input_NewAddressStreet(String streetName)
        {
            IWebElement newAddressStreets = SeleniumDriver.FindElement(By.Id("select2-newAddressStreets-container"), 100);
            newAddressStreets.Click();
            
            IWebElement inputField = SeleniumDriver.FindElement(By.ClassName("select2-search__field"), 100);
            inputField.SendKeys(streetName);
            inputField.SendKeys(Keys.Enter);
        }

        [Given("Я ввожу '(.*)' в поле 'Дом'")]
        public void Input_NewAddressHouse(String houseNumber)
        {
            IWebElement newAddressHouse = SeleniumDriver.FindElement(By.Id("newAddressHouse"), 100);
            
            newAddressHouse.SendKeys(Keys.Control + "a");
            newAddressHouse.SendKeys(houseNumber);
        }

        [Given("Я ввожу в поле комментария для курьера '(.*)'")]
        public void Input_CommentForCourier(String commentForCourier)
        {
            IWebElement inputCommentForCourier = SeleniumDriver.FindElement(By.Id("inputCommentForCourierTextArea"), 100);
            
            SeleniumDriver.WaitUntilElementAvailable(By.Id("inputCommentForCourierTextArea"));
            
            inputCommentForCourier.SendKeys(Keys.Control + "a");
            inputCommentForCourier.SendKeys(commentForCourier);
        }

        [Given("Я вижу в детализации заказа информацию в поле комментарий: '(.*)'")]
        public void CanSee_CommentForCourier(String commentForCourier)
        {
            IWebElement commentForCourierElement = SeleniumDriver.FindElement(By.Id("addressDescription"), 100);
            String commentForCourierElementText = commentForCourierElement.Text;
            
            if (commentForCourierElementText != commentForCourier)
            {
                throw new Exception($"Комментарий для курьера не соответствует {commentForCourier}!");
            }

        }

        [Given("Я вижу название торговой точки '(.*)' в окне детальной информации")]
        public void CanSee_TradeObjectName_InDetailInfo(String tradeObjectName)
        {
            SeleniumDriver.WaitUntilElementAvailable(By.Id("currentTradeObjectInfo"));
            
            IWebElement currentTradeObjectName = SeleniumDriver.FindElement(By.Id("currentTradeObjectName"), 100);
            String currentTradeObjectNameText = currentTradeObjectName.Text;
            
            if (currentTradeObjectNameText != tradeObjectName)
            {
                throw new Exception($"Название торговой точки не соответствует {tradeObjectName}!");
            }
        }

        [Given("Я добавляю один из готовых комментариев в поле комментария для курьера '(.*)'")]
        public void Pick_CommentForCourier(String pickCommentForCourier)
        {
            SeleniumDriver.WaitUntilElementAvailable(By.Id("inputCommentForCourierTextArea"));
            
            IWebElement pickCommentForCourierButton = SeleniumDriver.FindElement(By.Id("commentForCourier0"), 100);
            pickCommentForCourierButton.Click();
        }

        [Given("Я вижу адрес торговой точки '(.*)' в окне детальной информации")]
        public void CanSee_TradeObjectAdress_InDetailInfo(String tradeObjectAdress)
        {
            IWebElement currentTradeObjectAdress = SeleniumDriver.FindElement(By.Id("currentTradeObjectAdress"), 100);
            String currentTradeAdressNameText = currentTradeObjectAdress.Text;
            
            if (currentTradeAdressNameText != tradeObjectAdress)
            {
                throw new Exception($"Название торговой точки не соответствует {tradeObjectAdress}!");
            }
        }

        [Given("Я вижу время работы доставки торговой точки '(.*)' в окне детальной информации")]
        public void CanSee_TradeObjectDeliveryWorkTime(String tradeObjectDeliveryWorkTime)
        {
            ReadOnlyCollection<IWebElement> currentTradeObjectWorkTime = SeleniumDriver.FindElements(By.Name("currentTradeObjectWorkTime"), 100);
            IWebElement currentTradeObjectDeliveryWorkTime = currentTradeObjectWorkTime.FirstOrDefault(currentTradeObjectWorkTime => currentTradeObjectWorkTime.Text == tradeObjectDeliveryWorkTime);
            String currentTradeDeliveryWorkTimeText = currentTradeObjectDeliveryWorkTime.Text;
            
            if (currentTradeDeliveryWorkTimeText != tradeObjectDeliveryWorkTime)
            {
                throw new Exception(
                    $"Время осуществления доставки торговой точки не соответствует {tradeObjectDeliveryWorkTime}!");
            }
        }

        [Given("Я вижу время работы самовывоза '(.*)' в окне детальной информации")]
        public void CanSee_TradeObjectPickupWorkTime(String tradeObjectPickupWorkTime)
        {
            ReadOnlyCollection<IWebElement> currentTradeObjectWorkTime = SeleniumDriver.FindElements(By.Name("currentTradeObjectWorkTime"), 100);
            IWebElement currentTradeObjectPickupWorkTime = currentTradeObjectWorkTime.FirstOrDefault(currentTradeObjectWorkTime => currentTradeObjectWorkTime.Text == tradeObjectPickupWorkTime);
            String currentTradePickupWorkTimeText = currentTradeObjectPickupWorkTime.Text;
            
            if (currentTradePickupWorkTimeText != tradeObjectPickupWorkTime)
            {
                throw new Exception(
                    $"Время осуществления самовывоза торговой точки не соответствует{tradeObjectPickupWorkTime}!");
            }
        }

        [Given("Я вижу минимальную цену заказа '(.*)' в окне детальной информации")]
        public void CanSee_TradeObjectMinOrderPrice(String tradeObjectMinOrderPrice)
        {
            IWebElement currentTradeObjectMinOrderPrice = SeleniumDriver.FindElement(By.Id("currentTradeMinOrderPrice"), 100);
            String currentTradeMinOrderPriceText = currentTradeObjectMinOrderPrice.Text;
            
            if (currentTradeMinOrderPriceText != tradeObjectMinOrderPrice)
            {
                throw new Exception(
                    $"Минимальная цена заказа торговой точки не соответствует {tradeObjectMinOrderPrice}!");
            }
        }

        [Given("Я вижу стоимость доставки '(.*)' в окне детальной информации")]
        public void CanSee_TradeObjectDeliveryPrice(String tradeObjectDeliveryPrice)
        {
            IWebElement currentTradeObjectDeliveryPrice = SeleniumDriver.FindElement(By.Id("currentTradeDeliveryPrice"), 100);
            String currentTradeDeliveryPriceText = currentTradeObjectDeliveryPrice.Text;
            
            if (currentTradeDeliveryPriceText != tradeObjectDeliveryPrice)
            {
                throw new Exception($"Стоимость доставки торговой точки не соответствует {tradeObjectDeliveryPrice}!");
            }
        }

        [Given("Я вижу в поле улица: '(.*)'")]
        public void CanSee_CopiedClientAddress(String copiedClientHouseNumber)
        {
            IWebElement copiedClientStreet = SeleniumDriver.FindElement(By.Id("select2-newAddressStreets-container"), 100);
            String copiedClientStreetText = copiedClientStreet.Text;
            
            if (copiedClientStreetText != copiedClientHouseNumber)
            {
                throw new Exception(
                    $"Название улицы скопированного адреса не соответствует {copiedClientHouseNumber}!");
            }
        }

        [Given("Я вижу в поле дом: '(.*)'")]
        public void CanSee_CopiedClientHouseNumber(String copiedClientHouseNumber)
        {
            IWebElement copiedClientClientHouseNumber = SeleniumDriver.FindElement(By.Id("newAddressHouse"), 100);
            String copiedClientHouseNumberText = copiedClientClientHouseNumber.GetAttribute("value");
            
            if (copiedClientHouseNumberText != copiedClientHouseNumber)
            {
                throw new Exception($"Номер дома скопированного адреса не соответствует {copiedClientHouseNumber}!");
            }
        }

        [Given("Я ввожу сумму '(.*)' в поле 'У клиента'")]
        public void Input_ClientCashSum(String clientCashSum)
        {
            IWebElement inputClientCashSum = SeleniumDriver.FindElement(By.Id("inputClientCashSum"), 100);
            
            inputClientCashSum.SendKeys(Keys.Control + "a");
            
            inputClientCashSum.SendKeys(clientCashSum);
        }

        [Given("Я нажимаю на кнопку 'История заказов'")]
        public void ClickOn_OrderHistory()
        {
            IWebElement orderHistoryTab = SeleniumDriver.FindElement(By.Id("orderHistory"), 100);
            orderHistoryTab.Click();
        }

        [Given("Я вижу созданный заказ в 'Истории заказов' клиента с составом: '(.*)' и стоимостью заказа: '(.*)'")]
        public void CanSee_CreatedOrderInOrderHistory(String itemName, String orderPrice)
        {
            ReadOnlyCollection<IWebElement> oldOrderPrice = SeleniumDriver.FindElements(By.Name("orderPrice"), 100);
            String oldOrderPriceText = oldOrderPrice[0].Text;
            
            if (oldOrderPriceText != orderPrice)
            {
                throw new Exception("Цена заказа не совпадает");
            }

            ReadOnlyCollection<IWebElement> orderCompoundElement = SeleniumDriver.FindElements(By.Name("orderCompound"), 100);
            orderCompoundElement[0].Click();
            
            SeleniumDriver.WaitUntilElementAvailable(By.ClassName("popover-inner"));

            ReadOnlyCollection<IWebElement> orderCompound = SeleniumDriver.FindElements(By.Id("orderCompoundItems"), 100);
            
            List<String> compoundList = new List<String>()
            {
                {itemName}
            };
            
            List<String> orderCompoundString = new List<String>();
           
            foreach (var compound in orderCompound)
            {
                orderCompoundString.Add(compound.Text);
            }
            
            Boolean compoundEquals = compoundList.SequenceEqual(orderCompoundString);
            
            if (!compoundEquals)
            {
                throw new Exception($"Состав не соответствует {itemName}");
            }
        }
        
        [Given("Я нажимаю на кнопку 'Скрыть' напротив адреса '(.*)'")]
        public void ClickOn_GideClientDeliveryAddressButton(string addressName)
        {
            SeleniumDriver.WaitUntilElementAvailable(By.ClassName("address-table"));
            
            ReadOnlyCollection<IWebElement> addressesDivs = SeleniumDriver.FindElements(By.ClassName("address-row"), 100);
            IWebElement currentAddressDiv = addressesDivs.FirstOrDefault(cutAddressName => cutAddressName.Text.Replace("\r\nКопировать Скрыть", "") == addressName);
            IWebElement addressHideButton = currentAddressDiv?.FindElement(By.Id("hideClientDeliveryAddressButton"));
            
            addressHideButton?.Click();
        }
        
        [Given("Я нажимаю на кнопку 'Скрыть' в модальном окне")]
        public void ClickOn_HideClientDeliveryAddressModalButton()
        {
            IWebElement hideClientDeliveryAddressModalButton = SeleniumDriver.FindElement(By.Id("hideClientDeliveryAddressModalButton"), 100);
            hideClientDeliveryAddressModalButton.Click();
            
            SeleniumDriver.WaitUntilElementClose(By.Id("hideClientDeliveryAddressModalButton"));
        }
        
        [Given("Я не вижу скрытого адреса '(.*)'")]
        public void CantSee_HiddenClientAddress(String hiddenAddress)
        {
            SeleniumDriver.WaitUntilElementAvailable(By.ClassName("address-table"));
            
            ReadOnlyCollection<IWebElement> addressesDivs = SeleniumDriver.FindElements(By.ClassName("address-row"), 100);
            IWebElement currentAddressDiv = addressesDivs.FirstOrDefault(cutAddressName => cutAddressName.Text.Replace("\r\nКопировать Скрыть", "") == hiddenAddress);

            if (currentAddressDiv != null)
            { 
                throw new Exception($"Скрытый адрес {hiddenAddress} найден!");
            }
        }
        
        [Given("Я ввожу данные '(.*)' в поле 'Подъезд'")]
        public void Input_EntranceNumberArea(String entranceNumber)
        {
            IWebElement inputEntranceNumberArea = SeleniumDriver.FindElement(By.Id("inputEntranceNumberArea"), 100);
            SeleniumDriver.WaitUntilElementAvailable(By.Id("inputEntranceNumberArea"));
            
            Actions _action = new Actions(SeleniumDriver);
            _action.DoubleClick(inputEntranceNumberArea).Perform();
            inputEntranceNumberArea.SendKeys(entranceNumber);
        }
        
        [Given("Я ввожу данные '(.*)' в поле 'Этаж'")]
        public void Input_FloorNumberArea(String floorNumber)
        {
            IWebElement inputFloorNumberArea = SeleniumDriver.FindElement(By.Id("inputFloorNumberArea"), 100);
            
            Actions _action = new Actions(SeleniumDriver);
            _action.DoubleClick(inputFloorNumberArea).Perform();
            inputFloorNumberArea.SendKeys(floorNumber);
        }
        
        [Given("Я ввожу данные '(.*)' в поле 'Домофон/код'")]
        public void Input_IntercomCodeNumberArea(String intercomCodeNumber)
        {
            IWebElement inputIntercomCodeNumberArea = SeleniumDriver.FindElement(By.Id("inputIntercomCodeNumberArea"), 100);
            
            Actions _action = new Actions(SeleniumDriver);
            _action.DoubleClick(inputIntercomCodeNumberArea).Perform();
            inputIntercomCodeNumberArea.SendKeys(intercomCodeNumber);
        }
        
        [Given("Я ввожу данные '(.*)' в поле 'Квартира/офис'")]
        public void Input_NewFlatNumber(String flatNumber)
        {
            IWebElement inputNewFlatNumber = SeleniumDriver.FindElement(By.Id("inputNewFlatNumber"), 100);
            SeleniumDriver.WaitUntilElementAvailable(By.Id("inputNewFlatNumber"));
            
            Actions _action = new Actions(SeleniumDriver);
            _action.DoubleClick(inputNewFlatNumber).Perform();
            inputNewFlatNumber.SendKeys(flatNumber);
        }
        
        [Given("Я ввожу данные '(.*)' в поле 'Строение'")]
        public void Input_NewBuildingNumber(String buildingNumber)
        {
            IWebElement newBuildingNumber = SeleniumDriver.FindElement(By.Id("inputNewBuildingNumber"), 100);
            
            Actions _action = new Actions(SeleniumDriver);
            _action.DoubleClick(newBuildingNumber).Perform();
            newBuildingNumber.SendKeys(buildingNumber);
        }
        
        [Given("Я ввожу данные '(.*)' в поле 'Корпус'")]
        public void Input_NewHousingNumber(String housingNumber)
        {
            IWebElement newHousingNumber = SeleniumDriver.FindElement(By.Id("inputNewHousingNumber"), 100);
            
            Actions _action = new Actions(SeleniumDriver);
            _action.DoubleClick(newHousingNumber).Perform();
            newHousingNumber.SendKeys(housingNumber);
        }
        
        [Given("Я вижу адрес '(.*)'")]
        public void CanSee_ClientAddress(String clientAddress)
        {
            SeleniumDriver.WaitUntilElementAvailable(By.ClassName("address-table"));
            
            ReadOnlyCollection<IWebElement> addressesDivs = SeleniumDriver.FindElements(By.ClassName("address-row"), 100);
            IWebElement currentAddressDiv = addressesDivs.FirstOrDefault(cutAddressName => cutAddressName.Text.Replace("\r\nКопировать Скрыть", "") == clientAddress);
            
            if (currentAddressDiv == null)
            { 
                throw new Exception($"Отредактированный адрес {clientAddress} найден!");
            }
        }
        
        [Given("Я ввожу название продукта в поле поиска продуктов: '(.*)'")]
        public void Input_ProductNameInSearchingField(String productName)
        {
            IWebElement searchingProductField = SeleniumDriver.FindElement(By.Id("productSearchingField"), 100);
            
            searchingProductField.SendKeys(productName);
        }
        
        [Given("Я вижу найденный продукт: '(.*)'")]
        public void CanSee_FoundProduct(String productName)
        {
            ReadOnlyCollection<IWebElement> productBoxes = SeleniumDriver.FindElements(By.Name("productBox"), 100);
            
            IWebElement addToCart = productBoxes.FirstOrDefault(product => product.FindElement(By.Name("productName")).Text.Contains(productName));

            if (addToCart == null)
            {
                throw new Exception($"Продукт '{productName}' не найден!");
            }
        }
        
        [Given("Я нажимаю на кнопку информации о продукте: '(.*)'")]
        public void ClickOn_ProductInfoButton(String productName)
        {
            ReadOnlyCollection<IWebElement> productBoxes = SeleniumDriver.FindElements(By.Name("productBox"), 100);
            IWebElement productBox = productBoxes.FirstOrDefault(product => product.Text.Contains(productName));
            IWebElement productInfoButton = productBox.FindElement(By.Name("productInfoButton"));
            productInfoButton.Click();
        }
        
        [Given("Я нажимаю на категорию меню: '(.*)'")]
        public void ClickOn_CategoryGroup(String categoryName)
        {
            ReadOnlyCollection<IWebElement> categoryGroups = SeleniumDriver.FindElements(By.Name("menuCategoriesButtons"), 100);
            IWebElement categoryGroup = categoryGroups.FirstOrDefault(categoryGroup => categoryGroup.Text.Contains(categoryName));
            
            categoryGroup.Click();
        }
        
        [Given("Я выбираю группу продуктов: '(.*)'")]
        public void ClickOn_GroupOfProducts(String groupName)
        {
            ReadOnlyCollection<IWebElement> selectionClassNames = SeleniumDriver.FindElements(By.ClassName("select2-selection"), 100);
            IWebElement selectGroupOfProduct = selectionClassNames.FirstOrDefault(className=>className.Text.Contains("Выберите группу"));
            selectGroupOfProduct.Click();

            ReadOnlyCollection<IWebElement> groupOfProductSelect = SeleniumDriver.FindElements(By.TagName("option"), 100);
            IWebElement groupOfProduct = groupOfProductSelect.FirstOrDefault(groupOfProduct=>groupOfProduct.Text.Contains(groupName));
            groupOfProduct.Click();
        }
        
        [Given("Я ставлю фильтр: '(.*)'")]
        public void Choose_Filter(String filterName)
        {
            SeleniumDriver.WaitUntilElementAvailable(By.Name("productBox"));
            
            IWebElement filterModalWindowButton = SeleniumDriver.FindElement(By.Id("openFilterModalWindowButton"), 100);
            filterModalWindowButton.Click();
            
            ReadOnlyCollection<IWebElement> filtersLine = SeleniumDriver.FindElements(By.ClassName("form-group"), 100);
            IWebElement currentFilter = filtersLine.FirstOrDefault(filter => filter.Text.Contains(filterName));
            IWebElement filterSwitcher = currentFilter.FindElement(By.ClassName("material-switch"));
            filterSwitcher.Click();

            IWebElement applyFiltersButton = SeleniumDriver.FindElement(By.Id("applyFiltersButton"), 100);
            applyFiltersButton.Click();
            
            SeleniumDriver.WaitUntilElementClose(By.ClassName("modal-content"));
        }
        
        [Given("Я вижу описание сета: '(.*)'")]
        public void CanSee_SetDescription(String partOfSetDescription)
        {
            ReadOnlyCollection<IWebElement> pageDivs = SeleniumDriver.FindElements(By.TagName("div"), 100);
            IWebElement setDescription = pageDivs.FirstOrDefault(div=>div.Text.Contains(partOfSetDescription));

            if (setDescription == null)
            {
                throw new Exception($"Описание сета с таким текстом: '{partOfSetDescription}' не найдено!");
            }
        }
        
        [Given("Я вижу в составе сета: '(.*)'")]
        public void CanSee_ProductComposition(String productComposition)
        {
            ReadOnlyCollection<IWebElement> pageLabels = SeleniumDriver.FindElements(By.TagName("label"), 100);
            IWebElement productCompositionButton = pageLabels.FirstOrDefault(button => button.Text.Contains("Состав"));
            productCompositionButton.Click();

            ReadOnlyCollection<IWebElement> pageLies = SeleniumDriver.FindElements(By.TagName("li"), 100);
            IWebElement setComposition = pageLies.FirstOrDefault(li=>li.Text.Contains(productComposition));

            if (setComposition == null)
            {
                throw new Exception($"Состав сета с таким текстом: '{productComposition}' не найден!");
            }
        }
        
        [Given("Я активирую чек-бокс 'NFC оплата'")]
        public void Activate_NfcPaymentCheckBox()
        {
            SeleniumDriver.WaitUntilElementAvailable(By.Id("nfcPaymentCheckBox"));
            
            IWebElement nfcPaymentCheckBox = SeleniumDriver.FindElement(By.Id("nfcPaymentCheckBox"), 100);
            nfcPaymentCheckBox.Click();
        }
        
        [Given("Я не вижу чек-бокса с NFC")]
        public void CantSee_NfcPaymentCheckBox()
        {
            IWebElement nfcPaymentCheckBox = SeleniumDriver.TryFindElement(By.Id("nfcPaymentCheckBox"));
            
            if (nfcPaymentCheckBox != null)
            {
                throw new Exception("Чек-бокс с указанием наличия NFC у клиента найден!");
            }
        }
		
        [Given(@"Я вижу всплывающее сообщение о стопе торговой точки на предзаказ")]
        public void CanSee_alertAboutTradeObjectPreOrdersStop()
        {
            SeleniumDriver.WaitUntilElementAvailable(_informMessageSelector);
            
            IWebElement alertAboutTradeObjectStop = SeleniumDriver.FindElement(_informMessageSelector, 100);
            IWebElement alertAboutTradeObjectStopArea = alertAboutTradeObjectStop.FindElement(By.ClassName("ng-binding"));
			
            String alertAboutTradeObjectStopText = alertAboutTradeObjectStopArea.Text;
            String message = $" до {DateTime.Now.AddDays(1):dd.MM.yyyy} 12:00. Пожалуйста, укажите другое время или снимите галочку \"Заказ ко времени\"";

            if (!alertAboutTradeObjectStopText.Contains(message))
            {
                throw new Exception("Сообщение о стопе точки не найдено или не соответствует данным о стопе");
            }
        }
		
        [Given(@"Я вижу всплывающее сообщение о стопе торговой точки")]
        public void CanSee_alertAboutTradeObjectStop()
        {
            SeleniumDriver.WaitUntilElementAvailable(_informMessageSelector);
            
            IWebElement alertAboutTradeObjectStop = SeleniumDriver.FindElement(_informMessageSelector, 100);
            IWebElement alertAboutTradeObjectStopArea = alertAboutTradeObjectStop.FindElement(By.ClassName("ng-binding"));
			
            String alertAboutTradeObjectStopText = alertAboutTradeObjectStopArea.Text;
            String message = $" до {DateTime.Now.AddDays(1):dd.MM.yyyy} 12:00. Попробуйте оформить заказ ко времени";

            if (!alertAboutTradeObjectStopText.Contains(message))
            {
                throw new Exception("Сообщение о стопе точки не найдено или не соответствует данным о стопе");
            }
        }
		
        [Given(@"Я вижу всплывающее сообщение о стопе продукта: '(.*)'")]
        public void CanSee_alertAboutProductStop(String productName)
        {
            SeleniumDriver.WaitUntilElementAvailable(_informMessageSelector);
            
            IWebElement alertAboutTradeObjectStop = SeleniumDriver.FindElement(_informMessageSelector, 100);
            IWebElement alertAboutTradeObjectStopArea = alertAboutTradeObjectStop.FindElement(By.ClassName("ng-binding"));
			
            String alertAboutTradeObjectStopText = alertAboutTradeObjectStopArea.Text;
            String message = $"Продукт '{productName}' в стоп-листе. Его продажи остановлены";

            if (!alertAboutTradeObjectStopText.Contains(message))
            {
                throw new Exception($"Сообщение о стопе продукта {productName} не найдено!");
            }
        }
		
        [Given(@"Я добавляю в корзину продукт из быстрого доступа")]
        public void Add_QuickAccessProductToCart()
        {
            IWebElement quickAccessProductToCartButton = SeleniumDriver.FindElement(By.Id("quickAccessProductToCartButton"), 100);
            quickAccessProductToCartButton.Click();
        }
        
        //TODO: добавить id элементу, чтобы упростить поиск элемента
        [Given(@"Я выбираю город '(.*)'")]
        public void Choose_City(String city)
        {
            ReadOnlyCollection<IWebElement> pageElemets = SeleniumDriver.FindElements(By.ClassName("selection"), 100);
            IWebElement chooseCityBar = pageElemets.FirstOrDefault(element => element.Text.Contains("г. Орел"));
            chooseCityBar.Click();
            
            ReadOnlyCollection<IWebElement> cities = SeleniumDriver.FindElements(By.TagName("option"), 100);
            IWebElement necessaryСity = cities.FirstOrDefault(cityInList => cityInList.Text.Contains(city));
            necessaryСity.Click();
        }
        
    }
}