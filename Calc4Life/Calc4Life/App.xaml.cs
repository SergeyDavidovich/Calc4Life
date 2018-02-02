using Prism;
using Prism.Ioc;
using Calc4Life.ViewModels;
using Calc4Life.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Calc4Life
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");

            //var pageOne = new MainPage();
            //NavigationPage.SetHasNavigationBar(pageOne, true);
            //NavigationPage mypage = new NavigationPage(pageOne);

            //mypage.BackgroundColor = Color.Black;
            //mypage.BarBackgroundColor = Color.FromHex("#1976d2");
            //mypage.Title = "Calculator";
            //mypage.BarTextColor = Color.White;
            //mypage.BackgroundImage = "background.png";
            //mypage.Icon = "@drawable/icon";

            ////mypage.CurrentPage.Title = "Calc4life";
            //mypage.CurrentPage.IsBusy = true;
            //mypage.CurrentPage.BackgroundImage = "icon.png";

            //MainPage = mypage;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<OptionsTabbedPage>();
            containerRegistry.RegisterForNavigation<ConstantsContentPage>();
        }
    }
}
