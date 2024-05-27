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
	/// Логика взаимодействия для AuthenticationWindowView.xaml
	/// </summary>
	public partial class AuthenticationWindowView : ReactiveUserControl<AuthenticationWindowViewModel>
	{
		public AuthenticationWindowView()
		{
			InitializeComponent();

			ViewModel = AuthenticationWindowViewModel.Instance;

			this.WhenActivated(disposables =>
			{
				this.BindCommand(ViewModel,
						viewModel => viewModel.CloseSelfCommand,
						view => view.CloseWindowButton)
					.DisposeWith(disposables);

				this.OneWayBind(ViewModel,
						viewModel => viewModel.IsNotBusy,
						view => view.IsEnabled)
					.DisposeWith(disposables);

				ToggleButton
					.Events()
					.Checked
					.Subscribe(_ =>
					{
						LogInWindowExpander.IsExpanded = false;
						RegistrationWindowExpander.IsExpanded = true;
					})
					.DisposeWith(disposables);

				ToggleButton
					.Events()
					.Unchecked
					.Subscribe(_ =>
					{
						LogInWindowExpander.IsExpanded = true;
						RegistrationWindowExpander.IsExpanded = false;
					})
					.DisposeWith(disposables);
			});
		}
	}
}
