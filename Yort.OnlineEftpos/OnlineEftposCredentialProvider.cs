using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Default credentials provider.
	/// </summary>
	/// <remarks>
	/// <para>It is recommended you use the SecureStringOnlineEftposCredentialProvider if it is available for your platform. Alternatively, implement your own using the <see cref="IOnlineEftposCredentialProvider"/> interface, so you can control the life time and encryption status of credentials in memory.</para>
	/// <para>The class holds an instance of <see cref="OnlineEftposCredentials"/> in memory (containing unencryped values), passed via the constructor, and returns them through the <see cref="GetCredentials"/> method.</para>
	/// </remarks>
	public sealed class OnlineEftposCredentialsProvider : IOnlineEftposCredentialProvider
	{

		/// <summary>
		/// Returns an <see cref="OnlineEftposCredentials"/> instance containing the credentials required to obtain tokens from the Online Eftpos API.
		/// </summary>
		/// <returns>A <see cref="OnlineEftposCredentials"/> instance containing the required credentials.</returns>
		public OnlineEftposCredentials GetCredentials()
		{
			ExceptionHelper.ThrowYoureDoingItWrong();
			return null; //Avoid compiler warning.
		}

		/// <summary>
		/// Disposes this instance and all internal resources.
		/// </summary>
		/// <remarks>
		/// <para>This method will dispose the <see cref="OnlineEftposCredentials"/> instance passed in the constructor.</para>
		/// </remarks>
		public void Dispose()
		{
			ExceptionHelper.ThrowYoureDoingItWrong();
		}
	}
}