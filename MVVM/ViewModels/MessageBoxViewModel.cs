using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using YetAnotherMessenger.Misc;
using YetAnotherMessenger.MVVM.Models;

namespace YetAnotherMessenger.MVVM.ViewModels
{
	public class MessageBoxViewModel : ReactiveObject
	{
		private static MessageBoxViewModel? _instance;

		public static MessageBoxViewModel Instance
		{
			get
			{
				_instance ??= new MessageBoxViewModel();
				return _instance;
			}
		}

		public ReactiveCommand<Key, Unit> SendMessageCommand { get; }

		[Reactive] 
		public string? MessageDraft { get; set; }

		public MessageBoxViewModel()
		{
			var canExecute = this
				.WhenAnyValue(x => x.MessageDraft)
				.Select(messageDraft => !string.IsNullOrEmpty(messageDraft));

			SendMessageCommand = ReactiveCommand.CreateFromObservable<Key, Unit>((_) =>
			{
				using ApplicationContext db = new();

				var conversation = ConversationViewModel.Instance.Conversation;

				if (conversation!.Messages.Count == 0)
				{
					foreach (var user in conversation.Participants)
					{
						user.Conversations.Add(conversation);
					}

					db.Users.UpdateRange(conversation.Participants);
					db.Conversations.Add(conversation);
					db.SaveChanges();
				}

				var message = new TextMessageFactory().MessageBuilder(MessageDraft!, AppConfig.CurrentUser);

				db.Users.Update(AppConfig.CurrentUser);
				db.MessageContents.Add(message.Content!);
				db.Messages.Add(message);

				conversation.Messages.Add(message);
				db.Conversations.Update(conversation);

				db.SaveChanges();
				return Observable.Return(Unit.Default);
			}, canExecute);
		}
	}
}
