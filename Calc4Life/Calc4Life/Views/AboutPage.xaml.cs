using Calc4Life.ViewModels;
using Calc4Life.Views.CustomCells;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calc4Life.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        public bool IsAlreadyAnimated { get; set; }

        public AboutPageViewModel Vm => this.BindingContext as AboutPageViewModel;

        protected async override void OnAppearing()
        {
            if (!IsAlreadyAnimated)
            {
                double translationY = 0;
                foreach (var item in Vm.AboutMessages)
                {
                    switch (item.IsAnswer)
                    {
                        case true:
                            var cell = new AnswerViewCell() { HorizontalOptions = LayoutOptions.StartAndExpand, WidthRequest = 350, Text = item.Text };
                            cell.Opacity = 0;
                            chatLayout.Children.Add(cell, new Point(0, translationY));
                            await ViewExtensions.FadeTo(cell, 1, 700, Easing.SinIn);
                            translationY += chatLayout.Children[chatLayout.Children.Count - 1].Height;
                            break;
                        case false:
                            var cell2 = new QuestionViewCell() { HorizontalOptions = LayoutOptions.StartAndExpand, WidthRequest = 350, Text = item.Text };
                            cell2.Opacity = 0;
                            chatLayout.Children.Add(cell2, new Point(0, translationY));
                            await ViewExtensions.FadeTo(cell2, 1, 700, Easing.SinIn);
                            translationY += chatLayout.Children[chatLayout.Children.Count - 1].Height;
                            break;
                        default:
                            break;
                    }
                }
            }
            IsAlreadyAnimated = true;
            base.OnAppearing();
        }
    }
}
