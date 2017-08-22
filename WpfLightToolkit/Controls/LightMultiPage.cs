using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using WpfLightToolkit.Interfaces;
using WpfLightToolkit.Loader;

namespace WpfLightToolkit.Controls
{
	public class SelectionChangedEventArgs : EventArgs
	{
		public SelectionChangedEventArgs(object oldElement, object newElement)
		{
			OldElement = oldElement;
			NewElement = newElement;
		}

		public object NewElement { get; private set; }

		public object OldElement { get; private set; }
	}

	[ContentProperty("ItemsSource")]
	public class LightMultiPage : LightPage
	{
		public LightTransitioningContentControl LightContentControl { get; private set; }

		public event EventHandler<SelectionChangedEventArgs> SelectionChanged;

		public static readonly DependencyProperty ContentLoaderProperty = DependencyProperty.Register("ContentLoader", typeof(IContentLoader), typeof(LightMultiPage), new PropertyMetadata(new DefaultContentLoader()));
		public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<object>), typeof(LightMultiPage));
		public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(LightMultiPage), new PropertyMetadata(OnSelectedItemChanged));
		public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register("SelectedIndex", typeof(int), typeof(LightMultiPage), new PropertyMetadata(0));

		public IContentLoader ContentLoader
		{
			get { return (IContentLoader)GetValue(ContentLoaderProperty); }
			set { SetValue(ContentLoaderProperty, value); }
		}

		public ObservableCollection<object> ItemsSource
		{
			get { return (ObservableCollection<object>)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}

		public object SelectedItem
		{
			get { return (object)GetValue(SelectedItemProperty); }
			set { SetValue(SelectedItemProperty, value); }
		}

		public int SelectedIndex
		{
			get { return (int)GetValue(SelectedIndexProperty); }
			set { SetValue(SelectedIndexProperty, value); }
		}

		private static void OnSelectedItemChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			if (e.OldValue == e.NewValue) return;
			((LightMultiPage)o).OnSelectedItemChanged(e.OldValue, e.NewValue);
		}

		private void OnSelectedItemChanged(object oldValue, object newValue)
		{
			if (ItemsSource == null) return;
			SelectedIndex = ItemsSource.Cast<object>().ToList().IndexOf(newValue);
			SelectionChanged?.Invoke(this, new SelectionChangedEventArgs(oldValue, newValue));
		}

		public LightMultiPage()
		{
			SetValue(LightMultiPage.ItemsSourceProperty, new ObservableCollection<object>());
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			LightContentControl = Template.FindName("PART_Multi_Content", this) as LightTransitioningContentControl;
		}

		public override bool GetHasNavigationBar()
		{
			if (LightContentControl != null && LightContentControl.Content is LightPage page)
			{
				return page.GetHasNavigationBar();
			}
			return false;
		}
	}
}
