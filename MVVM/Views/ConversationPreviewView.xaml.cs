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
	/// Логика взаимодействия для ConversationPreviewView.xaml
	/// </summary>
	public partial class ConversationPreviewView : ReactiveUserControl<ConversationPreviewViewModel>
	{
		public ConversationPreviewView()
		{
			InitializeComponent();

			this.WhenActivated(disposables =>
			{
				this.OneWayBind(ViewModel,
						viewModel => viewModel.Conversation.Avatar,
						view => view.Avatar.Source,
						url => new BitmapImage(url))
					.DisposeWith(disposables);

				this.OneWayBind(ViewModel,
						viewModel => viewModel.Conversation.ConversationName,
						view => view.UserName.Text)
					.DisposeWith(disposables);

				this.OneWayBind(ViewModel,
						viewModel => viewModel.LastMessagePrescription,
						view => view.LastMessageTime.Text)
					.DisposeWith(disposables);

				this.OneWayBind(ViewModel,
						viewModel => viewModel.LastMessage!.Content,
						view => view.LastMessage.Text)
					.DisposeWith(disposables);

				this.BindCommand(ViewModel,
						viewModel => viewModel.OpenChatCommand,
						view => view.RadioButton,
						nameof(RadioButton.Checked))
					.DisposeWith(disposables);
			});
		}
	}
}
