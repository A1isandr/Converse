using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using YetAnotherMessenger.Misc;

namespace YetAnotherMessenger.MVVM.ViewModels
{
	public class ListMenuViewModel : ReactiveObject
	{
		#region Properties


		private static ListMenuViewModel? _instance;
		/// <summary>
		/// Returns the singleton instance.
		/// </summary>
		public static ListMenuViewModel Instance
		{
			get
			{
				_instance ??= new ListMenuViewModel();
				return _instance;
			}
		}

		private ReactiveCommand<string, Unit> SearchCommand { get; }

		private SearchBoxViewModel SearchBoxVM { get; set; } = SearchBoxViewModel.Instance;

		[Reactive]
		public ObservableCollection<ConversationPreviewViewModel> ConversationPreviews { get; set; }

		[Reactive]
		public ObservableCollection<ConversationPreviewViewModel> ConversationSearchResults { get; private set; } = [];

		[Reactive]
		public ObservableCollection<UserPreviewViewModel> GlobalSearchResults { get; private set; } = [];

		//[Reactive]
		//public ObservableCollection<MessagePreviewViewModel> MessagePreviews { get; set; }

		private readonly ObservableAsPropertyHelper<bool> _isSearching;
		public bool IsSearching => _isSearching.Value;

		private readonly ObservableAsPropertyHelper<bool> _isSearchInProgress;
		public bool IsSearchInProgress => _isSearchInProgress.Value;

		private readonly ObservableAsPropertyHelper<bool> _isAnyConversationSearchResults;
		public bool IsAnyConversationSearchResults => _isAnyConversationSearchResults.Value;

		private readonly ObservableAsPropertyHelper<bool> _isAnyGlobalSearchResults;
		public bool IsAnyGlobalSearchResults => _isAnyGlobalSearchResults.Value;

		private readonly ObservableAsPropertyHelper<bool> _isAnyConversationPreviews;
		public bool IsAnyConversationPreviews => _isAnyConversationPreviews.Value;


		#endregion


		#region Constructors


		private ListMenuViewModel()
		{
			ConversationPreviews =
			[
				//.. db.Conversations
				//	.Include(conversation => conversation.Messages)
				//	.ThenInclude(message => message.Content)
				//	.Include(conversation => conversation.Participants)
				//	.ThenInclude(user => user.Profile)
				//	.Select(chat => new ConversationPreviewViewModel { Conversation = chat })
				//	.OrderBy(chat => chat.Conversation.LastActivityTimeUtc)
			];

			SearchCommand = ReactiveCommand.CreateFromObservable<string, Unit>((term) =>
			{
				ConversationSearchResults =
				[
					.. ConversationPreviews
						.Where(result => result.Conversation.ConversationName.ToLower().Contains(term))
						.OrderBy(result => result.Conversation.LastActivityTimeUtc)
				];

				if (!IsAnyConversationSearchResults)
				{
					GlobalSearchResults =
					[
						//.. db.Users
						//	.Include(user => user.Profile)
						//	.Where(user => !user.IsSystem)
						//	.ToList()
						//	.Where(user => user.Profile!.FullName.Contains(term, StringComparison.CurrentCultureIgnoreCase) || user.Username.Contains(term, StringComparison.CurrentCultureIgnoreCase))
						//	.Select(user => new UserPreviewViewModel { User = user })
					];
				}

				//MessagePreviews =
				//[
				//	.. db.Messages.Include(message => message.Content)
				//				  .Where(message => message.Content.Content.Contains(term))
				//				  .Select(chat => new ConversationPreviewViewModel { Conversation = chat })
				//];

				return Observable.Return(Unit.Default);
			});

			SearchCommand.ThrownExceptions.Subscribe(error =>
			{
				MessageBox.Show($"{error.Message}\n----------\n{error.StackTrace}");
				Clipboard.SetText(error.Message);
			});

			this.WhenAnyValue(x => x.SearchBoxVM.SearchTerm)
				.Throttle(TimeSpan.FromMilliseconds(500))
				.Select(term => term?.Trim().ToLower())
				.DistinctUntilChanged()
				.Where(term => !string.IsNullOrWhiteSpace(term))
				.DistinctUntilChanged()
				.ObserveOn(RxApp.MainThreadScheduler)
				.InvokeCommand(SearchCommand!);

			this.WhenAnyValue(x => x.SearchBoxVM.SearchTerm)
				.Where(string.IsNullOrEmpty)
				.Subscribe(_ =>
				{
					ConversationSearchResults = [];
					GlobalSearchResults = [];
					//MessageSearchResults.Clear();
				});

			_isSearching = this
				.WhenAnyValue(x => x.SearchBoxVM.SearchTerm)
				.Select(string.IsNullOrEmpty)
				.ToProperty(this, x => x.IsSearching);

			_isAnyConversationSearchResults = this
				.WhenAnyValue(x => x.ConversationSearchResults)
				.Select(conversations => !conversations.Any())
				.ToProperty(this, x => x.IsAnyConversationSearchResults);

			_isAnyGlobalSearchResults = this
				.WhenAnyValue(x => x.GlobalSearchResults)
				.Select(globalSearchResults => globalSearchResults.Any())
				.ToProperty(this, x => x.IsAnyGlobalSearchResults);

			_isAnyConversationPreviews = this
				.WhenAnyValue(x => x.ConversationPreviews)
				.Select(conversationPreviews => conversationPreviews.Any() && !IsSearching)
				.ToProperty(this, x => x.IsAnyConversationPreviews);

			_isSearchInProgress = SearchCommand.IsExecuting.ToProperty(this, x => x.IsSearchInProgress);
		}


		#endregion
	}
}
