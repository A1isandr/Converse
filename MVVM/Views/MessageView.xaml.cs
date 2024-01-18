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
	/// Логика взаимодействия для MessageView.xaml
	/// </summary>
	public partial class MessageView : ReactiveUserControl<MessageViewModel>
	{
		public MessageView()
		{
			InitializeComponent();

			this.WhenActivated(disposables =>
			{
				this.OneWayBind(ViewModel,
						viewModel => viewModel.Message.Content,
						view => view.MessageContent.Text)
					.DisposeWith(disposables);

				this.OneWayBind(ViewModel,
						viewModel => viewModel.Message.HourMinute,
						view => view.MessageDateTime.Content)
					.DisposeWith(disposables);
			});
		}
	}
}
