using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Calc4Life.Controls
{
    public class CustomScrollView : ScrollView
    {
        public static readonly BindableProperty IsHorizontalScrollbarEnabledProperty =
   BindableProperty.Create(
       nameof(IsHorizontalScrollbarEnabled),
       typeof(bool),
       typeof(CustomScrollView),
       true,
       BindingMode.Default,
       null);
        /// <summary>
        /// Gets or sets the Horizontal scrollbar visibility
        /// </summary>
        public bool IsHorizontalScrollbarEnabled
        {
            get { return (bool)GetValue(IsHorizontalScrollbarEnabledProperty); }
            set { SetValue(IsHorizontalScrollbarEnabledProperty, value); }
        }


        public static readonly BindableProperty IsVerticalScrollbarEnabledProperty =
        BindableProperty.Create(
            nameof(IsVerticalScrollbarEnabled),
            typeof(bool),
            typeof(CustomScrollView),
            false,
            BindingMode.Default,
            null);
        /// <summary>
        /// Gets or sets the Vertical scrollbar visibility
        /// </summary>
        public bool IsVerticalScrollbarEnabled
        {
            get { return (bool)GetValue(IsVerticalScrollbarEnabledProperty); }
            set { SetValue(IsVerticalScrollbarEnabledProperty, value); }
        }
    }
}
