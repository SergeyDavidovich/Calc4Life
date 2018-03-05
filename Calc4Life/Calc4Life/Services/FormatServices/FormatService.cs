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
            double input;
            string format;
            string result;

            if (value.ToString().Length > 12)
            {
                format = "e4";
                result = value.ToString(format);
                return result;
            }

            int accuracy = (int)Settings.RoundAccuracy;
            string precisionSpecifier = accuracy.ToString();

            input = value;

            if (Settings.Rounding)
            {
                if (Settings.GrouppingDigits)
                    format = $"N{precisionSpecifier}";
                else
                    format = $"F{precisionSpecifier}";
            }
            else
            {
                if (Settings.GrouppingDigits)
                    format = "N17";
                else
                    format = "G";
            }

            result = input.ToString(format);
            result = result.TrimEnd('0');
            return result;
        }
    }
}
