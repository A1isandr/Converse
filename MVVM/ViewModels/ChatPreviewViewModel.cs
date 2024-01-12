using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ReactiveUI.Fody.Helpers;

namespace YetAnotherMessenger.MVVM.ViewModels
{
	public class ChatPreviewViewModel : ReactiveObject
	{
		[Reactive]
		public int ChatId { get; init; }

		[Reactive]
		public string? Name { get; init; }

		[Reactive]
		public string? LastMessage { get; init; }

		[Reactive]
		public string? LastMessageTime { get; init; }

		[Reactive]
		public Uri? AvatarUri { get; init; }

		public ReactiveCommand<Unit, Unit> OpenChatCommand { get; init; }

		public ChatPreviewViewModel()
		{
			OpenChatCommand = ReactiveCommand.Create<Unit, Unit>(_ =>
			{
				ChatViewModel.Instance.CurrentChatId = ChatId;
				return Unit.Default;
			});
		}
	}
}
