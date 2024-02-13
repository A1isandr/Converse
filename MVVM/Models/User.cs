using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace YetAnotherMessenger.MVVM.Models
{
	public class User
	{
		public int Id { get; init; }

		[MaxLength(50)]
		public required string Username { get; set; }

		[MaxLength(25)]
		public required string Password { get; set; }

		public bool IsSystem { get; set; }

		public UserProfile? Profile { get; set; }

		public List<Conversation> Conversations { get; set; } = [];

		public List<Message> Messages { get; set; } = [];
	}
}
