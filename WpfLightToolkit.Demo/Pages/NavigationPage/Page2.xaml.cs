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
	/// Logique d'interaction pour Page2.xaml
	/// </summary>
	public partial class Page2 : LightContentPage
	{
		public Page2()
		{
			InitializeComponent();
		}

		private void Pop_Click(object sender, EventArgs e)
		{
			Navigation.Pop();
		}

		private void GoToPage3_Click(object sender, EventArgs e)
		{
			Navigation.Push(new Page3(), false);
		}
	}
}
