using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Calc4Life.Services.RepositoryServices;
using System.Collections.ObjectModel;
using Calc4Life.Models;
using Xamarin.Forms;
using Prism.Services;
using System.Diagnostics;

namespace Calc4Life.ViewModels
{
    public class ConstantsPageViewModel : ViewModelBase
    {
        #region Declarations

        IConstantsRepositoryService _constantRepository;
        IPageDialogService _dialogService;
        ObservableCollection<Constant> _constants;
        Constant _selectedConstant;

        #endregion

        #region Constructors
        public ConstantsPageViewModel(INavigationService navigationService,
            IConstantsRepositoryService constantsRepository,
            IPageDialogService dialogService)
           : base(navigationService)
        {
            Title = "Constants";

            _constantRepository = constantsRepository;
            _dialogService = dialogService;

            NavigateToEditCommand = new DelegateCommand(NavigateToEditExecute);
<<<<<<< HEAD
            NavigateToCalcCommand = new DelegateCommand<Constant>(NavigateToCalcExecute, (s) => s != null);
            DeleteCommand = new DelegateCommand<Constant>(DeleteExecute, s => s != null);
=======
            NavigateToCalcCommand = new DelegateCommand(NavigateToCalcExecute, NavigateToCalcCanExecute);
            DeleteCommand = new DelegateCommand(DeleteExecute);

            MessagingCenter.Subscribe<ConstantsRepositoryServiceFake>(this, "deleted", ConstantDeleted);
>>>>>>> upstream/master
        }

        #endregion

        #region Commands

        public DelegateCommand NavigateToEditCommand { get; }
        private async void NavigateToEditExecute()
        {
            await NavigationService.NavigateAsync("EditConstPage", null, false, true);
        }
        public DelegateCommand<Constant> DeleteCommand { get; }
        public async void DeleteExecute(Constant selectedConstant)
        {
            if (selectedConstant == null) return;

            string title = "WARNING!";
            string message = $"Your are about deleting \"{selectedConstant.Name}\" \r\n\r\nDelete?";
            var answer = await _dialogService.DisplayAlertAsync(title, message, "Yes", "No");

            if (answer == true)
<<<<<<< HEAD
                Constants.Remove(selectedConstant);
            else return;
            //MessagingCenter.Subscribe<_constantRepository>()
=======
                await _constantRepository.DeleteAsync(SelectedConstant.Id);
>>>>>>> upstream/master
        }

        public DelegateCommand<Constant> NavigateToCalcCommand { get; }
        private async void NavigateToCalcExecute(Constant selectedConstant)
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("const", selectedConstant);
            await NavigationService.GoBackAsync(navigationParams);
        }
        private bool NavigateToCalcCanExecute()
        {
            if (SelectedConstant != null) return true;
            else return false;
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

        public Constant SelectedConstant
        {
            get { return _selectedConstant; }
            set { SetProperty(ref _selectedConstant, value); }
        }

        #endregion

        #region Messages actions
        /// <summary>
        /// message "deleted" from IConstRepositoryService
        /// </summary>
        private void ConstantDeleted(ConstantsRepositoryServiceFake sender)
        {
            Constants.Remove(SelectedConstant);
            SelectedConstant = null;

            //_dialogService.DisplayAlertAsync("SUCCESS!", "Constant deleted", "Close");
        }

        #endregion

    }
}
