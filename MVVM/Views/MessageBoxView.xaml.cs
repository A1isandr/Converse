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
using System.Xml.Linq;
using System.Xml.XPath;
using ReactiveUI;
using SharpVectors.Dom.Svg;
using YetAnotherMessenger.MVVM.ViewModels;
using SharpVectors.Converters;
using SharpVectors.Renderers.Wpf;
using System.Reflection.Metadata;
using System.Windows.Media.Animation;
using YetAnotherMessenger.Misc;

namespace YetAnotherMessenger.MVVM.Views
{
	/// <summary>
	/// Логика взаимодействия для MessageBoxView.xaml
	/// </summary>
	public partial class MessageBoxView : ReactiveUserControl<MessageBoxViewModel>
	{
		private readonly double _sendButtonWidth = 45;

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

		private void MessageBox_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			var textBox = (TextBox)sender;

			if (textBox.Text == string.Empty)
			{
				Animator.WidthAnimation
				(
					target: SendMessageButton,
					startingWidth: SendMessageButton.ActualWidth,
					targetWidth: 0,
					beginTime: TimeSpan.FromMilliseconds(0),
					duration: TimeSpan.FromMilliseconds(300),
					fillBehavior: FillBehavior.HoldEnd,
					easingFunction: new QuadraticEase{ EasingMode = EasingMode.EaseIn},
					onComplete: (_, _) => { SendMessageButton.Visibility = Visibility.Hidden; }
				);
			}
			else if (textBox.Text.Length == e.Changes.First().AddedLength)
			{
				Animator.WidthAnimation
				(
					target: SendMessageButton,
					startingWidth: SendMessageButton.ActualWidth,
					targetWidth: _sendButtonWidth,
					beginTime: TimeSpan.FromMilliseconds(0),
					duration: TimeSpan.FromMilliseconds(300),
					easingFunction: new QuadraticEase { EasingMode = EasingMode.EaseOut },
					onStart: () => { SendMessageButton.Visibility = Visibility.Visible; }
				);
			}
		}
	}
}
