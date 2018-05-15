using System;
using System.Collections.Generic;
using System.Text;
using Calc4Life.Models;

namespace Calc4Life.Services.OperationServices
{
    public interface IBinaryOperationService
    {
        #region Structure

        Operand? Operand1 { get; set; }
        Operand? Operand2 { get; set; }
        BinaryOperators? @Operator { get; set; }
        decimal? Result { get; }

        #endregion

        #region Input actions

        void SetOperand(Operand operand);
        void SetOperator(string @operator);
        void Clear();

        #endregion

        #region Output actions

        bool IsReadyForCalc();
        decimal? GetResult();
        string GetOperationExpression();

        #endregion
    }
}
