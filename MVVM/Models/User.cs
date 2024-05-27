using System.ComponentModel.DataAnnotations;
using System.Security;

namespace YetAnotherMessenger.MVVM.Models
{
	public class User
	{
		public string Id { get; set; } = string.Empty;

		public required string Username { get; set; }

		public required string Password { get; set; }

		public bool IsSystem { get; set; }

		public UserProfile? Profile { get; set; }

		public List<Conversation> Conversations { get; set; } = [];

		public List<Message> Messages { get; set; } = [];
	}
}
