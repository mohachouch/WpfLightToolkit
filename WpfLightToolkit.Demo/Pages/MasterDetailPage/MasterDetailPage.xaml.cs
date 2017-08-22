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

namespace WpfLightToolkit.Demo.Pages.MasterDetailPage
{
	/// <summary>
	/// Logique d'interaction pour MasterDetailPage.xaml
	/// </summary>
	public partial class MasterDetailPage : LightMasterDetailPage
	{
		public MasterDetailPage()
		{
			InitializeComponent();
		}

		private void Home_Click(object sender, RoutedEventArgs e)
		{
			ParentWindow.StartupPage = new RootPage();
		}
	}
}
