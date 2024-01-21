﻿using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace YetAnotherMessenger.MVVM.Models
{
	public class Message : ReactiveObject
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

		public string HourMinute => DateTime.ToString("HH:mm");

		public bool IsMine => Sender == AppConfig.CurrentUser;

		public string Content { get; set; }

		public User Sender { get; init; }

		public DateTime DateTime { get; set; }

		public MessageStatus Status { get; set; }

		protected Message(string content)
		{
			Content = content;
			Sender = AppConfig.CurrentUser;
			DateTime = DateTime.Now;
			Status = MessageStatus.Created;
		}
	}
}
