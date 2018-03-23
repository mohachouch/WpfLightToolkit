using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfLightToolkit.Interfaces;
using WpfLightToolkit.Helpers;
using System.ComponentModel;
using System.Windows.Media;

namespace WpfLightToolkit.Controls
{
    public class LightPage : UserControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(LightPage));
		public static readonly DependencyProperty BackButtonTitleProperty = DependencyProperty.Register("BackButtonTitle", typeof(string), typeof(LightPage));
		public static readonly DependencyProperty HasNavigationBarProperty = DependencyProperty.Register("HasNavigationBar", typeof(bool), typeof(LightPage), new PropertyMetadata(true));
		public static readonly DependencyProperty HasBackButtonProperty = DependencyProperty.Register("HasBackButton", typeof(bool), typeof(LightPage), new PropertyMetadata(true));
		public static readonly DependencyProperty PrimaryTopBarCommandsProperty = DependencyProperty.Register("PrimaryTopBarCommands", typeof(ObservableCollection<FrameworkElement>), typeof(LightPage));
        public static readonly DependencyProperty SecondaryTopBarCommandsProperty = DependencyProperty.Register("SecondaryTopBarCommands", typeof(ObservableCollection<FrameworkElement>), typeof(LightPage));
        public static readonly DependencyProperty PrimaryBottomBarCommandsProperty = DependencyProperty.Register("PrimaryBottomBarCommands", typeof(ObservableCollection<FrameworkElement>), typeof(LightPage));
        public static readonly DependencyProperty SecondaryBottomBarCommandsProperty = DependencyProperty.Register("SecondaryBottomBarCommands", typeof(ObservableCollection<FrameworkElement>), typeof(LightPage));
        public static readonly DependencyProperty ContentBottomBarProperty = DependencyProperty.Register("ContentBottomBar", typeof(object), typeof(LightPage));
		public static readonly DependencyProperty TitleBarBackgroundColorProperty = DependencyProperty.Register("TitleBarBackgroundColor", typeof(Brush), typeof(LightPage));
		public static readonly DependencyProperty TitleBarTextColorProperty = DependencyProperty.Register("TitleBarTextColor", typeof(Brush), typeof(LightPage));

		public Brush TitleBarBackgroundColor
		{
			get { return (Brush)GetValue(TitleBarBackgroundColorProperty); }
			set { SetValue(TitleBarBackgroundColorProperty, value); }
		}

		public Brush TitleBarTextColor
		{
			get { return (Brush)GetValue(TitleBarTextColorProperty); }
			set { SetValue(TitleBarTextColorProperty, value); }
		}

		public string Title
		{
			get { return (string)GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
		}

		public string BackButtonTitle
		{
			get { return (string)GetValue(BackButtonTitleProperty); }
			set { SetValue(BackButtonTitleProperty, value); }
		}

		public bool HasNavigationBar
		{
			get { return (bool)GetValue(HasNavigationBarProperty); }
			set { SetValue(HasNavigationBarProperty, value); }
		}

		public bool HasBackButton
		{
			get { return (bool)GetValue(HasBackButtonProperty); }
			set { SetValue(HasBackButtonProperty, value); }
		}

		public ObservableCollection<FrameworkElement> PrimaryTopBarCommands
        {
            get { return (ObservableCollection<FrameworkElement>)GetValue(PrimaryTopBarCommandsProperty); }
            set { SetValue(PrimaryTopBarCommandsProperty, value); }
        }

        public ObservableCollection<FrameworkElement> SecondaryTopBarCommands
        {
            get { return (ObservableCollection<FrameworkElement>)GetValue(SecondaryTopBarCommandsProperty); }
            set { SetValue(SecondaryTopBarCommandsProperty, value); }
        }

        public ObservableCollection<FrameworkElement> PrimaryBottomBarCommands
        {
            get { return (ObservableCollection<FrameworkElement>)GetValue(PrimaryBottomBarCommandsProperty); }
            set { SetValue(PrimaryBottomBarCommandsProperty, value); }
        }

        public ObservableCollection<FrameworkElement> SecondaryBottomBarCommands
        {
            get { return (ObservableCollection<FrameworkElement>)GetValue(SecondaryBottomBarCommandsProperty); }
            set { SetValue(SecondaryBottomBarCommandsProperty, value); }
        }
        
        public object ContentBottomBar
        {
            get { return (object)GetValue(ContentBottomBarProperty); }
            set { SetValue(ContentBottomBarProperty, value); }
        }

        public ILightNavigation Navigation
        {
            get
            {
                ILightNavigation nav = this.TryFindParent<LightNavigationPage>();
                return nav ?? new DefaultNavigation();
            }
        }

        public LightWindow ParentWindow
        {
            get
            {
                if (Application.Current.MainWindow is LightWindow parentWindow)
                    return parentWindow;
                return null;
            }
        }

        public LightPage()
        {
            this.SetValue(LightPage.PrimaryTopBarCommandsProperty, new ObservableCollection<FrameworkElement>());
            this.SetValue(LightPage.SecondaryTopBarCommandsProperty, new ObservableCollection<FrameworkElement>());
            this.SetValue(LightPage.PrimaryBottomBarCommandsProperty, new ObservableCollection<FrameworkElement>());
            this.SetValue(LightPage.SecondaryBottomBarCommandsProperty, new ObservableCollection<FrameworkElement>());

            this.Loaded += (sender, e) => Appearing();
            this.Unloaded += (sender, e) => Disappearing();
		}

		private void OnPropertyChanged(object sender, EventArgs arg)
		{
			ParentWindow?.SynchronizeAppBar();
		}

		private void Commands_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			ParentWindow?.SynchronizeToolbarCommands();
		}
        
        protected virtual void Appearing()
        {
            this.PrimaryTopBarCommands.CollectionChanged += Commands_CollectionChanged;
            this.SecondaryTopBarCommands.CollectionChanged += Commands_CollectionChanged;
            this.PrimaryBottomBarCommands.CollectionChanged += Commands_CollectionChanged;
            this.SecondaryBottomBarCommands.CollectionChanged += Commands_CollectionChanged;
			DependencyPropertyDescriptor.FromProperty(LightPage.TitleProperty, typeof(LightPage)).AddValueChanged(this, OnPropertyChanged);
			DependencyPropertyDescriptor.FromProperty(LightPage.HasBackButtonProperty, typeof(LightPage)).AddValueChanged(this, OnPropertyChanged);
			DependencyPropertyDescriptor.FromProperty(LightPage.HasNavigationBarProperty, typeof(LightPage)).AddValueChanged(this, OnPropertyChanged);
			DependencyPropertyDescriptor.FromProperty(LightPage.BackButtonTitleProperty, typeof(LightPage)).AddValueChanged(this, OnPropertyChanged);
			DependencyPropertyDescriptor.FromProperty(LightPage.TitleBarBackgroundColorProperty, typeof(LightPage)).AddValueChanged(this, OnPropertyChanged);
			DependencyPropertyDescriptor.FromProperty(LightPage.TitleBarTextColorProperty, typeof(LightPage)).AddValueChanged(this, OnPropertyChanged);
			ParentWindow?.SynchronizeToolbarCommands();
			ParentWindow?.SynchronizeAppBar();
		}

        protected virtual void Disappearing()
        {
            this.PrimaryTopBarCommands.CollectionChanged -= Commands_CollectionChanged;
            this.SecondaryTopBarCommands.CollectionChanged -= Commands_CollectionChanged;
            this.PrimaryBottomBarCommands.CollectionChanged -= Commands_CollectionChanged;
            this.SecondaryBottomBarCommands.CollectionChanged -= Commands_CollectionChanged;
			DependencyPropertyDescriptor.FromProperty(LightPage.TitleProperty, typeof(LightPage)).RemoveValueChanged(this, OnPropertyChanged);
			DependencyPropertyDescriptor.FromProperty(LightPage.HasBackButtonProperty, typeof(LightPage)).RemoveValueChanged(this, OnPropertyChanged);
			DependencyPropertyDescriptor.FromProperty(LightPage.HasNavigationBarProperty, typeof(LightPage)).RemoveValueChanged(this, OnPropertyChanged);
			DependencyPropertyDescriptor.FromProperty(LightPage.BackButtonTitleProperty, typeof(LightPage)).RemoveValueChanged(this, OnPropertyChanged);
			DependencyPropertyDescriptor.FromProperty(LightPage.TitleBarBackgroundColorProperty, typeof(LightPage)).RemoveValueChanged(this, OnPropertyChanged);
			DependencyPropertyDescriptor.FromProperty(LightPage.TitleBarTextColorProperty, typeof(LightPage)).RemoveValueChanged(this, OnPropertyChanged);
		}

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
		
        public virtual string GetTitle()
        {
            return this.Title;
        }

		public virtual bool GetHasNavigationBar()
		{
			return this.HasNavigationBar;
		}

		public virtual Brush GetTitleBarBackgroundColor()
		{
			return this.TitleBarBackgroundColor;
		}

		public virtual Brush GetTitleBarTextColor()
		{
			return this.TitleBarTextColor;
		}

		public virtual IEnumerable<FrameworkElement> GetPrimaryTopBarCommands()
		{
			return this.PrimaryTopBarCommands;
		}

		public virtual IEnumerable<FrameworkElement> GetSecondaryTopBarCommands()
		{
			return this.SecondaryTopBarCommands;
		}

		public virtual IEnumerable<FrameworkElement> GetPrimaryBottomBarCommands()
		{
			return this.PrimaryBottomBarCommands;
		}

		public virtual IEnumerable<FrameworkElement> GetSecondaryBottomBarCommands()
		{
			return this.SecondaryBottomBarCommands;
		}
	}  
}
