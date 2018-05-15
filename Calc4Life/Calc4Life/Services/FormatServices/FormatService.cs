using Calc4Life.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Calc4Life.Services.FormatServices
{
    public class FormatService
    {
        NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;

        decimal PositiveMax; // = 9999999999999; // 13 characters
        decimal PositiveMin; // = 0.00000000001; // 13 characters
        decimal NegativeMax; //= -0.00000000001; // 14 characters
        decimal NegativeMin; //= -9999999999999; // 14 characters

        string negativeSign; // знак перед целой частью
        string decimalSeparator; //десятичный знак
        string integerPart; //целая часть числа
        string fractionalPart; //дробная часть числа

        int maxResultLength; // максимальная длина выходной строки
        string exponentialFormatString; // строка форматирования в экспоненциальный формат

        public FormatService()
        {
            exponentialFormatString = "e2";

            maxResultLength = 14;
            if (!Settings.GrouppingDigits)
            {
                PositiveMax = 9999999999999m;   // 13
                PositiveMin = 0.00000000001m;   // 13
                NegativeMax = -0.00000000001m;  // 14
                NegativeMin = -9999999999999m;  // 14
            }
            else if (Settings.GrouppingDigits)
            {
                PositiveMax = 9999999999; // 10
                PositiveMin = 0.00000000001m;  // 13
                NegativeMin = -9999999999; // 10
                NegativeMax = -0.00000000001m; // 14
            }
        }
        public string FormatInput(decimal value) 
        {
            AnalizeValue(value);
            if (!IsInRange(value))
                return value.ToString(exponentialFormatString);
            if (Settings.Rounding)
                value = RoundValue(value);
            return TrimValue(value.ToString());
        }
        public string FormatResult(decimal value)
        {
            string result = "";
            decimal curValue; //число для внутренних преобразований

            #region 1 форматируем число 0
            if (value == 0) return "0";
            #endregion


            #region 2 форматируем числа, находящихся вне пределов диапазонов в экспоненциальный формат
            if (!IsInRange(value))
            {
                result = value.ToString(exponentialFormatString);
                return result;
            }
            #endregion

            #region 3 округляем число согласно Settings
            if (Settings.Rounding)
                curValue = RoundValue(value);
            else
                curValue = value;
            #endregion

            AnalizeValue(curValue);

            #region 4 вставка разделителей групп разрядов
            if (Settings.GrouppingDigits)
            {
                result = GroupValue(value);
            }
            else
                result = curValue.ToString("G");

            #endregion

            #region 5 отсечение (trimming)   
            result = TrimValue(result);
            #endregion
            return result;
        }

        private string TrimValue(string value)
        {
            if (decimalSeparator == string.Empty) return value; // целое число

            if (value.Length > maxResultLength)
            {
                value = value.Remove(maxResultLength - 1);//отнимаем единицу для того, чтобы оставить место для знака
            }
                value = value.TrimEnd('0');

            if (value.EndsWith(decimalSeparator)) // удаляем десятичный знак, если строка на него заканчивается
                value = value.Remove(value.Length - 1);

            return value;
        }
        private string GroupValue(decimal value)
        {
            char[] chars = integerPart.ToCharArray();
            Array.Reverse(chars);

            string output = "";
            for (int i = 0; i < integerPart.Length; i++)
            {
                if (i % 3 == 0 & i != 0)
                    output += CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator + chars[i].ToString();
                else
                    output += chars[i].ToString();
            }

            chars = output.ToCharArray();
            Array.Reverse(chars);
            output = "";
            foreach (var item in chars)
            {
                output += item.ToString();
            }

            var result = negativeSign + output + decimalSeparator + fractionalPart;
            return result;
        }
        private decimal RoundValue(decimal value)
        {
            return Math.Round(value, (int)Settings.RoundAccuracy, MidpointRounding.AwayFromZero);
        }
        /// <summary>
        /// Проверяет границы числа, в случае несоответствия выдаёт число в экспоненциальной форме
        /// если соответствует границам,то возвращает null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsInRange(decimal value)
        {
            if (value > 0)
            {
                // 2
                if (value > PositiveMax || value < PositiveMin)
                {
                    return false;
                }
            }
            else if (value < 0)
                if (value > NegativeMax || value < NegativeMin)
                {
                    return false;
                }
            return true;
        }
        /// <summary>
        /// Разбивает число на части(знак, целая часть, делиметр, дробная часть)
        /// </summary>
        /// <param name="value"></param>
        private void AnalizeValue(decimal value)
        {
            string input = value.ToString();

            negativeSign = input.StartsWith(numberFormat.NegativeSign) ? numberFormat.NegativeSign : String.Empty;
            decimalSeparator = input.Contains(numberFormat.NumberDecimalSeparator) ? numberFormat.NumberDecimalSeparator : String.Empty;

            if (input.Contains(numberFormat.NumberDecimalSeparator)) // дробное число
            {
                int index = input.IndexOf(numberFormat.NumberDecimalSeparator);

                integerPart = input.StartsWith(numberFormat.NegativeSign) ? input.Substring(1, index - 1) : input.Substring(0, index);
                fractionalPart = input.Substring(index + 1, input.Length - index - 1);
            }
            else // целое число
            {
                integerPart = input.StartsWith(numberFormat.NegativeSign) ? input.Substring(1) : input;
                fractionalPart = String.Empty;
            }
        }
    }
}
