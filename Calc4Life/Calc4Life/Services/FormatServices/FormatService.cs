using Calc4Life.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Calc4Life.Services.FormatServices
{
    public static class FormatService
    {

        public static string FormatResult(double value)
        {
            string result;
            double number = value;

            //int accuracy =(int)Settings.Accuracy;
            //string acStr = accuracy.ToString();
            //NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;

            //string format=$"N{acStr}";

            //if (Settings.Rounding)
            //    number = Math.Round(value, accuracy, MidpointRounding.AwayFromZero);

            //if (Settings.GrouppingDigits)
            //{
            //    nfi.NumberDecimalDigits = 3;
            //    //format = $"N{accuracy.ToString()}";
            //    result = number.ToString("N",nfi);
            //}
            //else
                result = number.ToString();

            return result;
        }
    }
}
