using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yort.OnlineEftpos.Net40.Tests
{
	[TestClass]
	public class BankDetailsTests
	{

		[TestMethod]
		public void BankDetails_CorrectDetails_PassesValidation()
		{
			var bankDetails = new BankDetails()
			{
				BankId = "ASB",
				PayerIdType = "MOBILE",
				PayerId = "0215551234"
			};
			bankDetails.EnsureValid();
		}

		#region PayerId Validation Tests

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void BankDetails_BlankPayerId_Invalid()
		{
			var bankDetails = new BankDetails()
			{
				BankId = "ASB",
				PayerIdType = "MOBILE",
				PayerId = String.Empty
			};
			bankDetails.EnsureValid();
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void BankDetails_NullPayerId_Invalid()
		{
			var bankDetails = new BankDetails()
			{
				BankId = "ASB",
				PayerIdType = "MOBILE",
				PayerId = null
			};
			bankDetails.EnsureValid();
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void BankDetails_WhitespacePayerId_Invalid()
		{
			var bankDetails = new BankDetails()
			{
				BankId = "ASB",
				PayerIdType = "MOBILE",
				PayerId = "     "
			};
			bankDetails.EnsureValid();
		}

		#endregion

		#region BankId Validation Tests

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void BankDetails_BankId_Invalid()
		{
			var bankDetails = new BankDetails()
			{
				BankId = String.Empty,
				PayerIdType = "MOBILE",
				PayerId = "0215551234"
			};
			bankDetails.EnsureValid();
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void BankDetails_NullBankId_Invalid()
		{
			var bankDetails = new BankDetails()
			{
				BankId = null,
				PayerIdType = "MOBILE",
				PayerId = "0215551234"
			};
			bankDetails.EnsureValid();
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void BankDetails_WhitespaceBankId_Invalid()
		{
			var bankDetails = new BankDetails()
			{
				BankId = "    ",
				PayerIdType = "MOBILE",
				PayerId = "0215551234"
			};
			bankDetails.EnsureValid();
		}

		#endregion

		#region PayerIdType Validation Tests

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void BankDetails_BlankPayerIdType_Invalid()
		{
			var bankDetails = new BankDetails()
			{
				BankId = "ASB",
				PayerIdType = String.Empty,
				PayerId = "0215551234"
			};
			bankDetails.EnsureValid();
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void BankDetails_NullPayerIdType_Invalid()
		{
			var bankDetails = new BankDetails()
			{
				BankId = "ASB",
				PayerIdType = null,
				PayerId = "0215551234"
			};
			bankDetails.EnsureValid();
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void BankDetails_WhitespacePayerIdType_Invalid()
		{
			var bankDetails = new BankDetails()
			{
				BankId = "ASB",
				PayerIdType = "    ",
				PayerId = "0215551234"
			};
			bankDetails.EnsureValid();
		}

		#endregion

	}
}