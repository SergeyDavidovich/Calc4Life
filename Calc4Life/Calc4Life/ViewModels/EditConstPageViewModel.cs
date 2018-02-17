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
        private INavigationService _navigationService;

        public EditConstPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            SaveCommand = new DelegateCommand(SaveExecute);
        }

        public DelegateCommand SaveCommand { get; }
        private async void SaveExecute()
        {
            await _navigationService.GoBackAsync();
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            var par = parameters["value"];

        }
    }
}
