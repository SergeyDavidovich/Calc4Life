using System;
using System.Collections.Generic;
using System.Text;

namespace Calc4Life.Services.FormatServices
{
    public interface IFormatService
    {
        // Термины:
        // 
        // Десятичный разделитель                                            (Decimal separator)
        // Разделитель групп разрядов                                        (Delimiter of the groups of digits) 
        // Экспоненциальная (научная) нотация числа                          (Exponential (scientific) number notation) 
        // Точность вычисления (количество отражаемых знаков после запятой)  (Accuracy of calculations (number of decimal places displayed))
        // Округлять результат (при указанной точности)                      (Round up the result? (up to specified accuracy))
        //

        bool DoGroup { get; set; }
        void ShowAsExponential();

    }

    public static class Settings
    {
        public static bool SeparateGroups
                    {
            get;
            set;
 }
        public static bool UseExponentialNotation { get; set; }
        public static int CalculationAccuracy { get; set; }
        public static bool RoundResult { get; set; }
    }
}
