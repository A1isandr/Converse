using ReactiveUI;
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
using YetAnotherMessenger.MVVM.ViewModels;

namespace YetAnotherMessenger.MVVM.Views
{
	/// <summary>
	/// Логика взаимодействия для ChatListMenuView.xaml
	/// </summary>
	public partial class ChatListMenuView : ReactiveUserControl<ChatListMenuViewModel>
	{
		public ChatListMenuView()
		{
			InitializeComponent();

			ViewModel = ChatListMenuViewModel.Instance;

			this.WhenActivated(disposables =>
			{
				this.OneWayBind(ViewModel,
						viewModel => viewModel.ChatPreviews,
						view => view.ChatPreviewsPresenter.ItemsSource)
					.DisposeWith(disposables);

				this.BindCommand(ViewModel,
						viewModel => viewModel.OpenMainMenuCommand,
						view => view.MainMenuButton)
					.DisposeWith(disposables);
			});
		}
	}
}
