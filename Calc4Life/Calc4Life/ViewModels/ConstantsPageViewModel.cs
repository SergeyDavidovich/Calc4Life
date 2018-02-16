using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Calc4Life.Services.RepositoryServices;
using System.Collections.ObjectModel;
using Calc4Life.Models;

namespace Calc4Life.ViewModels
{
    public class ConstantsPageViewModel : ViewModelBase
    {
        #region Declarations

        IConstantsRepositoryService _constantRepository;
        ObservableCollection<Constant> _constants;

        #endregion

        #region Constructors
        public ConstantsPageViewModel(INavigationService navigationService, IConstantsRepositoryService constantsRepository)
           : base(navigationService)
        {
            Title = "Constants";

            _constantRepository = constantsRepository;

            NavigateToEditCommand = new DelegateCommand(NavigateToEditExecute);
            NavigateToCalcCommand = new DelegateCommand(NavigateToCalcExecute);
        }

        #endregion

        #region Commands

        public DelegateCommand NavigateToEditCommand { get; }
        private async void NavigateToEditExecute()
        {
            await NavigationService.NavigateAsync("EditConstPage", null, false, true);
        }

        public DelegateCommand NavigateToCalcCommand { get; }
        private async void NavigateToCalcExecute()
        {
            await NavigationService.NavigateAsync("CalcPage", null, false, true);
        }

        #endregion

        #region Navigation
        public async override void OnNavigatedTo(NavigationParameters parameters)
        {
            var list = await _constantRepository.GetAllAsync();
            Constants = new ObservableCollection<Constant>(list);
        }
        #endregion

        #region Bindable properties

        
        public ObservableCollection<Constant> Constants
        {
            get { return _constants; }
            set { SetProperty(ref _constants, value); }
        }
        #endregion

    }
}
