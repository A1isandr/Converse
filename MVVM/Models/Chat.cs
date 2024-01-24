using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using YetAnotherMessenger.MVVM.ViewModels;

namespace YetAnotherMessenger.MVVM.Models
{
    public class Chat
    {
	    public Message? LastMessage => Messages.LastOrDefault();

		public int Id { get; init; }

		public string? Name { get; set; }

		public Uri Avatar { get; set; }

		public ObservableCollection<Message> Messages { get; set; }

		public ObservableCollection<User> Users { get; set; }
	}
}