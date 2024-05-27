using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ABI.Windows.System;
using Newtonsoft.Json;

namespace YetAnotherMessenger.Misc
{
	internal static class AuthorizationService
	{
		private static string? _token;

		public static Task<string?> GetToken() => Task.FromResult(_token);

		public static async Task<bool> LogInAsync(Credentials credentials)
		{
			HttpClientHandler clientHandler = new()
			{
				// убираем проверку сертификатов.
				ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
			};
			var httpClient = new HttpClient(clientHandler);

			StringContent content = new(JsonConvert.SerializeObject(credentials));
			content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			using var response = await httpClient.PostAsync("https://127.0.0.1:7173/login", content);

			if (response.IsSuccessStatusCode)
			{
				var authResponse = JsonConvert.DeserializeObject<AuthResponse>(await response.Content.ReadAsStringAsync());
				_token = authResponse?.Token;
			}

			return response.IsSuccessStatusCode;
		}

		public static async Task<bool> RegisterAsync(RegistrationCredentials credentials)	
		{
			HttpClientHandler clientHandler = new()
			{
				// убираем проверку сертификатов.
				ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
			};
			var httpClient = new HttpClient(clientHandler);

			StringContent content = new(JsonConvert.SerializeObject(credentials));
			content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			using var response = await httpClient.PostAsync("https://127.0.0.1:7173/register", content);

			var success = false;

			if (response.IsSuccessStatusCode)
			{
				success = await LogInAsync(new Credentials(credentials.Username, credentials.Password));
			}

			return success;
		}

		private record AuthResponse(string Token, string Username);
	}
}
