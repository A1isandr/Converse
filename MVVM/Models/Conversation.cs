using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using YetAnotherMessenger.MVVM.ViewModels;

namespace YetAnotherMessenger.MVVM.Models
{
    public class Conversation
    {
		public int Id { get; init; }

		[MaxLength(50)]
		public required string ConversationName { get; set; }

		public DateTime LastActivityTime { get; set; }

		public List<User> Participants { get; set; } = [];

		public List<Message>? Messages { get; set; } = [];
	}
}