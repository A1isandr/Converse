using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Globalization;
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
		public Chat Chat { get; init; }

		public ReactiveCommand<Unit, Unit> OpenChatCommand { get; init; }

		public string LastMessagePrescription
		{
			get
			{
				if (Chat.LastMessage?.DateTimeUtc.Date == DateTime.UtcNow.Date)
				{
					return Chat.LastMessage.DateTimeUtc.ToString("HH:mm", CultureInfo.CurrentCulture);
				}

				return Chat.LastMessage.DateTimeUtc.ToString(Chat.LastMessage?.DateTimeUtc.Year == DateTime.UtcNow.Year ?
					"M" :
					"yyyy MMMM dd", CultureInfo.CurrentCulture);
			}
		}

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
