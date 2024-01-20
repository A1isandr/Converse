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
using ReactiveUI;
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

			MenuSlideAnimation
			(
				target: menu,
				targetOffsetLeft: targetOffsetLeft,
				targetOffsetRight: targetOffsetRight,
				beginTime: TimeSpan.FromMilliseconds(0),
				duration: TimeSpan.FromMilliseconds(300),
				fillBehavior: FillBehavior.Stop,
				easingMode: EasingMode.EaseIn,
				onComplete: (_, _) => { menu.Visibility = Visibility.Hidden; }
			);
		}

		private void MainMenuExpander_OnExpanded(object sender, RoutedEventArgs e)
		{
			var menu = (Expander)sender;

			MenuSlideAnimation
			(
				target: menu,
				targetOffsetLeft: 0,
				targetOffsetRight: 0,
				beginTime: TimeSpan.FromMilliseconds(150),
				duration: TimeSpan.FromMilliseconds(300),
				fillBehavior: FillBehavior.HoldEnd,
				easingMode: EasingMode.EaseOut,
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

		private void MenuSlideAnimation(
			DependencyObject target, 
			double targetOffsetLeft,
			double targetOffsetRight,
			TimeSpan beginTime,
			TimeSpan duration,
			FillBehavior fillBehavior,
			EasingMode easingMode,
			Action? onStart = null,
			EventHandler? onComplete = null)
		{
			var slideAnimation = new ThicknessAnimation
			{
				To = new Thickness(targetOffsetLeft, 0, targetOffsetRight, 0),
				BeginTime = beginTime,
				Duration = duration,
				FillBehavior = fillBehavior,
				EasingFunction = new QuadraticEase()
				{
					EasingMode = easingMode
				}
			};

			var storyboard = new Storyboard();

			storyboard.Children.Add(slideAnimation);

			Storyboard.SetTarget(slideAnimation, target);
			Storyboard.SetTargetProperty(slideAnimation, new PropertyPath(MarginProperty));

			if (onComplete is not null)
			{
				storyboard.Completed += onComplete;
			}
			
			onStart?.Invoke();

			storyboard.Begin();
		}
	}
}