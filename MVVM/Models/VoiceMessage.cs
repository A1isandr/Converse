using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherMessenger.MVVM.Models
{
	internal class VoiceMessage(byte[] audio) : Message
	{
		public byte[] Audio { get; set; } = audio;
	}
}
