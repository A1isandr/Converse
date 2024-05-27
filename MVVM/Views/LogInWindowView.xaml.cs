using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using YetAnotherMessenger.MVVM.ViewModels;

namespace YetAnotherMessenger.MVVM.Views
{
	/// <summary>
	/// Логика взаимодействия для LogInWindowView.xaml
	/// </summary>
	public partial class LogInWindowView : ReactiveUserControl<LogInWindowViewModel>
	{
		public LogInWindowView()
		{
			InitializeComponent();

			ViewModel = LogInWindowViewModel.Instance;

			this.WhenActivated(disposables =>
			{
				this.Bind(ViewModel,
						viewModel => viewModel.Username,
						view => view.UsernameBox.Text)
					.DisposeWith(disposables);

				this.BindCommand(ViewModel,
						viewModel => viewModel.LogInCommand,
						view => view.LogInButton)
					.DisposeWith(disposables);

				this.OneWayBind(ViewModel,
						viewModel => viewModel.ButtonText,
						view => view.LogInButton.Content)
					.DisposeWith(disposables);

				PasswordBox.Events()
					.PasswordChanged
					.Subscribe(e =>
					{
						if (ViewModel is not null)
						{
							ViewModel.SecurePassword = ((PasswordBox)e.Source).SecurePassword;
						}
					})
					.DisposeWith(disposables);
			});
		}
	}
}
