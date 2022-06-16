using System;

namespace Buzzolls.SpecFlow.Tests
{
    public class Research
    {

        public static String BaseBackOfficeUrl = "https://dev.backoffice.buzzolls.ru/";
        //public static String BaseBackOfficeUrl = "https://stable.backoffice.buzzolls.ru";
        //public static String BaseBackOfficeUrl = "http://localhost:51275/";
        //public static String BaseBackOfficeUrl = "https://dev.backoffice.buzzolls.ru/";
        public static String BaseBackOfficeAuthUrl = BaseBackOfficeUrl + "Auth";
        public static String BaseClientSiteUrl = "https://dev.site.buzzolls.ru/";
        public static String BaseBackOfficeCashBoxUrl = "https://dev.backoffice.buzzolls.ru/Cashier/Cash/Index#!/listOrders?cashBoxDebugMode=true";
        
        
        public static String BackOffice_BaseUrl = "http://localhost:51275/" ;
        public static String ClientSite_BaseUrl = "http://localhost:51275/" ;
    }
}