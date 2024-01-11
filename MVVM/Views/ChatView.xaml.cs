using ReactiveUI;
using System;
using System.Collections.Generic;
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
using YetAnotherMessenger.MVVM.ViewModels;

namespace YetAnotherMessenger.MVVM.Views
{
	/// <summary>
	/// Логика взаимодействия для ChatView.xaml
	/// </summary>
	public partial class ChatView : ReactiveUserControl<ChatViewModel>
	{
		public ChatView()
		{
			InitializeComponent();

			ViewModel = ChatViewModel.Instance;

			this.WhenActivated(disposable =>
			{
				this.WhenAnyValue(x => x.ViewModel!.IsMenuButtonVisible)
					.BindTo(this, x => x.MenuButton.Visibility)
					.DisposeWith(disposable);
			});
		}
	}
}
