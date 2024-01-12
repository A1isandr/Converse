using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ReactiveUI;
using YetAnotherMessenger.MVVM.ViewModels;

namespace YetAnotherMessenger.MVVM.Views
{
    /// <summary>
    /// Логика взаимодействия для MenuView.xaml
    /// </summary>
    public partial class MenuView :  ReactiveUserControl<MenuViewModel>
    {
		public MenuView()
        {
            InitializeComponent();

			ViewModel = MenuViewModel.Instance;

			this.WhenActivated(disposables =>
			{
				this.OneWayBind(this.ViewModel, 
						viewModel => viewModel.ChatPreviews, 
						view => view.ChatPreviewsPresenter.ItemsSource)
					.DisposeWith(disposables);
			});
        }
    }
}
