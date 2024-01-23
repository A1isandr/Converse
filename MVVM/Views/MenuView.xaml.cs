using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
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
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using YetAnotherMessenger.Misc;
using YetAnotherMessenger.MVVM.ViewModels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace YetAnotherMessenger.MVVM.Views
{
	/// <summary>
	/// Логика взаимодействия для MenuView.xaml
	/// </summary>
	public partial class MenuView : ReactiveUserControl<MenuViewModel>
	{
		public MenuView()
		{
			InitializeComponent();

			ViewModel = MenuViewModel.Instance;

			this.WhenActivated(disposables =>
			{
				this.OneWayBind(ViewModel,
						viewModel => viewModel.IsMainMenuOpen,
						view => view.MainMenuExpander.IsExpanded)
					.DisposeWith(disposables);

				this.OneWayBind(ViewModel,
						viewModel => viewModel.IsChatListOpen,
						view => view.ChatListMenuExpander.IsExpanded)
					.DisposeWith(disposables);
			});

			MainMenuExpander.Margin = new Thickness(250, 0, -250, 0);
		}

		private void MainMenuExpander_OnCollapsed(object sender, RoutedEventArgs e)
		{
			var menu = (Expander)sender;
			var targetOffsetLeft = menu.ActualWidth;
			var targetOffsetRight = menu.ActualWidth;

			switch (menu.ExpandDirection)
			{
				case ExpandDirection.Left:
					targetOffsetRight *= -1;
					break;
				case ExpandDirection.Right:
					targetOffsetLeft *= -1;
					break;
			}

			Animator.SlideAnimation
			(
				target: menu,
				targetOffsetLeft: targetOffsetLeft,
				targetOffsetRight: targetOffsetRight,
				beginTime: TimeSpan.FromMilliseconds(0),
				duration: TimeSpan.FromMilliseconds(300),
				fillBehavior: FillBehavior.Stop,
				easingFunction: new QuadraticEase{EasingMode = EasingMode.EaseIn},
				onComplete: (_, _) => { menu.Visibility = Visibility.Hidden; }
			);
		}

		private void MainMenuExpander_OnExpanded(object sender, RoutedEventArgs e)
		{
			var menu = (Expander)sender;

			Animator.SlideAnimation
			(
				target: menu,
				targetOffsetLeft: 0,
				targetOffsetRight: 0,
				beginTime: TimeSpan.FromMilliseconds(150),
				duration: TimeSpan.FromMilliseconds(300),
				fillBehavior: FillBehavior.HoldEnd,
				easingFunction: new QuadraticEase { EasingMode = EasingMode.EaseOut },
				onStart: () =>
				{
					switch (menu.ExpandDirection)
					{
						case ExpandDirection.Left:
							menu.Margin = new Thickness(MenuWrapper.ActualWidth, 0, -MenuWrapper.ActualWidth, 0);
							break;
						case ExpandDirection.Right:
							menu.Margin = new Thickness(-MenuWrapper.ActualWidth, 0, MenuWrapper.ActualWidth, 0);
							break;
					}

					menu.Visibility = Visibility.Visible;
				}
			);
		}
	}
}