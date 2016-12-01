using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Provides a list of banks known to be supported, and the payer id types each one supports. 
	/// </summary>
	/// <remarks>
	/// <para>As this list is hard coded into the library it may or may not be up to date at any given point in time. It should be used only as a UI guide, and not
	/// a validation list/authority.</para>
	/// <para>The list is exposed as modifiable collections so if an authoritive source becomes available, this data can be modified at runtime.</para>
	/// </remarks>
	public static class KnownBanks
	{

		#region Fields

		private static ConcurrentDictionary<string, BankDefinition> _Banks;

		#endregion

		#region Constructors

		/// <summary>
		/// Static constructor which sets up known banks.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification="Need to load the actual content anyway, so no perf gain by following rule.")]
		static KnownBanks()
		{
			_Banks = new ConcurrentDictionary<string, BankDefinition>(StringComparer.OrdinalIgnoreCase);
			
			AddBank(new BankDefinition("ASB", new List<string>(1) { "MOBILE" }));
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Adds a <see cref="BankDefinition"/> to the set of known, supported banks.
		/// </summary>
		/// <param name="bank">A <see cref="BankDefinition"/> instance to be added to the collection of known banks.</param>
		/// <returns>True if the item was succesfully added, otherwise false (usually because the id is a duplicate).</returns>
		/// <exception cref="System.ArgumentException">Thrown if the id of the <paramref name="bank"/> reference is null, empty or only whitespace.</exception>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="bank"/> is null.</exception>
		public static bool AddBank(BankDefinition bank)
		{
			bank.GuardNull(nameof(bank));
			bank.Id.GuardNullEmptyOrWhitespace("bank.Id");

			return _Banks.TryAdd(bank.Id, bank);
		}

		/// <summary>
		/// Removes a bank from the list of known banks.
		/// </summary>
		/// <param name="id">The id of the bank defintion to remove.</param>
		/// <returns>True if the item was removed, false if it was not found.</returns>
		/// <exception cref="System.ArgumentException">Thrown if the id of the <paramref name="id"/> reference is null, empty or only whitespace.</exception>
		public static bool RemoveBank(string id)
		{
			id.GuardNullEmptyOrWhitespace(nameof(id));

			BankDefinition removedBank = null;
			return _Banks.TryRemove(id, out removedBank);
		}

		/// <summary>
		/// Returns the bank with the specified <paramref name="id"/>, or null if no bank with that id was found.
		/// </summary>
		/// <param name="id">The unique id of the bank object to retrieve.</param>
		/// <returns>A <see cref="BankDefinition"/> containing details of the known bank, or null if no relevant bank was found.</returns>
		public static BankDefinition GetBank(string id)
		{
			id.GuardNullEmptyOrWhitespace(nameof(id));

			BankDefinition retVal = null;
			_Banks.TryGetValue(id, out retVal);
			return retVal;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Returns all known banks as an <see cref="IEnumerable{T}"/> of <see cref="BankDefinition"/> instances.
		/// </summary>
		public static IEnumerable<BankDefinition> All { get { return _Banks.Values; } }

		#endregion

	}

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