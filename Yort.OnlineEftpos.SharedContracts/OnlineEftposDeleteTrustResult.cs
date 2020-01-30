using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Represents the result of a request to delete an established trust/autopay relationship.
	/// </summary>
	/// <seealso cref="OnlineEftpos.IOnlineEftposClient.DeleteTrust(OnlineEftposDeleteTrustOptions)"/>
	public class OnlineEftposDeleteTrustResult
	{

		#region Public Properties

		/// <summary>
		/// Returns the list of HATEOAS links included in the search results.
		/// </summary>
		[JsonProperty("links")]
		public IEnumerable<HateoasLink> Links { get; set; }

		/// <summary>
		/// Returns the UUID of the removed autopay/trust relationship.
		/// </summary>
		[JsonProperty("id")]
		public string Id { get; set; }

		/// <summary>
		/// The payer id associated with the removed autopay/trust relationship.
		/// </summary>
		[JsonProperty("payerId")]
		public string PayerId { get; set; }

		/// <summary>
		/// The payer id type associated with the removed autopay/trust relationship.
		/// </summary>
		[JsonProperty("payerIdType")]
		public string PayerIdType { get; set; }

		/// <summary>
		/// The bank id associated with the removed autopay/trust relationship.
		/// </summary>
		[JsonProperty("bankId")]
		public string BankId { get; set; }

		/// <summary>
		/// The merchant id associated with the removed autopay/trust relationship.
		/// </summary>
		[JsonProperty("merchantIdCode")]
		public string MerchantIdCode { get; set; }

		/// <summary>
		/// The merchant id associated with the removed autopay/trust relationship.
		/// </summary>
		[JsonProperty("bank")]
		public BankDeletedTrustRelationship Bank { get; set; }

		/// <summary>
		/// The date and time the autopay/trust relationship was created.
		/// </summary>
		[JsonProperty("creationTime")]
		public DateTimeOffset CreationTime { get; set; }

		/// <summary>
		/// The date and time the autopay/trust relationship was last modified.
		/// </summary>
		[JsonProperty("modificationTime")]
		public DateTimeOffset ModificationTime { get; set; }

		#endregion

	}

	/// <summary>
	/// Provides details of the response from a bank for a request to delete a trust/autopay relationship.
	/// </summary>
	public class BankDeletedTrustRelationship
	{
		/// <summary>
		/// The bank's unique id for this relationship.
		/// </summary>
		[JsonProperty("id")]
		public string Id { get; set; }
		/// <summary>
		/// The status returned by the bank.
		/// </summary>
		[JsonProperty("status")]
		public string Status { get; set; }
	}
}
