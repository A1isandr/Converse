using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using YetAnotherMessenger.Misc;
using YetAnotherMessenger.MVVM.ViewModels;

namespace YetAnotherMessenger.MVVM.Models
{
    public class Conversation
    {
		public int Id { get; init; }

		public string ConversationName { get; set; } = string.Empty;

		public DateTime LastActivityTimeUtc { get; set; } = DateTime.UtcNow;

		public Uri? Avatar { get; set; }

		public ObservableCollection<User> Participants { get; init; } = [];

		public ObservableCollection<Message> Messages { get; init; } = [];
	}
}