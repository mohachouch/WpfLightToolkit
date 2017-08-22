using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfLightToolkit.Controls
{
	public class LightCarouselPage : LightMultiPage
	{
		public static RoutedUICommand NextCommand = new RoutedUICommand("Next", "Next", typeof(LightCarouselPage));
		public static RoutedUICommand PreviousCommand = new RoutedUICommand("Previous", "Previous", typeof(LightCarouselPage));


		static LightCarouselPage()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(LightCarouselPage), new FrameworkPropertyMetadata(typeof(LightCarouselPage)));
			SelectedIndexProperty.OverrideMetadata(typeof(LightCarouselPage), new FrameworkPropertyMetadata(-1, OnSelectedIndexChanged));
		}

		public LightCarouselPage()
		{
			this.CommandBindings.Add(new CommandBinding(NextCommand, this.OnNextExecuted, this.OnNextCanExecute));
			this.CommandBindings.Add(new CommandBinding(PreviousCommand, this.OnPreviousExecuted, this.OnPreviousCanExecute));
		}

		protected override void Appearing()
		{
			base.Appearing();
			this.SelectedIndex = 0;
		}

		private void OnPreviousCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = this.SelectedIndex > 0;
		}

		private void OnPreviousExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			this.SelectedIndex -= 1;
			LightContentControl.Transition = TransitionType.Right;
		}

		private void OnNextCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			if (this.ItemsSource == null) return;
			e.CanExecute = this.SelectedIndex < (this.ItemsSource.Cast<object>().Count() - 1);
		}

		private void OnNextExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			this.SelectedIndex += 1;
			LightContentControl.Transition = TransitionType.Left;
		}
		
		private static void OnSelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var control = d as LightCarouselPage;

			control.OnSelectedIndexChanged(e);
		}

		private void OnSelectedIndexChanged(DependencyPropertyChangedEventArgs e)
		{
			if (this.ItemsSource == null) return;
			var items = this.ItemsSource.Cast<object>();

			if ((int)e.NewValue >= 0 && (int)e.NewValue < items.Count())
			{
				this.SelectedItem = items.ElementAt((int)e.NewValue);
			}
		}
	}
}
