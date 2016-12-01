using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Represents the details of a payer/account in a transaction.
	/// </summary>
	public sealed class BankDetails
	{
		/// <summary>
		/// An identifier value registered with the Online Eftpos API that represents a specific payer. This is usually provider by the payer at time of purchase.
		/// </summary>
		[JsonProperty("payerId")]
		public string PayerId { get; set; }

		/// <summary>
		/// The type of identifier provided in <see cref="PayerId"/>. Usually "MOBILE".
		/// </summary>
		[JsonProperty("payerIdType")]
		public string PayerIdType { get; set; }

		/// <summary>
		/// The id of the bank the payer is registered with. 
		/// </summary>
		[JsonProperty("bankId")]
		public string BankId { get; set; }

		/// <summary>
		/// Throws if the details are invalid.
		/// </summary>
		/// <remarks>
		/// <para>Throws if the <seealso cref="PayerId"/> is null, empty or only whitespace.</para>
		/// </remarks>
		/// <exception cref="OnlineEftposInvalidDataException">Thrown if the values of this instance are invalid.</exception>
		public void EnsureValid()
		{
			try
			{
				PayerId.GuardNullEmptyOrWhitespace(nameof(PayerId));
				BankId.GuardNullEmptyOrWhitespace(nameof(BankId));
				PayerIdType.GuardNullEmptyOrWhitespace(nameof(PayerIdType));
			}
			catch (ArgumentException ae)
			{
				throw new OnlineEftposInvalidDataException(ae); 
			}
		}
	}
}
