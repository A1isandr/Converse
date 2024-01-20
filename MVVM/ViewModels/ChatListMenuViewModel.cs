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
					Name = "Friend",
					Avatar = new Uri(@"D:\Projects\C#\YetAnotherMessenger\Resources\Images\clueless.jpg"),
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
					],
				},

				new Chat
				{
					Id = 2,
					Name = "Friend",
					Avatar = new Uri(@"D:\Projects\C#\YetAnotherMessenger\Resources\Images\clueless.jpg"),
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
