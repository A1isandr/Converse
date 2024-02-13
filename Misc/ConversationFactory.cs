using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetAnotherMessenger.MVVM.Models;

namespace YetAnotherMessenger.Misc
{
	internal class ConversationFactory
	{
		public Conversation ConversationBuilder(string conversationName)
		{
			using ApplicationContext db = new();

			return new Conversation
			{
				
			};
		}
	}
}
