using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace YetAnotherMessenger.MVVM.ViewModels
{
	public class MenuViewModel : ReactiveObject
	{
		private static MenuViewModel? _instance;

		public static MenuViewModel Instance
		{
			get
			{
				_instance ??= new MenuViewModel();
				return _instance;
			}
		}

		private ChatListMenuViewModel ChatListMenuVM { get; set; } = ChatListMenuViewModel.Instance;

		private MainMenuViewModel MainMenuVM { get; set; } = MainMenuViewModel.Instance;

		[Reactive]
		public bool IsMainMenuOpen { get; set; }

		[Reactive]
		public bool IsChatListOpen { get; set; } = true;
	}
}
