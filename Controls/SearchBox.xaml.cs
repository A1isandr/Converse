using ReactiveUI;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using YetAnotherMessenger.MVVM.ViewModels;
using YetAnotherMessenger.MVVM.Views;

namespace YetAnotherMessenger.Controls
{
	/// <summary>
	/// Логика взаимодействия для SearchBox.xaml
	/// </summary>
	public partial class SearchBox : UserControl, IViewFor<SearchViewModel>
	{
		public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
			nameof(ViewModel),
			typeof(SearchViewModel),
			typeof(SearchBox),
			new PropertyMetadata(null)
		);

		public SearchViewModel? ViewModel
		{
			get => (SearchViewModel)GetValue(ViewModelProperty);
			set => SetValue(ViewModelProperty, value);
		}

		object? IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = (SearchViewModel)value!;
		}

		public SearchBox()
		{
			InitializeComponent();

			ViewModel = new SearchViewModel();
		}
	}
}
