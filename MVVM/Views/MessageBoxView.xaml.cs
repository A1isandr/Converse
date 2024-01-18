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
	/// Логика взаимодействия для MessageBoxView.xaml
	/// </summary>
	public partial class MessageBoxView : ReactiveUserControl<MessageBoxViewModel>
	{
		public MessageBoxView()
		{
			InitializeComponent();

			ViewModel = MessageBoxViewModel.Instance;

			this.WhenActivated(disposables =>
			{
				this.Bind(ViewModel,
						viewModel => viewModel.MessageDraft,
						view => view.MessageBox.Text)
					.DisposeWith(disposables);

				this.BindCommand(ViewModel,
						viewModel => viewModel.SendMessageCommand,
						view => view.SendMessageButton)
					.DisposeWith(disposables);
			});
		}

		private void MessageBox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			var textBox = (TextBox)sender;

			if (e.Key != Key.Enter || !Keyboard.IsKeyDown(Key.LeftShift)) return;

			textBox.Text += "\r\n";
			textBox.CaretIndex = textBox.Text.Length;
			e.Handled = true;
		}
	}
}
