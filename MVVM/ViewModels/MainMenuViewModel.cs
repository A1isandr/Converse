using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using ReactiveUI;

namespace YetAnotherMessenger.MVVM.ViewModels
{
	public class MainMenuViewModel : ReactiveObject
	{
		private static MainMenuViewModel? _instance;

		public static MainMenuViewModel Instance
		{
			get
			{
				_instance ??= new MainMenuViewModel();
				return _instance;
			}
		}


	}
}
