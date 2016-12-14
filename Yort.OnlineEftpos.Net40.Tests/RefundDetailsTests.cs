using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yort.OnlineEftpos.Net40.Tests
{
	[TestClass]
	public class RefundDetailsTests
	{

		[TestMethod]
		public void RefundDetails_ValidatesWithCorrectValues()
		{
			var refundDetails = GetRefundDetails();
			refundDetails.EnsureValid();
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void RefundDetails_ThrowsOnNullOriginalPaymentId()
		{
			var refundDetails = GetRefundDetails();
			refundDetails.OriginalPaymentId = null;

			refundDetails.EnsureValid();
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void RefundDetails_ThrowsOnEmptyOriginalPaymentId()
		{
			var refundDetails = GetRefundDetails();
			refundDetails.OriginalPaymentId = String.Empty;

			refundDetails.EnsureValid();
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void RefundDetails_ThrowsOnWhitespaceOnlyOriginalPaymentId()
		{
			var refundDetails = GetRefundDetails();
			refundDetails.OriginalPaymentId = "    ";

			refundDetails.EnsureValid();
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void RefundDetails_ThrowsOnZeroAmount()
		{
			var refundDetails = GetRefundDetails();
			refundDetails.RefundAmount = 0;

			refundDetails.EnsureValid();
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void RefundDetails_ThrowsOnNegativeAmount()
		{
			var refundDetails = GetRefundDetails();
			refundDetails.RefundAmount = -1;

			refundDetails.EnsureValid();
		}

		[TestMethod]
		public void RefundDetails_AllowsNullReason()
		{
			var refundDetails = GetRefundDetails();
			refundDetails.RefundReason = null;

			refundDetails.EnsureValid();
		}

		[TestMethod]
		public void RefundDetails_AllowsEmptyReason()
		{
			var refundDetails = GetRefundDetails();
			refundDetails.RefundReason = String.Empty;

			refundDetails.EnsureValid();
		}

		[TestMethod]
		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		public void RefundDetails_ThrowsOnTooLongReason()
		{
			var refundDetails = GetRefundDetails();
			refundDetails.RefundReason = new string('R', 513);

			refundDetails.EnsureValid();
		}

		private RefundDetails GetRefundDetails()
		{
			return new RefundDetails()
			{
				RefundId = System.Guid.NewGuid().ToString(),
				OriginalPaymentId = "381a08c8-9189-4995-b07b-6c3821f70e35", 
				RefundAmount = 1000,
				RefundReason = "Defective goods",
				UserAgent = "Yort.OnlineEftpos (Test)",
				UserIPAddress = "192.168.1.10"
			};
		}
	}
}