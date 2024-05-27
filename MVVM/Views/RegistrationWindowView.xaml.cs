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
using ReactiveUI;
using YetAnotherMessenger.MVVM.ViewModels;

namespace YetAnotherMessenger.MVVM.Views
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindowView.xaml
    /// </summary>
    public partial class RegistrationWindowView : ReactiveUserControl<RegistrationWindowViewModel>
    {
        public RegistrationWindowView()
        {
            InitializeComponent();

            ViewModel = RegistrationWindowViewModel.Instance;

            this.WhenActivated(disposables =>
            {
	            this.Bind(ViewModel,
			            viewModel => viewModel.FirstName,
			            view => view.FirstNameBox.Text)
		            .DisposeWith(disposables);

				this.Bind(ViewModel,
						viewModel => viewModel.LastName,
						view => view.LastNameBox.Text)
					.DisposeWith(disposables);

				this.Bind(ViewModel,
						viewModel => viewModel.Username,
						view => view.UsernameBox.Text)
					.DisposeWith(disposables);

				this.OneWayBind(ViewModel,
						viewModel => viewModel.ButtonText,
						view => view.RegisterButton.Content)
					.DisposeWith(disposables);

				this.BindCommand(ViewModel,
						viewModel => viewModel.RegisterCommand,
						view => view.RegisterButton)
					.DisposeWith(disposables);
            });
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
	        ViewModel!.SecurePassword = ((PasswordBox)sender).SecurePassword;
        }


        private void PasswordConfirmationBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
	        ViewModel!.ConfirmPassword = ((PasswordBox)sender).SecurePassword;
        }
    }
}
