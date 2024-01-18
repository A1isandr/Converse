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
		public int Id { get; set; }

		[Reactive]
		public string Name { get; set; }

		[Reactive]
		public Uri Avatar { get; set; }
	}
}
