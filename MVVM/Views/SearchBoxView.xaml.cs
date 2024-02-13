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

		private readonly Storyboard _clearSearchTermButtonSlideIn;

		public SearchBoxView()
		{
			InitializeComponent();

			_clearSearchTermButtonSlideIn = new StoryboardBuilder()
				.AddDoubleAnimation
				(
					targetObject: ClearSearchTermButton,
					targetProperty: WidthProperty,
					startingValue: ClearSearchTermButton.ActualWidth,
					targetValue: _clearSearchTermButtonWidth,
					beginTime: TimeSpan.Zero,
					duration: TimeSpan.FromMilliseconds(300),
					easingFunction: new QuadraticEase { EasingMode = EasingMode.EaseInOut }
				)
				.End();

			ViewModel = SearchBoxViewModel.Instance;

			this.WhenActivated(disposables =>
			{
				this.Bind(ViewModel,
						viewModel => viewModel.SearchTerm,
						view => view.SearchBox.Text)
					.DisposeWith(disposables);

				this.BindCommand(ViewModel,
						viewModel => viewModel.ClearSearchTextCommand,
						view => view.ClearSearchTermButton)
					.DisposeWith(disposables);
			});
		}

		private void SearchBox_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			var textBox = (TextBox)sender;

			if (textBox.Text == string.Empty)
			{
				new StoryboardBuilder()
					.AddDoubleAnimation
					(
						targetObject: ClearSearchTermButton,
						targetProperty: WidthProperty,
						startingValue: ClearSearchTermButton.ActualWidth,
						targetValue: 0,
						beginTime: TimeSpan.Zero,
						duration: TimeSpan.FromMilliseconds(300),
						easingFunction: new QuadraticEase { EasingMode = EasingMode.EaseInOut }
					)
					.End()
					.Begin();
			}
			else if (textBox.Text.Length == e.Changes.First().AddedLength)
			{
				_clearSearchTermButtonSlideIn.Begin();
			}
		}
	}
}
