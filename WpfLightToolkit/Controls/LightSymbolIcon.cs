using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfLightToolkit.Controls
{
    public class LightSymbolIcon : LightElementIcon
    {
        public static readonly DependencyProperty SymbolProperty = DependencyProperty.Register("Symbol", typeof(Symbol), typeof(LightSymbolIcon));

        public Symbol Symbol
        {
            get { return (Symbol)GetValue(SymbolProperty); }
            set { SetValue(SymbolProperty, value); }
        }

        public LightSymbolIcon()
        {
            this.DefaultStyleKey = typeof(LightSymbolIcon);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }

    public class LightElementIcon : Control
    {

    }
}