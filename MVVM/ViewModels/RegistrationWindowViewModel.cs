using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace YetAnotherMessenger.MVVM.ViewModels
{
    public class RegistrationWindowViewModel : ReactiveObject
    {
	    private static RegistrationWindowViewModel? _instance;

	    public static RegistrationWindowViewModel Instance
	    {
		    get
		    {
			    _instance ??= new RegistrationWindowViewModel();
			    return _instance;
		    }
	    }
	}
}
