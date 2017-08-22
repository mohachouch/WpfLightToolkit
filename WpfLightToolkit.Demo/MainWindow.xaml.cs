using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfLightToolkit.Controls;
using WpfLightToolkit.Demo.Pages.CarouselPage;
using WpfLightToolkit.Demo.Pages.ContentPage;
using WpfLightToolkit.Demo.Pages.TabbedPage;

namespace WpfLightToolkit.Demo
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : LightWindow
    {
        public MainWindow()
        {
            InitializeComponent();
			this.StartupPage = new RootPage();
		}
    }
}
