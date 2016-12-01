using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Represents a request for payment.
	/// </summary>
	public sealed class OnlineEftposPaymentRequest
	{
		/// <summary>
		/// A <see cref="BankDetails"/> instance providing details of the payer/account to request payment from.
		/// </summary>
		[JsonProperty("bank")]
		public BankDetails Bank { get; set; }

		/// <summary>
		/// A <see cref="MerchantDetails"/> instance providing details of the merchant the payment is to be made to.
		/// </summary>
		[JsonProperty("merchant")]
		public MerchantDetails Merchant { get; set; }

		/// <summary>
		/// A <see cref="PaymentDetails"/> instance providing details of the transaction itself (i.e the amount, merchant reference etc).
		/// </summary>
		[JsonProperty("transaction")]
		public PaymentDetails Transaction { get; set; }

		/// <summary>
		/// Throws if the details are invalid.
		/// </summary>
		/// <remarks>
		/// <para>Throws if any of the properties are null, or EnsureValid method on any of the property values throws.</para>
		/// </remarks>
		/// <exception cref="OnlineEftposInvalidDataException">Thrown if the values of this instance are invalid.</exception>
		public void EnsureValid()
		{
			try
			{
				Bank.GuardNull(nameof(Bank));
				Merchant.GuardNull(nameof(Merchant));
				Transaction.GuardNull(nameof(Transaction));

				Bank.EnsureValid();
				Merchant.EnsureValid();
				Transaction.EnsureValid();
			}
			catch	(ArgumentException ae)
			{
				throw new OnlineEftposInvalidDataException(ae);
			}
		}
	}
}