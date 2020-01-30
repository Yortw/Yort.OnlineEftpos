using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Yort.OnlineEftpos
{
	internal class OnlineEftposDeleteTrustRequest
	{
		[JsonProperty("status")]
		public string Status { get { return "CANCELLED"; } }
	}
}