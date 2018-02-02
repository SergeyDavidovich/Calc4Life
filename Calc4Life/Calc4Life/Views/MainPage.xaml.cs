using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calc4Life.Views
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}
         async void ButtonConst_ClickAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConstantsPage(),true);
        }
	}
}
