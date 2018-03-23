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

namespace WpfLightToolkit.Demo.Pages.NavigationPage
{
	/// <summary>
	/// Logique d'interaction pour Page1.xaml
	/// </summary>
	public partial class Page1 : LightContentPage
	{
		public Page1()
		{
			InitializeComponent();
		}

		private void GoToPage2_Click(object sender, RoutedEventArgs e)
		{
			Navigation.Push(new Page2());
		}
	}
}
