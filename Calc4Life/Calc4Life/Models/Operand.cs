using System;
using System.Collections.Generic;
using System.Text;

namespace Calc4Life.Models
{

    public struct Operand
    {
        public decimal? OperandValue;
        public string OperandName;

        public Operand(decimal value, string name)
        {
            OperandValue = value;
            OperandName = name;
        }
        public bool IsConstant()
        {
            return OperandName == null ? false : true;
        }

    }
}
