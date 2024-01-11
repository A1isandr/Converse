using Splat;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Windows;
using ReactiveUI;

namespace YetAnotherMessenger
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private static List<CultureInfo> m_Languages = new List<CultureInfo>();

		public static List<CultureInfo> Languages => m_Languages;

		//Евент для оповещения всех окон приложения
		public static event EventHandler? LanguageChanged;

		public static CultureInfo Language
		{
			get => System.Threading.Thread.CurrentThread.CurrentUICulture;
			set
			{
				if (value == null) throw new ArgumentNullException(nameof(value));
				if (value == Thread.CurrentThread.CurrentUICulture) return;

				//1. Меняем язык приложения:
				Thread.CurrentThread.CurrentUICulture = value;

				//2. Создаём ResourceDictionary для новой культуры
				ResourceDictionary dict = new ResourceDictionary
				{
					Source = value.Name switch
					{
						"ru-RU" => new Uri($"Resources/lang.{value.Name}.xaml", UriKind.Relative),
						_ => new Uri("Resources/lang.xaml", UriKind.Relative)
					}
				};

				//3. Находим старый ResourceDictionary и удаляем его и добавляем новый ResourceDictionary
				ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
											  where d.Source != null && d.Source.OriginalString.StartsWith("Resources/lang.")
											  select d).First();
				if (oldDict != null)
				{
					int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
					Application.Current.Resources.MergedDictionaries.Remove(oldDict);
					Application.Current.Resources.MergedDictionaries.Insert(ind, dict);
				}
				else
				{
					Application.Current.Resources.MergedDictionaries.Add(dict);
				}

				//4. Вызываем евент для оповещения всех окон.
				LanguageChanged?.Invoke(Application.Current, EventArgs.Empty);
			}
		}
		public App()
		{
			Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());

			m_Languages.Clear();
			m_Languages.Add(new CultureInfo("en-US")); //Neutral culture for this application
			m_Languages.Add(new CultureInfo("ru-RU"));
		}
	}

}
