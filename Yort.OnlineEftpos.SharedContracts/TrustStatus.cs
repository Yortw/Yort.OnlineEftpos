using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Represents the status of a trust relationship for Online EFTPOS.
	/// </summary>
	/// <seealso cref="TrustStatus.Active"/>
	/// <seealso cref="TrustStatus.Cancelled"/>
	public class TrustStatus : IEqualityComparer<TrustStatus>
	{
		#region Instance Implementation

		private string _Name;
		private TrustStatus(string name)
		{
			_Name = name;
		}

		/// <summary>
		/// Converts a <see cref="TrustStatus"/> to it's name/string representation.
		/// </summary>
		/// <param name="status">The status to convert.</param>
		public static implicit operator String(TrustStatus status)
		{
			return status?._Name;
		}

		/// <summary>
		/// Performs a case insensitive conversion of a string ('active' or 'cancelled') to either <see cref="TrustStatus.Active"/> or <see cref="TrustStatus.Cancelled"/>.
		/// </summary>
		/// <remarks>
		/// <para>Unrecognised strings will be converted to null.</para>
		/// </remarks>
		/// <param name="name">The string to convert.</param>
		public static implicit operator TrustStatus(string name)
		{
			if (name == null) return null;

			if (String.Equals(name, TrustStatus.Active._Name, StringComparison.OrdinalIgnoreCase))
				return TrustStatus.Active;
			else if (String.Equals(name, TrustStatus.Cancelled._Name, StringComparison.OrdinalIgnoreCase))
				return TrustStatus.Cancelled;

			return null;
		}

		/// <summary>
		/// Returns the hashcode for this item.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return _Name.GetHashCode();
		}

		/// <summary>
		/// Returns the name of the item, i.e Active, Cancelled etc.
		/// </summary>
		/// <returns>A string containing the name of this status.</returns>
		public override string ToString()
		{
			return _Name;
		}

		/// <summary>
		/// Compares two <see cref="TrustStatus"/> instances and returns true if they represent the same status.
		/// </summary>
		/// <param name="obj">A <see cref="TrustStatus"/> to compare to.</param>
		/// <returns>True if <paramref name="obj"/> is a <see cref="TrustStatus"/> instance representing the same state as this instance.</returns>
		public override bool Equals(object obj)
		{
			if (!(obj is TrustStatus otherTs))
				return false;

			return StringComparer.OrdinalIgnoreCase.Equals(otherTs._Name, _Name);
		}

		/// <summary>
		/// Compares two <see cref="TrustStatus"/> instances and returns true if they represent the same status.
		/// </summary>
		/// <param name="other">A <see cref="TrustStatus"/> to compare to.</param>
		/// <returns>True if <paramref name="other"/> is a <see cref="TrustStatus"/> instance representing the same state as this instance.</returns>
		public bool Equals(TrustStatus other)
		{
			if (other == null) return false;

			return StringComparer.OrdinalIgnoreCase.Equals(other._Name, _Name);
		}

		/// <summary>
		/// Compares two <see cref="TrustStatus"/> instances and returns true if they represent the same status, or are both null.
		/// </summary>
		/// <param name="x">The first status to compare.</param>
		/// <param name="y">The second status to compare.</param>
		/// <returns>True if both instances are null or represent the same status, otherwise false.</returns>

		public bool Equals(TrustStatus x, TrustStatus y)
		{
			if (x == null && y == null) return true;
			if (x == null || y == null) return false;

			return x.Equals(y);
		}

		/// <summary>
		/// Returns the hash code for the given <see cref="TrustStatus"/> instance.
		/// </summary>
		/// <param name="obj">A <see cref="TrustStatus"/> instance to return the hash code for.</param>
		/// <returns>An integer containnig a hash code.</returns>
		public int GetHashCode(TrustStatus obj)
		{
			if (obj == null) return 0;

			return obj.GetHashCode();
		}

		#endregion

		#region Static Implementation

		private static TrustStatus s_Active;
		private static TrustStatus s_Cancelled;
		private static TrustStatus s_Pending;

		/// <summary>
		/// Returns a <see cref="TrustStatus"/> instance indicating that a trust relationship exists and is active.
		/// </summary>
		public static TrustStatus Active
		{
			get { return s_Active ?? (s_Active = new TrustStatus("ACTIVE")); }
		}

		/// <summary>
		/// Returns a <see cref="TrustStatus"/> instance indicating that a trust relationship has been cancelled.
		/// </summary>
		public static TrustStatus Cancelled
		{
			get { return s_Cancelled ?? (s_Cancelled = new TrustStatus("CANCELLED")); ; }
		}

		/// <summary>
		/// Returns a <see cref="TrustStatus"/> instance indicating that a trust relationship has been requested but not yet approved.
		/// </summary>
		public static TrustStatus Pending
		{
			get { return s_Pending ?? (s_Pending = new TrustStatus("PENDING")); ; }
		}

		#endregion

	}
}
