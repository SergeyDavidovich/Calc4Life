using Calc4Life.Controls;
using Calc4Life.iOS.CustomRenderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomScrollView),
typeof(CustomScrollViewRenderer))]
namespace Calc4Life.iOS.CustomRenderers
{
    public class CustomScrollViewRenderer : ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
                return;

            if (e.OldElement != null)
                e.OldElement.PropertyChanged -= OnElementPropertyChanged;
            e.NewElement.PropertyChanged += OnElementPropertyChanged;
        }

        private void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ShowsHorizontalScrollIndicator = ((CustomScrollView)this.Element).IsHorizontalScrollbarEnabled;
            ShowsVerticalScrollIndicator = ((CustomScrollView)this.Element).IsVerticalScrollbarEnabled;
        }
    }
}
