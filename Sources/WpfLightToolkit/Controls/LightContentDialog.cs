using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfLightToolkit.Helpers;
namespace WpfLightToolkit.Controls
{
	public class LightContentDialog : ContentControl, ILightContentDialog
	{

		TaskCompletionSource<LightContentDialogResult> tcs;

		public static RoutedUICommand PrimaryButtonRoutedCommand = new RoutedUICommand("Primary", "Primary", typeof(LightContentDialog));
		public static RoutedUICommand SecondaryButtonRoutedCommand = new RoutedUICommand("Secondary", "Secondary", typeof(LightContentDialog));

		public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(object), typeof(LightContentDialog));
		public static readonly DependencyProperty TitleTemplateProperty = DependencyProperty.Register("TitleTemplate", typeof(DataTemplate), typeof(LightContentDialog));
		public static readonly DependencyProperty FullSizeDesiredProperty = DependencyProperty.Register("FullSizeDesired", typeof(bool), typeof(LightContentDialog));
		public static readonly DependencyProperty IsPrimaryButtonEnabledProperty = DependencyProperty.Register("IsPrimaryButtonEnabled", typeof(bool), typeof(LightContentDialog));
		public static readonly DependencyProperty IsSecondaryButtonEnabledProperty = DependencyProperty.Register("IsSecondaryButtonEnabled", typeof(bool), typeof(LightContentDialog));
		public static readonly DependencyProperty PrimaryButtonCommandProperty = DependencyProperty.Register("PrimaryButtonCommand", typeof(ICommand), typeof(LightContentDialog));
		public static readonly DependencyProperty SecondaryButtonCommandProperty = DependencyProperty.Register("SecondaryButtonCommand", typeof(ICommand), typeof(LightContentDialog));
		public static readonly DependencyProperty PrimaryButtonTextProperty = DependencyProperty.Register("PrimaryButtonText", typeof(string), typeof(LightContentDialog));
		public static readonly DependencyProperty PrimaryButtonCommandParameterProperty = DependencyProperty.Register("PrimaryButtonCommandParameter", typeof(object), typeof(LightContentDialog));
		public static readonly DependencyProperty SecondaryButtonTextProperty = DependencyProperty.Register("SecondaryButtonText", typeof(string), typeof(LightContentDialog));
		public static readonly DependencyProperty SecondaryButtonCommandParameterProperty = DependencyProperty.Register("SecondaryButtonCommandParameter", typeof(object), typeof(LightContentDialog));

		public object Title
		{
			get { return (object)GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
		}

		public DataTemplate TitleTemplate
		{
			get { return (DataTemplate)GetValue(TitleTemplateProperty); }
			set { SetValue(TitleTemplateProperty, value); }
		}

		public bool FullSizeDesired
		{
			get { return (bool)GetValue(FullSizeDesiredProperty); }
			set { SetValue(FullSizeDesiredProperty, value); }
		}
		
		public bool IsPrimaryButtonEnabled
		{
			get { return (bool)GetValue(IsPrimaryButtonEnabledProperty); }
			set { SetValue(IsPrimaryButtonEnabledProperty, value); }
		}

		public bool IsSecondaryButtonEnabled
		{
			get { return (bool)GetValue(IsSecondaryButtonEnabledProperty); }
			set { SetValue(IsSecondaryButtonEnabledProperty, value); }
		}

		public ICommand PrimaryButtonCommand
		{
			get { return (ICommand)GetValue(PrimaryButtonCommandProperty); }
			set { SetValue(PrimaryButtonCommandProperty, value); }
		}
		
		public ICommand SecondaryButtonCommand
		{
			get { return (ICommand)GetValue(SecondaryButtonCommandProperty); }
			set { SetValue(SecondaryButtonCommandProperty, value); }
		}
		
		public string PrimaryButtonText
		{
			get { return (string)GetValue(PrimaryButtonTextProperty); }
			set { SetValue(PrimaryButtonTextProperty, value); }
		}
		
		public object PrimaryButtonCommandParameter
		{
			get { return (object)GetValue(PrimaryButtonCommandParameterProperty); }
			set { SetValue(PrimaryButtonCommandParameterProperty, value); }
		}

		public string SecondaryButtonText
		{
			get { return (string)GetValue(SecondaryButtonTextProperty); }
			set { SetValue(SecondaryButtonTextProperty, value); }
		}

		public object SecondaryButtonCommandParameter
		{
			get { return (object)GetValue(SecondaryButtonCommandParameterProperty); }
			set { SetValue(SecondaryButtonCommandParameterProperty, value); }
		}
		
		public event EventHandler<LightContentDialogClosedEventArgs> Closed;
		public event EventHandler<LightContentDialogClosingEventArgs> Closing;
		public event EventHandler<LightContentDialogOpenedEventArgs> Opened;
		public event EventHandler<LightContentDialogButtonClickEventArgs> PrimaryButtonClick;
		public event EventHandler<LightContentDialogButtonClickEventArgs> SecondaryButtonClick;

		public LightContentDialog()
		{
			this.DefaultStyleKey = typeof(LightContentDialog);
			this.CommandBindings.Add(new CommandBinding(PrimaryButtonRoutedCommand, OnPrimaryButtonRoutedExecuted));
			this.CommandBindings.Add(new CommandBinding(SecondaryButtonRoutedCommand, this.SecondaryButtonRoutedExecuted));
		}

		private void OnPrimaryButtonRoutedExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			LightContentDialogButtonClickEventArgs lightContentDialogButtonClickEventArgs = new LightContentDialogButtonClickEventArgs();

			PrimaryButtonClick?.Invoke(this, lightContentDialogButtonClickEventArgs);
			
			if (!lightContentDialogButtonClickEventArgs.Cancel)
			{
				PrimaryButtonCommand?.Execute(PrimaryButtonCommandParameter);
				tcs.TrySetResult(LightContentDialogResult.Primary);
			}
		}

		private void SecondaryButtonRoutedExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			LightContentDialogButtonClickEventArgs lightContentDialogButtonClickEventArgs = new LightContentDialogButtonClickEventArgs();

			SecondaryButtonClick?.Invoke(this, lightContentDialogButtonClickEventArgs);

			if (!lightContentDialogButtonClickEventArgs.Cancel)
			{
				SecondaryButtonCommand?.Execute(SecondaryButtonCommandParameter);
				tcs.TrySetResult(LightContentDialogResult.Secondary);
			}
		}

		public async Task<LightContentDialogResult> ShowAsync()
		{
			if(Application.Current.MainWindow is LightWindow window)
			{
				window.ShowContentDialog(this);
				LightContentDialogOpenedEventArgs lightContentDialogOpenedEventArgs = new LightContentDialogOpenedEventArgs();
				Opened?.Invoke(this, lightContentDialogOpenedEventArgs);
			}

			LightContentDialogResult contentDialogResult = LightContentDialogResult.None;
			bool exit = false;

			while (!exit)
			{
				tcs = new TaskCompletionSource<LightContentDialogResult>();
				contentDialogResult = await tcs.Task;
				exit = InternalHide(contentDialogResult);
			}
			
			return contentDialogResult;
		}

		private bool InternalHide(LightContentDialogResult contentDialogResult)
		{
			LightContentDialogClosingEventArgs lightContentDialogClosingEventArgs = new LightContentDialogClosingEventArgs(contentDialogResult);
			Closing?.Invoke(this, lightContentDialogClosingEventArgs);

			if (!lightContentDialogClosingEventArgs.Cancel && Application.Current.MainWindow is LightWindow window)
			{
				window.HideContentDialog();
				LightContentDialogClosedEventArgs lightContentDialogClosedEventArgs = new LightContentDialogClosedEventArgs(contentDialogResult);
				Closed?.Invoke(this, lightContentDialogClosedEventArgs);
				return true;
			}
			return false;
		}

		public void Hide()
		{
			InternalHide(LightContentDialogResult.None);
		}
	}

	internal interface ILightContentDialog
	{
		void Hide();

		Task<LightContentDialogResult> ShowAsync();

		bool FullSizeDesired { get; set; }
		bool IsPrimaryButtonEnabled { get; set; }
		bool IsSecondaryButtonEnabled { get; set; }
		ICommand PrimaryButtonCommand { get; set; }
		object PrimaryButtonCommandParameter { get; set; }
		string PrimaryButtonText { get; set; }
		ICommand SecondaryButtonCommand { get; set; }
		object SecondaryButtonCommandParameter { get; set; }
		string SecondaryButtonText { get; set; }
		object Title { get; set; }
		DataTemplate TitleTemplate { get; set; }

		event EventHandler<LightContentDialogClosedEventArgs> Closed;
		event EventHandler<LightContentDialogClosingEventArgs> Closing;
		event EventHandler<LightContentDialogOpenedEventArgs> Opened;
		event EventHandler<LightContentDialogButtonClickEventArgs> PrimaryButtonClick;
		event EventHandler<LightContentDialogButtonClickEventArgs> SecondaryButtonClick;
	}

	public enum LightContentDialogResult
	{
		None = 0,
		Primary = 1,
		Secondary = 2
	}

	public sealed class LightContentDialogClosedEventArgs 
	{
		public LightContentDialogResult Result { get; }

		public LightContentDialogClosedEventArgs(LightContentDialogResult result)
		{
			Result = result;
		}
	}

	public sealed class LightContentDialogClosingEventArgs
	{
		public bool Cancel { get; set; }
		public LightContentDialogResult Result { get; }

		public LightContentDialogClosingEventArgs(LightContentDialogResult result)
		{
			Result = result;
		}
	}

	public sealed class LightContentDialogOpenedEventArgs 
	{
	}

	public sealed class LightContentDialogButtonClickEventArgs
	{
		public bool Cancel { get; set; }
	}
}
