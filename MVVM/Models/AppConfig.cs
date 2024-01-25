using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace YetAnotherMessenger.MVVM.Models
{
    internal static class AppConfig
    {
	    public static User CurrentUser { get; set; } = new()
	    {
			Id = 1,
			FirstName = "Alex",
			LastName = "Pi",
			Username = "Alpi",
			Avatar = new Uri("https://i.pravatar.cc/299")
	    };
    }
}
