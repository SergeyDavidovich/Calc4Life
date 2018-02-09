using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace Calc4Life.ViewModels
{

    public class OptionsPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

        public OptionsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
        }
        public async override void OnNavigatedTo(NavigationParameters parameters)
        {
            //base.OnNavigatingTo(parameters);
            //await _navigationService.NavigateAsync("AboutPage");
        }

    }
}
