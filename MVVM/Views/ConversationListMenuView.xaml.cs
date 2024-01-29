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
using ReactiveMarbles.ObservableEvents;
using YetAnotherMessenger.MVVM.ViewModels;

namespace YetAnotherMessenger.MVVM.Views
{
	/// <summary>
	/// Логика взаимодействия для ConversationListMenuView.xaml
	/// </summary>
	public partial class ConversationListMenuView : ReactiveUserControl<ConversationListMenuViewModel>
	{
		public ConversationListMenuView()
		{
			InitializeComponent();

			ViewModel = ConversationListMenuViewModel.Instance;

			this.WhenActivated(disposables =>
			{
				this.OneWayBind(ViewModel,
						viewModel => viewModel.ConversationPreviews,
						view => view.ConversationPreviewsPresenter.ItemsSource)
					.DisposeWith(disposables);

				this.OneWayBind(ViewModel,
						viewModel => viewModel.GlobalSearchPreviews,
						view => view.GlobalSearchPreviewsPresenter.ItemsSource)
					.DisposeWith(disposables);
			});
		}
	}
}
