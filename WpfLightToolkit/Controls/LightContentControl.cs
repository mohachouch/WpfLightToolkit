using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfLightToolkit.Interfaces;
using WpfLightToolkit.Loader;

namespace WpfLightToolkit.Controls
{
    public class LightContentControl : ContentControl
    {

        private CancellationTokenSource tokenSource;
		
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(object), typeof(LightContentControl), new PropertyMetadata(OnSourceChanged));
        public static readonly DependencyProperty ContentLoaderProperty = DependencyProperty.Register("ContentLoader", typeof(IContentLoader), typeof(LightContentControl), new PropertyMetadata(new DefaultContentLoader(), OnContentLoaderChanged));
      
        public object Source
        {
            get { return (object)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public IContentLoader ContentLoader
        {
            get { return (IContentLoader)GetValue(ContentLoaderProperty); }
            set { SetValue(ContentLoaderProperty, value); }
        }

        public LightContentControl()
        {
            this.DefaultStyleKey = typeof(LightContentControl);
            this.SizeChanged += LightContentControl_SizeChanged;
        }

        private void LightContentControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.ContentLoader.SizeContentChanged(this, Source);
        }

        private static void OnContentLoaderChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
            {
                throw new ArgumentNullException("ContentLoader");
            }
        }

        private static void OnSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((LightContentControl)o).OnSourceChanged(e.OldValue, e.NewValue);
        }

        private void OnSourceChanged(object oldValue, object newValue)
        {
            if (newValue != null && newValue.Equals(oldValue)) return;
            
            var localTokenSource = new CancellationTokenSource();
            this.tokenSource = localTokenSource;

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            var task = this.ContentLoader.LoadContentAsync(this, newValue, this.tokenSource.Token);

            task.ContinueWith(t =>
            {
                try
                {
                    if (t.IsFaulted || t.IsCanceled || localTokenSource.IsCancellationRequested)
                    {
                        this.Content = null;
                    }
                    else
                    {
                        this.Content = t.Result;
                       
                    }
                }
                finally
                {
                    if (this.tokenSource == localTokenSource)
                    {
                        this.tokenSource = null;
                    }
                    localTokenSource.Dispose();
                }
            }, scheduler);
            return;
        }
    }
}
