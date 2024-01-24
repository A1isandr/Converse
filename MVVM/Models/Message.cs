using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace YetAnotherMessenger.MVVM.Models
{
	public abstract class Message(string content)
	{
		public enum MessageStatus
		{
			Created,
			Sent,
			Delivered,
			Viewed,
			DeletedBySender,
			DeletedByRecipient
		}

		public string Content { get; set; } = content;

		public User Sender { get; init; } = AppConfig.CurrentUser;

		public DateTime DateTimeUtc { get; set; } = DateTime.UtcNow;

		public MessageStatus Status { get; set; } = MessageStatus.Created;
	}
}
