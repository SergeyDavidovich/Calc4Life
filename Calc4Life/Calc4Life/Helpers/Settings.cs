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

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        private const bool SeparateGroupsKey = false;
        private static readonly bool SeparateGroupsDefault = true;

        #endregion


        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }

    }
}
