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
				NewAccountButton
					.Events()
					.Click
					.Subscribe(_ => AuthenticationWindowViewModel.Instance.OpenSelfCommand.Execute())
					.DisposeWith(disposables);
			});
		}
	}
}
