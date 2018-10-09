using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Represents the status of a payment transaction.
	/// </summary>
	/// <see cref="OnlineEftposClient"/>
	/// <see cref="OnlineEftposClient.RequestPayment(OnlineEftposPaymentRequest)"/>
	/// <see cref="OnlineEftposClient.CheckPaymentStatus(string)"/>
	/// <see cref="OnlineEftposPaymentRequest"/>
	public sealed class OnlineEftposPaymentStatus
	{
		/// <summary>
		/// Sets or returns the unique identifier of the payment.
		/// </summary>
		[JsonProperty("id")]
		public string Id { get; set; }

		/// <summary>
		/// Sets or returns a text based description of the payment status.
		/// </summary>
		/// <seealso cref="TransactionStatus"/>
		[JsonProperty("status")]
		public string Status { get; set; }

		/// <summary>
		/// Sets or returns the payer/bank details associated with the transaction.
		/// </summary>
		[JsonProperty("bank")]
		public BankDetails Bank { get; set; }

		/// <summary>
		/// Sets or returns information about the merchant related to this transaction.
		/// </summary>
		[JsonProperty("merchant")]
		public MerchantDetails Merchant { get; set; }

		/// <summary>
		/// Sets or returns the details of the transaction, such as amount.
		/// </summary>
		[JsonProperty("transaction")]
		public PaymentDetails Transaction { get; set; }

		/// <summary>
		/// Sets or returns the details of a trust relationship associated with this transaction, if any, otherwise null.
		/// </summary>
		public OnlineEftposTrust Trust { get; set; }

		/// <summary>
		/// The date and time the payment was created, as a univeral date time value.
		/// </summary>
		[JsonProperty("creationTime")]
		public DateTimeOffset CreationTime { get; set; }

		/// <summary>
		/// The date and time the payment was last updated, as a universal date time value.
		/// </summary>
		[JsonProperty("modificationTime")]
		public DateTimeOffset ModificationTime { get; set; }
	}
}