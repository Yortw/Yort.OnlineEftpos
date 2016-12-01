using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Represents a transaction status returned by the API.
	/// </summary>
	public sealed class TransactionStatus : IEquatable<TransactionStatus>, IEquatable<string>
	{

		private readonly string _Name;
		private readonly bool _IsTerminal;
		private readonly string _Description;

		internal TransactionStatus(string name, bool isTerminal, string description)
		{
			_Name = name;
			_IsTerminal = isTerminal;
			_Description = description;
		}

		/// <summary>
		/// The name of the status.
		/// </summary>
		public string Name
		{
			get
			{
				return _Name;
			}
		}

		/// <summary>
		/// True if a transacton in this status can never change status again.
		/// </summary>
		public bool IsTerminal
		{
			get
			{
				return _IsTerminal;
			}
		}

		/// <summary>
		/// A brief description of the status.
		/// </summary>
		public string Description
		{
			get
			{
				return _Description;
			}
		}

		/// <summary>
		/// Returns true if this status has the same name as the compared status.
		/// </summary>
		/// <param name="other">The <see cref="TransactionStatus"/> to compare to.</param>
		/// <returns>A boolean, true if the two statuses are equivalent.</returns>
		public bool Equals(TransactionStatus other)
		{
			if (other == null) return false;
			if (other == this) return true;

			return String.Compare(this.Name, other.Name, StringComparison.OrdinalIgnoreCase) == 0;
		}

		/// <summary>
		/// Returns true if this status has the same name as the compared status.
		/// </summary>
		/// <param name="obj">The <see cref="TransactionStatus"/> to compare to.</param>
		/// <returns>A boolean, true if the two statuses are equivalent.</returns>
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TransactionStatus);
		}

		/// <summary>
		/// Returns the hash code for this status.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		/// <summary>
		/// Returns a boolean indicating if the name of this status matches (case insensitive comparison) the value of the string to compare to.
		/// </summary>
		/// <param name="other">A string containing the name to compare.</param>
		/// <returns>True if the name of this status matches the value of the <paramref name="other"/> argument, ignoring case.</returns>
		public bool Equals(string other)
		{
			if (String.IsNullOrEmpty(other)) return false;

			return String.Compare(this.Name, other, StringComparison.OrdinalIgnoreCase) == 0;
		}

		/// <summary>
		/// Provides comparisons against strings
		/// </summary>
		/// <remarks>Comparisons are made against the <see cref="Name"/> property.</remarks>
		/// <param name="status">The <see cref="TransactionStatus"/> being compared.</param>
		/// <param name="comparisonValue">The string being compared.</param>
		/// <returns>True if the string is a case insensitive match to the <see cref="TransactionStatus.Name"/> value.</returns>
		public static bool operator ==(TransactionStatus status, string comparisonValue)
		{
			// If both are null, or both are same instance, return true.
			if (((object)status == null) || ((object)comparisonValue == null)) return false;

			// Return true if the fields match:
			return String.Compare(status.Name, comparisonValue, StringComparison.OrdinalIgnoreCase) == 0;
		}

		/// <summary>
		/// Provides comparisons against strings
		/// </summary>
		/// <remarks>Comparisons are made against the <see cref="Name"/> property.</remarks>
		/// <param name="status">The <see cref="TransactionStatus"/> being compared.</param>
		/// <param name="comparisonValue">The string being compared.</param>
		/// <returns>True if the string is NOT a case insensitive match to the <see cref="TransactionStatus.Name"/> value.</returns>
		public static bool operator !=(TransactionStatus status, string comparisonValue)
		{
			if (((object)status == null) || ((object)comparisonValue == null)) return false;

			return String.Compare(status.Name, comparisonValue, StringComparison.OrdinalIgnoreCase) != 0;
		}

		/// <summary>
		/// Provides comparisons against strings
		/// </summary>
		/// <remarks>Comparisons are made against the <see cref="Name"/> property.</remarks>
		/// <param name="status">The <see cref="TransactionStatus"/> being compared.</param>
		/// <param name="comparisonValue">The string being compared.</param>
		/// <returns>True if the string is a case insensitive match to the <see cref="TransactionStatus.Name"/> value.</returns>
		public static bool operator ==(string comparisonValue, TransactionStatus status)
		{
			// If both are null, or both are same instance, return true.
			if (((object)comparisonValue == null) || ((object)status == null)) return false;

			// Return true if the fields match:
			return String.Compare(comparisonValue, status.Name, StringComparison.OrdinalIgnoreCase) == 0;
		}

		/// <summary>
		/// Provides comparisons against strings
		/// </summary>
		/// <remarks>Comparisons are made against the <see cref="Name"/> property.</remarks>
		/// <param name="status">The <see cref="TransactionStatus"/> being compared.</param>
		/// <param name="comparisonValue">The string being compared.</param>
		/// <returns>True if the string is NOT a case insensitive match to the <see cref="TransactionStatus.Name"/> value.</returns>
		public static bool operator !=(string comparisonValue, TransactionStatus status)
		{
			if (((object)comparisonValue == null) || ((object)status == null)) return false;

			return String.Compare(comparisonValue, status.Name, StringComparison.OrdinalIgnoreCase) != 0;
		}

	}
}