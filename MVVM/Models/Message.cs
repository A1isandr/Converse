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

		[MaxLength(25)]
		public string? Discriminator { get; init; }

		public required MessageContent Content { get; init; }

		public required DateTime DateTimeCreated { get; set; }

		public DateTime? DateTimeEdited { get; set; }

		public required MessageStatus Status { get; set; }

		public Conversation Conversation { get; init; }

		public int ConversationId { get; init; }

		public User User { get; init; }

		public int UserId { get; init; }
	}
}
