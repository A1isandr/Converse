using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using DynamicData.Tests;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using YetAnotherMessenger.Misc;
using YetAnotherMessenger.MVVM.Models;

namespace YetAnotherMessenger.MVVM.ViewModels
{
	public class ChatListMenuViewModel : ReactiveObject
	{
		private static ChatListMenuViewModel? _instance;
		/// <summary>
		/// Returns the singleton instance.
		/// </summary>
		public static ChatListMenuViewModel Instance
		{
			get
			{
				_instance ??= new ChatListMenuViewModel();
				return _instance;
			}
		}

		public ReactiveCommand<Unit, Unit> OpenMainMenuCommand { get; }

		public ObservableCollection<Chat> Chats { get; set; }

		public List<ChatPreviewViewModel> ChatPreviews { get; set; }

		public ChatListMenuViewModel()
		{
			OpenMainMenuCommand = ReactiveCommand.Create<Unit, Unit>(_ => Unit.Default);

			Chats =
			[
				new Chat
				{
					Id = 1,
					Name = "Test",
					Users =
					[
						AppConfig.CurrentUser,
						new User
						{
							Id = 2,
							Avatar = new Uri("https://i.pravatar.cc/300"),
							FirstName = "Mr.",
							LastName = "Who",
							Username = "MrWho",
						}
					],
					Messages =
					[
						new TextMessage
						(
							content: "Hello! ☺"
						),

						new TextMessage
						(
							content: "Hi!"
						)
					]
				},

				new Chat
				{
					Id = 2,
					Name = "Test 2",
					Users =
					[
						AppConfig.CurrentUser,
						new User
						{
							Id = 3,
							Avatar = new Uri("https://i.pravatar.cc/200"),
							FirstName = "Some",
							LastName = "One",
							Username = "Someone",
						}
					],
					Messages =
					[
						new TextMessage
						(
							content: "When are you going to be at home?"
						),

						new TextMessage
						(
							content: "Not later than at 7"
						)
					],
				}
			];

			ChatPreviews = Chats.Select(chat => new ChatPreviewViewModel { Chat = chat }).ToList();
		}
	}
}
