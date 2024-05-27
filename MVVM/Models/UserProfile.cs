using System.ComponentModel.DataAnnotations;

namespace YetAnotherMessenger.MVVM.Models
{
	public class UserProfile
	{
		[MaxLength(50)]
		public required string Id { get; init; }

		[MaxLength(25)]
		public required string FirstName { get; set; }

		[MaxLength(25)]
		public string? LastName { get; set; }

		public required Uri Avatar { get; set; }

		public User User { get; init; }

		[MaxLength(50)]
		public string UserId { get; init; }

		public string FullName => $"{FirstName} {LastName}";
}
}
