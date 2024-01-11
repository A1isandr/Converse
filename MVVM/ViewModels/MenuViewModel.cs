using System.Collections.ObjectModel;
using DynamicData.Tests;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

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

		[Reactive] public bool IsPresent { get; set; } = true;

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
				}
			};
		}
	}
}
