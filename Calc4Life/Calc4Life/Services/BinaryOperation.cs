using Calc4Life.Services.OperationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc4Life.Services
{
    /// <summary>
    /// Абстракция бинарной операций
    /// </summary>
    public static class BinaryOperation1
    {
        private static double? operand1; //Первый опернад
        private static double? operand2; //Второй операнд
        private static double? result; //Результат последней операций 
        private static BinaryOperators? operation; //Последняя введёная операция
        private static bool _IsOperationFormed; //готова ли операция к выполнению

        /// <summary>
        /// Представляет первый операнд
        /// </summary>
        public static double? Operand1
        {
            get
            {
                return operand1;
            }
        }
        /// <summary>
        /// Представляет второй операнд
        /// </summary>
        public static double? Operand2
        {
            get
            {
                return operand2;
            }
        }
        /// <summary>
        /// Представляет оператор операции
        /// </summary>
        public static BinaryOperators? Operation
        {
            get { return operation; }
            set
            {
                operation = value;
                SetIsOperationFormed(operand1, operand2, operation);
            }
        }

        /// <summary>
        /// Представляет результат операций
        /// </summary>
        public static double? Result
        {
            get { return GetResult(); }
        }
        /// <summary>
        /// возвращает значение, указывающее готова ли операция к выполнению
        /// </summary>
        public static bool IsOperationFormed
        {
            get { return _IsOperationFormed; }
        }
        /// <summary>
        /// устанавливает значение, указывающее готова ли операция к выполнению
        /// </summary>
        /// <param name="operand1"></param>
        /// <param name="operand2"></param>
        /// <param name="operation"></param>
        private static void SetIsOperationFormed(double? operand1, double? operand2, BinaryOperators? operation)
        {
            if (operand1 != null & operand2 != null & operation != null)
                _IsOperationFormed = true;
            else
                _IsOperationFormed = false;
        }
        /// <summary>
        /// Устанавливает операнды
        /// </summary>
        /// <param name="operand"></param>
        public static void SetOperands(double operand)
        {
            if (Operation == null)
                operand1 = operand;
            else
                operand2 = operand;

            SetIsOperationFormed(operand1, operand2, operation);
        }
        /// <summary>
        /// Вычисляет результат операций
        /// </summary>
        private static double? GetResult()
        {
            switch (Operation)
            {
                case BinaryOperators.Plus:
                    result = operand1 + operand2;
                    break;
                case BinaryOperators.Multiplication:
                    result = operand1 * operand2;
                    break;
                case BinaryOperators.Division:
                    result = operand1 / operand2;
                    break;
                case BinaryOperators.Minus:
                    result = operand1 - operand2;
                    break;
                case BinaryOperators.Discount:
                    result = (operand1 * operand2) / 100;
                    break;
            }
            return result;
        }
        /// <summary>
        /// Удаляет значение элементов операций
        /// </summary>
        public static void Clear()
        {
            operand1 = null;
            operand2 = null;
            operation = null;

            SetIsOperationFormed(operand1, operand2, operation);
        }
    }
}
