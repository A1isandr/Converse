using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetAnotherMessenger.MVVM.Models;

namespace YetAnotherMessenger.Misc
{
	internal class MessageContentFactory
	{
		public MessageContent MessageContentBuilder(string content)
		{
			return new MessageContent
			{
				Content = content
			};
		}
	}
}
