using System;
using System.Collections.Generic;
using System.Text;

namespace Calc4Life.Validations
{
    /// <summary>
    /// Интерфейс для валидационного правила 
    /// Реализуйте его при создании валидационных правил
    /// </summary>
    /// <typeparam name="T">Тип проверяемого свойства</typeparam>
    public interface IValidationRule<T>
    {
        /// <summary>
        /// Сообщение при невалидности
        /// </summary>
        string ValidationMessage { get; set; }

        /// <summary>
        /// Проверка на валидность
        /// </summary>
        /// <param name="value">Значение проверяемого свойства</param>
        /// <returns>True - валидно</returns>
        bool Check(T value);
    }
}
