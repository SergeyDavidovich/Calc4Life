using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calc4Life.ViewModels
{
    public class ConstantsContentPageViewModel : ViewModelBase
    {
        public ConstantsContentPageViewModel(INavigationService navigationService)
           : base(navigationService)
        {
            Title = "Constants";
        }
    }
}
