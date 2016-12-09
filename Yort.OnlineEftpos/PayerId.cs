using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Provides methods for validation and normalising payer ids.
	/// </summary>
	public static class PayerId
	{
		/// <summary>
		/// Attempts to normalise a string containing a phone number to a form that can be used as a payer id.
		/// </summary>
		/// <param name="phoneNumber">A string containing a phone number.</param>
		/// <returns>A new string containing the normalised form of the phone number.</returns>
		/// <remarks>
		/// <para>This method will remove or ignore any invalid characters in the string provided and return the rules. The result is validated via the <see cref="IsPhoneNumberValidId(string)"/> method before being returned, and if invalid an exception is thrown.</para>
		/// </remarks>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="phoneNumber"/> is null.</exception>
		/// <exception cref="ArgumentException">Thrown if <paramref name="phoneNumber"/> is an empty string or contains only whitespace characters.</exception>
		/// <exception cref="OnlineEftposInvalidDataException">Thrown if the converted <paramref name="phoneNumber"/> value does not pass validation by the <see cref="IsPhoneNumberValidId(string)"/> method.</exception>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification="Argument is validated, but code analysis doesn't recognise this due to extension method used.")]
		public static string FromPhoneNumber(string phoneNumber)
		{
			ExceptionHelper.ThrowYoureDoingItWrong();
			return null;
		}

		/// <summary>
		/// Returns true if the specified strings matches the pattern for a phone number used as a payer id.
		/// </summary>
		/// <param name="phoneNumber">A string containing the phone number to check.</param>
		/// <returns>True if the string matches the pattern for a phone number that can be used as a payer id.</returns>
		public static bool IsPhoneNumberValidId(string phoneNumber)
		{
			ExceptionHelper.ThrowYoureDoingItWrong();
			return false;
		}
	}
}