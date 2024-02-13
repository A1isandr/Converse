using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace YetAnotherMessenger.MVVM.ViewModels
{
	public class LogInWindowViewModel : ReactiveObject
	{
		private static LogInWindowViewModel? _instance;
		/// <summary>
		/// Returns the singleton instance.
		/// </summary>
		public static LogInWindowViewModel Instance
		{
			get
			{
				_instance ??= new LogInWindowViewModel();
				return _instance;
			}
		}
	}
}
