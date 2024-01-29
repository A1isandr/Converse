using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using DynamicData.Tests;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using YetAnotherMessenger.Misc;
using YetAnotherMessenger.MVVM.Models;

namespace YetAnotherMessenger.MVVM.ViewModels
{
	public class ConversationListMenuViewModel : ReactiveObject
	{
		private static ConversationListMenuViewModel? _instance;
		/// <summary>
		/// Returns the singleton instance.
		/// </summary>
		public static ConversationListMenuViewModel Instance
		{
			get
			{
				_instance ??= new ConversationListMenuViewModel();
				return _instance;
			}
		}

		private ReactiveCommand<string, Unit> SearchCommand { get; }

		private SearchBoxViewModel SearchBoxVM { get; set; } = SearchBoxViewModel.Instance;

		[Reactive]
		public ObservableCollection<ConversationPreviewViewModel> ConversationPreviews { get; set; }

		[Reactive]
		public ObservableCollection<UserPreviewViewModel>? GlobalSearchPreviews { get; set; }

		//public List<MessagePreviewViewModel> MessagePreviews { get; set; }

		//private readonly ObservableAsPropertyHelper<IEnumerable<UserPreviewViewModel>> _userPreviews;
		//public IEnumerable<UserPreviewViewModel> UserPreviews => _userPreviews.Value;

		private ConversationListMenuViewModel()
		{
			var db = new ApplicationContext();

			ConversationPreviews = [.. db.Conversations.Select(chat => new ConversationPreviewViewModel { Conversation = chat })];

			SearchCommand = ReactiveCommand.CreateFromTask<string, Unit>(async (term) =>
			{
				ConversationPreviews =
				[
					.. db.Conversations.Where(conversation => conversation.ConversationName.Contains(term))
						.Select(conversation => new ConversationPreviewViewModel { Conversation = conversation })
				];

				if (ConversationPreviews.Count == 0)
				{
					GlobalSearchPreviews =
					[
						.. db.Users.Include(user => user.Profile)
							.ToList()
							.Where(user => user.Profile.FullName.Contains(term) || user.Username.Contains(term))
							.Select(user => new UserPreviewViewModel { User = user })
					];
				}
				
				//MessagePreviews =
				//[
				//	.. db.Messages.Include(message => message.Content)
				//				  .Where(message => message.Content.Content.Contains(term))
				//				  .Select(chat => new ConversationPreviewViewModel { Conversation = chat })
				//];

				return Unit.Default;
			});

			SearchCommand.ThrownExceptions.Subscribe(error =>
			{
				MessageBox.Show(error.Message + "\n----------" + error.StackTrace);
				Clipboard.SetText(error.Message);
			});

			this.WhenAnyValue(x => x.SearchBoxVM.SearchTerm)
				.Throttle(TimeSpan.FromMilliseconds(600))
				.Select(term => term?.Trim())
				.DistinctUntilChanged()
				.Where(term => !string.IsNullOrWhiteSpace(term))
				.ObserveOn(RxApp.MainThreadScheduler)
				.InvokeCommand(SearchCommand!);
		}
	}
}
