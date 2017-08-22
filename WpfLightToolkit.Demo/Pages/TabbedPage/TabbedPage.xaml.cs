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

namespace WpfLightToolkit.Demo.Pages.TabbedPage
{
	/// <summary>
	/// Logique d'interaction pour TabbedPage.xaml
	/// </summary>
	public partial class TabbedPage : LightTabbedPage
	{
		public TabbedPage()
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
		
		bool isDefaultBarColor = true;

		private void BarColorToolbarItem_Click(object sender, RoutedEventArgs e)
		{
			if (isDefaultBarColor)
				BarBackgroundColor = Brushes.Red;
			else
				ClearValue(BarBackgroundColorProperty);

			isDefaultBarColor = !isDefaultBarColor;
		}


		bool isDefaultTextBarColor = true;

		private void BarTextColorToolbarItem_Click(object sender, RoutedEventArgs e)
		{
			if (isDefaultTextBarColor)
				BarTextColor = Brushes.Yellow;
			else
				ClearValue(BarTextColorProperty);

			isDefaultTextBarColor = !isDefaultTextBarColor;
		}

		private void Home_Click(object sender, RoutedEventArgs e)
		{
			ParentWindow.StartupPage = new RootPage();
		}
	}
}
