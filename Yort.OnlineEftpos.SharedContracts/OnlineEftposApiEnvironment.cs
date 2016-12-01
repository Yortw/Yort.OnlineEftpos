using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Represents the possible production and sandbox environments for the Online Eftpos API.
	/// </summary>
	public enum OnlineEftposApiEnvironment
	{
		/// <summary>
		/// Development/test environment.
		/// </summary>
		Sandbox,
		/// <summary>
		/// User acceptance test environment.
		/// </summary>
		Uat,
		/// <summary>
		/// Live production environment for real transaction.
		/// </summary>
		Live
	}
}