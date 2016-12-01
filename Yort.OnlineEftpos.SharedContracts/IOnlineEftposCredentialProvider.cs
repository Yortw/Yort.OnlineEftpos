using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Interface for a component that can provide credentials submitted to the Online Eftpos API in order to obtain a token.
	/// </summary>
	/// <remarks>
	/// <para>Application code can implement this interface to load credentials from appropriate storage and manage their lifetime in memory.</para>
	/// </remarks>
	public interface IOnlineEftposCredentialProvider : IDisposable
	{
		/// <summary>
		/// Returns an <see cref="OnlineEftposCredentials"/> instance containing the credentials required to acquire a token.
		/// </summary>
		/// <returns>An instance of <see cref="OnlineEftposCredentials"/>.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification="This method may perform work, or return a different value between invocations with no other state changes. It is not appropriate as a property.")]
		OnlineEftposCredentials GetCredentials();
	}
}