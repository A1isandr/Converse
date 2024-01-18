using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows;
using DynamicData.Tests;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using YetAnotherMessenger.Misc;
using YetAnotherMessenger.MVVM.Models;

namespace YetAnotherMessenger.MVVM.ViewModels
{
	public class MenuViewModel : ReactiveObject
	{
		private static MenuViewModel? _instance;
		/// <summary>
		/// Returns the singleton instance.
		/// </summary>
		public static MenuViewModel Instance
		{
			get
			{
				_instance ??= new MenuViewModel();
				return _instance;
			}
		}

		public ObservableCollection<Chat> Chats { get; set; }

		public List<ChatPreviewViewModel> ChatPreviews { get; set; }

		public MenuViewModel()
		{
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
							content: "Hello!"
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
