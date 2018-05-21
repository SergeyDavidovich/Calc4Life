using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using System.Collections.ObjectModel;
using Calc4Life.Models;

namespace Calc4Life.ViewModels
{
    public class AboutPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        public AboutPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;

            NavigateCommand = new DelegateCommand(NavigateExecute);
            AboutMessages = new ObservableCollection<AboutMessage>();
            AboutMessages.Add(new AboutMessage() { IsAnswer = false, Text = "What a constant is?"});
            AboutMessages.Add(new AboutMessage() { IsAnswer = true, Text = "Constant is user data item has name and value. You may to store it directly in the calculator."});
            AboutMessages.Add(new AboutMessage() { IsAnswer = false, Text = "How to add a constant?"});
            AboutMessages.Add(new AboutMessage() { IsAnswer = true, Text = "Everywhere when you see the + button, click it, give the name and value. Then save this one."});
            AboutMessages.Add(new AboutMessage() { IsAnswer = false, Text = "How to use constants?"});
            AboutMessages.Add(new AboutMessage() { IsAnswer = true, Text = "Press the CONST button, select the constant and press the green button."});

        }
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            
        }
        private ObservableCollection<AboutMessage> _aboutMessages;
        public ObservableCollection<AboutMessage> AboutMessages
        {
            get => _aboutMessages;
            set => SetProperty(ref _aboutMessages, value);
        }
        public DelegateCommand NavigateCommand { get; }
        private void NavigateExecute()
        {
            Xamarin.Forms.Device.OpenUri(new Uri("mailto:writesd@hotmail.com?subject=Calc4Life%20Feedback"));
        }
    }
}
