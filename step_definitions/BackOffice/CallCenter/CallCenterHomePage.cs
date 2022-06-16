using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TechTalk.SpecFlow;
using Buzzolls.SpecFlow.Tools;

namespace Buzzolls.SpecFlow.Tests.step_definitions.BackOffice.CallCenter
{
    [Binding]
    public class HomePage : BasePage
    {
        private static IWebElement _searchingField;
        private static IWebElement _newOrderButton;
        private static String _clientPhone;
        private static String _clientNumber;
        private static String _orderNumberText;
        private static ReadOnlyCollection<IWebElement> _productNames;
        private static ReadOnlyCollection<IWebElement> _productQuantity;
        private static ReadOnlyCollection<IWebElement> _orderNumbers;
        private static Actions _action = new Actions(SeleniumDriver);

        protected override void Initialize()
        {
            _searchingField = SeleniumDriver.FindElement(By.Id("searchingField"), 100);
            _newOrderButton = SeleniumDriver.FindElement(By.Id("newOrderButton"), 100);
            _orderNumbers = SeleniumDriver.FindElements(By.Name("orderNumber"), 100);
        }

       //[Given(@"Я ввожу номер заказа в поле 'Поиск'")]
       //public void Input_LoginAndPasword_ForCentralAdmin()
       //{
       //    ReadOnlyCollection<IWebElement> orderNumbers = SeleniumDriver.FindElements(By.Id("orderNumber"));
       //    IWebElement orderNumber = orderNumbers[0];
       //    _searchingField.SendKeys(orderNumber.Text);
       //}

        [Given(@"Я ввожу номер заказа в поле 'Поиск' и вижу его")]
        public void Input_OrderNumber_AndCanSee_OrderEnteredNumber()
        {
            SeleniumDriver.WaitUntilElementAvailable(By.Name("order"));
            
            ReadOnlyCollection<IWebElement> ordersNumbers = SeleniumDriver.FindElements(By.Name("orderNumber"),100);
            
            IWebElement searchingField = SeleniumDriver.FindElement(By.Id("searchingField"), 100);
            
            String lastOrderNumberInfo = ordersNumbers[0].Text;
            String lastOrderNumber = lastOrderNumberInfo.Replace("№", "");
            
            searchingField.SendKeys(lastOrderNumber);
            
            IWebElement foundOrderNumber = SeleniumDriver.FindElement(By.Name("orderNumber"), 100);
            
            if (foundOrderNumber.Text != lastOrderNumberInfo)
            {
                throw new Exception("Созданный заказ найден!");
            }
        }

        [Given(@"Я нажимаю на кнопку 'Принять заказ'")]
        public void ClickOn_NewOrderButton()
        {
            Initialize();
            _newOrderButton.Click();
        }

        [Given(@"Я ввожу '(.*)' в поле 'Поиск'")]
        public void Input_FiveNumbersClientPhone(String firstFiveNumbers)
        {
            _searchingField = SeleniumDriver.FindElement(By.Id("searchingField"), 100);
            SeleniumDriver.WaitUntilElementAvailable(By.Id("searchingField"));

            _searchingField.InputCharacters(firstFiveNumbers);
        }

        [Given(@"Я вижу заказ с введенным номером телефона")]
        public void CanSee_ClientPhone()
        {
            ReadOnlyCollection<IWebElement> clientPhones = SeleniumDriver.FindElements(By.Name("clientPhone"));
            IWebElement clientPhone = clientPhones.FirstOrDefault(clientPhone => clientPhone.Text == "+79999999999");
            
            if (clientPhone == null)
            {
                throw new Exception("Заказ с номером '+79999999999' не найден!");
            }
        }

        [Given(@"Я нажимаю кнопку 'Заказы кухня'")]
        public void ClickOn_AboutKitchenOrdersButton()
        {
            IWebElement aboutKitchenOrdersButton = SeleniumDriver.FindElement(By.Id("aboutKitchenOrders"), 100);
            aboutKitchenOrdersButton.Click();
        }
        
        [Given(@"Я вижу открытое модальное окно 'Заказы кухня'")]
        public void CanSee_OrderStatisticModal()
        {
            IWebElement orderStatisticModal = SeleniumDriver.FindElement(By.Id("orderStatisticModal"), 100);
            
            if (orderStatisticModal == null)
            {
                throw new Exception("Модальное окно не найдено!");
            }
        }
        
        [Given(@"Я вижу информацию первой торговой точки")]
        public void CanSee_OrderStatistic()
        {
            IWebElement orderStatistic = SeleniumDriver.FindElement(By.Id("orderStatistic"), 100);
            ReadOnlyCollection<IWebElement> infoFields = orderStatistic.FindElements(By.TagName("td"));
            
            for (int x = 0; x < 4; x++)
            {
                if (infoFields[x].Text == null)
                {
                    throw new Exception("Информация не найдена!");
                }
            }
        }
        
        [Given(@"Я нажимаю кнопку 'Средний чек'")]
        public void ClickOn_AverageChequeButton()
        {
            SeleniumDriver.WaitUntilElementAvailable(By.Id("averageCheque"));
            
            IWebElement averageChequeButton = SeleniumDriver.FindElement(By.Id("averageCheque"), 100);
            averageChequeButton.Click();
        }
        
        [Given(@"Я закрываю сообщение об '(.*)'")]
        public void ClickOn_CloseAlertAboutSuccessOrdering(String alertMessageInfo)
        {
            SeleniumDriver.WaitUntilElementAvailable(By.Name("order"));

            By informMessageWrapClassLocator = By.ClassName("inform-message-wrap");
            IWebElement alertAboutSuccessOrdering = SeleniumDriver.FindElement(By.ClassName("inform"), 100);
            IWebElement informMessageWrapClass = alertAboutSuccessOrdering.FindElement(informMessageWrapClassLocator);
            IWebElement informMessageClass = informMessageWrapClass.FindElement(By.ClassName("inform-message"));
            IWebElement closeAlertAboutSuccessOrderingButton = informMessageClass.FindElement(By.TagName("button"));

            closeAlertAboutSuccessOrderingButton.Click();
            SeleniumDriver.WaitUntilElementClose(informMessageWrapClassLocator);
        }
        
        [Given(@"Я нахожу заказ по номеру телефона '(.*)' и нажимаю на него")]
        public void FindAndClickOn_ProblemOrder(String clientPhone)
        {
            SeleniumDriver.WaitUntilElementAvailable(By.Name("order"));
            
            ReadOnlyCollection<IWebElement> clientPhones = SeleniumDriver.FindElements(By.Name("clientPhone"), 100);

            IWebElement order = clientPhones.FirstOrDefault(clientPhoneNumber=>clientPhoneNumber.Text == clientPhone);

            if (order == null)
            {
                throw new Exception($"Заказ с номером телефона {clientPhone} не найден");
            }
            
            SeleniumDriver.WaitUntilElementClickable(order);

            order.Click();
        }

        [Given(@"Я вижу в детальной информации о заказе информацию о торговой точке, обслуживающей заказ '(.*)'")]
        public void CanSee_InOrderDetail_TradeObjectName(String orderDetailTradeObjectName)
        {
            IWebElement tradeObjectName = SeleniumDriver.FindElement(By.Id("detailInfoTradeObjectName"), 100);
            String tradeObjectNameText = tradeObjectName.Text;
            
            if (tradeObjectNameText != orderDetailTradeObjectName)
            {
                throw new Exception($"Торговая точка не соответствует {orderDetailTradeObjectName}!");
            }
        }

        [Given(@"Я вижу в детализации заказа информацию об адресе доставки '(.*)'")]
        public void CanSee_AtOrderDetailInDeliveryInfo(String deliveryInfo)
        {
            IWebElement deliveryInfoContainer = SeleniumDriver.FindElement(By.Id("clientAddress"), 100);

            if (deliveryInfoContainer.Text != deliveryInfo)
            {
                throw new Exception($"Информация об адресе доставки заказа не соответствует {deliveryInfo}!");
            }
        }
        
        [Given(@"Я вижу в детализации заказа информацию о номере клиента '(.*)'")]
        public void CanSee_AtOrderDetailInClientNumberInfo(String clientNumberInfo)
        {
            IWebElement clientNumberInfoContainer = SeleniumDriver.FindElement(By.Id("clientInfoPhone"), 100);

            if (clientNumberInfoContainer.Text != clientNumberInfo)
            {
                throw new Exception($"Информация о номере клиента не соответствует {clientNumberInfo}!");
            }
        }
        
        [Given(@"Я вижу в детализации заказа информацию состав заказа, куда входит продукт: '(.*)'")]
        public void CanSee_AtOrderDetailInOrderLIstInfo(String orderList)
        {
           IWebElement orderProductList = SeleniumDriver.FindElement(By.Id("productName"), 100);
           String orderProductLineText = orderProductList.Text;
           String orderProductNameText = orderProductLineText.Replace(" Скидка 7%", "");
           
           if (orderProductNameText != orderList)
           { 
               throw new Exception($"Состав заказа не соответствует: {orderList}!");
           }
        }

        [Given(@"Я нажимаю кнопку 'Доп. продажи'")]
        public void ClickOn_AadditionalProductsButton()
        {
            IWebElement aadditionalProductsButton = SeleniumDriver.FindElement(By.Id("additionalProducts"), 100);
            aadditionalProductsButton.Click();
         }
        
        [Given(@"Я нажимаю на поле 'Группа продуктов'")]
        public void ClickOn_AdditionalProductsGroupssButton()
        {
            IWebElement additionalProductsGroups = SeleniumDriver.FindElement(By.Id("additionalProductsGroups"), 100);
            additionalProductsGroups.Click();
        }
        
        [Given(@"Я нажимаю на 'Гамбургер'")]
        public void ClickOn_HamburgerMenu()
        {
            IWebElement hamburgerMenu = SeleniumDriver.FindElement(By.Id("hamburgerMenu"), 1000);
            hamburgerMenu.Click();
        }
        
        [Given(@"Я нажимаю на кнопку 'Копировать'")]
        public void ClickOn_CopyOrderButton()
        {
            IWebElement copyOrderButton = SeleniumDriver.FindElement(By.Id("copyOrderButton"), 100);
            copyOrderButton.Click();
        }
        
        [Given(@"Я выбираю 'Доп. продукты'")]
        public void Choose_AdditionalProducts()
        {
            ReadOnlyCollection<IWebElement> additionalProducts = SeleniumDriver.FindElements(By.TagName("option"), 100);
            IWebElement additionalProductText = additionalProducts.FirstOrDefault(x => x.Text == "Доп продукты");
            additionalProductText.Click();
        }
        
        [Given(@"Я вижу открытое модальное окно 'Средний чек'")]
        public void CanSee_AverageChequeModal()
        {
            IWebElement averageChequeModal = SeleniumDriver.FindElement(By.Id("averageChequeModal"), 100);
            
            if (averageChequeModal == null)
            {
                throw new Exception("Модальное окно не найдено!");
            }
        }
        
        //[Given(@"Я вижу совпадающие телефон клиента и адрес")] 
        //public void asd()
        //{
        //    ReadOnlyCollection<IWebElement> qwe = SeleniumDriver.FindElements(By.ClassName("ng-binding"));
        //    String address = null;
        //    // IWebElement rty = SeleniumDriver.FindElement(By.CssSelector("input[checked=\"checked\"]"));
        //    // IWebElement sdf = qwe.FirstOrDefault(x=> x.FindElement(By.CssSelector("input[checked=\"checked\"]") ) == rty);
//
        //    foreach (var q in qwe)
        //    {
        //        ReadOnlyCollection<IWebElement> rty = q.FindElements(By.CssSelector("input[checked=\"checked\"]"));
        //        if (rty.Count != null)
        //        {
        //            address = q.Text;
        //            break;
        //        }
        //    }
        //   
        //}
        
        [Given(@"Я вижу открытое модальное окно 'Дополнительные продажи'")]
        public void CanSee_AdditionalProductsModal()
        {
            IWebElement additionalProductsModal = SeleniumDriver.FindElement(By.Id("additionalProductsModal"), 100);
            
            if (additionalProductsModal == null)
            {
                throw new Exception("Модальное окно не найдено!");
            }
        }
        
        [Given(@"Я ввожу период дат в 7 дней в модальном окне 'Средний чек'")]
        public void Input_DatePeriodInAverageChequeModal()
        {
            SeleniumDriver.WaitUntilElementAvailable(By.Id("averageChequeBeginDate"));
            
            IWebElement averageChequeBeginDate = SeleniumDriver.FindElement(By.Id("averageChequeBeginDate"), 100);
            
            DateTime beginDate  = DateTime.Today.Date - TimeSpan.FromDays(7);
            averageChequeBeginDate.SendKeys(Keys.Control + "a");

            averageChequeBeginDate.SendKeys(beginDate.ToString("dd.MM.yyyy"));
            averageChequeBeginDate.SendKeys(Keys.Enter);
        }
        
        [Given(@"Я ввожу период дат в 7 дней в модальном окне 'Дополнительные продажи'")]
        public void Input_DatePeriodInAdditionalProducts()
        {
            IWebElement additionalProductsBeginDate = SeleniumDriver.FindElement(By.Id("additionalProductsBeginDate"), 100);
            DateTime beginDate  = DateTime.Today.Date - TimeSpan.FromDays(7);
            _action.DoubleClick(additionalProductsBeginDate).Perform();
            additionalProductsBeginDate.SendKeys(beginDate.ToString("dd.MM.yyyy"));
        }
        
        [Given(@"Я вижу информацию о среднем чеке")]
        public void CanSee_AverageCheque()
        {
            IWebElement averageCheque = SeleniumDriver.FindElement(By.Id("averageChequeText"), 100);
            
            if (averageCheque == null)
            {
                throw new Exception("Информация не найдена!");
            }
        }
        
        [Given(@"Я вижу сумму дополнительных продаж")]
        public void CanSee_AdditionalProductsText()
        {
            IWebElement additionalProductsText = SeleniumDriver.FindElement(By.Id("additionalProductsText"), 100);
            
            if (additionalProductsText == null)
            {
                throw new Exception("Информация не найдена!");
            }
        }
        
        [Given(@"Я нажимаю на кнопку 'Получить данные' в модальном окне 'Средний чек'")]
        public void ClickOn_GetDataButton()
        {
            SeleniumDriver.WaitUntilElementClose(By.ClassName("dropdown-menu"));
            
            IWebElement successButton = SeleniumDriver.FindElement(By.Id("successButton"), 100);
            successButton.Click();
        }

        [Given(@"Я нажимаю на кнопку 'Получить данные' в модальном окне 'Дополнительные продажи'")]
        public void ClickOn_SuccessButton()
        {
            IWebElement successButton = SeleniumDriver.FindElement(By.Id("successButton"), 100);
            successButton.Click();
        }
        
        [Given(@"Я нажимаю на кнопку 'Жалоба'")]
        public void ClickOn_ProblemButton()
        {
            IWebElement problemButton = SeleniumDriver.FindElement(By.Id("orderProblem"), 100);
            problemButton.Click();
        }
        
        [Given(@"Я дважды нажимаю на кнопку 'Добавить жалобу'")]
        public void DoubleClickOn_AddProblemButton()
        {
            IWebElement addProblemButton = SeleniumDriver.FindElement(By.Id("addProblemButton"), 100);
            addProblemButton.Click();
            addProblemButton.Click();
        }
        
        [Given(@"Я ввожу тексты жалоб: '(.*)', '(.*)'")]
        public void Input_FirstProblemOrderText(String firstProblemOrderText, String secondProblemOrderText)
        {
            ReadOnlyCollection<IWebElement> problemAreas = SeleniumDriver.FindElements(By.Name("orderProblemArea"), 100);
            problemAreas[0].Click();
            problemAreas[0].SendKeys(firstProblemOrderText);
            
            problemAreas[1].Click();
            problemAreas[1].SendKeys(secondProblemOrderText);
        }

        [Given(@"Я нажимаю на кнопку 'Сохранить' в модальном окне жалобы")]
        public void ClickOn_ModalProblemWindowSaveButton()
        {
            IWebElement modalProblemWindowSaveButton = SeleniumDriver.FindElement(By.Id("modalProblemWindowSaveButton"), 100);
            modalProblemWindowSaveButton.Click();
        }

        [Given(@"Я вижу в детальной информации о заказе тип оплаты '(.*)'")]
        public void CanSee_OrderDetailInfoPaymentType(String paymentType)
        {
            IWebElement paymentTypeInfo = SeleniumDriver.FindElement(By.Id("paymentType"), 100);
            String paymentTypeInfoText = paymentTypeInfo.Text;
            
            if (paymentTypeInfoText != paymentType)
            {
                throw new Exception($"Тип оплаты не соответствует: {paymentType}!");
            }
        }

        [Given(@"Я вижу в детальной информации о заказе сумму у клиента: '(.*)'")]
        public void CanSee_OrderDetailInfoClientCashSum(String clientCashSum)
        {
            IWebElement clientCashSumInfo = SeleniumDriver.FindElement(By.Id("clientCashSum"), 100);
            String clientCashSumInfoText = clientCashSumInfo.Text;
            
            if (clientCashSumInfoText != clientCashSum)
            {
                throw new Exception($"Сумма наличных у клиента не соответствует {clientCashSum}!");
            }
        }

        [Given(@"Я выбираю точку '(.*)' из списка торговых точек")]
        public void ClickOn_TradeObject(String tradeObject)
        {
            IWebElement allTradeObjectButton = SeleniumDriver.FindElement(By.Id("allTradeObjects"), 100);
            
            SeleniumDriver.WaitUntilElementClickable(allTradeObjectButton);
            allTradeObjectButton.Click();
            
            ReadOnlyCollection<IWebElement> allTradeObjectButtons = allTradeObjectButton.FindElements(By.TagName("option"));
            IWebElement pickTradeObject = allTradeObjectButtons.FirstOrDefault(tradeObjectButton => tradeObjectButton.Text == tradeObject);
           
            SeleniumDriver.WaitUntilElementClickable(pickTradeObject);
            pickTradeObject.Click();
        }
        
        [Given(@"Я нажимаю на кнопку выхода из учетной записи оператора Call-центра")]
        public void ClickOn_CallCenterSignOutButton()
        {
            IWebElement signOutButton = SeleniumDriver.FindElement(By.Id("callCenterSignOutButton"), 100);
            signOutButton.Click();
        }
        
        [Given(@"Я нажимаю на кнопку 'Отложенные заказы'")]
        public void ClickOn_DeferredOrdersButton()
        {
            SeleniumDriver.WaitUntilElementAvailable(By.Id("deferredOrders"));
            
            IWebElement deferredOrdersButton = SeleniumDriver.FindElement(By.Id("deferredOrders"), 100);
            deferredOrdersButton.Click();
            
            SeleniumDriver.WaitUntilElementClose(By.ClassName("block-ui-visible"));
        }
        
        [Given(@"Я вижу в детализации заказа информацию о стоимости заказа: '(.*)'")]
        public void CanSee_OrderPriceInDetailOrderINfo(String price)
        {
            ReadOnlyCollection<IWebElement> detailOrderInfo =  SeleniumDriver.FindElements(By.ClassName("line-inform"), 100);
            IWebElement orderPrice = detailOrderInfo.FirstOrDefault(infoLine => infoLine.Text.Contains("К оплате:"));
			
            String orderPriceString = orderPrice.Text;
			
            if (orderPriceString != price)
            {
                throw new Exception($"Стоимость не соответствует: {price} ");
            }
        }
        
        [Given(@"Я вижу поле 'Доставка' в окне детальной информации о заказе")]
        public void CanSee_InDetailOrderInfo_DeliveryLine()
        {
            IWebElement deliveryLine = SeleniumDriver.FindElement(By.Id("deliveryTime"), 100);
			
            if (deliveryLine == null)
            {
                throw new Exception("Строка 'Доставка' не найдена в детальной информации о заказе");
            }
        }

        [Given("Я вижу заказ с номером телефона '(.*)', не подсвеченный зеленым цветом")]
        public void CanSee_NotHighlightedOrder_WithClientNumber(String clientPhone)
        {
            SeleniumDriver.WaitUntilElementAvailable(By.Name("order"));
            
            String completedOrderStyle = "rgba(0, 0, 0, 0) none repeat scroll 0% 0% / auto padding-box border-box";
            ReadOnlyCollection<IWebElement> orders = SeleniumDriver.FindElements(By.Name("order"));
            IWebElement order = orders.FirstOrDefault(order => IsSearchingOrder(order, clientPhone));
            String orderColor = order.GetCssValue("background");

            if (orderColor != completedOrderStyle)
            {
                throw new Exception($"Заказ не подсвечивается белым!");
            }
        }

        private static Boolean IsSearchingOrder(IWebElement order, String clientPhone)
        {
            String orderClientPhone = order.FindElement(By.Name("clientPhone")).Text;
            return orderClientPhone == clientPhone;
        }
        
        [Given("Я вижу заказ с номером телефона '(.*)', подсвеченный зеленым цветом")]
        public void CanSee_HighlightedOrder_WithClientNumber(String clientPhone)
        {
            SeleniumDriver.WaitUntilElementAvailable(By.Name("order"));
            
            String completedOrderStyle = "rgba(0, 0, 0, 0) linear-gradient(rgba(226, 251, 203, 0.7) 3%, rgba(201, 232, 173, 0.7) 47%) repeat scroll 0% 0% / auto padding-box border-box";
            ReadOnlyCollection<IWebElement> orders = SeleniumDriver.FindElements(By.Name("order"), 100);
            IWebElement order = orders.FirstOrDefault(order => IsSearchingOrder(order, clientPhone));
            String orderColor = order.GetCssValue("background");

            if (orderColor != completedOrderStyle)
            {
                throw new Exception($"Заказ не подсвечивается зеленым!");
            }
        }

    }
}