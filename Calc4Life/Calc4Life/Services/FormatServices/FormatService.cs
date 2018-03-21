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

        double PositiveMax; // = 99999999;//999; // 11 characters
        double PositiveMin; // = 0.000000001; // 11 characters
        double NegativeMax; //= -0.000000001;
        double NegativeMin; //= -99999999999;

        string negativeSign; // знак перед целой частью
        string decimalSeparator; //десятичный знак
        string integerPart; //целая часть числа
        string fractionalPart; //дробная часть числа

        int maxResultLenth; // максимальная длина выходной строки
        string exponentialFormatString; // строка форматирования в экспоненциальный формат

        public FormatService()
        {
            exponentialFormatString = "e2";

            maxResultLenth = 14;
            if (!Settings.GrouppingDigits)
            {
                PositiveMax = 9999999999999; // 13
                PositiveMin = 0.00000000001; // 13
                NegativeMax = -0.00000000001;  // 14
                NegativeMin = -9999999999999;  // 14
            }
            else if(Settings.GrouppingDigits)
            {

            }
        }

        public string FormatResult(double value)
        {
            string result = "";
            double curValue; //число для внутренних преобразований

            #region 1 форматируем число 0
            if (value == 0) return "0";
            #endregion

            #region 2 форматируем числа, находящихся вне пределов диапазонов в экспоненциальный формат
            if (value > 0)
            {
                // 2
                if (value > PositiveMax || value < PositiveMin)
                {
                    result = value.ToString(exponentialFormatString);
                    return result;
                }
            }
            else if (value < 0)
                if (value > NegativeMax || value < NegativeMin)
                {
                    result = value.ToString(exponentialFormatString);
                    return result;
                }
            #endregion

            #region 3 округляем число согласно Settings
            if (Settings.Rounding)
                curValue = Math.Round(value, (int)Settings.RoundAccuracy, MidpointRounding.AwayFromZero);
            else
                curValue = value;
            #endregion

            AnalizeValue(curValue);


            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private void AnalizeValue(double value)
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
                integerPart = input;
                fractionalPart = String.Empty;
            }
        }

    }
    //public class FormatService
    //{
    //    private double PositiveMax = 99999999;//999; // 11 characters
    //    private double PositiveMin = 0.000000001; // 11 characters
    //    private double NegativeMax = -0.000000001;
    //    private double NegativeMin = -99999999999;

    //    const int maxResultLenth = 12;
    //    const string expFormatString = "e2";

    //    public string FormatResult(double value)
    //    {
    //        string result;
    //        // 1
    //        if (value == 0) return "0";
    //        if (value > 0)
    //        {
    //            // 2
    //            if (value > PositiveMax || value < PositiveMin)
    //            {
    //                result = value.ToString(expFormatString);
    //                return result;
    //            }
    //        }
    //        else if (value < 0)
    //            if (value > NegativeMax || value < NegativeMin)
    //            {
    //                result = value.ToString(expFormatString);
    //                return result;
    //            }
    //        //3
    //        result = DoFormatString(value);
    //        return result;
    //    }

    //    public string FormatResult(string input)
    //    {
    //        string result;
    //        result = input;
    //        return result;
    //    }

    //    private string DoFormatString(double value)
    //    {
    //        string result = ""; // строка для вывода результата функции
    //        string input; // строка для внутренних преобразований 
    //        double curValue; //число для внутренних преобразований

    //        #region Rounding

    //        if (Settings.Rounding)
    //            curValue = Math.Round(value, (int)Settings.RoundAccuracy, MidpointRounding.AwayFromZero);
    //        else
    //            curValue = value;

    //        #endregion

    //        input = curValue.ToString("G");

    //        #region Groupping

    //        string negativeSign = ""; // знак перед целой частью
    //        string integerPart = ""; //целая часть числа
    //        string decimalSeparator = ""; //десятичный знак
    //        string fractionalPart = ""; //дробная часть числа

    //        string DecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
    //        string NegativeSign = CultureInfo.CurrentCulture.NumberFormat.NegativeSign;
    //        string GroupSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;

    //        if (Settings.GrouppingDigits)
    //        {
    //            if (input.StartsWith(NegativeSign))
    //            {
    //                negativeSign = NegativeSign;
    //                int index = input.LastIndexOf(NegativeSign);
    //                input = input.Substring(index + 1);
    //            }
    //            if (input.Contains(DecimalSeparator))// 
    //            {
    //                int index = input.LastIndexOf(DecimalSeparator);

    //                integerPart = input.Substring(0, index);
    //                fractionalPart = input.Substring(index + 1);
    //                decimalSeparator = DecimalSeparator;
    //            }
    //            else
    //            {
    //                integerPart = input;
    //                decimalSeparator = "";
    //            }

    //            char[] chars = integerPart.ToCharArray();
    //            Array.Reverse(chars);

    //            string output = "";
    //            for (int i = 0; i < integerPart.Length; i++)
    //            {
    //                if (i % 3 == 0 & i != 0)
    //                    output += CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator + chars[i].ToString();
    //                else
    //                    output += chars[i].ToString();
    //            }

    //            chars = output.ToCharArray();
    //            Array.Reverse(chars);
    //            output = "";
    //            foreach (var item in chars)
    //            {
    //                output += item.ToString();
    //            }

    //            result = negativeSign + output + decimalSeparator + fractionalPart;
    //        }
    //        else
    //            result = curValue.ToString("G");

    //        #endregion

    //        #region Trimming
    //        //if (result.Contains(decimalSeparator))
    //        //{
    //            if (result.Length > maxResultLenth)
    //                result = result.Remove(maxResultLenth);
    //        //}
    //        if (decimalSeparator != String.Empty)
    //            result = result.TrimEnd('0');

    //        if (result.EndsWith(DecimalSeparator))
    //            result = result.Remove(result.Length - 1);

    //        #endregion

    //        return result;
    //    }
    //}
}
