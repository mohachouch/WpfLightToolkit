using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfLightToolkit.Controls
{
    public class LightAppBarButton : Button
    {
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(LightElementIcon), typeof(LightAppBarButton));
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(LightAppBarButton));
        
        public LightElementIcon Icon
        {
            get { return (LightElementIcon)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public LightAppBarButton()
        {
            this.DefaultStyleKey = typeof(LightAppBarButton);
        }
    }
}
