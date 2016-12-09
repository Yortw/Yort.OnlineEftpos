using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Represents account credentials used to obtain tokens from the Online EFTPOS API.
	/// </summary>
	public sealed class OnlineEftposCredentials : IDisposable
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="consumerKey">The consumer key value issued to your application when it was registered with the Online Eftpos API.</param>
		/// <param name="consumerSecret">The consumer secret value issued to your application when it was registered with the Online Eftpos API.</param>
		public OnlineEftposCredentials(string consumerKey, string consumerSecret)
		{
			ExceptionHelper.ThrowYoureDoingItWrong();
		}

		/// <summary>
		/// Returns the consumer key value sent to the API to obtain tokens.
		/// </summary>
		public string ConsumerKey
		{
			get
			{
				ExceptionHelper.ThrowYoureDoingItWrong();
				return null; // Work around compiler warning.
			}
		}

		/// <summary>
		/// Returns the consumer secret value sent to the API to obtain tokens.
		/// </summary>
		public string ConsumerSecret
		{
			get
			{
				ExceptionHelper.ThrowYoureDoingItWrong();
				return null; // Work around compiler warning.
			}
		}

		/// <summary>
		/// Disposes this class and all internal resources.
		/// </summary>
		/// <remarks>
		/// <para>Disposing the credentials class ovewrites the contents of the consumer key and secret values held by disposed instance and unpins them in memory.</para>
		/// </remarks>
		public void Dispose()
		{
			ExceptionHelper.ThrowYoureDoingItWrong();
		}
	}
}
