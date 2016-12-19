using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Provides a list of the possible states for a payment transaction, and details of each status.
	/// </summary>
	public static class PaymentStatus 
	{

		#region Static Implementation

		private static TransactionStatus _New;
		private static TransactionStatus _Error;
		private static TransactionStatus _Submitted;
		private static TransactionStatus _Authorised;
		private static TransactionStatus _Declined;
		private static TransactionStatus _Expired;
		private static TransactionStatus _Refunded;

		/// <summary>
		/// The 'New' status.
		/// </summary>
		/// <remarks>
		/// <para>A new payment request from a merchant, which has not been processed yet.</para>
		/// <para>Possible states following this one; <see cref="Submitted"/>, <see cref="Error"/></para>
		/// </remarks>
		public static TransactionStatus New
		{
			get
			{
				return _New ?? (_New = new TransactionStatus("NEW", false, "A new payment request from a merchant, which has not been processed yet."));
			}
		}

		/// <summary>
		/// The status for payment that has encountered a processing error.
		/// </summary>
		/// <remarks>
		/// <para>A payment request could not be successfully sent to the bank.</para>
		/// <para>This is a 'final' status.</para>
		/// </remarks>
		public static TransactionStatus Error
		{
			get
			{
				return _Error ?? (_Error = new TransactionStatus("ERROR", true, "A payment request could not be successfully sent to the bank."));
			}
		}

		/// <summary>
		/// The 'Submitted' status.
		/// </summary>
		/// <remarks>
		/// <para>A payment request which has been sent to a bank for processing, and for which the bank has acknowledge the request.</para>
		/// <para>Possible states following this one; <see cref="Authorised"/>, <see cref="Declined"/>, <see cref="Error"/>.</para>
		/// </remarks>
		public static TransactionStatus Submitted
		{
			get
			{
				return _Submitted ?? (_Submitted = new TransactionStatus("SUBMITTED", false, "A payment request which has been sent to a bank for processing, and for which the bank has acknowledge the request."));
			}
		}

		/// <summary>
		/// The 'Authorised' status, indicating the payment has been processed succesfully and funds transferred.
		/// </summary>
		/// <remarks>
		/// <para></para>
		/// <para>This is NOT a 'final' status as the payment could changed to a <see cref="Refunded"/> status.</para>
		/// </remarks>
		public static TransactionStatus Authorised
		{
			get
			{
				return _Authorised ?? (_Authorised = new TransactionStatus("AUTHORISED", false, "A payment request, for which the bank has notified Paymark that the account holder has authorised the payment."));
			}
		}

		/// <summary>
		/// The 'Declined' status indicating the payment was refused or could not be processed due to insufficient funds etc.
		/// </summary>
		/// <remarks>
		/// <para>A payment request, for which the bank has notified Paymark that the account holder has declined the payment.</para>
		/// <para>This is a 'final' status.</para>
		/// </remarks>
		public static TransactionStatus Declined
		{
			get
			{
				return _Declined ?? (_Declined = new TransactionStatus("DECLINED", true, "A payment request, for which the bank has notified Paymark that the account holder has declined the payment."));
			}
		}

		/// <summary>
		/// The 'Expired' status indicates a payment was not approved or declined within the timeframe allowed by the API.
		/// </summary>
		/// <remarks>
		/// <para>A payment request, for which the bank has notified Paymark that the account holder has declined the payment.</para>
		/// <para>This is a 'final' status.</para>
		/// </remarks>
		public static TransactionStatus Expired
		{
			get
			{
				return _Expired ?? (_Expired = new TransactionStatus("EXPIRED", true, "A payment request, for which the bank has notified Paymark that the expiry timeout has been passed."));
			}
		}

		/// <summary>
		/// The 'Refunded' status indicates at least part of this payment has been refunded.
		/// </summary>
		/// <remarks>
		/// <para>A payment request which was successfully authorised, but which has been subsequently successfully refunded.</para>
		/// <para>This status is not considered 'final' as part refunds can be refunded again, though the status technically changes it will still be 'refunded'.</para>
		/// </remarks>
		public static TransactionStatus Refunded
		{
			get
			{
				return _Refunded ?? (_Refunded = new TransactionStatus("REFUNDED", false, "A payment request which was successfully authorised, but which has been subsequently successfully refunded."));
			}
		}

		/// <summary>
		/// Returns a <see cref="TransactionStatus"/> instance from the provided string containing the name of the status.
		/// </summary>
		/// <param name="status">A string containing a value that matches a <see cref="TransactionStatus.Name"/> for a status that is valid for a payment.</param>
		/// <returns>A <see cref="TransactionStatus"/> instance.</returns>
		/// <exception cref="Yort.OnlineEftpos.OnlineEftposInvalidDataException">Thrown if <paramref name="status"/> does not match any known status.</exception>
		public static TransactionStatus FromString(string status)
		{
			if (status == New) return New;
			if (status == Error) return Error;
			if (status == Submitted) return Submitted;
			if (status == Authorised) return Authorised;
			if (status == Declined) return Declined;
			if (status == Expired) return Expired;
			if (status == Refunded) return Refunded;

			throw new OnlineEftposInvalidDataException($"Unknown status \"{status}\"");
		}

		#endregion

	}
}