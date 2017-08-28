using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfLightToolkit.Interfaces;
using WpfLightToolkit.Loader;
using WpfLightToolkit.Extensions;

namespace WpfLightToolkit.Controls
{
	[TemplatePart(Name = "PART_Navigation_Content", Type = typeof(LightTransitioningContentControl))]
	public class LightNavigationPage : LightPage, ILightNavigation
    {
		public LightTransitioningContentControl LightContentControl { get; private set; }
		
		public ObservableCollection<object> InternalChildren { get; } = new ObservableCollection<object>();
        
        public static readonly DependencyProperty ContentLoaderProperty = DependencyProperty.Register("ContentLoader", typeof(IContentLoader), typeof(LightNavigationPage), new PropertyMetadata(new DefaultContentLoader()));
        public static readonly DependencyProperty CurrentPageProperty = DependencyProperty.Register("CurrentPage", typeof(object), typeof(LightNavigationPage));

		public IContentLoader ContentLoader
        {
            get { return (IContentLoader)GetValue(ContentLoaderProperty); }
            set { SetValue(ContentLoaderProperty, value); }
        }

        public object CurrentPage
        {
            get { return (object)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
		}
		
		public int StackDepth
        {
            get { return InternalChildren.Count; }
        }

        public LightNavigationPage()
        {
            this.DefaultStyleKey = typeof(LightNavigationPage);
        }
		
        public LightNavigationPage(object root)
            : this()
        {
			this.Push(root);
		}
		
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			LightContentControl = Template.FindName("PART_Navigation_Content", this) as LightTransitioningContentControl;
		}
		
		public void InsertPageBefore(object page, object before)
        {
			int index = InternalChildren.IndexOf(before);
			InternalChildren.Insert(index, page);
			ParentWindow?.SynchronizeAppBar();
		}

        public void RemovePage(object page)
        {
			if (InternalChildren.Remove(page))
			{
				if(LightContentControl != null)
				{
					LightContentControl.Transition = TransitionType.Normal;
				}
				CurrentPage = InternalChildren.Last();
			}

			ParentWindow?.SynchronizeAppBar();
		}

        public void Pop()
        {
			Pop(true);
		}

        public void Pop(bool animated)
        {
			if (StackDepth <= 1)
				return;

			if (InternalChildren.Remove(InternalChildren.Last()))
			{
				if(LightContentControl != null)
				{
					LightContentControl.Transition = animated ? TransitionType.Right : TransitionType.Normal;
				}
				CurrentPage = InternalChildren.Last();
			}
		}

        public void PopToRoot()
        {
			PopToRoot(true);
		}

        public void PopToRoot(bool animated)
        {
			if (StackDepth <= 1)
				return;

			object[] childrenToRemove = InternalChildren.Skip(1).ToArray();
			foreach (object child in childrenToRemove)
				InternalChildren.Remove(child);

			if (LightContentControl != null)
			{
				LightContentControl.Transition = animated ? TransitionType.Right : TransitionType.Normal;
			}
			CurrentPage = InternalChildren.Last();
		}

		public void Push(object page)
		{
			Push(page, true);
		}

		public void Push(object page, bool animated)
		{
			InternalChildren.Add(page);
			if(LightContentControl != null)
			{
				LightContentControl.Transition = animated ? TransitionType.Left : TransitionType.Normal;
			}
			CurrentPage = page;
		}

		public override string GetTitle()
        {
			if (LightContentControl != null && LightContentControl.Content is LightPage page)
			{
				return page.GetTitle();
			}
			return "";
		}

		public override bool GetHasNavigationBar()
		{
			if (LightContentControl != null && LightContentControl.Content is LightPage page)
			{
				return page.GetHasNavigationBar();
			}
			return false;
		}

		public bool GetHasBackButton()
		{
			if (LightContentControl != null && LightContentControl.Content is LightPage page)
			{
				return page.HasBackButton && StackDepth > 1;
			}
			return false;
		}

		public string GetBackButtonTitle()
		{
			if (StackDepth > 1)
			{
				return this.InternalChildren[StackDepth - 2].GetPropValue<string>("Title") ?? "Back";
			}
			return "";
		}
		
		public void PopModal()
		{
			PopModal(true);
		}

        public void PopModal(bool animated)
        {
			ParentWindow?.PopModal(animated);
        }
		
        public void PushModal(object page)
        {
			PushModal(page, true);
		}

        public void PushModal(object page, bool animated)
        {
			ParentWindow?.PushModal(page, animated);
        }


		public virtual void OnBackButtonPressed()
		{
			Pop();
		}
    }
}
