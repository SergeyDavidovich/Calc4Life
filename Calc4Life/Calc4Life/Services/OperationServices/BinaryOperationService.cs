using Calc4Life.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calc4Life.Services.OperationServices
{
    public class BinaryOperationService : IBinaryOperationService
    {
        public Operand? Operand1 { get; set; }
        public Operand? Operand2 { get; set; }
        public BinaryOperators? Operator { get; set; }
        public decimal? Result { get; private set; }

        public void Clear()
        {
            Operand1 = null;
            Operand2 = null;
            Operator = null;
            Result = null;
        }

        public bool IsReadyForCalc()
        {
            if (Operand1 != null & Operand2 != null & Operator != null)
                return true;
            else
                return false;
        }

        public decimal? GetResult()
        {
            decimal? result = null;

            if (Operand1 == null || Operand2 == null)
            {
                Result = result;
                return result;
            }

            switch (Operator)
            {
                case BinaryOperators.Plus:
                    result = Operand1.Value.OperandValue + Operand2.Value.OperandValue;
                    break;
                case BinaryOperators.Minus:
                    result = Operand1.Value.OperandValue - Operand2.Value.OperandValue;
                    break;
                case BinaryOperators.Multiplication:
                    result = Operand1.Value.OperandValue * Operand2.Value.OperandValue;
                    break;
                case BinaryOperators.Division:
                    if (Operand2.Value.OperandValue != 0)
                        result = Operand1.Value.OperandValue / Operand2.Value.OperandValue;
                    else
                        throw new DivideByZeroException("infinity");
                    break;
                case BinaryOperators.Discount:
                    result = (Operand1.Value.OperandValue * Operand2.Value.OperandValue) / 100;
                    break;
            }
            Result = result;
            return result;
        }

        public void SetOperand(Operand operand)
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
