using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ReactiveUI.Fody.Helpers;

namespace YetAnotherMessenger.MVVM.ViewModels
{
	public class MainViewModel : ReactiveObject
	{
		private static MainViewModel? _instance;

		public static MainViewModel Instance
		{
			get
			{
				_instance ??= new MainViewModel();
				return _instance;
			}
		}

		[Reactive] 
		public MenuViewModel MenuViewModel { get; set; } = MenuViewModel.Instance;

		[Reactive] 
		public ChatViewModel ChatViewModel { get; set; } = ChatViewModel.Instance;

		[Reactive] 
		public double MenuColumnMinWidth { get; set; } = 250.0;

		[Reactive]
		public GridLength MenuColumnWidth { get; set; } = new(1, GridUnitType.Star);
	}
}
