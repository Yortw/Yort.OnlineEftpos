using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Provides transaction details specific to a payment (request).
	/// </summary>
	public sealed class PaymentDetails : TransactionRequestDetails
	{

		private static readonly char[] InvalidOrderIdCharacters = new char[] { '@', '#', '^', '%', '&', '|', '<', '>', '"', ';', '.', '\\', '/', '!', ':', ',' };

		/// <summary>
		/// Sets or returns the amount of the payment in the minimum unit of the currency (i.e for NZ, Australia, US this would be cents).
		/// </summary>
		/// <remarks>
		/// <para>Must be greater than 0.</para>
		/// </remarks>
		[JsonProperty("amount")]
		public int Amount { get; set; }

		/// <summary>
		/// Sets or returns a string containing a three digit code representing the currency of the transaction. If not provided the API will assume a default.
		/// </summary>
		/// <remarks>
		/// <para>For the Paymark Online Eftpos API the default value assumed if not provided is NZD (New Zealand Dollar).</para>
		/// </remarks>
		[JsonProperty("currency")]
		public string Currency { get; set; }

		/// <summary>
		/// Sets or returns a string containing a short description of the payment request, potentially shown to the user and/or on their bank statement.
		/// </summary>
		/// <remarks>
		/// <para>Cannot be more than 12 characters long.</para>
		/// <para>This value is optional, but recommended.</para>
		/// </remarks>
		[JsonProperty("description")]
		public string Description { get; set; }

		/// <summary>
		/// A string containing a client generated reference for this payment, provided by the application code. This value will be passed back to the callback url provided with the transaction when the payment status changes.
		/// </summary>
		/// <remarks>
		/// <para>This value does not need to be unique, but it is recommended to be unique to facilitate tracking of transactions.</para>
		/// <para>Cannot be more than 100 characters long. The first 12 characters will be shown on the customer's bank statement.</para>
		/// <para>Special characters are not permitted, namely: @ # ^ ’ % &amp; | &lt; &gt; " ; . \ / ! : ,</para>
		/// </remarks>
		[JsonProperty("orderId")]
		public string OrderId { get; set; }

		/// <summary>
		/// Sets or returns a string describing the type of transaction to be performed.
		/// </summary>
		/// <remarks>
		/// <para>See <see cref="OnlineEftposTransactionTypes"/> for constants providing known trnasaction types.</para>
		/// <para>Defaults to <see cref="OnlineEftposTransactionTypes.Regular"/>.</para>
		/// </remarks>
		[JsonProperty("transactionType")]
		public string TransactionType { get; set; } = OnlineEftposTransactionTypes.Regular;

		/// <summary>
		/// If known, the date and time the transaction was actually settled with the bank.
		/// </summary>
		/// <remarks>
		/// <para>Usually only returned from <see cref="IOnlineEftposClient.PaymentSearch(OnlineEftposPaymentSearchOptions)"/> for older transactions that have been settled, otherwise null.</para>
		/// </remarks>
		[JsonProperty("actualSettlementDate")]
		public DateTimeOffset SettlementDate { get; set; }

		/// <summary>
		/// Throws if the details are invalid.
		/// </summary>
		/// <remarks>
		/// <para>Throws if any of the properties are null, or EnsureValid method on any of the property values throws.</para>
		/// </remarks>
		/// <exception cref="OnlineEftposInvalidDataException">Thrown if the values of this instance are invalid.</exception>
		public override void EnsureValid()
		{
			base.EnsureValid();

			try
			{
				Amount.GuardMinValue(1, nameof(Amount));

				Description.GuardMaxLength(12, nameof(Description));
				TransactionType.GuardNullEmptyOrWhitespace(nameof(TransactionType));
				ValidateOrderId(OrderId);
				ValidateCurrency(Currency);
			}
			catch (ArgumentException ae)
			{
				throw new OnlineEftposInvalidDataException(ae);
			}
		}

		private static void ValidateOrderId(string orderId)
		{
			if (String.IsNullOrEmpty(orderId)) throw new ArgumentException("OrderId is required.");

			orderId.GuardMaxLength(100, nameof(OrderId));
			var invalidCharIndex = orderId.IndexOfAny(InvalidOrderIdCharacters);
			if (invalidCharIndex >= 0) throw new ArgumentException("OrderId cannot contain " + orderId.Substring(invalidCharIndex, 1));
		}

		private static void ValidateCurrency(string currency)
		{
			if (String.IsNullOrEmpty(currency)) return;
			if (currency.Length != 3) throw new OnlineEftposInvalidDataException(new ArgumentException("Currency must be three upper case letters."));

			foreach (var c in currency)
			{
					if (!Char.IsLetter(c) || !Char.IsUpper(c))
						throw new OnlineEftposInvalidDataException(new ArgumentException("Currency must be three upper case letters."));
			}
		}
	}
}