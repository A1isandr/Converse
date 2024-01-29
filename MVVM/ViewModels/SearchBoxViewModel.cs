using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace YetAnotherMessenger.MVVM.ViewModels
{
	public class SearchBoxViewModel : ReactiveObject
	{
		private static SearchBoxViewModel? _instance;
		/// <summary>
		/// Returns the singleton instance.
		/// </summary>
		public static SearchBoxViewModel Instance
		{
			get
			{
				_instance ??= new SearchBoxViewModel();
				return _instance;
			}
		}

		public ReactiveCommand<Unit, Unit> ClearSearchTextCommand { get; }

		[Reactive]
		public string? SearchTerm { get; private set; }

		public SearchBoxViewModel()
		{
			ClearSearchTextCommand = ReactiveCommand.CreateFromObservable<Unit, Unit>((_) =>
			{
				SearchTerm = string.Empty;
				return Observable.Return(Unit.Default);
			});
		}
	}
}
