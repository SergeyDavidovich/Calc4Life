using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace Calc4Life.ViewModels
{
    public class EditConstPageViewModel : ViewModelBase
    {
        #region declarations

        private INavigationService _navigationService;
        private string _value;

        #endregion
        #region Constructors

        public EditConstPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            SaveCommand = new DelegateCommand(SaveExecute);
        }

        #endregion
        #region Commands

        public DelegateCommand SaveCommand { get; }
        private async void SaveExecute()
        {
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

        #endregion
    }
}
