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
		[Reactive] 
		public MenuViewModel MenuViewModel { get; set; } = MenuViewModel.Instance;

		[Reactive] 
		public ChatViewModel ChatViewModel { get; set; } = ChatViewModel.Instance;
	}
}
