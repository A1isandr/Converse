using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherMessenger.MVVM.ViewModels
{
	public class ChatPreviewViewModel : ReactiveObject
	{
		private int _chatId;
		/// <summary>
		/// 
		/// </summary>
		public int ChatId
		{
			get => _chatId;
			set => this.RaiseAndSetIfChanged(ref _chatId, value);
		}

		private string? _name;
		/// <summary>
		/// 
		/// </summary>
		public string? Name
		{
			get => _name;
			set => this.RaiseAndSetIfChanged(ref _name, value);
		}

		private string? _lastMessage;
		/// <summary>
		/// 
		/// </summary>
		public string? LastMessage
		{
			get => _lastMessage;
			set => this.RaiseAndSetIfChanged(ref _lastMessage, value);
		}

		private string? _lastMessageTime;
		/// <summary>
		/// 
		/// </summary>
		public string? LastMessageTime
		{
			get => _lastMessageTime; 
			set => this.RaiseAndSetIfChanged(ref _lastMessageTime, value);
		}

		private Uri? _avatarUri;
		/// <summary>
		/// 
		/// </summary>
		public Uri? AvatarUri
		{
			get => _avatarUri;
			set => this.RaiseAndSetIfChanged(ref _avatarUri, value);
		}
	}
}
