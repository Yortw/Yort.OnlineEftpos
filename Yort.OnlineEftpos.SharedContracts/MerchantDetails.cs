using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Details of a merchant account associated with a transaction.
	/// </summary>
	public sealed class MerchantDetails
	{
		/// <summary>
		/// The unique id of the merchant. Required.
		/// </summary>
		[JsonProperty("merchantIdCode")]
		public string MerchantIdCode { get; set; }
		/// <summary>
		/// The web site address for the merchant. Optional.
		/// </summary>
		[JsonProperty("merchantUrl")]
		public Uri MerchantUrl { get; set; }
		/// <summary>
		/// An HTTP endpoint the API can callback when a transaction is approved by the payer. Required. 
		/// </summary>
		[JsonProperty("callbackUrl")]
		public Uri CallbackUrl { get; set; }

		/// <summary>
		/// Throws if the details are invalid.
		/// </summary>
		/// <remarks>
		/// <para>Throws if the <seealso cref="MerchantIdCode"/> or <see cref="CallbackUrl"/>/> are invalid.</para>
		/// </remarks>
		/// <exception cref="OnlineEftposInvalidDataException">Thrown if the values of this instance are invalid.</exception>
		public void EnsureValid()
		{
			try
			{
				MerchantIdCode.GuardNullEmptyOrWhitespace(nameof(MerchantIdCode));
				CallbackUrl.GuardNull(nameof(CallbackUrl));
			}
			catch (ArgumentException ae)
			{
				throw new OnlineEftposInvalidDataException(ae);
			}
		}
	}
}