using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive;
using System.Reactive.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using YetAnotherMessenger.Misc;
using YetAnotherMessenger.MVVM.Models;

namespace YetAnotherMessenger.MVVM.ViewModels
{
	public class LogInWindowViewModel : ReactiveObject
	{
		private static LogInWindowViewModel? _instance;
		/// <summary>
		/// Returns the singleton instance.
		/// </summary>
		public static LogInWindowViewModel Instance
		{
			get
			{
				_instance ??= new LogInWindowViewModel();
				return _instance;
			}
		}

		public ReactiveCommand<Unit, Unit> LogInCommand { get; }

		[Reactive] 
		public string Username { get; set; } = string.Empty;

		[Reactive] 
		public SecureString SecurePassword { get; set; } = new();

		private readonly ObservableAsPropertyHelper<string> _buttonText;
		public string ButtonText => _buttonText.Value;

		public LogInWindowViewModel()
		{
			var canLogin = this
				.WhenAnyValue(x => x.Username, x => x.SecurePassword,
					(username, password) =>
						!string.IsNullOrWhiteSpace(username) &&
						password.Length != 0)
				.DistinctUntilChanged();

			LogInCommand = ReactiveCommand.CreateFromTask<Unit, Unit>(async _ =>
			{
				string password = new NetworkCredential("", SecurePassword).Password;
				var credentials = new Credentials(Username, password);

				var success = await AuthorizationService.LogInAsync(credentials);

				if (success)
				{
					await AuthenticationWindowViewModel.Instance.CloseSelfCommand.Execute();
				}
				return Unit.Default;
			}, canLogin);

			_buttonText = LogInCommand.IsExecuting
				.Select(isExecuting => isExecuting ? "Please Wait..." : "Log In")
				.ToProperty(this, x => x.ButtonText, "Log In");

			LogInCommand.ThrownExceptions.Subscribe(error =>
			{
				MessageBox.Show($"{error.Message}\n----------\n{error.InnerException}\n----------\n{error.StackTrace}");
				Clipboard.SetText(error.Message);
			});
		}
	}
}
