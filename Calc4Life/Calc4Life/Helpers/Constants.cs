using System;
using System.Collections.Generic;
using System.Text;

namespace Calc4Life.Helpers
{
    public static class AppConstants
    {
        //in app messages
        public const string SETTINGS_CHANGED_MESSAGE = "settings_changed";

        //Application.Properties keys
        public const string KEY_CONSTANTS_NUMBER = "constants_number"; // текущее количество констант
        public const string KEY_MAX_CONSTANTS_NUMBER = "max_constants_number"; // количество констант, доступных без покупки
        public const string CONSTANTS_PPODUCT_ID = "constants_unblocked"; // имя (id) продукта для продажи (зарегистрировано в Google Play)
        public const string IS_CONSTANT_PURCHASED = "is_constant_purchased"; // приобретен ли продукт "constants_unblocked"

        //common app constants

    }
}
