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
            
            if(!IsAlreadyAnimated)
            {
                int translationY = 0;
                foreach (var item in Vm.AboutMessages)
                {
                    switch (item.IsAnswer)
                    {
                        case true:
                            var cell = new AnswerViewCell() { HorizontalOptions = LayoutOptions.StartAndExpand, WidthRequest = 350 };
                            cell.Opacity = 0;
                            chatLayout.Children.Add(cell);
                            cell.Opacity = 100;
                            await ViewExtensions.TranslateTo(cell, 0, translationY, 1000);
                            translationY += 70;
                            break;
                        case false:
                            var cell2 = new QuestionViewCell() { HorizontalOptions = LayoutOptions.StartAndExpand, WidthRequest = 350 };
                            cell2.Opacity = 0;
                            chatLayout.Children.Add(cell2);
                            cell2.Opacity = 100;
                            await ViewExtensions.TranslateTo(cell2, 0, translationY, 1000);
                            translationY += 70;
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
