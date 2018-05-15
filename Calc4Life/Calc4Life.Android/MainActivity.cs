using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Plugin.InAppBilling;
using Prism;
using Prism.Ioc;

namespace Calc4Life.Droid
{
    [Activity(Label = "Calc4Life", 
        Icon = "@drawable/appicon_a", 
        Theme = "@style/MainTheme",
        MainLauncher = false,
        ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);


            Xamarin.Forms.Forms.SetFlags("FastRenderers_Experimental");

            global::Xamarin.Forms.Forms.Init(this, bundle);


            LoadApplication(new App(new AndroidInitializer()));

            Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity = this;
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            InAppBillingImplementation.HandleActivityResult(requestCode, resultCode, data);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry container)
        {
            // Register any platform specific implementations
        }
    }
}

