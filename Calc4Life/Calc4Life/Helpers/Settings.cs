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
    // Экспоненциальная (научная) нотация числа                          (Exponential (scientific) number notation) 
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

        #region Setting Constants

        // Разделитель групп разрядов

        private const string GrouppingDigitsKey = "GrouppingDigits";
        private static readonly bool GrouppingDigitsDefault = true;

        // Экспоненциальная (научная) нотация числа  
        //private const string ExponentialNotationKey = "ExponentialNotation";
        //private static readonly bool ExponentialNotationDefault = true;

        // Точность вычисления (количество отражаемых знаков после запятой)
        private const string CalcAccuracyKey = "CalcAccuracy";
        private static readonly decimal CalcAccuracyDefault = 2;

        // Точность округления 
        private const string RoundAccuracyKey = "RoundAccuracy";
        private static readonly decimal RoundAccuracyDefault = 2;


        // Округлять результат (при указанной точности) 
        private const string RoundingKey = "Rounding";
        private static readonly bool RoundingDefault = false;

        #endregion


        public static bool GrouppingDigits
        {
            get
            {
                return AppSettings.GetValueOrDefault(GrouppingDigitsKey, GrouppingDigitsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(GrouppingDigitsKey, value);
            }
        }
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
    }
}
