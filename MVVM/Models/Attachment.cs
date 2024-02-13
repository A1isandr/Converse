using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherMessenger.MVVM.Models
{
	public class Attachment
	{
		public int Id { get; init; }

		[MaxLength(200)]
		public string Content { get; init; }

		public int TextMessageId { get; set; }

		public TextMessage TextMessage { get; set; }
	}
}
