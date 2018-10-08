using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yort.OnlineEftpos.Net40.Tests
{
	[TestClass]
	public class CooperativeBankCustomerIdPayerIdTests
	{

		#region Constants

		private readonly string[] ValidPayerIds = new string[]
		{
			"1234567",
			"5467891",
			"9012439"
		};

		private readonly string[] InvalidPayerIds = new string[]
		{
			"021-012-345",
			"0123456",
			"A123456",
			"123 456",
			"1",
			null,
			String.Empty,
			"12345678",
			"    "
		};

		#endregion
		
		#region IsValid Tests

		[TestMethod]
		public void PayerId_IsValid_ValidatesCorrectPayerId()
		{
			var payerIdType = new CooperativeBankCustomerIdPayerId();
			foreach (var number in ValidPayerIds)
			{
				Assert.AreEqual(true, payerIdType.IsValid(number), $"Failed to validate co-operative payer id {number}");
			}
		}

		[TestMethod]
		public void PayerId_IsValid_ValidatesInvalidCustomerIds()
		{
			var payerIdType = new CooperativeBankCustomerIdPayerId();
			foreach (var number in InvalidPayerIds)
			{
				Assert.AreEqual(false, payerIdType.IsValid(number), $"Failed to validate co-operative payer id {number}");
			}
		}
		
		[TestMethod]
		public void PayerId_IsValid_NullIsInvalid()
		{
			var payerIdType = new CooperativeBankCustomerIdPayerId();
			Assert.AreEqual(false, payerIdType.IsValid(null));
		}

		[TestMethod]
		public void PayerId_IsValid_EmptyStringIsInvalid()
		{
			var payerIdType = new CooperativeBankCustomerIdPayerId();
			Assert.AreEqual(false, payerIdType.IsValid(String.Empty));
		}

		[TestMethod]
		public void PayerId_IsValid_WhitespaceOnlyIsInvalid()
		{
			var payerIdType = new CooperativeBankCustomerIdPayerId();
			Assert.AreEqual(false, payerIdType.IsValid("   "));
		}

		[TestMethod]
		public void PayerId_IsValid_TooShortNumberInvalid()
		{
			var payerIdType = new CooperativeBankCustomerIdPayerId();
			Assert.AreEqual(false, payerIdType.IsValid("123"));
		}

		[TestMethod]
		public void PayerId_IsValid_TooLongNumberInvalid()
		{
			var payerIdType = new CooperativeBankCustomerIdPayerId();
			Assert.AreEqual(false, payerIdType.IsValid("123456789012345"));
		}

		#endregion

		#region Normalize Tests

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PayerId_Normalize_ThrowsOnNull()
		{
			var payerIdType = new CooperativeBankCustomerIdPayerId();
			payerIdType.Normalize(null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void PayerId_Normalize_ThrowsOnEmptyString()
		{
			var payerIdType = new CooperativeBankCustomerIdPayerId();
			payerIdType.Normalize(String.Empty);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void PayerId_Normalize_ThrowsOnWhitespaceOnly()
		{
			var payerIdType = new CooperativeBankCustomerIdPayerId();
			payerIdType.Normalize("    ");
		}

		[TestMethod]
		public void PayerId_Normalize_RemovesFormatting()
		{
			var payerIdType = new CooperativeBankCustomerIdPayerId();
			Assert.AreEqual("1234567", payerIdType.Normalize("123 456 7"));
			Assert.AreEqual("1234567", payerIdType.Normalize("123-456-7"));
		}

		[TestMethod]
		public void PayerId_Normalize_ValidNumberUnchanged()
		{
			var payerIdType = new CooperativeBankCustomerIdPayerId();
			Assert.AreEqual("1234567", payerIdType.Normalize("1234567"));
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void PayerId_Normalize_ThrowsOnInvalidCustomerId()
		{
			var payerIdType = new CooperativeBankCustomerIdPayerId();
			payerIdType.Normalize("Not a payer id");
		}

		#endregion

	}
}