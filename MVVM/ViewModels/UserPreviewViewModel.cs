using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using ReactiveUI;
using YetAnotherMessenger.Misc;
using YetAnotherMessenger.MVVM.Models;

namespace YetAnotherMessenger.MVVM.ViewModels
{
	public class UserPreviewViewModel : ReactiveObject
	{
		public User User { get; init; }

		public ReactiveCommand<Unit, Unit> StartNewConversation { get; }

		public UserPreviewViewModel()
		{
			StartNewConversation = ReactiveCommand.CreateFromObservable<Unit, Unit>( _ =>
			{
				ConversationViewModel.Instance.Conversation = new Conversation
				{
					ConversationName = User.Profile!.FullName,
					Participants = [User]
				};

				SearchBoxViewModel.Instance.ClearSearchTextCommand.Execute(Unit.Default);

				return Observable.Return(Unit.Default);
			});

			StartNewConversation.ThrownExceptions.Subscribe(error =>
			{
				MessageBox.Show($"{error.InnerException?.Message}\n----------\n{error.StackTrace}");
				Clipboard.SetText(error.InnerException!.Message);
			});
		}
	}
}
