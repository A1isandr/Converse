using Splat;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Net.Http;
using System.Reactive;
using System.Reflection;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;
using ReactiveUI;
using YetAnotherMessenger.MVVM.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Net.Http.Headers;
using YetAnotherMessenger.Misc;

namespace YetAnotherMessenger
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private static List<CultureInfo> _languages = [];

		public static List<CultureInfo> Languages => _languages;

		//Ивент для оповещения всех окон приложения
		public static event EventHandler? LanguageChanged;

		public static CultureInfo Language
		{
			get => System.Threading.Thread.CurrentThread.CurrentUICulture;
			set
			{
				ArgumentNullException.ThrowIfNull(value);
				if (value.Equals(Thread.CurrentThread.CurrentUICulture)) return;

				//1. Меняем язык приложения:
				Thread.CurrentThread.CurrentUICulture = value;

				//2. Создаём ResourceDictionary для новой культуры
				ResourceDictionary dict = new()
				{
					Source = value.Name switch
					{
						"ru-RU" => new Uri($"Resources/lang.{value.Name}.xaml", UriKind.Relative),
						_ => new Uri("Resources/lang.en-EN.xaml", UriKind.Relative)
					}
				};

				//3. Находим старый ResourceDictionary и удаляем его и добавляем новый ResourceDictionary
				ResourceDictionary oldDict = (from d in Current.Resources.MergedDictionaries
											  where d.Source != null && d.Source.OriginalString.StartsWith("Resources/lang.")
											  select d).First();
				if (oldDict != null)
				{
					int ind = Current.Resources.MergedDictionaries.IndexOf(oldDict);
					Current.Resources.MergedDictionaries.Remove(oldDict);
					Current.Resources.MergedDictionaries.Insert(ind, dict);
				}
				else
				{
					Current.Resources.MergedDictionaries.Add(dict);
				}

				//4. Вызываем евент для оповещения всех окон.
				LanguageChanged?.Invoke(Application.Current, EventArgs.Empty);
			}
		}

		private static HubConnection? _connection;
		public static HubConnection? Connection => _connection;

		public App()
		{
			// Создание экземпляра HttpClient с вашим токеном
			HttpClient httpClient = new();
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "ваш_токен");

			_connection = new HubConnectionBuilder()
				.WithUrl("https://localhost:7098/chat", options =>
				{
					options.AccessTokenProvider = AuthorizationService.GetToken;
				})
				.WithAutomaticReconnect()
				.Build();

			_connection.On<string, string>("Receive", (user, message) =>
			{
				
			});

			Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());

			_languages.Clear();
			_languages.Add(new CultureInfo("en-US"));
			_languages.Add(new CultureInfo("ru-RU"));
		}
	}

}