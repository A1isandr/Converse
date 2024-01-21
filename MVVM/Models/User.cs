using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace YetAnotherMessenger.MVVM.Models
{
	public class User : ReactiveObject
	{
		public string FullName => $"{FirstName} {LastName}";

		public int Id { get; init; }

		[Reactive]
		public string FirstName { get; set; }

		[Reactive]
		public string? LastName { get; set; }

		[Reactive]
		public string UserName { get; set; }

		[Reactive]
		public Uri Avatar { get; set; }
	}
}
