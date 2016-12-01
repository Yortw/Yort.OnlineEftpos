using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yort.OnlineEftpos.Net40.Tests
{
	[TestClass]
	public class PayerIdTests
	{

		#region IsValidPhoneNumber Tests

		[TestMethod]
		public void PayerId_IsValidPhoneNumber_ValidatesNZPhoneNumber()
		{
			Assert.AreEqual(true, PayerId.IsPhoneNumberValidId("0215551234"));
		}

		[TestMethod]
		public void PayerId_IsValidPhoneNumber_ValidatesInternationalPhoneNumber()
		{
			Assert.AreEqual(true, PayerId.IsPhoneNumberValidId("+64215551234"));
		}

		[TestMethod]
		public void PayerId_IsValidPhoneNumber_TextIsInvalid()
		{
			Assert.AreEqual(false, PayerId.IsPhoneNumberValidId("Not a phone number"));
		}

		[TestMethod]
		public void PayerId_IsValidPhoneNumber_FormattedPhoneNumberIsInvalid()
		{
			Assert.AreEqual(false, PayerId.IsPhoneNumberValidId("(021)-555-1234"));
		}

		[TestMethod]
		public void PayerId_IsValidPhoneNumber_NullIsInvalid()
		{
			Assert.AreEqual(false, PayerId.IsPhoneNumberValidId(null));
		}

		[TestMethod]
		public void PayerId_IsValidPhoneNumber_EmptyStringIsInvalid()
		{
			Assert.AreEqual(false, PayerId.IsPhoneNumberValidId(String.Empty));
		}

		[TestMethod]
		public void PayerId_IsValidPhoneNumber_WhitespaceOnlyIsInvalid()
		{
			Assert.AreEqual(false, PayerId.IsPhoneNumberValidId("   "));
		}

		[TestMethod]
		public void PayerId_IsValidPhoneNumber_TooShortNumberInvalid()
		{
			Assert.AreEqual(false, PayerId.IsPhoneNumberValidId("123"));
		}

		[TestMethod]
		public void PayerId_IsValidPhoneNumber_TooLongNumberInvalid()
		{
			Assert.AreEqual(false, PayerId.IsPhoneNumberValidId("123456789012345"));
		}

		[TestMethod]
		public void PayerId_IsValidPhoneNumber_TooLongNumberWithInternationalPrefixInvalid()
		{
			Assert.AreEqual(false, PayerId.IsPhoneNumberValidId("+123456789012345"));
		}

		[TestMethod]
		public void PayerId_IsValidPhoneNumber_MaxLengthNumberWithInternationalPrefixIsValid()
		{
			Assert.AreEqual(true, PayerId.IsPhoneNumberValidId("+12345678901234"));
		}

		#endregion

		#region FromPhoneNumber Tests

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PayerId_FromPhoneNumber_ThrowsOnNull()
		{
			PayerId.FromPhoneNumber(null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void PayerId_FromPhoneNumber_ThrowsOnEmptyString()
		{
			PayerId.FromPhoneNumber(String.Empty);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void PayerId_FromPhoneNumber_ThrowsOnWhitespaceOnly()
		{
			PayerId.FromPhoneNumber("    ");
		}

		[TestMethod]
		public void PayerId_FromPhoneNumber_RemovesFormatting()
		{
			Assert.AreEqual("0215551234", PayerId.FromPhoneNumber("(021)-555-1234"));
		}

		[TestMethod]
		public void PayerId_FromPhoneNumber_ValidNumberUnchanged()
		{
			Assert.AreEqual("0215551234", PayerId.FromPhoneNumber("0215551234"));
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void PayerId_FromPhoneNumber_ThrowsOnInvalidPhoneNumber()
		{
			Assert.AreEqual("0215551234", PayerId.FromPhoneNumber("Not a phone number"));
		}

		#endregion

	}
}