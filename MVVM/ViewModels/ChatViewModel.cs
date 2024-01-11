using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI.Fody.Helpers;

namespace YetAnotherMessenger.MVVM.ViewModels
{

	public class ChatViewModel : ReactiveObject
	{
		private static ChatViewModel? _instance;

		public static ChatViewModel Instance
		{
			get
			{
				_instance ??= new ChatViewModel();
				return _instance;
			}
		}

		[Reactive]
		public bool IsMenuButtonVisible { get; set; }
	}
}
