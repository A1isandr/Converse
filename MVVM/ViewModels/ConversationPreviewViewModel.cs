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
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Message = YetAnotherMessenger.MVVM.Models.Message;

namespace YetAnotherMessenger.MVVM.ViewModels
{
	public class ConversationPreviewViewModel : ReactiveObject
	{
		public Conversation Conversation { get; init; }

		public ReactiveCommand<Unit, Unit> OpenChatCommand { get; }

		public Message LastMessage => Conversation.Messages.Last();

		public string LastMessagePrescription
		{
			get
			{
				if (LastMessage?.DateTimeCreated.Date == DateTime.UtcNow.Date)
				{
					return LastMessage.DateTimeCreated.ToString("HH:mm", CultureInfo.CurrentCulture);
				}

				return LastMessage!.DateTimeCreated.ToString(LastMessage.DateTimeCreated.Year == DateTime.UtcNow.Year ?
					"M" :
					"yyyy MMMM dd", CultureInfo.CurrentCulture);
			}
		}

		public ConversationPreviewViewModel()
		{
			OpenChatCommand = ReactiveCommand.Create<Unit, Unit>(_ =>
			{
				ConversationViewModel.Instance.Conversation = Conversation;
				return Unit.Default;
			});
		}
	}
}
