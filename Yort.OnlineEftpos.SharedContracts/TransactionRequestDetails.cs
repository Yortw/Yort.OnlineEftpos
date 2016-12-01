using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Linq;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Represents transaction details common to all transaction types.
	/// </summary>
	public class TransactionRequestDetails 
	{
		/// <summary>
		/// Sets or returns the user agent string of the client used to start the transaction. Required.
		/// </summary>
		/// <remarks>
		/// <para>Maximum length is 8192 characters.</para>
		/// </remarks>
		[JsonProperty("userAgent")]
		public string UserAgent { get; set; }

		/// <summary>
		/// Sets or returns the IP address of the client used to start the transaction. Required.
		/// </summary>
		/// <remarks>
		/// <para>IPv4 address in dot notation, or IPv6 in colon notation.</para>
		/// </remarks>
		[JsonProperty("userIpAddress")]
		public string UserIPAddress { get; set; }

		/// <summary>
		/// Throws if the details are invalid.
		/// </summary>
		/// <remarks>
		/// <para>Throws if any of the properties are null, empty string or whitespace. Also thrown if the <see cref="UserAgent"/> value is greater than 8192 characters. Also throws if the <see cref="UserIPAddress"/> is not an IPv4 or 6 address, or is specified in an unsupported notation.</para>
		/// </remarks>
		/// <exception cref="OnlineEftposInvalidDataException">Thrown if the values of this instance are invalid.</exception>
		public virtual void EnsureValid()
		{
			try
			{
				UserAgent.GuardNullEmptyOrWhitespace(nameof(UserAgent));
				UserIPAddress.GuardNullEmptyOrWhitespace(nameof(UserIPAddress));

				UserAgent.GuardMaxLength(8192, nameof(UserAgent));

				ValidateIPAddress(UserIPAddress);
			}
			catch (ArgumentException ae)
			{
				throw new OnlineEftposInvalidDataException(ae);
			}
		}

		private static void ValidateIPAddress(string userIpAddress)
		{
			var isValid = true;

#if SUPPORTS_IPADDRESSTYPE
			IPAddress address = null;
			if (!IPAddress.TryParse(userIpAddress, out address))
				isValid = false;

			if (isValid)
			{
				if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
				{
					if (userIpAddress.Count((c) => c == '.') != 3)
						isValid = false;
				}
				else if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
				{
					if (!userIpAddress.Contains(":"))
						isValid = false;
				}
				else
					isValid = false;
			}
#else
			isValid = ContainsOnlyNumbersAndPeriods(userIpAddress) || ContainsOnlyAlphaNumbersAndColons(userIpAddress);
#endif

			if (!isValid)
				throw new OnlineEftposInvalidDataException(new ArgumentException("Must provide a valid IP Address. IP Address must be v4 in dot notation or v6 in colon notation."));
		}

#if !SUPPORTS_IPADDRESSTYPE

		private static bool ContainsOnlyNumbersAndPeriods(string value)
		{
			return ContainsOnlyAllowedCharacters(value, "1234567890.");
		}

		private static bool ContainsOnlyAlphaNumbersAndColons(string userIpAddress)
		{
			return ContainsOnlyAllowedCharacters(userIpAddress, "1234567890ABCDEFabcdef:");
		}

		private static bool ContainsOnlyAllowedCharacters(string value, string allowedChars)
		{
			foreach (var c in value)
			{
				if (!allowedChars.ToCharArray().Contains(c)) return false;
			}
			return true;
		}
#endif
	}
}