using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Provides methods for validation and normalising a Co-operative Bank customer id.
	/// </summary>
	public class CooperativeBankCustomerIdPayerId : IPayerIdType
	{

		#region Fields

		private static readonly char[] IgnoredChars = new char[] { '-', '.' };

		#endregion

		#region Public Methods

		/// <summary>
		/// Returns the string "CUSTOMERID" (without the quotes).
		/// </summary>
		public string Name { get { return "CUSTOMERID"; } }

		/// <summary>
		/// Returns the string "Customer ID" without the quotes.
		/// </summary>
		public string DisplayName { get { return "Customer ID"; } }

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
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification="Argument is validated, but code analysis doesn't recognise this due to extension method used.")]
		public string Normalize(string userInput)
		{
			userInput.GuardNullEmptyOrWhitespace(nameof(userInput));

			var sb = new StringBuilder(userInput.Length);
			var chars = userInput.ToCharArray();
			for (int cnt = 0; cnt < chars.Length; cnt++)
			{
				var thisChar = chars[cnt];
				if (Char.IsWhiteSpace(thisChar) || IgnoredChars.Contains(thisChar)) continue;

				if (Char.IsDigit(thisChar))
					sb.Append(thisChar);
				else
					throw new OnlineEftposInvalidDataException(new ArgumentException($"Could not convert value to payer id. Value is not a valid or allowed {this.Name} payer id.", nameof(userInput)));
			}

			return sb.ToString();
		}

		/// <summary>
		/// Returns true if the specified strings matches the pattern for a Co-operative Bank customer id.
		/// </summary>
		/// <param name="value">A string containing the customer id to check.</param>
		/// <remarks>
		/// <para>Ensures the value is exactly 7 digits long and starts with a character from 1-9 (inclusive).</para>
		/// </remarks>
		/// <returns>True if the string matches the pattern for a Co-operative Bank customer id.</returns>
		public bool IsValid(string value)
		{
			if (String.IsNullOrWhiteSpace(value)) return false;

			if (value.Length != 7) return false;
			var firstChar = value.Substring(0, 1)[0];
			if (firstChar == '0'|| !Char.IsDigit(firstChar)) return false;

			return true;
		}

		#endregion

	}
}