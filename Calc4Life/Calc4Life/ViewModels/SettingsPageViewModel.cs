using System;
using System.Collections.Generic;
using System.Text;
using Calc4Life.Helpers;
using Calc4Life.Models;
using Calc4Life.Services.FormatServices;
using Calc4Life.Services.PurchasingServices;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace Calc4Life.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        #region Declarations

        private INavigationService _navigationService;
        private ConstantsPurchasingService _purchasingService;
        IPageDialogService _dialogService;

        decimal sampleValue = 12345.6789m;
        FormatService _formatService;

        List<Constant> Constants;
        #endregion
        #region Constructors

        public SettingsPageViewModel(INavigationService navigationService,
            FormatService formatService,
            ConstantsPurchasingService purchasingService,
            IPageDialogService dialogService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _formatService = formatService;
            _purchasingService = purchasingService;
            _dialogService = dialogService;

            SetDefaultCommang = new DelegateCommand(SetDefaultExecute);
            SaveCommand = new DelegateCommand(SaveExecute);
            PurchaseCommand = new DelegateCommand(PurchaseExecute, PurchaseCanExecute);

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

        bool _vibrateButtons;
        public bool VibrateButtons
        {
            get { return Settings.Vibration; }
            set
            {
                Settings.Vibration = value;
                SetProperty(ref _vibrateButtons, Settings.Vibration);
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

        decimal _roundAccuracy;
        public decimal RoundAccuracy
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
            Settings.RoundAccuracy = 2;
            Settings.Rounding = false;
            Settings.Vibration = true;

            GroupingDigits = Settings.GrouppingDigits;
            RoundAccuracy = Settings.RoundAccuracy;
            Rounding = Settings.Rounding;
            VibrateButtons = Settings.Vibration;

            Sample = _formatService.FormatResult(sampleValue);
        }

        public DelegateCommand SaveCommand { get; set; }
        private void SaveExecute()
        {
            NavigationService.GoBackAsync();
        }

        public DelegateCommand PurchaseCommand { get; }
        private async void PurchaseExecute()
        {
            if (Settings.ConstProductPurchased)
                await NavigationService.NavigateAsync("EditConstPage", null, false, true);
            else
            {
                bool purchased = await _purchasingService.PurchaseNonConsumableItem(AppConstants.CONSTANTS_PPODUCT_ID, "payload");
                //bool purchased = await _purchasingService.PurchaseNonConsumableItem("android.test.purchased", "payload");

                //string title, message;
                //if (purchased)
                //{
                    //title = "Congratulations!";
                    //message = " You succefully purchase the product";
                //}
                //else
                //{
                //    title = "Something has gone wrong";
                //    message = "Please, try it later ";
                //}
                //await _dialogService.DisplayAlertAsync(title, message, "OK");
            }
        }
        public bool PurchaseCanExecute()
        {
            return !Settings.ConstProductPurchased;
        }

        #endregion
        #region Navigation

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            this.PropertyChanged += SettingsPageViewModel_PropertyChanged;
            Constants = await App.Database.GetItemsAsync();
        }
        /// <summary>
        ///  каждый раз когда меняется свойство привязки(настройки калькулятора) отправляем сообщение 
        ///  об изменение настроек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsPageViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MessagingCenter.Send(this, AppConstants.SETTINGS_CHANGED_MESSAGE);
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            this.PropertyChanged -= SettingsPageViewModel_PropertyChanged;
            GroupingDigits = Settings.GrouppingDigits;
            Rounding = Settings.Rounding;
            RoundAccuracy = Settings.RoundAccuracy;
        }

        #endregion
    }
}
