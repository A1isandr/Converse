using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ReactiveUI.Fody.Helpers;
using YetAnotherMessenger.Misc;
using YetAnotherMessenger.MVVM.Models;

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

		private MessageBoxViewModel MessageBoxVM { get; set; } = MessageBoxViewModel.Instance;

		[Reactive] 
		public Chat? Chat { get; set; } = MenuViewModel.Instance.Chats.First();

		[Reactive]
		public List<MessageViewModel> Messages { get; set; }

		public ChatViewModel()
		{
			this.WhenAnyValue(x => x.Chat)
				.Subscribe(chat => Messages = chat!.Messages.Select(message => new MessageViewModel {Message = message}).ToList());

			MessageBoxVM.SendMessageCommand.IsExecuting.Subscribe(isExecuting =>
			{
				if (isExecuting)
				{
					Chat?.Messages.Add(new TextMessage
					(
						content: MessageBoxVM.LastMessage!
					));
				}
			});
		}
	}
}
