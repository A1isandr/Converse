using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Linq;
using YetAnotherMessenger.MVVM.Models;

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
		public List<MessageViewModel> Messages { get; private set; } = [];

		private readonly ObservableAsPropertyHelper<bool> _isConversationOpened;
		public bool IsConversationOpened => _isConversationOpened.Value;

		private readonly ObservableAsPropertyHelper<bool> _hasMessages;
		public bool HasMessages => _hasMessages.Value;

		private ConversationViewModel()
		{
			this.WhenAnyValue(x => x.Conversation)
				.Where(conversation => conversation != null)
				.Subscribe(conversation =>
				{
					Messages = conversation!.Messages
						.Select(message => new MessageViewModel { Message = message })
						.ToList();
				});

			_isConversationOpened = this
				.WhenAnyValue(x => x.Conversation)
				.Select(conversation => conversation is not null)
				.ToProperty(this, x => x.IsConversationOpened);

			_hasMessages = this
				.WhenAnyValue(x => x.Messages)
				.Select(messages => !messages.Any())
				.ToProperty(this, x => x.HasMessages);
		}
	}
}
