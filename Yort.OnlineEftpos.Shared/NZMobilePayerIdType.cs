using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Provides methods for validation and normalising NZ mobile phone number based payer ids.
	/// </summary>
	public class NZMobilePayerIdType : IPayerIdType
	{

		#region Fields

		private static readonly char[] IgnoredChars = new char[] { '(', ')', '-', '+' };
		private static System.Text.RegularExpressions.Regex _PhoneNumberValidationRegex;

		#endregion

		#region Public Methods

		/// <summary>
		/// Returns the string "MOBILE" (without the quotes).
		/// </summary>
		public string Name { get { return "MOBILE"; } }

		/// <summary>
		/// Returns the string "NZ Mobile Number" without the quotes.
		/// </summary>
		public string DisplayName
		{
			get { return "NZ Mobile Number"; }
		}

		/// <summary>
		/// Attempts to normalise a string containing a phone number to a form that can be used as a payer id.
		/// </summary>
		/// <param name="userInput">A string containing a phone number.</param>
		/// <returns>A new string containing the normalised form of the phone number.</returns>
		/// <remarks>
		/// <para>This method will remove non-numeric characters commonly used in phone numbers (+ ( ) - etc) as well as whitespace in the string provided. The result is validated via the <see cref="IsValid(string)"/> method before being returned, and if invalid an exception is thrown.</para>
		/// <para>This method will also attempt to convert international numbers (i.e those starting with +642x or 642x) to local numbers.</para>
		/// </remarks>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="userInput"/> is null.</exception>
		/// <exception cref="ArgumentException">Thrown if <paramref name="userInput"/> is an empty string or contains only whitespace characters.</exception>
		/// <exception cref="OnlineEftposInvalidDataException">Thrown if the converted <paramref name="userInput"/> value does not pass validation by the <see cref="IsValid(string)"/> method, or if it contains non-numeric characters that are not commonly used in phone numbers.</exception>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Argument is validated, but code analysis doesn't recognise this due to extension method used.")]
		public string Normalize(string userInput)
		{
			userInput.GuardNullEmptyOrWhitespace(nameof(userInput));

			var sb = new StringBuilder(userInput.Length);
			var chars = userInput.ToCharArray();
			for (var cnt = 0; cnt < chars.Length; cnt++)
			{
				var thisChar = chars[cnt];
				if (Char.IsWhiteSpace(thisChar) || IgnoredChars.Contains(thisChar)) continue;

				if (Char.IsDigit(thisChar))
					sb.Append(thisChar);
				else
					throw new OnlineEftposInvalidDataException(new ArgumentException("Could not convert value to payer id. Value is not a valid or allowed phone number.", nameof(userInput)));
			}

			var retVal = sb.ToString();
			retVal = ReplacePrefix(retVal, "+642", "02");
			retVal = ReplacePrefix(retVal, "642", "02");

			if (!IsValid(retVal))
				throw new OnlineEftposInvalidDataException(new ArgumentException("Could not convert value to payer id. Value is not a valid or allowed phone number.", nameof(userInput)));

			return retVal;
		}

		/// <summary>
		/// Returns true if the specified strings matches the pattern for a phone number used as a payer id.
		/// </summary>
		/// <param name="value">A string containing the phone number to check.</param>
		/// <returns>True if the string matches the pattern for a phone number that can be used as a payer id.</returns>
		public bool IsValid(string value)
		{
			if (String.IsNullOrWhiteSpace(value)) return false;

			// (0)(2)[012789]\d{6,11}

			/*
			 Begins with 020 / 1 / 2 / 7 / 8 / 9 (zero two …).
				023, 024, 025, 026 not allowed.
				Minimum 9 digits, maximum 11 digits.
				Only numeric characters allowed.
			 */

			var regex = GetPhoneNumberValidationRegex();
			return regex.IsMatch(value);
		}

		#endregion

		#region Private Methods
		private static string ReplacePrefix(string data, string prefix, string replacement)
		{
			if (data.StartsWith(prefix) && data.Length > prefix.Length)
				return replacement + data.Substring(prefix.Length);

			return data;
		}

		private static System.Text.RegularExpressions.Regex GetPhoneNumberValidationRegex()
		{
			return _PhoneNumberValidationRegex ??
			(
				_PhoneNumberValidationRegex = new System.Text.RegularExpressions.Regex
					(
						//"[+]{0,1}[0-9]{8,14}",
						"(0)(2)[012789]\\d{6,11}",
#if SUPPORTS_COMPILEDREGEX
						System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.Singleline
#else
						System.Text.RegularExpressions.RegexOptions.Singleline
#endif
					)
			);
		}

		#endregion

	}
}