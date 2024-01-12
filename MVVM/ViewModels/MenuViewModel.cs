using System.Collections.ObjectModel;
using System.Windows;
using DynamicData.Tests;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using YetAnotherMessenger.Misc;

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

		public ObservableCollection<ChatPreviewViewModel> ChatPreviews { get; set; }

		public MenuViewModel()
		{
			ChatPreviews = new ObservableCollection<ChatPreviewViewModel>
			{
				new()
				{
					ChatId = 1,
					Name = "Test",
					AvatarUri = new Uri(@"D:\Projects\C#\YetAnotherMessenger\Resources\Images\clueless.jpg"),
					LastMessage = "Hello",
					LastMessageTime = "10:00"
				},

				new()
				{
					ChatId = 2,
					Name = "Test2",
					AvatarUri = new Uri(@"D:\Projects\C#\YetAnotherMessenger\Resources\Images\clueless.jpg"),
					LastMessage = "Bye!",
					LastMessageTime = "22:30"
				}
			};
		}
	}
}
