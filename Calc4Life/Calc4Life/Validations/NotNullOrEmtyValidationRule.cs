using System;
using System.Collections.Generic;
using System.Text;

namespace Calc4Life.Validations
{
    /// <summary>
    /// Валидационное правило, которое проверяет заполнено ли свойство значение
    /// </summary>
    /// <typeparam name="T">Тип валидационного свойства</typeparam>    
    public class NotNullOrEmtyValidationRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;

            return !string.IsNullOrWhiteSpace(str);
        }
    }
}
