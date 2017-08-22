using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfLightToolkit.Controls
{
    public class LightBitmapIcon : LightElementIcon
    {
        public static readonly DependencyProperty UriSourceProperty = DependencyProperty.Register("UriSource", typeof(Uri), typeof(LightBitmapIcon), new PropertyMetadata(OnSourceChanged));

        public Uri UriSource
        {
            get { return (Uri)GetValue(UriSourceProperty); }
            set { SetValue(UriSourceProperty, value); }
        }

        public LightBitmapIcon()
        {
            this.DefaultStyleKey = typeof(LightBitmapIcon);
           
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }


        private static void OnSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((LightBitmapIcon)o).OnSourceChanged(e.OldValue, e.NewValue);
        }

        private void OnSourceChanged(object oldValue, object newValue)
        {
            if(newValue is Uri uri && !uri.IsAbsoluteUri)
            {
                var name = Assembly.GetEntryAssembly().GetName().Name;
                UriSource = new Uri(string.Format("pack://application:,,,/{0};component/{1}", name, uri.OriginalString));
            }
        }
    }
}
