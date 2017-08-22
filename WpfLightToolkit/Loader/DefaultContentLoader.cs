using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WpfLightToolkit.Interfaces;

namespace WpfLightToolkit.Loader
{
    public class DefaultContentLoader : IContentLoader
    {
        public Task<object> LoadContentAsync(FrameworkElement parent, object page, CancellationToken cancellationToken)
        {
            if (!Application.Current.Dispatcher.CheckAccess())
                throw new InvalidOperationException("UIThreadRequired");
            
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            return Task.Factory.StartNew(() => LoadContent(page), cancellationToken, TaskCreationOptions.None, scheduler);
        }

        public void SizeContentChanged(FrameworkElement parent, object page)
        {

        }

        protected virtual object LoadContent(object page)
        {
            if (page is FrameworkElement)
                return page;

            if (page is Uri)
                return Application.LoadComponent(page as Uri);

            if (page is string)
            {
                if (Uri.TryCreate(page as string, UriKind.RelativeOrAbsolute, out Uri uri))
                {
                    return Application.LoadComponent(uri);
                }
            }

            return null;
        }
    }
}
