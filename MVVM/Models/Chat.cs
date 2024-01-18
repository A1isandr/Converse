using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using YetAnotherMessenger.Controls;
using YetAnotherMessenger.MVVM.ViewModels;

namespace YetAnotherMessenger.MVVM.Models
{
    public class Chat : ReactiveObject
    {
	    public Message? LastMessage => Messages.LastOrDefault();

		public int Id { get; init; }

		[Reactive]
		public string? Name { get; set; }

		[Reactive]
		public Uri Avatar { get; set; }

		[Reactive]
		public ObservableCollection<Message> Messages { get; set; }

		[Reactive]
		public ObservableCollection<User> Users { get; set; }
	}
}