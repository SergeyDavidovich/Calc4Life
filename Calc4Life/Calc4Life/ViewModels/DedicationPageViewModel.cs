using System;
using System.Collections.Generic;
using System.Text;
using Calc4Life.Services.RepositoryServices;
using Prism.Navigation;
using Prism.Services;


namespace Calc4Life.ViewModels
{
    public class DedicationPageViewModel : ViewModelBase
    {
        DedicationService _dedicationService;
        public DedicationPageViewModel(INavigationService navigationService, DedicationService dedicationService)
            : base(navigationService)
        {
            _dedicationService = dedicationService;
        }
        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            if (parameters.ContainsKey("code")) //  переход со страницы CalcPage
            {
                string par =(string)parameters["code"];
                DedicationContent = _dedicationService.GetDedicationContent(par);
                DedicationName=_dedicationService.GetDedicationName(par);
            }
        }
        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        private string _DedicationContent;
        public string DedicationContent
        {
            get { return _DedicationContent; }
            set { SetProperty(ref _DedicationContent, value); }
        }
        private string _DedicationName;
        public string DedicationName
        {
            get { return _DedicationName; }
            set { SetProperty(ref _DedicationName, value); }
        }
    }
}
