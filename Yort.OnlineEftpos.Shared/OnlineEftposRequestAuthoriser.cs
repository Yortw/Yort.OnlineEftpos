using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Yort.OnlineEftpos
{
	internal sealed class OnlineEftposRequestAuthoriser : IDisposable
	{

		#region Fields

		private IOnlineEftposCredentialProvider _CredentialProvider;
		private OnlineEftposApiRouter _ApiRouter;
		private HttpClient _HttpClient;

		private ErasableString _Token;
		private DateTime? _TokenExpiry;

		#endregion

		#region Constructors

		public OnlineEftposRequestAuthoriser(IOnlineEftposCredentialProvider credentialProvider, HttpClient client, OnlineEftposApiRouter apiRouter)
		{
			credentialProvider.GuardNull(nameof(credentialProvider));
			client.GuardNull(nameof(client));
			apiRouter.GuardNull(nameof(apiRouter));

			_CredentialProvider = credentialProvider;
			_ApiRouter = apiRouter;
			_HttpClient = client;
		}

		#endregion

		#region Methods

		public async Task AuthoriseRequest(HttpRequestMessage request)
		{
			request.GuardNull(nameof(request));

			var token = await AcquireToken().ConfigureAwait(false);
			request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Value);
		}

		#endregion

		#region Private Members

		private async Task<ErasableString> AcquireToken()
		{
			if (HaveValidToken())
				return _Token;
			else
			{
				_Token?.Dispose();

				return await RequestToken().ConfigureAwait(false);
			}
		}

		private async Task<ErasableString> RequestToken()
		{
			var keyValues = new List<KeyValuePair<string, string>>();
			keyValues.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
			var content = new System.Net.Http.FormUrlEncodedContent(keyValues);
		
			var tokenRequestMessage = new HttpRequestMessage(HttpMethod.Post, _ApiRouter.GetUrl("bearer"))
			{
				Content = content
			};

			HttpResponseMessage tokenResponse = null;
			using (var creds = _CredentialProvider.GetCredentials())
			{
				using (var combinedCredentials = new ErasableString(creds.ConsumerKey + ":" + creds.ConsumerSecret))
				{
					using (var encodedCredentials = SecureToBase64String(combinedCredentials.Value))
					{
						tokenRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", encodedCredentials.Value);
						tokenResponse = await _HttpClient.SendAsync(tokenRequestMessage).ConfigureAwait(false);
					}
				}
			}

			return await ProcessTokenResponse(tokenResponse).ConfigureAwait(false);
		}

		private static ErasableString SecureToBase64String(string value)
		{
			ErasableString retVal = null;

			var bytes = System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(value);
			GCHandle handle = GCHandle.Alloc(bytes);
			try
			{ 
				retVal = new ErasableString(System.Convert.ToBase64String(bytes));

				for (int cnt = 0; cnt < bytes.Length; cnt++)
				{
					bytes[cnt] = (byte)0;
				}
			}
			finally
			{
				if (handle.IsAllocated)
					handle.Free();
			}

			return retVal;
		}

		private async Task<ErasableString> ProcessTokenResponse(HttpResponseMessage tokenResponse)
		{
			if (tokenResponse.Content == null)
				tokenResponse.EnsureSuccessStatusCode();

			var responseContent = await tokenResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

			if (tokenResponse.IsSuccessStatusCode)
			{
				var tokenData = JsonConvert.DeserializeObject<OAuth2TokenResponse>(responseContent);
				if (tokenData.ExpiresIn > 0)
					_TokenExpiry = DateTime.Now.AddSeconds(tokenData.ExpiresIn);
				else
					_TokenExpiry = null;

				_Token = new ErasableString(tokenData.AccessToken);
				return _Token;
			}
			else
			{
				var tokenError = JsonConvert.DeserializeObject<OAuth2TokenError>(responseContent);
				throw new OnlineEftposAuthenticationException(tokenError.Error, tokenError.ErrorDescription, tokenError.ErrorUri, tokenResponse.StatusCode, tokenResponse.ReasonPhrase);
			}
		}

		private bool HaveValidToken()
		{
			return _Token != null
				&& !_Token.IsCleared
				&& !_Token.IsDisposed
				&& (_TokenExpiry == null || _TokenExpiry > DateTime.Now.AddMinutes(2));
		}

		#endregion

		#region IDiposable Members

		public void Dispose()
		{
			if (_Token != null)
			{
				_Token.Dispose();
				_Token = null;
			}
		}

		#endregion

		#region Private Classes

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification="Instantiated via Json deserialisation (reflection).")]
#if __IOS__
		[Foundation.Preserve]
#endif
		private class OAuth2TokenResponse
		{

#if __IOS__
			[Foundation.Preserve]
#endif
			public OAuth2TokenResponse()
			{
			}

			[JsonProperty("access_token")]
			public string AccessToken { get; set; }
			[JsonProperty("token_type")]
			public string TokenType { get; set; }
			[JsonProperty("expires_in")]
			public int ExpiresIn { get; set; }
			[JsonProperty("refresh_token")]
			public string RefreshToken { get; set; }
		}

#if __IOS__
		[Foundation.Preserve]
#endif
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Instantiated via Json deserialisation (reflection).")]
		private class OAuth2TokenError
		{

#if __IOS__
			[Foundation.Preserve]
#endif
			public OAuth2TokenError()
			{
			}

			[JsonProperty("error")]
			public string Error { get; set; }

			[JsonProperty("error_description")]
			public string ErrorDescription { get; set; }

			[JsonProperty("error_uri")]
			public string ErrorUri { get; set; }
		}

#endregion

	}
}