using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfLightToolkit.Controls
{
    public class LightTabbedPage : LightMultiPage
    {
		public static readonly DependencyProperty BarBackgroundColorProperty = DependencyProperty.Register("BarBackgroundColor", typeof(Brush), typeof(LightTabbedPage));
        public static readonly DependencyProperty BarTextColorProperty = DependencyProperty.Register("BarTextColor", typeof(Brush), typeof(LightTabbedPage));
		
        public Brush BarBackgroundColor
        {
            get { return (Brush)GetValue(BarBackgroundColorProperty); }
            set { SetValue(BarBackgroundColorProperty, value); }
        }

        public Brush BarTextColor
        {
            get { return (Brush)GetValue(BarTextColorProperty); }
            set { SetValue(BarTextColorProperty, value); }
        }
		
        public LightTabbedPage()
        {
            this.DefaultStyleKey = typeof(LightTabbedPage);
        }
    }
}
