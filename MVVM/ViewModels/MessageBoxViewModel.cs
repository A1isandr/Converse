using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Sms;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Diagnostics;

namespace YetAnotherMessenger.MVVM.ViewModels
{
	public class MessageBoxViewModel : ReactiveObject
	{
		private static MessageBoxViewModel? _instance;

		public static MessageBoxViewModel Instance
		{
			get
			{
				_instance ??= new MessageBoxViewModel();
				return _instance;
			}
		}

		public ReactiveCommand<Key, Unit> CRLFCommand { get; }

		public ReactiveCommand<Key, Unit> SendMessageCommand { get; }

		[Reactive] 
		public string? MessageDraft { get; set; }

		public MessageBoxViewModel()
		{
			var canExecute = this.WhenAnyValue(x => x.MessageDraft,
				messageDraft => !string.IsNullOrEmpty(messageDraft));

			SendMessageCommand = ReactiveCommand.CreateFromObservable<Key, Unit>((_) => Observable.Return(Unit.Default), canExecute);
		}
	}
}
