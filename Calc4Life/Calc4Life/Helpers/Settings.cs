// Helpers/Settings.cs// Helpers/Settings.cs
using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Calc4Life.Helpers
{
    // Settings Keys:
    // 
    // Разделитель групп разрядов                                        (Delimiter of the groups of digits) 
    // Точность вычисления (количество отражаемых знаков после запятой)  (Accuracy of calculations (number of decimal places displayed))
    // Точность округления                                               (Rounding accuracy)
    // Округлять результат (при указанной точности)                      (Round up the result (up to specified accuracy))
    //


    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Calc result formatting settings

        // Разделитель групп разрядов
        private const string GrouppingDigitsKey = "GrouppingDigits";
        private static readonly bool GrouppingDigitsDefault = true;
        public static bool GrouppingDigits
        {
            get {return AppSettings.GetValueOrDefault(GrouppingDigitsKey, GrouppingDigitsDefault);}
            set {AppSettings.AddOrUpdateValue(GrouppingDigitsKey, value);}
        }

        // Точность вычисления (количество отражаемых знаков после запятой)
        private const string CalcAccuracyKey = "CalcAccuracy";
        private static readonly decimal CalcAccuracyDefault = 2;
        public static decimal CalcAccuracy
        {
            get
            {
                return AppSettings.GetValueOrDefault(CalcAccuracyKey, CalcAccuracyDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(CalcAccuracyKey, value);
            }
        }

        // Точность округления 
        private const string RoundAccuracyKey = "RoundAccuracy";
        private static readonly decimal RoundAccuracyDefault = 2;
        public static decimal RoundAccuracy
        {
            get
            {
                return AppSettings.GetValueOrDefault(RoundAccuracyKey, RoundAccuracyDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(RoundAccuracyKey, value);
            }
        }

        // Округлять результат (при указанной точности) 
        private const string RoundingKey = "Rounding";
        private static readonly bool RoundingDefault = false;
        public static bool Rounding
        {
            get
            {
                return AppSettings.GetValueOrDefault(RoundingKey, RoundingDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(RoundingKey, value);
            }
        }
        
        #endregion

        #region Purchasing settings

        //приобретен ли продукт "constants_unblocked"
        private const string ConstProductPurchasedKey = "ConstProductPurchased";
        private static readonly bool ConstProductPurchasedDefault = false;
        public static bool ConstProductPurchased
        {
            get { return AppSettings.GetValueOrDefault(ConstProductPurchasedKey, ConstProductPurchasedDefault); }
            set { AppSettings.AddOrUpdateValue(ConstProductPurchasedKey, value); }
        }

        #endregion

        #region Common app settings
        //установить вибрацию
        private const string VibrationKey = "Vibration";
        private static readonly bool VibrationDefault = false;
        public static bool Vibration
        {
            get { return AppSettings.GetValueOrDefault(VibrationKey, VibrationDefault); }
            set { AppSettings.AddOrUpdateValue(VibrationKey, value); }
        }

        #endregion

    }
}
