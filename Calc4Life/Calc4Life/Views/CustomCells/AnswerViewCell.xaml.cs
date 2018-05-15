using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calc4Life.Views.CustomCells
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AnswerViewCell : ContentView
	{
		public AnswerViewCell ()
		{
			InitializeComponent ();
		}


        public static readonly BindableProperty TextProperty =
      BindableProperty.Create("Text", typeof(string), typeof(AnswerViewCell),
                  default(string));


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

    }
}