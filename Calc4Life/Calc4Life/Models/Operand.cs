using System;
using System.Collections.Generic;
using System.Text;

namespace Calc4Life.Models
{
    public struct Operand
    {
        public double? OperandValue;
        public string OperandName;

        public override string ToString()
        {
            if (OperandName == null)
                return OperandValue.ToString();
            else
                return OperandName;
        }
        public bool IsConstant()
        {
            return OperandName == null ? false : true;
        }
    }
}
