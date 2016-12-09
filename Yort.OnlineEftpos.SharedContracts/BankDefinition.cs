using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Represents a bank known to be supported by Online EFTPOS, and the associated payer id types that is supports.
	/// </summary>
	public class BankDefinition
	{

		/// <summary>
		/// Partial constructor.
		/// </summary>
		/// <param name="id">The unique id of this bank, case insensitive. Also used for <see cref="DisplayName"/>.</param>
		/// <param name="payerIdTypes">A list of strings containing allowed payer id types for this bank.</param>
		/// <exception cref="System.ArgumentException">Thrown if <see cref="Id"/> is an empty string or whitespace.</exception>
		/// <exception cref="System.ArgumentNullException">Thrown if <see cref="Id"/> or <see cref="PayerIdTypes"/> is null.</exception>
		public BankDefinition(string id, IList<string> payerIdTypes) : this(id, null, payerIdTypes)
		{
		}

		/// <summary>
		/// Full constructor.
		/// </summary>
		/// <param name="id">The unique id of this bank, case insensitive.</param>
		/// <param name="displayName">The display name of this bank. If null then <paramref name="id"/> will be used.</param>
		/// <param name="payerIdTypes">A list of strings containing allowed payer id types for this bank.</param>
		/// <exception cref="System.ArgumentException">Thrown if <see cref="Id"/> is an empty string or whitespace.</exception>
		/// <exception cref="System.ArgumentNullException">Thrown if <see cref="Id"/> or <see cref="PayerIdTypes"/> is null.</exception>
		public BankDefinition(string id, string displayName, IList<string> payerIdTypes)
		{
			id.GuardNullEmptyOrWhitespace(nameof(id));
			payerIdTypes.GuardNull(nameof(displayName));

			this.Id = id;
			this.DisplayName = displayName ?? id;
			this.PayerIdTypes = payerIdTypes;
		}

		/// <summary>
		/// Returns the unique, case insensitive, id of this bank.
		/// </summary>
		/// <remarks>
		/// <para>This must be the actual id passed to the Online EFTPOS API via <see cref="BankDetails.BankId"/> when making transaction requests.</para>
		/// </remarks>
		public string Id { get; private set; }

		/// <summary>
		/// Returns the display name of this bank.
		/// </summary>
		public string DisplayName { get; private set; }

		/// <summary>
		/// A list of payer id types supported by this bank.
		/// </summary>
		public IList<string> PayerIdTypes { get; private set; }
	}
}