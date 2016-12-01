using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Represents a request to send a refund for a previous payment.
	/// </summary>
	public sealed class OnlineEftposRefundRequest
	{

		/// <summary>
		/// A <see cref="MerchantDetails"/> instance providing details of the merchant the refund is to be made from.
		/// </summary>
		[JsonProperty("merchant")]
		public MerchantDetails Merchant { get; set; }

		/// <summary>
		/// A <see cref="RefundDetails"/> instance providing details of the transaction itself (i.e the amount, refund reason etc).
		/// </summary>
		[JsonProperty("transaction")]
		public RefundDetails Transaction { get; set; }

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
				Merchant.GuardNull(nameof(Merchant));
				Transaction.GuardNull(nameof(Transaction));
			}
			catch (ArgumentException ae)
			{
				throw new OnlineEftposInvalidDataException(ae);
			}
		}
	}
}