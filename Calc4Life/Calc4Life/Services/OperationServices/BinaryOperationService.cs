using System;
using System.Collections.Generic;
using System.Text;

namespace Calc4Life.Services.OperationServices
{
    public class BinaryOperationService : IBinaryOperationService
    {
        public double? Operand1 { private get; set; }
        public double? Operand2 { private get; set; }
        public BinaryOperators? Operator {private get; set; }

        public void Clear()
        {
            Operand1 = null;
            Operand2 = null;
            Operator = null;
        }

        public bool IsReadyForCalc()
        {
            if (Operand1 != null & Operand2 != null & Operator != null)
                return true;
            else
                return false;
        }

        public double? Result()
        {
            double? result = null;
            switch (Operator)
            {
                case BinaryOperators.Plus:
                    result = Operand1.Value + Operand2.Value;
                    break;
                case BinaryOperators.Minus:
                    result = Operand1.Value - Operand2.Value;
                    break;
                case BinaryOperators.Multiplication:
                    result = Operand1.Value * Operand2.Value;
                    break;
                case BinaryOperators.Division:
                    result = Operand1.Value / Operand2.Value;
                    break;
                case BinaryOperators.Discount:
                    result = (Operand1.Value * Operand2.Value) / 100;
                    break;
            }
            return result;
        }

        public void SetOperand(double operand)
        {
            if (Operator == null)
                Operand1 = operand;
            else
                Operand2 = operand;
        }

        public void SetOperator(string @operator)
        {
            switch (@operator)
            {
                case "+":
                    Operator = BinaryOperators.Plus;
                    break;
                case "-":
                    Operator = BinaryOperators.Minus;
                    break;
                case "*":
                    Operator = BinaryOperators.Multiplication;
                    break;
                case "/":
                    Operator = BinaryOperators.Division;
                    break;
                case "%":
                    Operator = BinaryOperators.Discount;
                    break;

            }
        }
    }
}
