using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherMessenger.MVVM.Models
{
	public class UserProfile
	{
		public string FullName => $"{FirstName} {LastName}";

		public int Id { get; init; }

		[MaxLength(25)]
		public  required string FirstName { get; set; }

		[MaxLength(25)]
		public string? LastName { get; set; }

		public required Uri Avatar { get; set; }

		public User User { get; init; }

		public int UserId { get; init; }
	}
}
