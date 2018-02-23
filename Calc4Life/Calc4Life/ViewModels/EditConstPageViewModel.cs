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

namespace Calc4Life.ViewModels
{
    public class EditConstPageViewModel : ViewModelBase
    {
        #region declarations

        INavigationService _navigationService;
        IConstantsRepositoryService _repositoryService;
        IPageDialogService _dialogService;
        string _value;
        string _name;
        string _note;

        #endregion
        #region Constructors

        public EditConstPageViewModel(INavigationService navigationService, 
            IConstantsRepositoryService repositoryService, IPageDialogService dialogService) 
            : base(navigationService)
        {
            _navigationService = navigationService;
            _repositoryService = repositoryService;
            _dialogService = dialogService;

            SaveCommand = new DelegateCommand(SaveExecute);
        }

        #endregion
        #region Commands

        public DelegateCommand SaveCommand { get; }
        private async void SaveExecute()
        {
                await _repositoryService.AddAsync(
                    new Constant { Name = Name, Value = Double.Parse(Value, CultureInfo.CurrentCulture), Note = Note });
                await _navigationService.GoBackAsync();
        }

        #endregion
        #region Navigation

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            string par =(string)parameters["value"];
            Value = par;
        }

        #endregion
        #region Bindable properties

        public string Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        public string Note
        {
            get { return _note; }
            set { SetProperty(ref _note, value); }
        }
        #endregion
    }
}
