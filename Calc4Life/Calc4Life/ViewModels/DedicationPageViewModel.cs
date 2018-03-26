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
                DedicationText = _dedicationService.GetDedication(par);
            }
        }
        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        private string _DedicationText;
        public string DedicationText
        {
            get { return _DedicationText; }
            set { SetProperty(ref _DedicationText, value); }
        }
    }
}
