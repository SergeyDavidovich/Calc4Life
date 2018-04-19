using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using System.Threading.Tasks;

namespace Calc4Life.ViewModels
{
    public class AboutPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        public AboutPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;

            NavigateCommand = new DelegateCommand(NavigateExecute);
        }

        public DelegateCommand NavigateCommand { get; }
        private void NavigateExecute()
        {
            Xamarin.Forms.Device.OpenUri(new Uri("mailto:writesd@hotmail.com?subject=Calc4Life%20Feedback"));
        }

       
    }
}
