using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace YetAnotherMessenger.MVVM.Models
{
	public class TextMessage(string content, List<object>? attachments = null) : Message(content: content)
	{
		public List<object>? Attachments { get; set; } = attachments;
	}
}
