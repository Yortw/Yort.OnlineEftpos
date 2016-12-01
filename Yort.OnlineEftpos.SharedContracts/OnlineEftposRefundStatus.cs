using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Represents the status of a refund transaction.
	/// </summary>
	/// <see cref="OnlineEftposClient"/>
	/// <see cref="OnlineEftposClient.SendRefund(OnlineEftposRefundRequest)"/>
	/// <see cref="OnlineEftposClient.CheckRefundStatus(string)"/>
	/// <see cref="OnlineEftposRefundRequest"/>
	public sealed class OnlineEftposRefundStatus
	{

		/// <summary>
		/// Sets or returns the unique identifier of the payment.
		/// </summary>
		[JsonProperty("id")]
		public string Id { get; set; }

		/// <summary>
		/// Sets or returns a text based description of the refund status.
		/// </summary>
		/// <seealso cref="TransactionStatus"/>
		[JsonProperty("status")]
		public string Status { get; set; }

		/// <summary>
		/// Sets or returns information about the merchant related to this transaction.
		/// </summary>
		[JsonProperty("merchant")]
		public MerchantDetails Merchant { get; set; }

		/// <summary>
		/// Sets or returns the details of the transaction, such as amount.
		/// </summary>
		[JsonProperty("transaction")]
		public RefundDetails Transaction { get; set; }

		/// <summary>
		/// The date and time the refund was created, as a univeral date time value.
		/// </summary>
		[JsonProperty("creationTime")]
		public DateTimeOffset CreationTime { get; set; }

		/// <summary>
		/// The date and time the refund was last updated, as a universal date time value.
		/// </summary>
		[JsonProperty("modificationTime")]
		public DateTimeOffset ModificationTime { get; set; }
	}
}