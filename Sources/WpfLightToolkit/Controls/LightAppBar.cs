using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace WpfLightToolkit.Controls
{

    [TemplatePart(Name = "PART_More", Type = typeof(ToggleButton))]
    [TemplatePart(Name = "PART_Command", Type = typeof(ItemsControl))]
    public class LightAppBar : ContentControl
    {
        ToggleButton btnMore;

        public static readonly DependencyProperty PrimaryCommandsProperty = DependencyProperty.Register("PrimaryCommands", typeof(IEnumerable<FrameworkElement>), typeof(LightAppBar), new PropertyMetadata(new List<FrameworkElement>()));
        public static readonly DependencyProperty SecondaryCommandsProperty = DependencyProperty.Register("SecondaryCommands", typeof(IEnumerable<FrameworkElement>), typeof(LightAppBar), new PropertyMetadata(new List<FrameworkElement>()));

        public IEnumerable<FrameworkElement> PrimaryCommands
        {
            get { return (IEnumerable<FrameworkElement>)GetValue(PrimaryCommandsProperty); }
            set { SetValue(PrimaryCommandsProperty, value); }
        }

        public IEnumerable<FrameworkElement> SecondaryCommands
        {
            get { return (IEnumerable<FrameworkElement>)GetValue(SecondaryCommandsProperty); }
            set { SetValue(SecondaryCommandsProperty, value); }
        }
        
        public LightAppBar()
        {
            this.DefaultStyleKey = typeof(LightAppBar);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            btnMore = Template.FindName("PART_More", this) as ToggleButton;
        }

        public void Reset()
        {
            if(btnMore != null)
            {
                btnMore.IsChecked = false;
            }
        }
    }
}
