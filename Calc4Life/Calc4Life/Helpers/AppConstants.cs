using System;
using System.Collections.Generic;
using System.Text;

namespace Calc4Life.Helpers
{
    public static class AppConstants
    {
        //in app messages
        public const string SETTINGS_CHANGED_MESSAGE = "settings_changed";
        public const string CONSTANTS_UPDATED_MESSAGE = "constants_updated";

        //constant purchasing 
        public const int MAX_CONSTANTS_NUMBER = 3; // количество констант, доступных без покупки
        public const string CONSTANTS_PPODUCT_ID = "constants"; // имя (id) продукта для продажи (зарегистрировано в Google Play)

        //common app constants

    }
}
