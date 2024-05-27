using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using YetAnotherMessenger.MVVM.ViewModels;
using System.Reflection.Metadata;
using System.Windows.Media.Animation;
using YetAnotherMessenger.Misc;
using System.Reactive.Linq;
using ReactiveMarbles.ObservableEvents;

namespace YetAnotherMessenger.MVVM.Views
{
	/// <summary>
	/// Логика взаимодействия для MessageBoxView.xaml
	/// </summary>
	public partial class MessageBoxView : ReactiveUserControl<MessageBoxViewModel>
	{
		private readonly double _sendButtonWidth = 45;

		private readonly Storyboard _sendMessageButtonSlideIn;

		public MessageBoxView()
		{
			InitializeComponent();

			_sendMessageButtonSlideIn = new StoryboardBuilder()
				.AddDoubleAnimation
				(
					targetObject: SendMessageButton,
					targetProperty: WidthProperty,
					startingValue: SendMessageButton.ActualWidth,
					targetValue: _sendButtonWidth,
					beginTime: TimeSpan.Zero,
					duration: TimeSpan.FromMilliseconds(300),
					easingFunction: new QuadraticEase { EasingMode = EasingMode.EaseInOut }
				)
				.Build();

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

				this.Events().KeyUp
					.Select(x => x.Key)
					.Where(key => key == Key.Enter && (Keyboard.Modifiers & ModifierKeys.Shift) != ModifierKeys.Shift)
					.InvokeCommand(ViewModel.SendMessageCommand);

				this.Events().KeyDown
					.Select(x => x.Key)
					.Where(key => key == Key.Enter && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
					.Subscribe(_ =>
					{
						MessageBox.Text += "\r\n";
						MessageBox.CaretIndex = MessageBox.Text.Length;
					});
			});
		}

		private void MessageBox_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			var textBox = (TextBox)sender;

			if (textBox.Text == string.Empty)
			{
				new StoryboardBuilder()
					.AddDoubleAnimation
					(
						targetObject: SendMessageButton,
						targetProperty: WidthProperty,
						startingValue: SendMessageButton.ActualWidth,
						targetValue: 0,
						beginTime: TimeSpan.Zero,
						duration: TimeSpan.FromMilliseconds(300),
						fillBehavior: FillBehavior.HoldEnd,
						easingFunction: new QuadraticEase { EasingMode = EasingMode.EaseInOut }
					)
					.Build()
					.Begin();
			}
			else if (textBox.Text.Length == e.Changes.First().AddedLength)
			{
				_sendMessageButtonSlideIn.Begin();
			}
		}
	}
}
