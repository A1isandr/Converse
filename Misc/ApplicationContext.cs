using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.Logging;
using YetAnotherMessenger.MVVM.Models;

namespace YetAnotherMessenger.Misc
{
	internal sealed class ApplicationContext : DbContext
	{
		public DbSet<User> Users => Set<User>();
		public DbSet<Conversation> Conversations => Set<Conversation>();
		public DbSet<Message> Messages => Set<Message>();
		public DbSet<MessageContent> MessageContents => Set<MessageContent>();

		private static readonly string LogPath = Path.GetFullPath(@"Logs\DBLog.txt");

		private readonly StreamWriter _logStream = new(LogPath, append: false);

		public ApplicationContext()
		{
			//Database.EnsureDeleted();
			Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var connectionString = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.SetBasePath(Directory.GetCurrentDirectory())
				.Build()
				.GetConnectionString("DefaultConnection");

			optionsBuilder.UseMySql(connectionString,
				ServerVersion.AutoDetect(connectionString));

			optionsBuilder.LogTo(Console.WriteLine);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			User bob = new() { Id = 1, Username = "BobOd", Password = "123"};
			User tom = new() { Id = 2, Username = "Tommm", Password = "321"};
			User alexandr = new() { Id = 3, Username = "AlPi", Password = "012"};

			UserProfile bobProfile = new() { Id = 1, Avatar = new Uri("https://i.pravatar.cc/300"), FirstName = "Bob", LastName = "Odenkirk", UserId = bob.Id };
			UserProfile tomProfile = new() { Id = 2, Avatar = new Uri("https://i.pravatar.cc/299"), FirstName = "Tom", UserId = tom.Id };
			UserProfile alexandrProfile = new() { Id = 3, Avatar = new Uri("https://i.pravatar.cc/298"), FirstName = "Alexandr", LastName = "Pi", UserId = alexandr.Id };

			//Conversation conversation1 = new() { Id = 1, ConversationName = "Test1", Participants = [alexandr, bob] };
			//Conversation conversation2 = new() { Id = 2, ConversationName = "Test2", Participants = [alexandr, tom] };

			modelBuilder.Entity<User>().HasData(bob, tom, alexandr);
			modelBuilder.Entity<UserProfile>().HasData(bobProfile, tomProfile, alexandrProfile);
			//modelBuilder.Entity<Conversation>().HasData(conversation1, conversation2);
		}

		public override void Dispose()
		{
			base.Dispose();
			_logStream.Dispose();
		}

		public override async ValueTask DisposeAsync()
		{
			await base.DisposeAsync();
			await _logStream.DisposeAsync();
		}
	}
}
