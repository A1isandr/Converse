using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using YetAnotherMessenger.MVVM.ViewModels;

namespace YetAnotherMessenger.MVVM.Models
{
	public class Message
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

		public int Id { get; init; }

		//[MaxLength(25)]
		//public string? Discriminator { get; init; }

		public MessageContent? Content { get; set; }

		public DateTime DateTimeCreated { get; init; } = DateTime.UtcNow;

		public DateTime? DateTimeEdited { get; set; }

		public MessageStatus Status { get; set; } = MessageStatus.Created;

		public Conversation? Conversation { get; set; }

		public int ConversationId { get; set; }

		public User? User { get; init; }

		public int UserId { get; init; }
	}
}
