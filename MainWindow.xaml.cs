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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using ReactiveUI;
using Splat;
using YetAnotherMessenger.MVVM.ViewModels;
using YetAnotherMessenger.MVVM.Views;

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
	}
}