using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfLightToolkit.Interfaces;
using WpfLightToolkit.Extensions;
using WpfLightToolkit.Loader;

namespace WpfLightToolkit.Controls
{
	public class LightMasterDetailPage : LightPage
	{
		LightContentControl lightContentControl;

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
			lightContentControl = Template.FindName("PART_Detail_Content", this) as LightContentControl;
		}

		public override string GetTitle()
		{
			if (lightContentControl != null && lightContentControl.Content is LightPage page)
			{
				return page.GetTitle();
			}
			return "";
		}
	}
}
