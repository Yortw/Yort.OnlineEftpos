using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Base class for <see cref="INotificationVerifier"/> providing reusable logic. Prefer <see cref="INotificationVerifier"/> for references.
	/// </summary>
	public abstract class NotificationVerifierBase : INotificationVerifier
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
		protected NotificationVerifierBase(string base64PublicKey) : this(Base64ToBinaryKey(base64PublicKey))
		{
		}

		/// <summary>
		/// Constructs a new instance using the specified raw bytes for the public key.
		/// </summary>
		/// <param name="publicKey">A byte array containing the binary representation of the public key to use when verifying notification signatures.</param>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="publicKey"/> is null.</exception>
		/// <exception cref="System.ArgumentException">Thrown if <paramref name="publicKey"/> is is zero length.</exception>
		protected NotificationVerifierBase(byte[] publicKey)
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
		public abstract bool Verify(OnlineEftposNotification notification);

		/// <summary>
		/// Returns a byte array containing the raw binary representation of the public key this instance was initialised with.
		/// </summary>
		protected byte[] GetPublicKey()
		{
			return _PublicKey;
		}

		#endregion


		#region Private Methods

		private static byte[] Base64ToBinaryKey(string base64PublicKey)
		{
			if (base64PublicKey.StartsWith("-----BEGIN PUBLIC KEY-----"))
			{
				var sb = new StringBuilder(base64PublicKey);
				sb.Replace("-----BEGIN PUBLIC KEY-----", String.Empty);
				sb.Replace("-----END PUBLIC KEY-----", String.Empty);
				return System.Convert.FromBase64String(sb.ToString());
			}
			else
				return System.Convert.FromBase64String(base64PublicKey);
		}

		#endregion

	}
}