using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Calc4Life.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Calc4Life.Controls;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(CustomScrollView),
typeof(CustomScrollViewRenderer))]
namespace Calc4Life.Droid.CustomRenderers
{
    public class CustomScrollViewRenderer : ScrollViewRenderer
    {
        public CustomScrollViewRenderer() : base(Android.App.Application.Context)
        {
        }
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
                return;

            if (e.OldElement != null)
                e.OldElement.PropertyChanged -= OnElementPropertyChanged;

            e.NewElement.PropertyChanged += OnElementPropertyChanged;
        }

        protected void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.HorizontalScrollBarEnabled = ((CustomScrollView)this.Element).IsHorizontalScrollbarEnabled;
            this.VerticalScrollBarEnabled = ((CustomScrollView)this.Element).IsVerticalScrollbarEnabled;
        }
    }
}