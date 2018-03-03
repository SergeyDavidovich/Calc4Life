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

            int accuracy = (int)Settings.CalcAccuracy - 1;
            string precisionSpecifier = accuracy.ToString();

            //if (Settings.Rounding)
            //    input = Math.Round(value, accuracy);
            //else
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
