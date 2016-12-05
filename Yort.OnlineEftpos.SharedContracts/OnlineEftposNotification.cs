using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Represents a notification about a payment status change from the Online EFTPOS system.
	/// </summary>
	public sealed class OnlineEftposNotification
	{

		#region Constants

		private static readonly char[] KeyValuePairSeparators = new char[] { '&' };
		private static readonly char[] KeyValueSeparators = new char[] { '=' };

		#endregion

		#region Fields

		private string _MerchantOrderId;
		private string _Status;
		private string _Signature;
		private string _SignedData;

		#endregion

		#region Constructors

		/// <summary>
		/// Full constructor.
		/// </summary>
		/// <param name="formUrlEncodedContent">The content of the notification which should be form url encoded data.</param>
		/// <exception cref="ArgumentException">Thrown if 
		/// <list type="Bullet">
		/// <item><paramref name="formUrlEncodedContent"/> is an empty string or only whitespace.</item>
		/// <item><paramref name="formUrlEncodedContent"/> is not a form url encoded string.</item>
		/// <item><paramref name="formUrlEncodedContent"/> does not contain a key for signature, status or merchantOrderId.</item>
		/// </list> 
		/// </exception>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="formUrlEncodedContent"/> is null.</exception>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#", Justification ="Not actually a uri.")]
		public OnlineEftposNotification(string formUrlEncodedContent)
		{
			formUrlEncodedContent.GuardNullEmptyOrWhitespace(nameof(formUrlEncodedContent));

			ParseContent(formUrlEncodedContent);
			Validate();

			// Not sure of the best way to do this. Technically this is bad because it relies on the order of parameters
			// in the string (i.e assumes signature is last). However that is what we get and what the document describes.
			// More to the point, the order of the non-signature data *is* important when verifying the signature. 
			// If we attempt to reconstruct the string after parsing, we risk corrupting it, plus we have no idea if parameters
			// following the signature should be included in the signed data or not. As such, this seems best for now.
			_SignedData = formUrlEncodedContent.Substring(0, formUrlEncodedContent.IndexOf("&signature=", StringComparison.OrdinalIgnoreCase));
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Returns the merchant id of the notification.
		/// </summary>
		public string MerchantOrderId
		{
			get
			{
				return _MerchantOrderId;
			}
		}

		/// <summary>
		/// Returns the status of the notification.
		/// </summary>
		/// <seealso cref="PaymentStatus"/>
		/// <seealso cref="RefundStatus"/>
		public string Status
		{
			get
			{
				return _Status;
			}
		}

		/// <summary>
		/// Returns the signature of the notification.
		/// </summary>
		/// <remarks>
		/// <para>At time of writing the signature is an SHA512withRSA signature.</para>
		/// </remarks>
		public string Signature
		{
			get
			{
				return _Signature;
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Verifies the signature using the specified public key and returns true if the verification succeeds.
		/// </summary>
		/// <param name="publicKey">The public key to use during verification.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="publicKey"/> is null.</exception>
		/// <exception cref="System.ArgumentException">Thrown if <paramref name="publicKey"/> has a zero length.</exception>
		public bool VerifySignature(byte[] publicKey)
		{
			if (publicKey == null) throw new ArgumentNullException(nameof(publicKey));
			if (publicKey.Length == 0) throw new ArgumentException("Length of publicKey cannot be zero.", nameof(publicKey));

			//TODO: Actually verify the signature.
			
#if DEBUG
			return !String.IsNullOrWhiteSpace(_SignedData);
#else
			throw new NotImplementedException("Signature verification not yet implemented.");
#endif
		}

		/// <summary>
		/// Checks the signature using the specified public key, and throws an <see cref="InvalidOperationException"/> if it is not valid.
		/// </summary>
		/// <param name="publicKey">The public key to use during verification.</param>
		/// <exception cref="System.InvalidOperationException">Thrown if the signature is not verfiyable using the publichkey.</exception>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="publicKey"/> is null.</exception>
		/// <exception cref="System.ArgumentException">Thrown if <paramref name="publicKey"/> has a zero length.</exception>
		public void ThrowIfSignatureInvalid(byte[] publicKey)
		{
			if (!VerifySignature(publicKey))
				throw new InvalidOperationException("The notification signature did not verify.");
		}

		#endregion

		#region Private Methods

		private void ParseContent(string formUrlEncodedContent)
		{
			var keyValuePairs = formUrlEncodedContent.Split(KeyValuePairSeparators, StringSplitOptions.RemoveEmptyEntries);
			foreach (var kvp in keyValuePairs)
			{
				var parts = kvp.Split(KeyValueSeparators);
				var key = Uri.UnescapeDataString(parts[0]);
				string value = null;
				if (parts.Length > 1 && !String.IsNullOrEmpty(parts[1]))
					value = Uri.UnescapeDataString(parts[1]);

				LoadProperty(key, value);
			}
		}

		private void LoadProperty(string key, string value)
		{
			switch (key.ToUpperInvariant())
			{
				case "MERCHANTORDERID":
					_MerchantOrderId = value;
					break;
				case "STATUS":
					_Status = value;
					break;
				case "SIGNATURE":
					_Signature = value;
					break;
			}
		}

		private void Validate()
		{
			if (String.IsNullOrEmpty(MerchantOrderId)) throw new ArgumentException("MerchantOrderId is missing from notification data.");
			if (String.IsNullOrEmpty(Status)) throw new ArgumentException("Status is missing from notification data.");
			if (String.IsNullOrEmpty(Signature)) throw new ArgumentException("Signature is missing from notification data.");
		}

		#endregion

	}
}