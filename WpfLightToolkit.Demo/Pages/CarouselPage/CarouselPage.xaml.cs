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
using WpfLightToolkit.Demo.Dialog;

namespace WpfLightToolkit.Demo.Pages.CarouselPage
{
	/// <summary>
	/// Logique d'interaction pour CarouselPage.xaml
	/// </summary>
	public partial class CarouselPage : LightCarouselPage
	{
		public CarouselPage()
		{
			InitializeComponent();
		}

		private void AddPageToolbarItem_Click(object sender, RoutedEventArgs e)
		{

			string dateTime = DateTime.Now.ToString();

			TextBlock label = new TextBlock()
			{
				Text = "New Page : " + dateTime,
				Foreground = Brushes.Black,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center,
				FontSize = 30
			};

			ItemsSource.Add(new LightContentPage() { Title = "New Page : " + dateTime, Content = label });
		}

		private void RemovePageToolbarItem_Click(object sender, RoutedEventArgs e)
		{
			if (ItemsSource.Count > 0)
				ItemsSource.Remove(ItemsSource.Last());
		}

		private void Home_Click(object sender, RoutedEventArgs e)
		{
			ParentWindow.StartupPage = new RootPage();
		}

	}
}
