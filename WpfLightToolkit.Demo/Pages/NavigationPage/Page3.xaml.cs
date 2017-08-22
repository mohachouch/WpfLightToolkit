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
	/// Logique d'interaction pour Page3.xaml
	/// </summary>
	public partial class Page3 : LightContentPage
	{
		public Page3()
		{
			InitializeComponent();
		}

		private void RemovePage2_Click(object sender, EventArgs e)
		{
			/*var page2 = Navigation.NavigationStack.FirstOrDefault(x => x.Title == "Page 2");
			if (page2 != null)
			{
				Navigation.RemovePage(page2);
				(sender as Button).IsEnabled = false;
			}*/
		}

		private void Pop_Click(object sender, EventArgs e)
		{
			Navigation.Pop();
		}

		private void PopToRoot_Click(object sender, EventArgs e)
		{
			Navigation.PopToRoot();
		}
	}
}
