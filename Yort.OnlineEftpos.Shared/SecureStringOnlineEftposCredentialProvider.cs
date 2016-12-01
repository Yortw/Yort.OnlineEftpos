#if SUPPORTS_SECURESTRING

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Holds the credentials required to obtain tokens from the Online Eftpos API as <seealso cref="System.Security.SecureString"/> instances.
	/// </summary>
	/// <remarks>
	/// <para>This credential provider should be *marginally* more secure than the <seealso cref="OnlineEftposCredentialsProvider"/> on platforms where it is supported.</para>
	/// <para>Use of this class does *not* guarantee any level of security, but it should help to *reduce* the likelihood of specific risks such as unencrypted credentials being saved in swap files or being available in buffer overrun situations.</para>
	/// <para>This class is not available on all platforms. On some platforms <seealso cref="System.Security.SecureString"/> is not available. On other platforms <seealso cref="System.Security.SecureString"/> is available but does not provide the same level of security as on standard .Net and is deliberately not supported so as not to induce false expectations.</para>
	/// </remarks>
	public sealed class SecureStringOnlineEftposCredentialProvider : IOnlineEftposCredentialProvider
	{

		#region Fields

		private SecureString _ConsumerKey;
		private SecureString _ConsumerSecret;

		#endregion

		#region Constructors

		/// <summary>
		/// Full constructor.
		/// </summary>
		/// <param name="consumerKey">The consumer key value issued to the application when it was registered with the Online Eftpos API, as <seealso cref="System.Security.SecureString"/> reference.</param>
		/// <param name="consumerSecret">The consumer secret value issued to the application when it was registered with the Online Eftpos API, as <seealso cref="System.Security.SecureString"/> reference.</param>
		public SecureStringOnlineEftposCredentialProvider(SecureString consumerKey, SecureString consumerSecret)
		{
			consumerKey.GuardNull(nameof(consumerKey));
			consumerSecret.GuardNull(nameof(consumerSecret));

			_ConsumerKey = consumerKey;
			_ConsumerSecret = consumerSecret;	
		}

		#endregion

		#region IOnlineEftposCredentialProvider Members

		/// <summary>
		/// Returns an <see cref="OnlineEftposCredentials"/> instance containing the credentials required to obtain tokens from the Online Eftpos API.
		/// </summary>
		/// <returns>A <see cref="OnlineEftposCredentials"/> instance containing the required credentials.</returns>
		public OnlineEftposCredentials GetCredentials()
		{
			// Yes, two small villages just got set on fire (http://blogs.msdn.com/b/fpintos/archive/2009/06/12/how-to-properly-convert-securestring-to-string.aspx).
			// The point here is not to eliminate risk (since we can't, the unecrypted credentails are going to be passed to HttpClient and put on the network and pass
			// through only Microsoft know how many layers) but to reduce the chance of leakage.
			// The credentials object will pin the strings to *reduce* the number of copies of the raw copies in memory (GC won't make copies moving the data around), 
			// and we only create the insecure strings when we need to request a token which is infrequent. 
			// Yes, at any point there could be one or more copies left in memory, but there *might* not be any this way. Even if there are, it's the minimum 
			// number of copies we can have given the constraints of .Net etc. In addition to hoping there might be zero copies in memory (for crash dump/swap file 
			// protection etc), the reduced number of copies also *reduces* the *likelihood* of a buffer overflow attack (similar to heart bleed) managing to 
			// obtain the raw credentials. To some degree it's all smoke and mirrors (or at least if, buts and maybes). It relies on the client creating the
			// secure strings properly anyway. But I still believe it's better than not doing it all, for clients who want to *maximize* security (without ensuring
			// it, since again, that is not possible).
			// Update: *Some* of this risk is hopefully mitigated by using the ErasableString type in place of a regular string, however even that is only
			// going to reduce risk at best.
			return new OnlineEftposCredentials(ToInsecureString(_ConsumerKey), ToInsecureString(_ConsumerSecret));
		}

		#endregion

		#region Private Members

		private static string ToInsecureString(SecureString secureString)
		{
			string plainString;
			IntPtr bstr = IntPtr.Zero;

			if (secureString == null || secureString.Length == 0)
				return String.Empty;

			try
			{
				bstr = Marshal.SecureStringToBSTR(secureString);
				plainString = Marshal.PtrToStringBSTR(bstr);
			}
			finally
			{
				if (bstr != IntPtr.Zero)
					Marshal.ZeroFreeBSTR(bstr);
			}
			return plainString;
		}

		#endregion

		#region IDisposable Members

		/// <summary>
		/// Disposes this instance and all internal resources.
		/// </summary>
		/// <remarks>
		/// <para>This will clear the secure string instances provided in the constructor.</para>
		/// </remarks>
		public void Dispose()
		{
			if (_ConsumerKey != null)
				_ConsumerKey.Clear();

			if (_ConsumerSecret != null)
				_ConsumerSecret.Clear();
		}

		#endregion

	}
}

#endif