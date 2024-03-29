﻿using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YetAnotherMessenger.MVVM.ViewModels;

namespace YetAnotherMessenger.MVVM.Views
{
	/// <summary>
	/// Логика взаимодействия для ConversationView.xaml
	/// </summary>
	public partial class ConversationView : ReactiveUserControl<ConversationViewModel>
	{
		public ConversationView()
		{
			InitializeComponent();

			ViewModel = ConversationViewModel.Instance;

			this.WhenActivated(disposables =>
			{
				this.OneWayBind(ViewModel,
						viewModel => viewModel.IsConversationOpened,
						view => view.ChatWindow.Visibility)
					.DisposeWith(disposables);

				this.OneWayBind(ViewModel,
						viewModel => viewModel.Conversation!.ConversationName,
						view => view.ChatName.Text)
					.DisposeWith(disposables);

				this.OneWayBind(ViewModel,
						viewModel => viewModel.Messages,
						view => view.MessagesPresenter.ItemsSource)
					.DisposeWith(disposables);

				this.OneWayBind(ViewModel,
						viewModel => viewModel.HasMessages,
						view => view.NoMessagesLabel.Visibility)
					.DisposeWith(disposables);
			});
		}
	}
}