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
	/// Логика взаимодействия для ChatPreviewView.xaml
	/// </summary>
	public partial class ChatPreviewView : ReactiveUserControl<ChatPreviewViewModel>
	{
		public ChatPreviewView()
		{
			InitializeComponent();

			this.WhenActivated(disposables =>
			{
				this.OneWayBind(ViewModel,
						viewModel => viewModel.Chat.Avatar,
						view => view.Avatar.Source,
						url => new BitmapImage(url))
					.DisposeWith(disposables);

				this.OneWayBind(ViewModel,
						viewModel => viewModel.Chat.Name,
						view => view.UserName.Text)
					.DisposeWith(disposables);

				this.OneWayBind(ViewModel,
						viewModel => viewModel.LastMessagePrescription,
						view => view.LastMessageTime.Text)
					.DisposeWith(disposables);

				this.OneWayBind(ViewModel,
						viewModel => viewModel.Chat.LastMessage!.Content,
						view => view.LastMessage.Text)
					.DisposeWith(disposables);

				this.BindCommand(ViewModel,
						viewModel => viewModel.OpenChatCommand,
						view => view.RadioButton,
						nameof(RadioButton.Checked))
					.DisposeWith(disposables);
			});
		}

		//private void ChatPreview_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		//{
		//	var chatPreview = (RadioButton)sender;

		//	Point relativePoint = chatPreview.TranslatePoint(new Point(0, 0), this);

		//	Point mousePosition = e.GetPosition(this);

		//	Ellipse wave = new Ellipse
		//	{
		//		Width = 0,
		//		Height = 0,
		//		Fill = new SolidColorBrush(Colors.Red),
		//		StrokeThickness = 1,
		//		Clip = new RectangleGeometry(new Rect(relativePoint, new Size(chatPreview.ActualWidth, chatPreview.ActualHeight)), 10, 10)
		//	};

		//	wave.SetValue(Canvas.LeftProperty, mousePosition.X);
		//	wave.SetValue(Canvas.TopProperty, mousePosition.Y);
		//	wave.SetValue(Panel.ZIndexProperty, -1);
		//	ChatPreviewGrid.Children.Add(wave);

		//	DoubleAnimation waveWidthAnimation = new DoubleAnimation
		//	{
		//		From = wave.ActualWidth,
		//		To = chatPreview.ActualWidth + 100,
		//		Duration = TimeSpan.FromMilliseconds(1000),
		//		EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
		//	};

		//	DoubleAnimation waveHeightAnimation = new DoubleAnimation
		//	{
		//		From = wave.ActualHeight,
		//		To = chatPreview.ActualHeight + 100,
		//		Duration = TimeSpan.FromMilliseconds(1000),
		//		EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
		//	};

		//	DoubleAnimation waveOpacityAnimation = new DoubleAnimation
		//	{
		//		From = 1,
		//		To = 0,
		//		Duration = TimeSpan.FromMilliseconds(2000),
		//		EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
		//	};

		//	Storyboard storyboard = new Storyboard();

		//	storyboard.Completed += (s, e) => ChatPreviewGrid.Children.Remove(wave);

		//	storyboard.Children.Add(waveWidthAnimation);
		//	storyboard.Children.Add(waveHeightAnimation);
		//	storyboard.Children.Add(waveOpacityAnimation);

		//	Storyboard.SetTarget(waveWidthAnimation, wave);
		//	Storyboard.SetTargetProperty(waveWidthAnimation, new PropertyPath(WidthProperty));

		//	Storyboard.SetTarget(waveHeightAnimation, wave);
		//	Storyboard.SetTargetProperty(waveHeightAnimation, new PropertyPath(HeightProperty));

		//	Storyboard.SetTarget(waveOpacityAnimation, wave);
		//	Storyboard.SetTargetProperty(waveOpacityAnimation, new PropertyPath(OpacityProperty));

		//	storyboard.Begin();
		//}
	}
}
