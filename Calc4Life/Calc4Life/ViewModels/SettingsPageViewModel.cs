using System;
using System.Collections.Generic;
using System.Text;
using Prism.Navigation;

namespace Calc4Life.ViewModels
{

    public class SettingsPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        public SettingsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
