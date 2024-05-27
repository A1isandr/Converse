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
				
				return Observable.Return(Unit.Default);
			}, canExecute);
		}
	}
}
