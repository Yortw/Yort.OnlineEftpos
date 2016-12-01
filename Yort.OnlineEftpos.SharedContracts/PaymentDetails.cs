﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Provides transaction details specific to a payment (request).
	/// </summary>
	public sealed class PaymentDetails : TransactionRequestDetails
	{
		/// <summary>
		/// Sets or returns the amount of the payment in the minimum unit of the currency (i.e for NZ, Australia, US this would be cents).
		/// </summary>
		/// <remarks>
		/// <para>Must be greater than 0.</para>
		/// </remarks>
		[JsonProperty("amount")]
		public int Amount { get; set; }

		/// <summary>
		/// Sets or returns a string containing a three digit code representing the currency of the transaction. If not provided the API will assume a default.
		/// </summary>
		/// <remarks>
		/// <para>For the Paymark Online Eftpos API the default value assumed if not provided is NZD (New Zealand Dollar).</para>
		/// </remarks>
		[JsonProperty("currency")]
		public string Currency { get; set; }

		/// <summary>
		/// Sets or returns a string containing a short description of the payment request, potentially shown to the user and/or on their bank statement.
		/// </summary>
		/// <remarks>
		/// <para>Cannot be more than 12 characters long.</para>
		/// <para>This value is optional, but recommended.</para>
		/// </remarks>
		[JsonProperty("description")]
		public string Description { get; set; }

		/// <summary>
		/// A string containing a unique reference for this payment, provided by the application code. This value will be passed back to the callback url provided with the transaction when the payment status changes.
		/// </summary>
		/// <remarks>
		/// <para>Cannot be more than 100 characters long.</para>
		/// <para>This value is optional, but recommended.</para>
		/// </remarks>
		[JsonProperty("orderId")]
		public string OrderId { get; set; }

		/// <summary>
		/// Throws if the details are invalid.
		/// </summary>
		/// <remarks>
		/// <para>Throws if any of the properties are null, or EnsureValid method on any of the property values throws.</para>
		/// </remarks>
		/// <exception cref="OnlineEftposInvalidDataException">Thrown if the values of this instance are invalid.</exception>
		public override void EnsureValid()
		{
			base.EnsureValid();

			try
			{
				Amount.GuardMinValue(1, nameof(Amount));

				Description.GuardMaxLength(12, nameof(Description));
				OrderId.GuardMaxLength(100, nameof(OrderId));

				ValidateCurrency(Currency);
			}
			catch (ArgumentException ae)
			{
				throw new OnlineEftposInvalidDataException(ae);
			}
		}

		private void ValidateCurrency(string currency)
		{
			if (String.IsNullOrEmpty(Currency)) return;
			if (Currency.Length != 3) throw new OnlineEftposInvalidDataException(new ArgumentException("Currency must be three upper case letters."));

			foreach (var c in currency)
			{
					if (!Char.IsLetter(c) || !Char.IsUpper(c))
						throw new OnlineEftposInvalidDataException(new ArgumentException("Currency must be three upper case letters."));
			}
		}
	}
}