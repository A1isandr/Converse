using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using YetAnotherMessenger.MVVM.Models;

namespace YetAnotherMessenger.MVVM.ViewModels
{
	public class AccountPreviewViewModel : ReactiveObject
	{
		public User User { get; set; } = AppConfig.CurrentUser;
	}
}
