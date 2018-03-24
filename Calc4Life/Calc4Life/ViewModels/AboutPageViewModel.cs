using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;


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
        private async void NavigateExecute()
        {
          await  NavigationService.NavigateAsync(new Uri("https://www.facebook.com/writesd", UriKind.Absolute));
        }
    }
}
