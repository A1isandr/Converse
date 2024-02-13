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
	public class StoryboardBuilder
	{
		private readonly Storyboard _storyboard = new();

		public StoryboardBuilder AddThicknessAnimation(
			DependencyObject targetObject,
			DependencyProperty targetProperty,
			Thickness startingThickness,
			Thickness targetThickness,
			TimeSpan beginTime,
			TimeSpan duration,
			FillBehavior fillBehavior = FillBehavior.HoldEnd,
			EasingFunctionBase? easingFunction = null)
		{
			var thicknessAnimation = new ThicknessAnimation
			{
				From = startingThickness,
				To = targetThickness,
				BeginTime = beginTime,
				Duration = duration,
				FillBehavior = fillBehavior,
				EasingFunction = easingFunction
			};

			_storyboard.Children.Add(thicknessAnimation);

			Storyboard.SetTarget(thicknessAnimation, targetObject);
			Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(targetProperty));

			return this;
		}

		public StoryboardBuilder AddDoubleAnimation(
			DependencyObject targetObject,
			DependencyProperty targetProperty,
			double startingValue,
			double targetValue,
			TimeSpan beginTime,
			TimeSpan duration,
			FillBehavior fillBehavior = FillBehavior.HoldEnd,
			EasingFunctionBase? easingFunction = null)
		{
			var doubleAnimation = new DoubleAnimation
			{
				From = startingValue,
				To = targetValue,
				BeginTime = beginTime,
				Duration = duration,
				FillBehavior = fillBehavior,
				EasingFunction = easingFunction
			};

			_storyboard.Children.Add(doubleAnimation);
			Storyboard.SetTarget(doubleAnimation, targetObject);
			Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(targetProperty));

			return this;
		}

		public StoryboardBuilder AddOnCompleteAction(EventHandler action)
		{
			_storyboard.Completed += action;

			return this;
		}

		public Storyboard End()
		{
			return _storyboard;
		}
	}
}
