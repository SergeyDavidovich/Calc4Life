using Calc4Life.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Calc4Life.Services.FormatServices
{
    public class FormatService1
    {
        public string FormatResult(double value)
        {
            double input;
            string format;
            string result;

            if (value == 0) return "0";

            //
            if (value > 99999999999 || value < 0.0000000001)
            {
                format = "e4";
                result = value.ToString(format);
                return result;
            }


            int precisionRound = (int)Settings.RoundAccuracy;
            int precisionCalc = precisionRound + 1;

            string precisionRoundSpecifier = precisionRound.ToString();
            string precisionCalcSpecifier = precisionCalc.ToString();

            input = value;

            if (Settings.Rounding)
            {
                if (Settings.GrouppingDigits)
                    format = $"N{precisionRoundSpecifier}";
                else
                    format = $"F{precisionRoundSpecifier}";
            }
            else
            {
                if (Settings.GrouppingDigits)
                    format = $"G{precisionCalcSpecifier}";
                else
                    format = "G";
            }

            result = input.ToString(format);//.TrimEnd('0');

            //if (result.EndsWith(NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator))


            if (result.Length > 12)
                result = result.Remove(12);

            return result;
        }
    }

    public class FormatService
    {
        private double maxValue = 99999999999; // 11 characters
        private double minValue = 0.000000001; // 11 characters
        const int maxResultLenth = 12;

        public string FormatResult(double value)
        {
            string result;
            // 1
            if (value == 0) return "0";

            // 2
            if (value > maxValue || value < minValue)
            {
                result = value.ToString("e4");
                return result;
            }
            //3
            result = DoFormatString(value);
            return result;
        }

        public string FormatResult(string input)
        {
            string result;
            result = input;
            return result;
        }

        string DoFormatString(double value)
        {
            string result = ""; // строка для вывода результата функции
            string input; // строка для внутренних преобразований 
            double curValue; //число для внутренних преобразований

            #region Rounding

            if (Settings.Rounding)
                curValue = Math.Round(value, (int)Settings.RoundAccuracy, MidpointRounding.AwayFromZero);
            else
                curValue = value;

            #endregion

            input = curValue.ToString("G");

            #region Groupping

            string negativeSign = ""; // знак перед целой частью
            string integerPart = ""; //целая часть числа
            string fractionalPart = ""; //дробная часть числа
            string decimalSeparator = ""; //десятичныйй знак

            string DecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            string NegativeSign = CultureInfo.CurrentCulture.NumberFormat.NegativeSign;
            string GroupSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;

            if (Settings.GrouppingDigits)
            {
                if (input.Contains(NegativeSign))
                {
                    negativeSign = NegativeSign;
                    int index = input.LastIndexOf(NegativeSign);
                    input = input.Substring(index + 1);
                }
                if (input.Contains(DecimalSeparator))
                {
                    int index = input.LastIndexOf(DecimalSeparator);

                    integerPart = input.Substring(0, index);
                    fractionalPart = input.Substring(index + 1);
                    decimalSeparator = DecimalSeparator;
                }
                else
                {
                    integerPart = input;
                    decimalSeparator = "";
                }

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

                result = negativeSign + output + decimalSeparator + fractionalPart;

                //if (result.Length > 12)

                //return result;
            }
            else
                result = curValue.ToString("G");

            #endregion

            #region Trimming

            if (result.Length > maxResultLenth)
                result = result.Remove(maxResultLenth);
            if (result.EndsWith(DecimalSeparator))
                result = result.Remove(result.Length - 1);


            #endregion

            return result;
        }

    }
}
