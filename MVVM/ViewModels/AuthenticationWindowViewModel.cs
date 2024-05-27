using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using YetAnotherMessenger.Misc;
using YetAnotherMessenger.MVVM.Models;

namespace YetAnotherMessenger.MVVM.ViewModels
{
	public class AuthenticationWindowViewModel : ReactiveObject
	{
		private static AuthenticationWindowViewModel? _instance;

		public static AuthenticationWindowViewModel Instance
		{
			get
			{
				_instance ??= new AuthenticationWindowViewModel();
				return _instance;
			}
		}

		public LogInWindowViewModel LogInWindowVM = LogInWindowViewModel.Instance;

		public RegistrationWindowViewModel RegistrationWindowVM = RegistrationWindowViewModel.Instance;

		public ReactiveCommand<Unit, Unit> OpenSelfCommand { get; }

		public ReactiveCommand<Unit, Unit> CloseSelfCommand { get; }

		[Reactive]
		public bool IsOpen { get; private set; }

		private readonly ObservableAsPropertyHelper<bool> _isNotBusy;
		public bool IsNotBusy => _isNotBusy.Value;

		public AuthenticationWindowViewModel()
		{
			//var canClose = this
			//	.WhenAnyValue(x => AuthorizationService.GetToken().Result)
			//	.Select(token => token is not null);

			OpenSelfCommand = ReactiveCommand.CreateFromObservable<Unit, Unit>(_ =>
			{
				IsOpen = !IsOpen;

				return Observable.Return(Unit.Default);
			});

			CloseSelfCommand = ReactiveCommand.CreateFromObservable<Unit, Unit>(_ =>
			{
				IsOpen = !IsOpen;

				return Observable.Return(Unit.Default);
			});

			_isNotBusy = LogInWindowVM.LogInCommand.IsExecuting
				.Select(state => !state)
				.ToProperty(this, x => x.IsNotBusy);
		}
	}
}
