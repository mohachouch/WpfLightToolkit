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
using WpfLightToolkit.Demo.Pages.CarouselPage;
using WpfLightToolkit.Demo.Pages.ContentPage;
using WpfLightToolkit.Demo.Pages.MasterDetailPage;
using WpfLightToolkit.Demo.Pages.NavigationPage;
using WpfLightToolkit.Demo.Pages.TabbedPage;

namespace WpfLightToolkit.Demo
{
	/// <summary>
	/// Logique d'interaction pour RootPage.xaml
	/// </summary>
	public partial class RootPage : LightContentPage
	{
		public RootPage()
		{
			InitializeComponent();
		}

		private void BtnContentPageDemo_Clicked(object sender, RoutedEventArgs e)
		{
			ParentWindow.StartupPage = new ContentPage();
		}

		private void BtnTabbedPageDemo_Clicked(object sender, RoutedEventArgs e)
		{
			ParentWindow.StartupPage = new TabbedPage();
		}

		private void BtnCarouselPageDemo_Clicked(object sender, RoutedEventArgs e)
		{
			ParentWindow.StartupPage = new CarouselPage();
		}

		private void BtnNavigationPageDemo_Clicked(object sender, RoutedEventArgs e)
		{
			var startupPage = new LightNavigationPage(new Page1());

			LightAppBarButton appBarNav = new LightAppBarButton() { Label = "Navigation", Icon = new LightSymbolIcon() { Symbol = Symbol.GlobalNavigationButton } };
			startupPage.PrimaryTopBarCommands.Add(appBarNav);

			LightAppBarButton appBarButton = new LightAppBarButton() { Label = "Home", Icon = new LightSymbolIcon() { Symbol = Symbol.Home } };
			appBarButton.Click += (x, y) =>
			{
				ParentWindow.StartupPage = new RootPage();
			};
			startupPage.SecondaryTopBarCommands.Add(appBarButton);
			ParentWindow.StartupPage = startupPage;
		}

		private void BtnMasterDetailPageDemo_Clicked(object sender, RoutedEventArgs e)
		{
			ParentWindow.StartupPage = new MasterDetailPage();
		}

		private void BtnShowModalDemo_Clicked(object sender, RoutedEventArgs e)
		{
			Button button = new Button()
			{
				Content = "Pop",
				Width = 200,
				Height = 100
			};

			button.Click += (x, y) => 
			{
				Navigation.PopModal();
			};

			var lightPage = new LightContentPage() { Background = Brushes.Transparent, Content = button };
			LightAppBarButton appBarNav = new LightAppBarButton() { Label = "Navigation", Icon = new LightSymbolIcon() { Symbol = Symbol.GlobalNavigationButton } };

			appBarNav.Click += (x, y) =>
			{
				lightPage.Background = Brushes.White;
			};

			lightPage.PrimaryTopBarCommands.Add(appBarNav);

			Navigation.PushModal(lightPage);
		}
	}
}
