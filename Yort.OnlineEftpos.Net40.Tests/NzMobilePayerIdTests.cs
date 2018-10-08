using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yort.OnlineEftpos.Net40.Tests
{
	[TestClass]
	public class NzMobilePayerIdTests
	{

		#region Constants

		private string[] ValidNZMobileNumbers = new string[]
		{
			"0215551234",
			"021012345",
			"0221234567"
		};

		private string[] InvalidNZMobileNumbers = new string[]
		{
			"021-012-345",
			"+64 22 123 4567",
			"026123456",
			"abc",
			"Not a phone number",
			null,
			String.Empty,
			"(021)-555-1234",
			"    ",
			"123",
			"12345678901234567890"
		};

		#endregion
		
		#region IsValid Tests

		[TestMethod]
		public void PayerId_IsValid_ValidatesCorrectNZPhoneNumbers()
		{
			var payerIdType = new NZMobilePayerIdType();
			foreach (var number in ValidNZMobileNumbers)
			{
				Assert.AreEqual(true, payerIdType.IsValid(number), $"Failed to validate number {number}");
			}
		}

		[TestMethod]
		public void PayerId_IsValid_ValidatesInvalidNZPhoneNumbers()
		{
			var payerIdType = new NZMobilePayerIdType();
			foreach (var number in InvalidNZMobileNumbers)
			{
				Assert.AreEqual(false, payerIdType.IsValid(number), $"Failed to validate number {number}");
			}
		}

		[TestMethod]
		public void PayerId_IsValid_DoesNotValidateInternationalPhoneNumber()
		{
			var payerIdType = new NZMobilePayerIdType();
			Assert.AreEqual(false, payerIdType.IsValid("+64215551234"));
		}

		[TestMethod]
		public void PayerId_IsValid_TextIsInvalid()
		{
			var payerIdType = new NZMobilePayerIdType();
			Assert.AreEqual(false, payerIdType.IsValid("Not a phone number"));
		}

		[TestMethod]
		public void PayerId_IsValid_FormattedPhoneNumberIsInvalid()
		{
			var payerIdType = new NZMobilePayerIdType();
			Assert.AreEqual(false, payerIdType.IsValid("(021)-555-1234"));
		}

		[TestMethod]
		public void PayerId_IsValid_NullIsInvalid()
		{
			var payerIdType = new NZMobilePayerIdType();
			Assert.AreEqual(false, payerIdType.IsValid(null));
		}

		[TestMethod]
		public void PayerId_IsValid_EmptyStringIsInvalid()
		{
			var payerIdType = new NZMobilePayerIdType();
			Assert.AreEqual(false, payerIdType.IsValid(String.Empty));
		}

		[TestMethod]
		public void PayerId_IsValid_WhitespaceOnlyIsInvalid()
		{
			var payerIdType = new NZMobilePayerIdType();
			Assert.AreEqual(false, payerIdType.IsValid("   "));
		}

		[TestMethod]
		public void PayerId_IsValid_TooShortNumberInvalid()
		{
			var payerIdType = new NZMobilePayerIdType();
			Assert.AreEqual(false, payerIdType.IsValid("123"));
		}

		[TestMethod]
		public void PayerId_IsValid_TooLongNumberInvalid()
		{
			var payerIdType = new NZMobilePayerIdType();
			Assert.AreEqual(false, payerIdType.IsValid("123456789012345"));
		}

		[TestMethod]
		public void PayerId_IsValid_TooLongNumberWithInternationalPrefixInvalid()
		{
			var payerIdType = new NZMobilePayerIdType();
			Assert.AreEqual(false, payerIdType.IsValid("+123456789012345"));
		}

		#endregion

		#region Normalize Tests

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PayerId_Normalize_ThrowsOnNull()
		{
			var payerIdType = new NZMobilePayerIdType();
			payerIdType.Normalize(null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void PayerId_Normalize_ThrowsOnEmptyString()
		{
			var payerIdType = new NZMobilePayerIdType();
			payerIdType.Normalize(String.Empty);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void PayerId_Normalize_ThrowsOnWhitespaceOnly()
		{
			var payerIdType = new NZMobilePayerIdType();
			payerIdType.Normalize("    ");
		}

		[TestMethod]
		public void PayerId_Normalize_RemovesFormatting()
		{
			var payerIdType = new NZMobilePayerIdType();
			Assert.AreEqual("0215551234", payerIdType.Normalize("(021)-555-1234"));
		}

		[TestMethod]
		public void PayerId_Normalize_ValidNumberUnchanged()
		{
			var payerIdType = new NZMobilePayerIdType();
			Assert.AreEqual("0215551234", payerIdType.Normalize("0215551234"));
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void PayerId_Normalize_ThrowsOnInvalidPhoneNumber()
		{
			var payerIdType = new NZMobilePayerIdType();
			Assert.AreEqual("0215551234", payerIdType.Normalize("Not a phone number"));
		}

		[TestMethod]
		public void PayerId_Normalize_NormalizesInternationalNumberWithPrefix()
		{
			var payerIdType = new NZMobilePayerIdType();
			Assert.AreEqual("0215551234", payerIdType.Normalize("+64215551234"));
		}

		[TestMethod]
		public void PayerId_Normalize_NormalizesInternationalNumberWithoutPrefix()
		{
			var payerIdType = new NZMobilePayerIdType();
			Assert.AreEqual("0215551234", payerIdType.Normalize("64215551234"));
		}

		public void PayerId_Normalize_NormalizesFormattedNumber()
		{
			var payerIdType = new NZMobilePayerIdType();
			Assert.AreEqual("0215551234", payerIdType.Normalize("+64 (21) 555-1234"));
		}

		#endregion

	}
}