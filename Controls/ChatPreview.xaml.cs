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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ABI.Windows.UI.Core.AnimationMetrics;
using ReactiveUI;
using SharpVectors.Dom;
using YetAnotherMessenger.MVVM.ViewModels;

namespace YetAnotherMessenger.MVVM.Views
{
	/// <summary>
	/// Логика взаимодействия для ChatPreview.xaml
	/// </summary>
	public partial class ChatPreview : ReactiveUserControl<ChatPreviewViewModel>
	{
		public ChatPreview()
		{
			InitializeComponent();

			this.WhenActivated(disposable =>
			{
				// Our 4th parameter we convert from Url into a BitmapImage. 
				// This is an easy way of doing value conversion using ReactiveUI binding.
				this.OneWayBind(ViewModel,
						viewModel => viewModel.AvatarUri,
						view => view.Avatar.Source,
						url => new BitmapImage(url))
					.DisposeWith(disposable);

				this.OneWayBind(ViewModel,
						viewModel => viewModel.Name,
						view => view.UserName.Text)
					.DisposeWith(disposable);

				this.OneWayBind(ViewModel,
						viewModel => viewModel.LastMessageTime,
						view => view.LastMessageTime.Text)
					.DisposeWith(disposable);

				this.OneWayBind(ViewModel,
						viewModel => viewModel.LastMessage,
						view => view.LastMessage.Text)
					.DisposeWith(disposable);
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
