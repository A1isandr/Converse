using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Security.Policy;
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
using YetAnotherMessenger.MVVM.ViewModels;

namespace YetAnotherMessenger.MVVM.Views
{
	/// <summary>
	/// Логика взаимодействия для UserPreviewView.xaml
	/// </summary>
	public partial class UserPreviewView : ReactiveUserControl<UserPreviewViewModel>
	{
		public UserPreviewView()
		{
			InitializeComponent();

			ViewModel = new UserPreviewViewModel();

			this.WhenActivated(disposables =>
			{
				this.OneWayBind(ViewModel,
						viewModel => viewModel.User.Profile!.Avatar,
						view => view.Avatar.Source,
						url => new BitmapImage(url))
					.DisposeWith(disposables);

				this.OneWayBind(ViewModel,
						viewModel => viewModel.User.Profile!.FullName,
						view => view.FullName.Text)
					.DisposeWith(disposables);

				this.OneWayBind(ViewModel,
						viewModel => viewModel.User.Username,
						view => view.UserName.Text)
					.DisposeWith(disposables);

				this.BindCommand(ViewModel,
						viewModel => viewModel.StartNewConversation,
						view => view.Button)
					.DisposeWith(disposables);
			});
		}
	}
}
