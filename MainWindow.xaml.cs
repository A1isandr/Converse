using System.Reactive.Disposables;
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
using System.Windows.Media.Effects;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using ReactiveUI;
using Splat;
using YetAnotherMessenger.Misc;
using YetAnotherMessenger.MVVM.ViewModels;
using YetAnotherMessenger.MVVM.Views;

namespace YetAnotherMessenger
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
	{
		 private readonly Storyboard _blackOutStoryboard;
		 private readonly Storyboard _lighteningStoryboard;

		 private const string WindowMaximizeIconKey = "WindowMaximizeIcon";
		 private const string WindowRestoreIconKey = "WindowRestoreIcon";

		public MainWindow()
		{
			InitializeComponent();

			_blackOutStoryboard = new StoryboardBuilder()
				.AddDoubleAnimation
				(
					targetObject: BackgroundBlackOutEffectBorder,
					targetProperty: OpacityProperty,
					startingValue: 0,
					targetValue: 1,
					beginTime: TimeSpan.Zero,
					duration: TimeSpan.FromMilliseconds(300),
					easingFunction: new CubicEase() { EasingMode = EasingMode.EaseOut }
				)
				.End();

			_lighteningStoryboard = new StoryboardBuilder()
				.AddDoubleAnimation
				(
					targetObject: BackgroundBlackOutEffectBorder,
					targetProperty: OpacityProperty,
					startingValue: 1,
					targetValue: 0,
					beginTime: TimeSpan.Zero,
					duration: TimeSpan.FromMilliseconds(300),
					easingFunction: new CubicEase() { EasingMode = EasingMode.EaseOut }
				)
				.AddOnCompleteAction((_, _) => BackgroundBlackOutEffectBorder.Visibility = Visibility.Collapsed)
				.End();

			ViewModel = new MainWindowViewModel();

			ViewModel
				.WhenAnyValue(x => x.IsBlurRequired)
				.Subscribe(BlurAnimation);

			ViewModel
				.WhenAnyValue(x => x.IsBlackOutRequired)
				.Subscribe(BlackOutAnimation);

			this.WhenActivated(disposables =>
			{
				this.OneWayBind(ViewModel,
						viewModel => viewModel.AuthenticationWindowVM.IsOpen,
						view => view.AuthenticationWindow.Visibility)
					.DisposeWith(disposables);
			});
		}

		private void Header_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (WindowState == WindowState.Maximized)
			{
				var mouseWindowRelativeCoords = Mouse.GetPosition(this);

				Left = mouseWindowRelativeCoords.X;
				Top = mouseWindowRelativeCoords.Y - 5;

				WindowState = WindowState.Normal;
			}

			DragMove();
		}

		private void BlurAnimation(bool isRequired)
		{
			BackgroundBlurEffect.Radius = isRequired ? 20 : 0;
		}

		private void BlackOutAnimation(bool isRequired)
		{
			if (isRequired)
			{
				BackgroundBlackOutEffectBorder.Visibility = Visibility.Visible;
				_blackOutStoryboard.Begin();
			}
			else
			{
				_lighteningStoryboard.Begin();
			}
		}

		private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void MaxWindowButton_Click(object sender, RoutedEventArgs e)
		{
			switch (WindowState)
			{
				case WindowState.Maximized:
				{
					WindowState = WindowState.Normal;

					var resource = Application.Current.FindResource(WindowMaximizeIconKey);

					if (resource is not null)
					{
						MaxWindowButton.Content = ((GeometryDrawing)resource).Geometry;
					}

					break;
				}
				case WindowState.Normal:
				{
					WindowState = WindowState.Maximized;

					var resource = Application.Current.FindResource(WindowRestoreIconKey);

					if (resource is not null)
					{
						MaxWindowButton.Content = ((GeometryDrawing)resource).Geometry;
					}

					break;
				}
			}
		}

		private void MinWindowButton_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized;
		}
	}
}