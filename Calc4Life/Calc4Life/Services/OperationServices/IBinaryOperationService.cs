using System;
using System.Collections.Generic;
using System.Text;

namespace Calc4Life.Services.OperationServices
{
    public interface IBinaryOperationService
    {
        #region Structure

        double? Operand1 { set; }
        double? Operand2 { set; }
        BinaryOperators? @Operator { set; }

        #endregion

        #region Input actions

        void SetOperand(double operand);
        void SetOperator(string @operator);
        void Clear();

        #endregion

        #region Output actions

        bool IsReadyForCalc();
        double? Result();

        #endregion
    }
}
