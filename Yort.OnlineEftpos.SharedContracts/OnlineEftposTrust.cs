using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Represents details about an associated trust relationship in the response to a <see cref="OnlineEftposPaymentRequest"/>.
	/// </summary>
	public class OnlineEftposTrust
	{
		/// <summary>
		/// The id of the newly requested trust relationship.
		/// </summary>
		[JsonProperty("id")]
		public string Id { get; set; }

		/// <summary>
		/// A string describing the status of the trust relationship.
		/// </summary>
		/// <seealso cref="TrustStatus"/>
		[JsonProperty("trustPaymentStatus")]
		public string Status { get; set; }
	}
}