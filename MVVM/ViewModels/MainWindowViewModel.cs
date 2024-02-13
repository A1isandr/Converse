using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ReactiveUI.Fody.Helpers;
using YetAnotherMessenger.MVVM.Views;

namespace YetAnotherMessenger.MVVM.ViewModels
{
	public class MainWindowViewModel : ReactiveObject
	{
		public MainViewModel MainVM { get; set; } = new();

		public AuthenticationWindowViewModel AuthenticationWindowVM { get; set; } = AuthenticationWindowViewModel.Instance;

		private readonly ObservableAsPropertyHelper<bool> _isBlurRequired;
		public bool IsBlurRequired => _isBlurRequired.Value;

		private readonly ObservableAsPropertyHelper<bool> _isBlackOutRequired;
		public bool IsBlackOutRequired => _isBlurRequired.Value;

		public MainWindowViewModel()
		{
			_isBlurRequired = this
				.WhenAnyValue(x => x.AuthenticationWindowVM.IsOpen)
				.Select(state => state)
				.ToProperty(this, x => x.IsBlurRequired);

			_isBlackOutRequired = this
				.WhenAnyValue(x => x.AuthenticationWindowVM.IsOpen)
				.Select(state => state)
				.ToProperty(this, x => x.IsBlackOutRequired);
		}
	}
}
