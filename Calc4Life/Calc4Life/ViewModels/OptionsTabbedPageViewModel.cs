using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calc4Life.ViewModels
{
    public class OptionsTabbedPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

        public OptionsTabbedPageViewModel(INavigationService navigationService) :base(navigationService)
        {
            _navigationService = navigationService;
        }

       
    }
}
