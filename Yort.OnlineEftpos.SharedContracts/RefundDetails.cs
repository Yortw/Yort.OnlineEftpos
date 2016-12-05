using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Provides transaction details specific to a refund (request).
	/// </summary>
	public class RefundDetails : TransactionRequestDetails
	{
		/// <summary>
		/// Sets or returns the amount of the refund in the minimum unit of the currency (i.e for NZ, Australia, US this would be cents).
		/// </summary>
		/// <remarks>
		/// <para>Must be greater than 0.</para>
		/// </remarks>		
		[JsonProperty("refundAmount")]
		public int RefundAmount { get; set; }

		/// <summary>
		/// Cause of refund as indicated by merchant.
		/// </summary>
		/// <remarks>
		/// <para>Optional, cannot be more tha 512 characters.</para>
		/// </remarks>
		[JsonProperty("refundReason")]
		public string RefundReason { get; set; }

		/// <summary>
		/// The UUID of the original payment that is being refunded; used by the API to validate <see cref="RefundAmount"/>.
		/// </summary>
		/// <remarks>
		/// <para>Required.</para>
		/// </remarks>
		[JsonProperty("originalPaymentId")]
		public string OriginalPaymentId { get; set; }

		/// <summary>
		/// The merchant reference for this refund id. Not required to be unique, but recommended.
		/// </summary>
		[JsonProperty("refundId")]
		public string RefundId { get; set; }

		/// <summary>
		/// Throws if the details are invalid.
		/// </summary>
		/// <remarks>
		/// <para>Throws if the <see cref="RefundAmount"/> is less than 1, the <see cref="OriginalPaymentId"/> is not provided, or the <see cref="RefundReason"/> is more than 512 characters.</para>
		/// </remarks>
		/// <exception cref="OnlineEftposInvalidDataException">Thrown if the values of this instance are invalid.</exception>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
		public override void EnsureValid()
		{
			try
			{
				base.EnsureValid();

				RefundAmount.GuardMinValue(1, nameof(RefundAmount));
				OriginalPaymentId.GuardNullEmptyOrWhitespace(nameof(OriginalPaymentId));

				if ((RefundReason?.Length ?? 0) > 512)
					throw new ArgumentException(nameof(RefundReason) + "cannot be more than 512 characters.", nameof(RefundReason));
			}
			catch (ArgumentException ae)
			{
				throw new OnlineEftposInvalidDataException(ae);
			}
		}
	}
}