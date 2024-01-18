﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABI.Windows.UI.Popups;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using YetAnotherMessenger.Controls;
using YetAnotherMessenger.MVVM.Models;

namespace YetAnotherMessenger.MVVM.ViewModels
{
	public class MessageViewModel : ReactiveObject
	{
		[Reactive] 
		public Message Message { get; set; }
	}
}