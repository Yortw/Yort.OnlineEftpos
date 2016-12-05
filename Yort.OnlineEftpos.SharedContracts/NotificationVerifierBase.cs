using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Base class for <see cref="INotificationVerifier"/> providing reusable logic. Prefer <see cref="INotificationVerifier"/> for references.
	/// </summary>
	public class NotificationVerifierBase : INotificationVerifier
	{

		#region Fields

		private readonly byte[] _PublicKey;

		#endregion

		#region Constructors

		/// <summary>
		/// Constructs a new instance using the specified base 64 encoded public key.
		/// </summary>
		/// <param name="base64PublicKey">A string containing the base 64 encoded public key to use for signature verification.</param>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="base64PublicKey"/> is null.</exception>
		/// <exception cref="System.ArgumentException">Thrown if <paramref name="base64PublicKey"/> is not a valid base 64 string.</exception>
		public NotificationVerifierBase(string base64PublicKey) : this(System.Convert.FromBase64String(base64PublicKey))
		{
			//TODO: Handle commong header a footer when parsing key.
		}

		/// <summary>
		/// Constructs a new instance using the specified raw bytes for the public key.
		/// </summary>
		/// <param name="publicKey">A byte array containing the binary representation of the public key to use when verifying notification signatures.</param>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="publicKey"/> is null.</exception>
		/// <exception cref="System.ArgumentException">Thrown if <paramref name="publicKey"/> is is zero length.</exception>
		public NotificationVerifierBase(byte[] publicKey)
		{
			publicKey.GuardNull(nameof(publicKey));
			if (publicKey.Length == 0) throw new ArgumentException("Length of publicKey cannot be 0.", nameof(publicKey));

			_PublicKey = publicKey;
		}

		/// <summary>
		/// Verifies <paramref name="notification"/> and throws as <see cref="System.Security.SecurityException"/> if the verification fails.
		/// </summary>
		/// <param name="notification">The <see cref="OnlineEftposNotification"/> to be verified.</param>
		/// <exception cref="System.InvalidOperationException">Thrown if verification fails.</exception>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="notification"/> is null.</exception>
		public void ThrowIfNotVerified(OnlineEftposNotification notification)
		{
			if (!Verify(notification)) throw new InvalidOperationException("Notification did not pass verification.");
		}

		/// <summary>
		/// Returns true if the specified <see cref="OnlineEftposNotification"/> is verified valid and authentic, otherwise false.
		/// </summary>
		/// <param name="notification">The <see cref="OnlineEftposNotification"/> to verify.</param>
		/// <returns>A boolean indicating if <paramref name="notification"/> passes verification.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="notification"/> is null.</exception>
		public bool Verify(OnlineEftposNotification notification)
		{
			notification.GuardNull(nameof(notification));

			//TODO: This.
#if DEBUG
			return true;
#else
			throw new NotImplementedException();
#endif
		}

		/// <summary>
		/// Returns a byte array containing the raw binary representation of the public key this instance was initialised with.
		/// </summary>
		protected byte[] GetPublicKey()
		{
			return _PublicKey;
		}

		#endregion

	}
}