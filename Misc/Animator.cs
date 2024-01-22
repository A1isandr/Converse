using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Controls;

namespace YetAnotherMessenger.Misc
{
	static class Animator
	{
		public static void SlideAnimation(
			DependencyObject target,
			double targetOffsetLeft,
			double targetOffsetRight,
			TimeSpan beginTime,
			TimeSpan duration,
			FillBehavior fillBehavior = FillBehavior.HoldEnd,
			EasingFunctionBase? easingFunction = null,
			Action? onStart = null,
			EventHandler? onComplete = null)
		{
			var slideAnimation = new ThicknessAnimation
			{
				To = new Thickness(targetOffsetLeft, 0, targetOffsetRight, 0),
				BeginTime = beginTime,
				Duration = duration,
				FillBehavior = fillBehavior,
				EasingFunction = easingFunction
			};

			var storyboard = new Storyboard();

			storyboard.Children.Add(slideAnimation);

			Storyboard.SetTarget(slideAnimation, target);
			Storyboard.SetTargetProperty(slideAnimation, new PropertyPath(FrameworkElement.MarginProperty));

			if (onComplete is not null)
			{
				storyboard.Completed += onComplete;
			}

			onStart?.Invoke();

			storyboard.Begin();
		}

		public static void WidthAnimation(
			DependencyObject target,
			double startingWidth,
			double targetWidth,
			TimeSpan beginTime,
			TimeSpan duration,
			FillBehavior fillBehavior = FillBehavior.HoldEnd,
			EasingFunctionBase? easingFunction = null,
			Action? onStart = null,
			EventHandler? onComplete = null)
		{
			var widthAnimation = new DoubleAnimation()
			{
				From = startingWidth,
				To = targetWidth,
				BeginTime = beginTime,
				Duration = duration,
				FillBehavior = fillBehavior,
				EasingFunction = easingFunction
			};

			var storyboard = new Storyboard();

			storyboard.Children.Add(widthAnimation);

			Storyboard.SetTarget(widthAnimation, target);
			Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(FrameworkElement.WidthProperty));

			if (onComplete is not null)
			{
				storyboard.Completed += onComplete;
			}

			onStart?.Invoke();

			storyboard.Begin();
		}
	}
}
