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
	/// Логика взаимодействия для MainMenuView.xaml
	/// </summary>
	public partial class MainMenuView : ReactiveUserControl<MainMenuViewModel>
	{
		public MainMenuView()
		{
			InitializeComponent();

			ViewModel = MainMenuViewModel.Instance;

			this.WhenActivated(disposables =>
			{
				this.BindCommand(ViewModel,
						viewModel => viewModel.CloseMainMenuCommand,
						view => view.CloseMainMenuButton)
					.DisposeWith(disposables);
			});
		}
	}
}
