using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Instantiated via Json deserialisation (reflection).")]
#if __IOS__
		[Foundation.Preserve]
#endif
	internal sealed class OnlineEftposApiError
	{
		[JsonProperty("error")]
		public string Error { get; set; }

		[JsonProperty("reference")]
		public string Reference { get; set; }

		[JsonProperty("messages")]
		public ApiErrorMessage[] Messages { get; set; }
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Instantiated via Json deserialisation (reflection).")]
#if __IOS__
		[Foundation.Preserve]
#endif
	internal sealed class ApiErrorMessage
	{
		[JsonProperty("field")]
		public string Field { get; set; }
		[JsonProperty("message")]
		public string Message { get; set; }
	}
}