using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ReactiveUI.Fody.Helpers;

namespace YetAnotherMessenger.MVVM.ViewModels
{
	public class MainWindowViewModel : ReactiveObject
	{
		private const int FullscreenChatStartingWidth = 600;
		private const int MenuColumnMinWidth = 250;

		[Reactive]
		private MainViewModel MainVM { get; set; } = MainViewModel.Instance;

		public void UpdateChatCollapseState(double actualWidth)
		{
			if (actualWidth <= FullscreenChatStartingWidth)
			{
				MainVM.MenuViewModel.IsPresent = false;

				MainVM.ChatViewModel.IsMenuButtonVisible = true;

				MainVM.MenuColumnWidth = new GridLength(0, GridUnitType.Auto);
				MainVM.MenuColumnMinWidth = 0;
			}
			else if (actualWidth > FullscreenChatStartingWidth)
			{
				MainVM.MenuViewModel.IsPresent = true;

				MainVM.ChatViewModel.IsMenuButtonVisible = false;

				MainVM.MenuColumnWidth = new GridLength(1, GridUnitType.Star);
				MainVM.MenuColumnMinWidth = MenuColumnMinWidth;
			}
		}
	}
}
