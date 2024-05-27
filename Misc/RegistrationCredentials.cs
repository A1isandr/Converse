using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherMessenger.Misc
{
	public record RegistrationCredentials(string Username, string Password, string FirstName, string? LastName);
}
