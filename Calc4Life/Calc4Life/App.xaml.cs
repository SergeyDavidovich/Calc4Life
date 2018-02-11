using Prism;
using Prism.Ioc;
using Calc4Life.ViewModels;
using Calc4Life.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;
using Calc4Life.Data;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Calc4Life
{

    public partial class App : PrismApplication
    {
        static ConstItemDatabase database;

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

            Resources = new ResourceDictionary();
            Resources.Add("primaryBlue", Color.FromHex("0d47a1"));
            Resources.Add("colorTitle", Color.WhiteSmoke);

            var pageOne = new CalcPage();
            NavigationPage.SetHasNavigationBar(pageOne, true);
            NavigationPage navPage = new NavigationPage(pageOne);

            navPage.BarBackgroundColor = (Color)App.Current.Resources["primaryBlue"];
            navPage.BarTextColor = (Color)App.Current.Resources["colorTitle"];

            this.MainPage = navPage;

            await NavigationService.NavigateAsync("CalcPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<CalcPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<ConstantsPage>();
            containerRegistry.RegisterForNavigation<OptionsPage>();
            containerRegistry.RegisterForNavigation<AboutPage>();
            containerRegistry.RegisterForNavigation<SettingsPage>();
            containerRegistry.RegisterForNavigation<EditConstPage>();

        }
    }
}
