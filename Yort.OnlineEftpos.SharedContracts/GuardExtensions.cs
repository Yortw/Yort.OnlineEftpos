using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	internal static class GuardExtensions
	{

		public static void GuardNull(this object value, string argumentName)
		{
			if (value == null) throw new ArgumentNullException(argumentName);
		}

		public static void GuardOnlyWhiteSpace(this string value, string argumentName)
		{
			if (String.IsNullOrWhiteSpace(value)) throw new ArgumentException($"{argumentName} cannot be only 'whitespace'.", argumentName);
		}

		public static void GuardNullEmptyOrWhitespace(this string value, string argumentName)
		{
			value.GuardNull(argumentName);
			value.GuardOnlyWhiteSpace(argumentName);
		}

		public static void GuardMaxLength(this string value, int maxLength, string argumentName)
		{
			if (!String.IsNullOrEmpty(value) && value.Length > maxLength)
				throw new ArgumentException($"{argumentName} cannot be longer than {maxLength} characters.");
		}


		public static void GuardMinValue(this int value, int minValue, string argumentName)
		{
			if (value < minValue)
				throw new ArgumentException($"{argumentName} cannot be less than {minValue}.");
		}

	}
}