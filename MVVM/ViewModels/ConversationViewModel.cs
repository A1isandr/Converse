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
using ABI.Windows.AI.MachineLearning;
using ReactiveUI.Fody.Helpers;
using YetAnotherMessenger.Misc;
using YetAnotherMessenger.MVVM.Models;
using DynamicData;
using DynamicData.Binding;

namespace YetAnotherMessenger.MVVM.ViewModels
{
	public class ConversationViewModel : ReactiveObject
	{
		private static ConversationViewModel? _instance;

		public static ConversationViewModel Instance
		{
			get
			{
				_instance ??= new ConversationViewModel();
				return _instance;
			}
		}

		private MessageBoxViewModel MessageBoxVM { get; set; } = MessageBoxViewModel.Instance;

		[Reactive] 
		public Conversation? Conversation { get; set; }

		[Reactive]
		public List<MessageViewModel>? Messages { get; private set; }

		[Reactive] 
		public bool IsChatWindowVisible { get; private set; }

		private ConversationViewModel()
		{
			this.WhenAnyValue(x => x.Conversation)
				.Where(conversation => conversation != null)
				.Subscribe(conversation =>
				{
					Messages = conversation!.Messages.Select(message => new MessageViewModel { Message = message })
						.ToList();

					if (!IsChatWindowVisible)
					{
						IsChatWindowVisible = !IsChatWindowVisible;
					}
				});

			MessageBoxVM.SendMessageCommand.IsExecuting.Subscribe(isExecuting =>
			{
				if (!isExecuting) return;

				//Conversation?.Messages.Add(new TextMessage
				//(
				//	content: MessageBoxVM.MessageDraft!
				//));

				MessageBoxVM.MessageDraft = string.Empty;
			});
		}
	}
}
