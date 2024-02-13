using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YetAnotherMessenger.MVVM.Models;

namespace YetAnotherMessenger.Misc
{
	internal sealed class ApplicationContext : DbContext
	{
		public DbSet<User> Users => Set<User>();
		public DbSet<Conversation> Conversations => Set<Conversation>();
		public DbSet<Message> Messages => Set<Message>();
		public DbSet<TextMessage> TextMessages => Set<TextMessage>();
		public DbSet<MessageContent> MessageContents => Set<MessageContent>();
		public DbSet<Attachment> Attachments => Set<Attachment>();

		private static readonly string LogPath = Path.GetFullPath(@"Logs\DBLog.txt");

		private static readonly StreamWriter LogStream;

		static ApplicationContext()
		{
			LogStream = new StreamWriter(LogPath, append: false);
		}

		public ApplicationContext()
		{
			//Database.EnsureDeleted();
			Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

			optionsBuilder.UseMySql(connectionString,
				ServerVersion.AutoDetect(connectionString));

			optionsBuilder.LogTo(Console.WriteLine);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			User system = new() { Id = 1, Username = "System", Password = "IamR0BOT", IsSystem = true};
			User bob = new() { Id = 2, Username = "bOb", Password = "123"};
			User tom = new() { Id = 3, Username = "Tommm", Password = "321"};
			User alexandr = new() { Id = 4, Username = "AlPi", Password = "012"};
			User bonnie = new() { Id = 5, Username = "boni", Password = "210" };

			UserProfile bobProfile = new() { Id = 1, Avatar = new Uri("https://i.pravatar.cc/300"), FirstName = "Bob", LastName = "Bullock", UserId = bob.Id };
			UserProfile tomProfile = new() { Id = 2, Avatar = new Uri("https://i.pravatar.cc/299"), FirstName = "Tom", LastName = "Horne", UserId = tom.Id };
			UserProfile alexandrProfile = new() { Id = 3, Avatar = new Uri("https://i.pravatar.cc/298"), FirstName = "Alexandr", LastName = "Pi", UserId = alexandr.Id };
			UserProfile bonnieProfile = new() { Id = 4, Avatar = new Uri("https://i.pravatar.cc/297"), FirstName = "Bonnie", LastName = "Garrett", UserId = bonnie.Id };


			//Conversation conversation1 = new() { Id = 1, ConversationName = "Test1", Participants = [alexandr, bob] };
			//Conversation conversation2 = new() { Id = 2, ConversationName = "Test2", Participants = [alexandr, tom] };

			modelBuilder.Entity<User>().HasData(system, bob, tom, alexandr, bonnie);
			modelBuilder.Entity<UserProfile>().HasData(bobProfile, tomProfile, alexandrProfile, bonnieProfile);
			//modelBuilder.Entity<Conversation>().HasData(conversation1, conversation2);
		}

		public override void Dispose()
		{
			base.Dispose();
			LogStream.Dispose();
		}

		public override async ValueTask DisposeAsync()
		{
			await base.DisposeAsync();
			await LogStream.DisposeAsync();
		}
	}
}
