using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accessibility;

namespace YetAnotherMessenger.MVVM.Models
{
	public class MessageContent
	{
		public int Id { get; init; }

		[MaxLength(10000)]
		public string Content { get; set; } = string.Empty;

		public Message? Message { get; set; }

		public int MessageId { get; init; }

		public override string ToString()
		{
			return Message switch
			{
				TextMessage => Content,
				VideoMessage => "Video Message",
				VoiceMessage => "Voice Message",
				_ => string.Empty
			};
		}
	}
}
