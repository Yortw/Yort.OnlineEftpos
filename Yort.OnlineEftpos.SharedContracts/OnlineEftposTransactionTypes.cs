using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Provides string constants for known 'transaction types' used by Online EFTPOS.
	/// </summary>
	/// <seealso cref="PaymentDetails"/>
	public static class OnlineEftposTransactionTypes
	{
		/// <summary>
		/// A standard payment/refund transaction. The default value for most transactions.
		/// </summary>
		/// <remarks>
		/// <para>Returns the string "REGULAR" (without the quotes).</para>
		/// </remarks>
		public const string Regular = "REGULAR";

		/// <summary>
		/// A transaction type that indicates along with a regular purchase or refund, a trust relationship is requested
		/// so the merchant can charge this customer again in the future without the customer having to explicitly authorise
		/// each payment
		/// </summary>
		/// <remarks>
		/// <para>Returns the string "TRUSTSETUP" (without the quotes).</para>
		/// </remarks>
		public const string TrustSetup = "TRUSTSETUP";
	}
}