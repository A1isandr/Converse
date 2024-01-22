using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

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

		public ReactiveCommand<Unit, Unit> SendMessageCommand { get; }

		[Reactive]
		public string? MessageDraft { get; set; } = null;

		public MessageBoxViewModel()
		{
			var canExecute = this.WhenAnyValue(x => x.MessageDraft,
				messageDraft => !string.IsNullOrEmpty(messageDraft));

			SendMessageCommand = ReactiveCommand.Create<Unit, Unit>(_ => Unit.Default, canExecute);
		}
	}
}
