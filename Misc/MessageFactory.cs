using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABI.Windows.ApplicationModel.Background;
using YetAnotherMessenger.MVVM.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace YetAnotherMessenger.Misc
{
	public abstract class MessageFactory
	{
		public abstract Message MessageBuilder(string content, User user);
	}

	public class TextMessageFactory : MessageFactory
	{
		public override Message MessageBuilder(string content, User user)
		{
			var messageContent = new MessageContentFactory().MessageContentBuilder(content: content);

			var message = new TextMessage
			{
				User = user,
				Content = messageContent,
			};

			messageContent.Message = message;
			user.Messages.Add(message);

			return message;
		}
	}
}
