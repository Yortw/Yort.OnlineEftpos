using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Provides a list of the known and supported Online Eftpos API versions.
	/// </summary>
	public enum OnlineEftposApiVersion
	{
		/// <summary>
		/// Indicates no API segment should be included in call paths. This is the default from version 2.0 onwards as the API no longer uses versioned paths.
		/// </summary>
		None = 0,
		/// <summary>
		/// Version 1.1.
		/// </summary>
		[Obsolete("Use OnlineEftposApiVersion.None - from February 2017 on the API no longer uses versioned URLs.")]
		V1P1 = 1
	}
}