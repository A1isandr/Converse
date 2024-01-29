using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherMessenger.MVVM.Models
{
	public class MessageContent
	{
		public int Id { get; init; }

		public string Content { get; set; }

		public Message Message { get; init; }

		public int MessageId { get; init; }
	}
}
