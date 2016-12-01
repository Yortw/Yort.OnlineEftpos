using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yort.OnlineEftpos.Net40.Tests
{
	[TestClass]
	public class PaymentDetailsTests
	{

		[TestMethod]
		public void PaymentDetails_ValidatesWithCorrectValues()
		{
			var paymentDetails = GetPaymentDetails();

			paymentDetails.EnsureValid();
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void PaymentDetails_ThrowsOnNullUserIpAddress()
		{
			var paymentDetails = GetPaymentDetails();
			paymentDetails.UserIPAddress = null;

			paymentDetails.EnsureValid();
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void PaymentDetails_ThrowsOnEmptyUserIpAddress()
		{
			var paymentDetails = GetPaymentDetails();
			paymentDetails.UserIPAddress = String.Empty;

			paymentDetails.EnsureValid();
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void PaymentDetails_ThrowsOnInvalidIpAddress()
		{
			var paymentDetails = GetPaymentDetails();
			paymentDetails.UserIPAddress = "not an ip address";

			paymentDetails.EnsureValid();
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void PaymentDetails_ThrowsOnPartialIp4Address()
		{
			var paymentDetails = GetPaymentDetails();
			paymentDetails.UserIPAddress = "192.168";

			paymentDetails.EnsureValid();
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void PaymentDetails_ThrowsOnInvalidIp4Notation()
		{
			var paymentDetails = GetPaymentDetails();
			var address = System.Net.IPAddress.Parse("192.168.1.10");
#pragma warning disable CS0618 // Type or member is obsolete
			paymentDetails.UserIPAddress = address.Address.ToString();
#pragma warning restore CS0618 // Type or member is obsolete

			paymentDetails.EnsureValid();
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void PaymentDetails_ThrowsOnInvalidIp6Notation()
		{
			var paymentDetails = GetPaymentDetails();
			paymentDetails.UserIPAddress = "FE80.0.0.0.2AA.FF.FE9A.4CA2";

			paymentDetails.EnsureValid();
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void PaymentDetails_ThrowsOnZeroAmount()
		{
			var paymentDetails = GetPaymentDetails();
			paymentDetails.Amount = 0;

			paymentDetails.EnsureValid();
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void PaymentDetails_ThrowsOnNegativeAmount()
		{
			var paymentDetails = GetPaymentDetails();
			paymentDetails.Amount = -1;

			paymentDetails.EnsureValid();
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void PaymentDetails_ThrowsOnTooLongCurrency()
		{
			var paymentDetails = GetPaymentDetails();
			paymentDetails.Currency = "notacurrency";

			paymentDetails.EnsureValid();
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void PaymentDetails_ThrowsOnLowerCaseCurrency()
		{
			var paymentDetails = GetPaymentDetails();
			paymentDetails.Currency = "nzd";

			paymentDetails.EnsureValid();
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void PaymentDetails_ThrowsOnTooShortCurrency()
		{
			var paymentDetails = GetPaymentDetails();
			paymentDetails.Currency = "nz";

			paymentDetails.EnsureValid();
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void PaymentDetails_ThrowsOnCurrencyWithNonAlphaCharacter()
		{
			var paymentDetails = GetPaymentDetails();
			paymentDetails.Currency = "N Z";

			paymentDetails.EnsureValid();
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void PaymentDetails_ThrowsOnTooLongDescription()
		{
			var paymentDetails = GetPaymentDetails();
			paymentDetails.Description = "1234567890123";

			paymentDetails.EnsureValid();
		}

		private PaymentDetails GetPaymentDetails()
		{
			return new PaymentDetails()
			{
				Amount = 10000,
				Currency = "NZD",
				Description = "Test pmt",
				OrderId = "145",
				UserAgent = "Yort.OnlineEftpos (Test)",
				UserIPAddress = "192.168.1.10"
			};
		}
	}
}