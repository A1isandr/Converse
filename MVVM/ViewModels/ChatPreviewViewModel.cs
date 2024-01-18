using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ReactiveUI.Fody.Helpers;
using YetAnotherMessenger.MVVM.Models;

namespace YetAnotherMessenger.MVVM.ViewModels
{
	public class ChatPreviewViewModel : ReactiveObject
	{
		public Chat Chat { get; set; }

		public ReactiveCommand<Unit, Unit> OpenChatCommand { get; init; }

		public ChatPreviewViewModel()
		{
			OpenChatCommand = ReactiveCommand.Create<Unit, Unit>(_ =>
			{
				ChatViewModel.Instance.Chat = Chat;
				return Unit.Default;
			});
		}
	}
}
