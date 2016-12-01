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
		/// Indicate the latest supported API version, whatever that is.
		/// </summary>
		Latest = 0,
		/// <summary>
		/// Version 1.1.
		/// </summary>
		V1P1 = 1
	}
}