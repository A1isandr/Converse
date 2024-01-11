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
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : ReactiveUserControl<MainViewModel>
    {
		public MainView()
        {
            InitializeComponent();

            ViewModel = MainViewModel.Instance;
            var disposables = new CompositeDisposable();

			this.WhenAnyValue(x => x.ViewModel!.MenuColumnMinWidth)
				.BindTo(this, x => x.MenuColumn.MinWidth)
				.DisposeWith(disposables);

			this.WhenAnyValue(x => x.ViewModel!.MenuColumnWidth)
				.BindTo(this, x => x.MenuColumn.Width)
				.DisposeWith(disposables);
		}
    }
}
