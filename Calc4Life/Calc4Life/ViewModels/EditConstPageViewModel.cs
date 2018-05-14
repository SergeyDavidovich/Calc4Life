using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using Calc4Life.Services.RepositoryServices;
using Calc4Life.Models;
using System.Globalization;
using Prism.Services;
using Calc4Life.Validations;
using Calc4Life.Helpers;

namespace Calc4Life.ViewModels
{
    public class EditConstPageViewModel : ViewModelBase
    {
        #region declarations

        INavigationService _navigationService;
        IConstantsRepositoryService _repositoryService;
        IPageDialogService _dialogService;

        int currentId = 0;

        //валидационные свойства(свойства, подверженные валидации)
        ValidatableObject<string> _value;
        ValidatableObject<string> _name;
        string _note;

        #endregion
        #region Constructors

        public EditConstPageViewModel(INavigationService navigationService,
            IConstantsRepositoryService repositoryService, IPageDialogService dialogService)
            : base(navigationService)
        {
            _navigationService = navigationService;

            //_repositoryService = repositoryService;
            _repositoryService = App.Database;

            _dialogService = dialogService;

            SaveCommand = new DelegateCommand(SaveExecute);
            //Команды, которые будут валидировать свойства каждый раз, когда пользователь вводит текст
            ValidateNameCommand = new DelegateCommand(() => ValidateName());
            ValidateValueCommand = new DelegateCommand(() => ValidateValue());

            //создаем экземпляры валидационных свойств
            _value = new ValidatableObject<string>();
            _name = new ValidatableObject<string>();
            //добавляем валидационные правила
            AddValidations();

        }

        #endregion
        #region Commands
        public DelegateCommand ValidateNameCommand { get; }
        public DelegateCommand ValidateValueCommand { get; }
        public DelegateCommand SaveCommand { get; }
        private async void SaveExecute()
        {
            if (!Validate()) return;

            var constant = new Constant { Id = currentId, Name = Name.Value, Value = decimal.Parse(Value.Value, CultureInfo.CurrentCulture), Note = Note };
            await _repositoryService.SaveAsync(constant);

            App.Current.Properties[AppConstants.KEY_CONSTANTS_NUMBER] = ((int)App.Current.Properties[AppConstants.KEY_CONSTANTS_NUMBER]) + 1;

            var parameters = new NavigationParameters();
            parameters.Add("const", constant);

            await _navigationService.GoBackAsync(parameters);
        }

        #endregion
        #region Navigation

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("value")) //  переход со страницы CalcPage
            {
                string par = (string)parameters["value"];
                Value.Value = par;
            }

            if (parameters.ContainsKey("edit")) //  переход со страницы ConstantsPage по команде Edit
            {
                Constant par = (Constant)parameters["edit"];
                currentId = par.Id;
                Value.Value = par.Value.ToString();
                Name.Value = par.Name;
                Note = par.Note;
            }
        }

        #endregion
        #region Bindable properties

        public ValidatableObject<string> Value
        {
            get { return _value; }
            set
            {
                SetProperty(ref _value, value);
            }
        }

        /// <summary>
        /// Имя константы
        /// </summary>
        public ValidatableObject<string> Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
            }
        }
        public string Note
        {
            get { return _note; }
            set { SetProperty(ref _note, value); }
        }
        #endregion
        #region Validation
        /// <summary>
        /// Создает и добавляет валидационные правила 
        /// </summary>
        private void AddValidations()
        {
            _name.Validations.Add(new NotNullOrEmtyValidationRule<string> { ValidationMessage = "Enter the constant's name!" });
            _value.Validations.Add(new NotNullOrEmtyValidationRule<string> { ValidationMessage = "Enter the constant's value!" });
        }
        /// <summary>
        /// Проверяет на валидность каждое свойство
        /// используем данный метод перед сохранением константы
        /// </summary>
        /// <returns>True - если все свойства валидны</returns>
        private bool Validate()
        {
            bool isValidName = ValidateName();
            bool isValidValue = ValidateValue();
            return isValidName && isValidValue;
        }
        //Валидирует имя константы
        private bool ValidateName()
        {
            return _name.Validate();
        }
        //Валидирует значение константы
        private bool ValidateValue()
        {
            return _value.Validate();
        }
        #endregion
    }
}
