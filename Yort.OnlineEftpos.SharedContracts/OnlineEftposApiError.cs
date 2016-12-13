using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Represents error information returned from the Online EFTPOS API.
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Instantiated via Json deserialisation (reflection).")]
#if __IOS__
		[Foundation.Preserve]
#endif
	public sealed class OnlineEftposApiError
	{
		/// <summary>
		/// An error message.
		/// </summary>
		[JsonProperty("error")]
		public string Error { get; set; }
		/// <summary>
		/// A unique reference for the error that can be reported to Paymark for more detail.
		/// </summary>
		[JsonProperty("reference")]
		public string Reference { get; set; }
		/// <summary>
		/// An enumerable of <see cref="ApiErrorMessage"/> containing distinct errors with more detail (in the case of multiple errors, particularly with field validation).
		/// </summary>
		[JsonProperty("messages")]
		public IEnumerable<ApiErrorMessage> Messages { get; set; }
	}

	/// <summary>
	/// Represents an error message, often relating to a specific request field.
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Instantiated via Json deserialisation (reflection).")]
#if __IOS__
		[Foundation.Preserve]
#endif
	public sealed class ApiErrorMessage
	{
		/// <summary>
		/// The request field this message relates to, if any.
		/// </summary>
		[JsonProperty("field")]
		public string Field { get; set; }
		/// <summary>
		/// An error message.
		/// </summary>
		[JsonProperty("message")]
		public string Message { get; set; }
	}
}