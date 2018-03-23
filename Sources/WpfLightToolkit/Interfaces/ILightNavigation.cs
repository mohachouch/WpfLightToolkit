using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfLightToolkit.Controls;

namespace WpfLightToolkit.Interfaces
{
    public interface ILightNavigation
    {
        int StackDepth { get; }

        void InsertPageBefore(object page, object before);
        void Pop();
        void Pop(bool animated);
        void PopModal();
        void PopModal(bool animated);
        void PopToRoot();
        void PopToRoot(bool animated);

        void Push(object page);

        void Push(object page, bool animated);
        void PushModal(object page);
        void PushModal(object page, bool animated);

        void RemovePage(object page);
    }

	public class DefaultNavigation : ILightNavigation
	{
		public LightWindow ParentWindow
		{
			get
			{
				if (Application.Current.MainWindow is LightWindow)
					return Application.Current.MainWindow as LightWindow;
				return null;
			}
		}

		public int StackDepth => throw new InvalidOperationException("StackDepth is not supported globally on Windows, please use a LightNavigationPage.");

		public void InsertPageBefore(object page, object before)
		{
			throw new InvalidOperationException("InsertPageBefore is not supported globally on Windows, please use a LightNavigationPage."); 
		}

		public void Pop()
		{
			Pop(true);
		}

		public void Pop(bool animated)
		{
			throw new InvalidOperationException("Pop is not supported globally on Windows, please use a LightNavigationPage.");
		}

		public void PopToRoot()
		{
			PopToRoot(true);
		}

		public void PopToRoot(bool animated)
		{
			throw new InvalidOperationException("PopToRoot is not supported globally on Windows, please use a LightNavigationPage.");
		}

		public void Push(object page)
		{
			Push(page, true);
		}

		public void Push(object page, bool animated)
		{
			throw new InvalidOperationException("Push is not supported globally on Windows, please use a LightNavigationPage.");
		}

		public void RemovePage(object page)
		{
			throw new InvalidOperationException("RemovePage is not supported globally on Windows, please use a LightNavigationPage.");
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
	}
}
