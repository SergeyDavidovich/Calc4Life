using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calc4Life.Validations
{
    /// <summary>
    /// Класс, который представляет валидационное свойство
    /// </summary>
    /// <typeparam name="T">Тип валидациногго свойства</typeparam>
    public class ValidatableObject<T> : ExtendedBindableObject, IValidity
    {
        private readonly List<IValidationRule<T>> _validations;
        private List<string> _errors;
        private T _value;
        private bool _isValid;
        
        /// <summary>
        /// Список валидационных правил
        /// Используйте данную коллекцию для добавления правил
        /// </summary>
        public List<IValidationRule<T>> Validations => _validations;

        /// <summary>
        /// Список ошибок
        /// </summary>
        public List<string> Errors
        {
            get
            {
                return _errors;
            }
            set
            {
                _errors = value;
                RaisePropertyChanged(() => Errors);
            }
        }

        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                RaisePropertyChanged(() => Value);
            }
        }

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
        }

        public ValidatableObject()
        {
            _isValid = true;
            _errors = new List<string>();
            _validations = new List<IValidationRule<T>>();
        }

        public bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = _validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return this.IsValid;
        }
    }
}
