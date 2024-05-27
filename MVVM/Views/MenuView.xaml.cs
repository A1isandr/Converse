using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using YetAnotherMessenger.Misc;
using YetAnotherMessenger.MVVM.ViewModels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace YetAnotherMessenger.MVVM.Views
{
	/// <summary>
	/// Логика взаимодействия для MenuView.xaml
	/// </summary>
	public partial class MenuView : ReactiveUserControl<MenuViewModel>
	{
		public MenuView()
		{
			InitializeComponent();

			ViewModel = MenuViewModel.Instance;

			this.WhenActivated(disposables =>
			{
				this.OneWayBind(ViewModel,
						viewModel => viewModel.IsChatListOpen,
						view => view.ListMenuExpander.IsExpanded)
					.DisposeWith(disposables);

				this.OneWayBind(ViewModel,
						viewModel => viewModel.IsMainMenuOpen,
						view => view.MainMenuExpander.IsExpanded)
					.DisposeWith(disposables);

				MainMenuView.CloseMainMenuButton
					.Events()
					.Click
					.Subscribe(_ =>
					{
						ViewModel.IsMainMenuOpen = false;
						ViewModel.IsChatListOpen = true;
					})
					.DisposeWith(disposables);

				ListMenuView.MainMenuButton
					.Events()
					.Click
					.Subscribe(_ =>
					{
						ViewModel.IsChatListOpen = false;
						ViewModel.IsMainMenuOpen = true;
					})
					.DisposeWith(disposables);
			});
		}
	}
}