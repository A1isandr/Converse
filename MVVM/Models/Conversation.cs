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

		[MaxLength(50)] 
		public string ConversationName { get; set; } = string.Empty;

		[Column("LastActivityTimeUtc")]
		public DateTime LastActivityTime { get; set; } = DateTime.UtcNow;

		public ObservableCollection<User> Participants { get; set; } = [];

		public ObservableCollection<Message> Messages { get; set; } = [];

		public Conversation()
		{
			Messages.CollectionChanged += UpdateLastActivityTime;
			Participants.CollectionChanged += UpdateLastActivityTime;
		}

		private void UpdateLastActivityTime(object? sender, NotifyCollectionChangedEventArgs args)
		{
			using ApplicationContext db = new();

			LastActivityTime = DateTime.UtcNow;

			db.SaveChanges();
		}
	}
}