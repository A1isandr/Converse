using System.Reactive.Linq;
using System.Text;
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
using System.Windows.Shell;
using ReactiveUI;
using Splat;
using YetAnotherMessenger.MVVM.ViewModels;

namespace YetAnotherMessenger
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
	{
		public MainWindow()
		{
			InitializeComponent();

			ViewModel = new MainWindowViewModel();

			//_minMenuWrapperWidth = (int)MainView.MainGrid.ColumnDefinitions[0].MinWidth;

			this.WhenAnyValue(x => x.ActualWidth)
				.Subscribe(width => ViewModel?.UpdateChatCollapseState(width));
		}


		private void Header_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (WindowState == WindowState.Maximized)
			{
				Point mouseWindowRelativeCoords = Mouse.GetPosition(this);

				Left = mouseWindowRelativeCoords.X;
				Top = mouseWindowRelativeCoords.Y - 5;

				WindowState = WindowState.Normal;
			}

			DragMove();
		}

		private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void MaxWindowButton_Click(object sender, RoutedEventArgs e)
		{
			WindowState = (WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized);
		}

		private void MinWindowButton_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized;
		}

		//private void WindowStartUpClosingAnimation((double, double, int) width, (double, double, int) height, (double, double, int) opacity, EventHandler? afterCompletion = null)
		//{
		//	DoubleAnimation widthAnimation = new DoubleAnimation
		//	{
		//		From = width.Item1,
		//		To = width.Item2,
		//		Duration = TimeSpan.FromMilliseconds(width.Item3),
		//		EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
		//	};

		//	DoubleAnimation heightAnimation = new DoubleAnimation
		//	{
		//		From = height.Item1,
		//		To = height.Item2,
		//		Duration = TimeSpan.FromMilliseconds(height.Item3),
		//		BeginTime = TimeSpan.FromMilliseconds(width.Item3),
		//		EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
		//	};

		//	DoubleAnimation opacityAnimation = new DoubleAnimation
		//	{
		//		From = opacity.Item1,
		//		To = opacity.Item2,
		//		Duration = TimeSpan.FromMilliseconds(opacity.Item3),
		//		EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
		//	};

		//	if (afterCompletion is not null)
		//	{
		//		opacityAnimation.Completed += afterCompletion;
		//	}

		//	Storyboard storyboard = new Storyboard();

		//	storyboard.Children.Add(widthAnimation);
		//	storyboard.Children.Add(heightAnimation);
		//	storyboard.Children.Add(opacityAnimation);

		//	Storyboard.SetTarget(widthAnimation, this);
		//	Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(WidthProperty));

		//	Storyboard.SetTarget(heightAnimation, this);
		//	Storyboard.SetTargetProperty(heightAnimation, new PropertyPath(HeightProperty));

		//	Storyboard.SetTarget(opacityAnimation, this);
		//	Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(OpacityProperty));

		//	storyboard.Begin();
		//}
	}
}