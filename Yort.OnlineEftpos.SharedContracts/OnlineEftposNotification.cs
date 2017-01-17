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
		private string _TransactionId;
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
		/// Returns the Paymark transaction id included in the notification, representing the transaction the notification is for.
		/// </summary>
		/// <remarks>
		/// <para>This value is included in transactions from February 2017 onwards, may not be included in earlier notifications and may return null or "" instead.</para>
		/// </remarks>
		public string TransactionId
		{
			get { return _TransactionId; }
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

		/// <summary>
		/// Returns the content of the notification that was signed as proof of authenticity.
		/// </summary>
		public string SignedData
		{
			get { return _SignedData; }
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
				case "TRANSACTIONID":
					_TransactionId = value;
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