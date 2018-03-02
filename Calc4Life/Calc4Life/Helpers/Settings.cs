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
    // Округлять результат (при указанной точности)                      (Round up the result? (up to specified accuracy))
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
        private const string AccuracyKey = "Accuracy";
        private static readonly double AccuracyDefault = 2.0;

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
        public static double Accuracy
        {
            get
            {
                return AppSettings.GetValueOrDefault(AccuracyKey, AccuracyDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AccuracyKey, value);
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
