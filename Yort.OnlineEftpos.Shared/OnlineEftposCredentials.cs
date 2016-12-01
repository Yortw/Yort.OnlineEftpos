using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Represents account credentials used to obtain tokens from the Online EFTPOS API.
	/// </summary>
	public sealed class OnlineEftposCredentials : IDisposable
	{

		#region Fields

		private ErasableString _ConsumerKey;
		private ErasableString _ConsumerSecret;

		#endregion

		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="consumerKey">The consumer key value issued to your application when it was registered with the Online Eftpos API.</param>
		/// <param name="consumerSecret">The consumer secret value issued to your application when it was registered with the Online Eftpos API.</param>
		public OnlineEftposCredentials(string consumerKey, string consumerSecret)
		{
			consumerKey.GuardNullEmptyOrWhitespace(nameof(consumerKey));
			consumerSecret.GuardNullEmptyOrWhitespace(nameof(consumerSecret));

			try
			{
				_ConsumerKey = new ErasableString(consumerKey);
				_ConsumerSecret = new ErasableString(consumerSecret);
			}
			catch
			{
				EraseCredentials();
				throw;
			}
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns the consumer key value sent to the API to obtain tokens.
		/// </summary>
		public string ConsumerKey
		{
			get
			{
				return _ConsumerKey?.Value;
			}
		}

		/// <summary>
		/// Returns the consumer secret value sent to the API to obtain tokens.
		/// </summary>
		public string ConsumerSecret
		{
			get
			{
				return _ConsumerSecret?.Value;
			}
		}

		#endregion

		#region IDisposable Members

		/// <summary>
		/// Disposes this class and all internal resources.
		/// </summary>
		/// <remarks>
		/// <para>Disposing the credentials class ovewrites the contents of the consumer key and secret values held by disposed instance and unpins them in memory.</para>
		/// </remarks>
		public void Dispose()
		{
			try
			{
				EraseCredentials();
			}
			finally
			{
				GC.SuppressFinalize(this);
			}
		}

		#endregion

		#region Private Members

		private void EraseCredentials()
		{
			try
			{
				if (_ConsumerKey != null)
				{
					_ConsumerKey.Dispose();
					_ConsumerKey = null;
				}
			}
			finally
			{
				if (_ConsumerSecret != null)
				{
					_ConsumerSecret.Dispose();
					_ConsumerSecret = null;
				}
			}
		}

		#endregion

	}
}