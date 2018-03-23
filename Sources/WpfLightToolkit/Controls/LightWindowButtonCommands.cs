using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfLightToolkit.Helpers;

namespace WpfLightToolkit.Controls
{
    [TemplatePart(Name = "PART_Min", Type = typeof(Button))]
    [TemplatePart(Name = "PART_Max", Type = typeof(Button))]
    [TemplatePart(Name = "PART_Close", Type = typeof(Button))]
    public class LightWindowButtonCommands : ContentControl
    {
        private Button min;
        private Button max;
        private Button close;

        private LightWindow _parentWindow;

        public LightWindow ParentWindow
        {
            get { return _parentWindow; }
            set
            {
                _parentWindow = value;
            }
        }

        public LightWindowButtonCommands()
        {
            this.DefaultStyleKey = typeof(LightWindowButtonCommands);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            close = Template.FindName("PART_Close", this) as Button;
            if (close != null)
            {
                close.Click += CloseClick;
            }

            max = Template.FindName("PART_Max", this) as Button;
            if (max != null)
            {
                max.Click += MaximizeClick;
            }

            min = Template.FindName("PART_Min", this) as Button;
            if (min != null)
            {
                min.Click += MinimizeClick;
            }
            this.ParentWindow = this.TryFindParent<LightWindow>();
        }

        private void MinimizeClick(object sender, RoutedEventArgs e)
        {
            if (null == this.ParentWindow) return;
            Microsoft.Windows.Shell.SystemCommands.MinimizeWindow(this.ParentWindow);
        }

        private void MaximizeClick(object sender, RoutedEventArgs e)
        {
            if (null == this.ParentWindow) return;
            if (this.ParentWindow.WindowState == WindowState.Maximized)
            {
                Microsoft.Windows.Shell.SystemCommands.RestoreWindow(this.ParentWindow);
            }
            else
            {
                Microsoft.Windows.Shell.SystemCommands.MaximizeWindow(this.ParentWindow);
            }
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            if (null == this.ParentWindow) return;
            this.ParentWindow.Close();
        }
    }
}
