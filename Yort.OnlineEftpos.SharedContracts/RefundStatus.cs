using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Provides a list of the possible states for a refund transaction, and details of each status.
	/// </summary>
	public static class RefundStatus 
	{

		#region Static Implementation

		private static TransactionStatus _Unsubmitted;
		private static TransactionStatus _Refunded;
		private static TransactionStatus _Declined;
		private static TransactionStatus _Error;

		/// <summary>
		/// The 'unsubmitted' status.
		/// </summary>
		/// <remarks>
		/// <para>Created but not yet received by bank API; could occur if bank API becomes unavailable.</para>
		/// <para>Possible states following this one; <see cref="Refunded"/>, <see cref="Declined"/>, <see cref="Error"/></para>
		/// </remarks>
		public static TransactionStatus Unsubmitted
		{
			get
			{
				return _Unsubmitted ?? (_Unsubmitted = new TransactionStatus("UNSUBMITTED", false, "Created but not yet received by bank API; could occur if bank API becomes unavailable."));
			}
		}

		/// <summary>
		/// The 'Refunded' status indicates a successful refund.
		/// </summary>
		/// <remarks>
		/// <para>Bank API has responded to refund request with success code.</para>
		/// <para>This is a final status.</para>
		/// </remarks>
		public static TransactionStatus Refunded
		{
			get
			{
				return _Refunded ?? (_Refunded = new TransactionStatus("REFUNDED", false, "Bank API has responded to refund request with success code."));
			}
		}

		/// <summary>
		/// The 'Declined' status indicating the refund was refused or could not be processed due to insufficient funds etc.
		/// </summary>
		/// <remarks>
		/// <para>The bank API declined refund request e.g. due to insufficient merchant/gateway funds, or other business logic.</para>
		/// <para>This is a 'final' status.</para>
		/// </remarks>
		public static TransactionStatus Declined
		{
			get
			{
				return _Declined ?? (_Declined = new TransactionStatus("DECLINED", true, "Bank API declined refund request e.g. due to insufficient merchant/gateway funds, or other business logic."));
			}
		}

		/// <summary>
		/// The bank responded to refund request with a system error 
		/// </summary>
		/// <remarks>
		/// <para>Bank responded to refund request with a system error </para>
		/// <para>This is a 'final' status.</para>
		/// </remarks>
		public static TransactionStatus Error
		{
			get
			{
				return _Error ?? (_Error = new TransactionStatus("ERROR", true, "Bank responded to refund request with a system error."));
			}
		}

		/// <summary>
		/// Returns a <see cref="TransactionStatus"/> instance from the provided string containing the name of the status.
		/// </summary>
		/// <param name="status">A string containing a value that matches a <see cref="TransactionStatus.Name"/> for a status that is valid for a refund.</param>
		/// <returns>A <see cref="TransactionStatus"/> instance.</returns>
		/// <exception cref="Yort.OnlineEftpos.OnlineEftposInvalidDataException">Thrown if <paramref name="status"/> does not match any known status.</exception>
		public static TransactionStatus FromString(string status)
		{
			if (status == Unsubmitted) return Unsubmitted;
			if (status == Error) return Error;
			if (status == Refunded) return Refunded;
			if (status == Declined) return Declined;

			throw new OnlineEftposInvalidDataException($"Unknown status \"{status}\"");
		}

		#endregion

	}
}