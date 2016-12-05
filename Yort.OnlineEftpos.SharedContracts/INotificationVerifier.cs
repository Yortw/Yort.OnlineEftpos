using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Interface for components that can verify a <see cref="OnlineEftposNotification"/> as being valid and sent by the Online EFTPOS system.
	/// </summary>
	public interface INotificationVerifier
	{

		/// <summary>
		/// Returns true if the specified <see cref="OnlineEftposNotification"/> is verified valid and authentic, otherwise false.
		/// </summary>
		/// <param name="notification">The <see cref="OnlineEftposNotification"/> to verify.</param>
		/// <returns>A boolean indicating if <paramref name="notification"/> passes verification.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="notification"/> is null.</exception>
		bool Verify(OnlineEftposNotification notification);

		/// <summary>
		/// Verifies <paramref name="notification"/> and throws as <see cref="System.Security.SecurityException"/> if the verification fails.
		/// </summary>
		/// <param name="notification">The <see cref="OnlineEftposNotification"/> to be verified.</param>
		/// <exception cref="System.InvalidOperationException">Thrown if verification fails.</exception>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="notification"/> is null.</exception>
		void ThrowIfNotVerified(OnlineEftposNotification notification);

	}
}