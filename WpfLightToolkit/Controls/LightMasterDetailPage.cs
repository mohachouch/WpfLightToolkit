using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfLightToolkit.Interfaces;
using WpfLightToolkit.Extensions;
using WpfLightToolkit.Loader;
using System.Windows.Media;

namespace WpfLightToolkit.Controls
{
	public class LightMasterDetailPage : LightPage
	{
		LightContentControl lightDetailContentControl;

		public static readonly DependencyProperty MasterPageProperty = DependencyProperty.Register("MasterPage", typeof(object), typeof(LightMasterDetailPage));
		public static readonly DependencyProperty DetailPageProperty = DependencyProperty.Register("DetailPage", typeof(object), typeof(LightMasterDetailPage));
		public static readonly DependencyProperty ContentLoaderProperty = DependencyProperty.Register("ContentLoader", typeof(IContentLoader), typeof(LightMasterDetailPage), new PropertyMetadata(new DefaultContentLoader()));
		public static readonly DependencyProperty IsPresentedProperty = DependencyProperty.Register("IsPresented", typeof(bool), typeof(LightMasterDetailPage));

		public object MasterPage
		{
			get { return (object)GetValue(MasterPageProperty); }
			set { SetValue(MasterPageProperty, value); }
		}

		public object DetailPage
		{
			get { return (object)GetValue(DetailPageProperty); }
			set { SetValue(DetailPageProperty, value); }
		}

		public bool IsPresented
		{
			get { return (bool)GetValue(IsPresentedProperty); }
			set { SetValue(IsPresentedProperty, value); }
		}

		public IContentLoader ContentLoader
		{
			get { return (IContentLoader)GetValue(ContentLoaderProperty); }
			set { SetValue(ContentLoaderProperty, value); }
		}

		public LightMasterDetailPage()
		{
			this.DefaultStyleKey = typeof(LightMasterDetailPage);
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			lightDetailContentControl = Template.FindName("PART_Detail_Content", this) as LightContentControl;
		}

		public override string GetTitle()
		{
			if (lightDetailContentControl != null && lightDetailContentControl.Content is LightPage page)
			{
				return page.GetTitle();
			}
			return this.Title;
		}

		public override Brush GetTitleBarBackgroundColor()
		{
			if (lightDetailContentControl != null && lightDetailContentControl.Content is LightPage page)
			{
				return page.GetTitleBarBackgroundColor();
			}
			return this.TitleBarBackgroundColor;
		}

		public override Brush GetTitleBarTextColor()
		{
			if (lightDetailContentControl != null && lightDetailContentControl.Content is LightPage page)
			{
				return page.GetTitleBarTextColor();
			}
			return this.TitleBarTextColor;
		}
	}
}
