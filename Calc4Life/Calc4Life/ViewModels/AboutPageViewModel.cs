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
            AboutMessages.Add(new AboutMessage() { IsAnswer = false, Text = "Что такое константа?" });
            AboutMessages.Add(new AboutMessage() { IsAnswer = true, Text = "Константы это постоянные значения в виде цены на товар или услугу. Константы позволяют хранить все значения прямо в калькуляторе" });
            AboutMessages.Add(new AboutMessage() { IsAnswer = false, Text = "Как добавить константу?" });
            AboutMessages.Add(new AboutMessage() { IsAnswer = true, Text = "Для этого нужно нажать ввести значения и нажать на кнопку +, дать имя константе и сохранить" });
            AboutMessages.Add(new AboutMessage() { IsAnswer = false, Text = "Как использовать константы при счёте?" });
            AboutMessages.Add(new AboutMessage() { IsAnswer = true, Text = "Нажмите на кнопку CONST, выберите константу и нажмите на зеленую кнопку" });

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
