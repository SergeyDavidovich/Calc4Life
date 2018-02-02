using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calc4Life.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConstantsPage : ContentPage
    {
        public ObservableCollection<string> Consts { get; set; }

        public ConstantsPage()
        {
            InitializeComponent();

            Consts = new ObservableCollection<string>
            {
                "Constant 1",
                "Constant 2",
                "Constant 3",
                "Constant 4",
                "Constant 1",
                "Constant 2",
                "Constant 3",
                "Constant 4",
                "Constant 1",
                "Constant 2",
                "Constant 3",
                "Constant 4",
                "Constant 1",
                "Constant 2",
                "Constant 3",
                "Constant 4",
                "Constant 1",
                "Constant 2",
                "Constant 3",
                "Constant 4",
                "Constant 1",
                "Constant 2",
                "Constant 3",
                "Constant 4",
                "Constant 5"
            };
			
			MyListView.ItemsSource = Consts;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            //Deselect Const
            ((ListView)sender).SelectedItem = null;
           await Navigation.PopAsync(true);
        }
         void EntryFocused(object sender, EventArgs e)
        {
            var element = (Entry)sender;
            element.Text = "";
            element.TextColor = Color.Black;
        }
    }
}
