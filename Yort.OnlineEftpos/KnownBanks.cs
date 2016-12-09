using System;
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
			ExceptionHelper.ThrowYoureDoingItWrong();
			return false;
		}

		/// <summary>
		/// Removes a bank from the list of known banks.
		/// </summary>
		/// <param name="id">The id of the bank defintion to remove.</param>
		/// <returns>True if the item was removed, false if it was not found.</returns>
		/// <exception cref="System.ArgumentException">Thrown if the id of the <paramref name="id"/> reference is null, empty or only whitespace.</exception>
		public static bool RemoveBank(string id)
		{
			ExceptionHelper.ThrowYoureDoingItWrong();
			return false;
		}

		/// <summary>
		/// Returns the bank with the specified <paramref name="id"/>, or null if no bank with that id was found.
		/// </summary>
		/// <param name="id">The unique id of the bank object to retrieve.</param>
		/// <returns>A <see cref="BankDefinition"/> containing details of the known bank, or null if no relevant bank was found.</returns>
		public static BankDefinition GetBank(string id)
		{
			ExceptionHelper.ThrowYoureDoingItWrong();
			return null;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Returns all known banks as an <see cref="IEnumerable{T}"/> of <see cref="BankDefinition"/> instances.
		/// </summary>
		public static IEnumerable<BankDefinition> All
		{
			get
			{
				ExceptionHelper.ThrowYoureDoingItWrong();
				return null;
			}
		}

		#endregion

	}
}