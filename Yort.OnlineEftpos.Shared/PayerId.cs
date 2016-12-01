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
		private static System.Text.RegularExpressions.Regex _PhoneNumberValidationRegex;

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
			phoneNumber.GuardNullEmptyOrWhitespace(nameof(phoneNumber));

			var sb = new StringBuilder(phoneNumber.Length);
			var chars = phoneNumber.ToCharArray();
			for (int cnt = 0; cnt < chars.Length; cnt++)
			{
				var thisChar = chars[cnt];
				if (cnt == 0 && thisChar == '+')
					sb.Append(thisChar);
				else if (Char.IsDigit(thisChar))
					sb.Append(thisChar);
			}

			var retVal = sb.ToString();
			if (!IsPhoneNumberValidId(retVal))
				throw new OnlineEftposInvalidDataException(new ArgumentException("Could not convert value to payer id. Value is not a valid or allowed phone number.", nameof(phoneNumber)));

			return retVal;
		}

		/// <summary>
		/// Returns true if the specified strings matches the pattern for a phone number used as a payer id.
		/// </summary>
		/// <param name="phoneNumber">A string containing the phone number to check.</param>
		/// <returns>True if the string matches the pattern for a phone number that can be used as a payer id.</returns>
		public static bool IsPhoneNumberValidId(string phoneNumber)
		{
			if (String.IsNullOrWhiteSpace(phoneNumber)) return false;

			var regex = GetPhoneNumberValidationRegex();
			return regex.IsMatch(phoneNumber) &&
			(
				phoneNumber.Length <= 14 ||
				phoneNumber.Length == 15 && phoneNumber.StartsWith("+")
			); 
		}

		private static System.Text.RegularExpressions.Regex GetPhoneNumberValidationRegex()
		{
			return _PhoneNumberValidationRegex ??
			(
				_PhoneNumberValidationRegex = new System.Text.RegularExpressions.Regex
					(
						"[+]{0,1}[0-9]{8,14}",
#if SUPPORTS_COMPILEDREGEX
						System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.Singleline
#else
						System.Text.RegularExpressions.RegexOptions.Singleline
#endif
					)
			);
		}
	}
}