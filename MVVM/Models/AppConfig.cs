using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using ApplicationContext = YetAnotherMessenger.Misc.ApplicationContext;

namespace YetAnotherMessenger.MVVM.Models
{
    internal static class AppConfig
    {
	    public static User CurrentUser { get; set; }

	    static AppConfig()
	    {
			using ApplicationContext db = new();

			CurrentUser = db.Users.Include(user => user.Profile).First(user => user.Username == "AlPi");
	    }

	}
}
