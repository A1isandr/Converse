using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ReactiveUI.Fody.Helpers;
using YetAnotherMessenger.MVVM.Views;

namespace YetAnotherMessenger.MVVM.ViewModels
{
	public class MainWindowViewModel : ReactiveObject
	{
		[Reactive]
		public MainViewModel MainVM { get; set; } = new();

		//[Reactive] 
		//public object CurrentLayout { get; private set; } = new MainView();

		//public void MainWindow_WidthChanged(double width)
		//{
		//	if (width <= VerticalModeStartingWidth && CurrentLayout is MainView)
		//	{
		//		CurrentLayout = new VerticalMainView();
		//	}
		//	else if (width > VerticalModeStartingWidth && CurrentLayout is VerticalMainView)
		//	{
		//		CurrentLayout = new MainView();
		//	}
		//}
	}
}
