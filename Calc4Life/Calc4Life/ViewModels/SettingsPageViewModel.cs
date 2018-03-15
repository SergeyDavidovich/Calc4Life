using System;
using System.Collections.Generic;
using System.Text;
using Calc4Life.Helpers;
using Calc4Life.Services.FormatServices;
using Prism.Commands;
using Prism.Navigation;

namespace Calc4Life.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        #region Declarations

        private INavigationService _navigationService;
        double sampleValue = 1234567.5678;
        FormatService _formatService;
        #endregion
        #region Constructors

        public SettingsPageViewModel(INavigationService navigationService, FormatService formatService) : base(navigationService)
        {
            _navigationService = navigationService;
            _formatService = formatService;

            SetDefaultCommang = new DelegateCommand(SetDefaultExecute);
            SaveCommand = new DelegateCommand(SaveExecute);
            Sample = _formatService.FormatResult(sampleValue);
        }

        #endregion
        #region Binding propeties

        bool _grouppingDigits;
        public bool GroupingDigits
        {
            get { return _grouppingDigits; }
            set
            {
                Settings.GrouppingDigits = value;
                SetProperty(ref _grouppingDigits, Settings.GrouppingDigits);
                Sample = _formatService.FormatResult(sampleValue);
            }
        }

        string _sample;
        public string Sample
        {
            get { return _sample; }
            set { SetProperty(ref _sample, value); }
        }

        bool _rounding;
        public bool Rounding
        {
            get { return _rounding; }
            set
            {
                Settings.Rounding = value;
                SetProperty(ref _rounding, Settings.Rounding);
                Sample = _formatService.FormatResult(sampleValue);
            }
        }

        double _roundAccuracy;
        public double RoundAccuracy
        {
            get { return _roundAccuracy; }
            set
            {
                Settings.RoundAccuracy = value;
                SetProperty(ref _roundAccuracy, value);
                Sample = _formatService.FormatResult(sampleValue);
            }
        }

        #endregion
        #region Commands

        public DelegateCommand SetDefaultCommang { get; set; }
        private void SetDefaultExecute()
        {
            Settings.GrouppingDigits = true;
            Settings.RoundAccuracy = 2.0;
            Settings.Rounding = false;

            GroupingDigits = Settings.GrouppingDigits;
            RoundAccuracy = Settings.RoundAccuracy;
            Rounding = Settings.Rounding;

            Sample = _formatService.FormatResult(sampleValue);
        }

        public DelegateCommand SaveCommand { get; set; }
        private void SaveExecute()
        {
            NavigationService.GoBackAsync();
        }

        #endregion
        #region Navigation

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            GroupingDigits = Settings.GrouppingDigits;
            Rounding = Settings.Rounding;
            RoundAccuracy = Settings.RoundAccuracy;
        }

        #endregion
    }
}
