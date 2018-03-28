using Calc4Life.Models;
using Calc4Life.Services.FormatServices;
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
        public FormatServices.FormatService _formatService;

        public BinaryOperationService(FormatService formatService)
        {
            _formatService = formatService;
        }

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

        public string GetOperationExpression()
        {
            string output;
            string operand1;
            string operand2;

            //получаем операнды из операции
            if (Operand1 == null)
                operand1 = String.Empty;
            else
            {
                if (Operand1.Value.IsConstant())
                    operand1 = Operand1.Value.OperandName;
                else
                    operand1 = _formatService.FormatInput(Operand1.Value.OperandValue.Value);
            }
            if (Operand2 == null)
                operand2 = String.Empty;
            else
            {
                if (Operand2.Value.IsConstant())
                    operand2 = Operand2.Value.OperandName;
                else
                    operand2 = _formatService.FormatInput(Operand2.Value.OperandValue.Value);
            }

            //заворачиваем в скобки, если отрицательные
            if (operand1.StartsWith("-")) operand1 = $"({operand1})";
            if (operand2.StartsWith("-")) operand2 = $"({operand2})";

            //получаем оператор из операции
            string oper = "";

            {
                switch (Operator)
                {
                    case BinaryOperators.Plus:
                        oper = "+"; break;
                    case BinaryOperators.Minus:
                        oper = "-"; break;
                    case BinaryOperators.Multiplication:
                        oper = "×"; break;
                    case BinaryOperators.Division:
                        oper = "÷"; break;
                    case BinaryOperators.Discount:
                        oper = "%"; break;
                }
            }

            //добавляем (или нет) знак равенства в выражение 
            string equal;
            decimal? result = Result;
            if (result != null)
                equal = " =";
            else equal = "";

            //формируем строку вывода выражения
            oper = oper.Length == 0 ? "" : " " + oper;
            operand2 = operand2.Length == 0 ? "" : " " + operand2;

            output = $"{operand1}{oper}{operand2}{equal}";
            return output;
        }
    }
}
