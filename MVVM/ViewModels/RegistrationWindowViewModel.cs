using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reactive;
using System.Reactive.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using YetAnotherMessenger.Misc;

namespace YetAnotherMessenger.MVVM.ViewModels
{
    public class RegistrationWindowViewModel : ReactiveObject
    {
	    private static RegistrationWindowViewModel? _instance;

	    public static RegistrationWindowViewModel Instance
	    {
		    get
		    {
			    _instance ??= new RegistrationWindowViewModel();
			    return _instance;
		    }
	    }

		public ReactiveCommand<Unit, Unit> RegisterCommand { get; }

		[Reactive]
		public string Username { get; set; } = string.Empty;

		[Reactive] 
		public SecureString SecurePassword { get; set; } = new();

		[Reactive] 
		public SecureString ConfirmPassword { get; set; } = new();

		[Reactive]
		public string FirstName { get; set; } = string.Empty;

		[Reactive]
		public string? LastName { get; set; }

		private readonly ObservableAsPropertyHelper<string> _buttonText;
		public string ButtonText => _buttonText.Value;

		public RegistrationWindowViewModel()
		{
			var canRegister = this
				.WhenAnyValue(x => x.Username,
					x => x.SecurePassword,
					x => x.ConfirmPassword,
					x => x.FirstName,
					(username, password, confirm, name) =>
					!string.IsNullOrWhiteSpace(username) && password.Length != 0 && password.Equals(confirm) && !string.IsNullOrWhiteSpace(name))
				.DistinctUntilChanged();

			RegisterCommand = ReactiveCommand.CreateFromTask<Unit, Unit>(async _ =>
			{
				string password = new NetworkCredential("", SecurePassword).Password;
				var credentials = new RegistrationCredentials(Username, password, FirstName, LastName);

				var success = await AuthorizationService.RegisterAsync(credentials);

				if (success)
				{
					await AuthenticationWindowViewModel.Instance.CloseSelfCommand.Execute();
				}
				return Unit.Default;
			}, canRegister);

			_buttonText = RegisterCommand.IsExecuting
				.Select(isExecuting => isExecuting ? "Please Wait..." : "Register")
				.ToProperty(this, x => x.ButtonText, "Register");

			RegisterCommand.ThrownExceptions.Subscribe(error =>
			{
				MessageBox.Show($"{error.Message}\n----------\n{error.InnerException}\n----------\n{error.StackTrace}");
				Clipboard.SetText(error.Message);
			});
		}
	}
}
