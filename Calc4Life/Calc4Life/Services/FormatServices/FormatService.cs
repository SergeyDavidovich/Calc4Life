using Calc4Life.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calc4Life.Services.FormatServices
{
    public static class FormatService
    {

        public static string FormatResult(double value)
        {
            string result;
            int accuracy = (int)Settings.Accuracy;
            string format;

            if (Settings.GrouppingDigits && !Settings.Rounding)
            {
                result = "";
                format = $"N{accuracy.ToString()}";
                result = value.ToString();
            }
            else if (Settings.GrouppingDigits && Settings.Rounding)
            {
                result = "";
                value = Math.Round(value, accuracy, MidpointRounding.AwayFromZero);
                format = $"N{(accuracy-1).ToString()}";
                result = value.ToString(format);
            }
            else
                result = value.ToString();

            return result;
        }
    }
}
