using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Represents details of trust/autopay relationship to be deleted via <see cref="IOnlineEftposClient.DeleteTrust(OnlineEftposDeleteTrustOptions)"/>.
	/// </summary>
	public class OnlineEftposDeleteTrustOptions
	{
		#region Public Properties

		/// <summary>
		/// The unique id of the trust relationship to delete.
		/// </summary>
		public string TrustId { get; set; }

		#endregion

		#region Public Methods

		/// <summary>
		/// Throws if the details are invalid.
		/// </summary>
		/// <remarks>
		/// <para>Throws if any of the properties are null, or EnsureValid method on any of the property values throws.</para>
		/// </remarks>
		/// <exception cref="OnlineEftposInvalidDataException">Thrown if the values of this instance are invalid.</exception>
		public void EnsureValid()
		{
			try
			{
				TrustId.GuardNullEmptyOrWhitespace(nameof(TrustId));
			}
			catch (ArgumentException ae)
			{
				throw new OnlineEftposInvalidDataException(ae);
			}
		}

		#endregion

	}
}
