using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Prism.Navigation;

namespace Calc4Life.Views
{
    public partial class OptionsPage : TabbedPage, INavigatingAware
    {
        public OptionsPage()
        {
            InitializeComponent();
        }


        public void OnNavigatingTo(NavigationParameters parameters)
        {
            foreach (var child in this.Children)
            {
                Prism.Common.PageUtilities.OnNavigatingTo(child, parameters);
            }

        }
       
    }
}
