using System;
using System.Collections.Generic;
using System.Linq;
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
using YetAnotherMessenger.Misc;
using YetAnotherMessenger.MVVM.ViewModels;

namespace YetAnotherMessenger.MVVM.Views
{
	/// <summary>
	/// Логика взаимодействия для SearchBoxView.xaml
	/// </summary>
	public partial class SearchBoxView : ReactiveUserControl<SearchBoxViewModel>
	{
		private readonly double _clearSearchTermButtonWidth = 25.0;

		public SearchBoxView()
		{
			InitializeComponent();
		}

		private void MessageBox_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			var textBox = (TextBox)sender;

			if (textBox.Text == string.Empty)
			{
				Animator.WidthAnimation
				(
					target: ClearSearchTermButton,
					startingWidth: ClearSearchTermButton.ActualWidth,
					targetWidth: 0,
					beginTime: TimeSpan.FromMilliseconds(0),
					duration: TimeSpan.FromMilliseconds(300),
					fillBehavior: FillBehavior.HoldEnd,
					easingFunction: new QuadraticEase { EasingMode = EasingMode.EaseInOut }
				);
			}
			else if (textBox.Text.Length == e.Changes.First().AddedLength)
			{
				Animator.WidthAnimation
				(
					target: ClearSearchTermButton,
					startingWidth: ClearSearchTermButton.ActualWidth,
					targetWidth: _clearSearchTermButtonWidth,
					beginTime: TimeSpan.FromMilliseconds(0),
					duration: TimeSpan.FromMilliseconds(300),
					easingFunction: new QuadraticEase { EasingMode = EasingMode.EaseInOut }
				);
			}
		}
	}
}
