using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yort.OnlineEftpos.Net40.Tests
{
	[TestClass]
	public class RequestBuilderTests
	{

		#region Purchase Request Tests

		[TestMethod]
		public void RequestBuilder_PurchaseRequest_BuildsValidRequestWithGoodData()
		{
			var builder = GetBuilder();

			var paymentRequest = builder.CreatePaymentRequest("0215551234", "MOBILE", "ASB", "145", 10.00M);
			paymentRequest.EnsureValid();
		}

		[TestMethod]
		public void RequestBuilder_PurchaseRequest_AmountIsCorrect()
		{
			var builder = GetBuilder();

			var paymentRequest = builder.CreatePaymentRequest("0215551234", "MOBILE", "ASB", "145", 10.00M);
			Assert.AreEqual(1000, paymentRequest.Transaction.Amount);
		}

		[TestMethod]
		public void RequestBuilder_PurchaseRequest_PayerIdIsCorrect()
		{
			var builder = GetBuilder();

			var paymentRequest = builder.CreatePaymentRequest("0215551234", "MOBILE", "ASB", "145", 10.00M);
			Assert.AreEqual("0215551234", paymentRequest.Bank.PayerId);
		}

		[TestMethod]
		public void RequestBuilder_PurchaseRequest_OrderIdIsCorrect()
		{
			var builder = GetBuilder();

			var paymentRequest = builder.CreatePaymentRequest("0215551234", "MOBILE", "ASB", "145", 10.00M);
			Assert.AreEqual("145", paymentRequest.Transaction.OrderId);
		}

		[TestMethod]
		public void RequestBuilder_PurchaseRequest_UsesDefault_UserAgent()
		{
			var builder = GetBuilder();

			var paymentRequest = builder.CreatePaymentRequest("0215551234", "MOBILE", "ASB", "145", 10.00M);
			Assert.AreEqual("MyUserAgent", paymentRequest.Transaction.UserAgent);
		}

		[TestMethod]
		public void RequestBuilder_PurchaseRequest_UsesDefault_UserIP()
		{
			var builder = GetBuilder();

			var paymentRequest = builder.CreatePaymentRequest("0215551234", "MOBILE", "ASB", "145", 10.00M);
			Assert.AreEqual("192.168.1.10", paymentRequest.Transaction.UserIPAddress);
		}

		[TestMethod]
		public void RequestBuilder_PurchaseRequest_UsesDefault_Currency()
		{
			var builder = GetBuilder();

			var paymentRequest = builder.CreatePaymentRequest("0215551234", "MOBILE", "ASB", "145", 10.00M);
			Assert.AreEqual("NZD", paymentRequest.Transaction.Currency);
		}

		[TestMethod]
		public void RequestBuilder_PurchaseRequest_UsesDefault_MerchantId()
		{
			var builder = GetBuilder();

			var paymentRequest = builder.CreatePaymentRequest("0215551234", "MOBILE", "ASB", "145", 10.00M);
			Assert.AreEqual("Merch-1234", paymentRequest.Merchant.MerchantIdCode);
		}

		[TestMethod]
		public void RequestBuilder_PurchaseRequest_UsesDefault_MerchantUrl()
		{
			var builder = GetBuilder();

			var paymentRequest = builder.CreatePaymentRequest("0215551234", "MOBILE", "ASB", "145", 10.00M);
			Assert.AreEqual(new Uri("www.mycoolsite.com", UriKind.Relative), paymentRequest.Merchant.MerchantUrl);
		}

		[TestMethod]
		public void RequestBuilder_PurchaseRequest_BuildsTemplatedCallbackUrl()
		{
			var builder = GetBuilder();

			var paymentRequest = builder.CreatePaymentRequest("0215551234", "MOBILE", "ASB", "145", 10.00M);
			Assert.AreEqual(new Uri("https://www.mycoolsite.com/payments/callback?reference=145"), paymentRequest.Merchant.CallbackUrl);
		}

		[TestMethod]
		public void RequestBuilder_PurchaseRequest_HandlesNullCallbackUrlTemplate()
		{
			var builder = GetBuilder();
			builder.CallbackUrlTemplate = null;

			var paymentRequest = builder.CreatePaymentRequest("0215551234", "MOBILE", "ASB", "145", 10.00M);
			Assert.IsNull(paymentRequest.Merchant.CallbackUrl);
		}

		[TestMethod]
		public void RequestBuilder_PurchaseRequest_HandlesEmptyCallbackUrlTemplate()
		{
			var builder = GetBuilder();
			builder.CallbackUrlTemplate = String.Empty;

			var paymentRequest = builder.CreatePaymentRequest("0215551234", "MOBILE", "ASB", "145", 10.00M);
			Assert.IsNull(paymentRequest.Merchant.CallbackUrl);
		}

		[TestMethod]
		public void RequestBuilder_PurchaseRequest_HandlesNullOrderIdForCallbackUrl()
		{
			var builder = GetBuilder();

			var paymentRequest = builder.CreatePaymentRequest("0215551234", "MOBILE", "ASB", null, 10.00M);
			Assert.IsNull(paymentRequest.Merchant.CallbackUrl);
		}

		[TestMethod]
		public void RequestBuilder_PurchaseRequest_HandlesEmptyOrderIdForCallbackUrl()
		{
			var builder = GetBuilder();

			var paymentRequest = builder.CreatePaymentRequest("0215551234", "MOBILE", "ASB", String.Empty, 10.00M);
			Assert.IsNull(paymentRequest.Merchant.CallbackUrl);
		}

		[TestMethod]
		public void RequestBuilder_PurchaseRequest_HandlesFixedCallbackUrlTemplate()
		{
			var builder = GetBuilder();
			builder.CallbackUrlTemplate = "https://www.mycoolsite.com/payment/notification";

			var paymentRequest = builder.CreatePaymentRequest("0215551234", "MOBILE", "ASB", "145", 10.00M);
			Assert.AreEqual(new Uri("https://www.mycoolsite.com/payment/notification"), paymentRequest.Merchant.CallbackUrl);
		}

		[TestMethod]
		public void RequestBuilder_PurchaseRequest_BuildsTemplatedDescription()
		{
			var builder = GetBuilder();

			var paymentRequest = builder.CreatePaymentRequest("0215551234", "MOBILE", "ASB", "145", 10.00M);
			Assert.AreEqual("Order 145", paymentRequest.Transaction.Description);
		}

		[TestMethod]
		public void RequestBuilder_PurchaseRequest_HandlesEmptyDescriptionTemplate()
		{
			var builder = GetBuilder();
			builder.PurchaseDescriptionTemplate = String.Empty;

			var paymentRequest = builder.CreatePaymentRequest("0215551234", "MOBILE", "ASB", "145", 10.00M);
			Assert.IsNull(paymentRequest.Transaction.Description);
		}

		[TestMethod]
		public void RequestBuilder_PurchaseRequest_HandlesNonTemplatedDescription()
		{
			var builder = GetBuilder();
			builder.PurchaseDescriptionTemplate = "Pay us!";

			var paymentRequest = builder.CreatePaymentRequest("0215551234", "MOBILE", "ASB", "145", 10.00M);
			Assert.AreEqual("Pay us!", paymentRequest.Transaction.Description);
		}

		[TestMethod]
		public void RequestBuilder_PurchaseRequest_HandlesNullDescriptionTemplate()
		{
			var builder = GetBuilder();
			builder.PurchaseDescriptionTemplate = null;

			var paymentRequest = builder.CreatePaymentRequest("0215551234", "MOBILE", "ASB", "145", 10.00M);
			Assert.IsNull(paymentRequest.Transaction.Description);
		}

		#endregion

		#region Refund Request Tests

		[TestMethod]
		public void RequestBuilder_RefundRequest_BuildsValidRequestWithGoodData()
		{
			var builder = GetBuilder();

			var RefundRequest = builder.CreateRefundRequest("145", "381a08c8-9189-4995-b07b-6c3821f70e35", 10.00M);
			RefundRequest.EnsureValid();
		}

		[TestMethod]
		public void RequestBuilder_RefundRequest_AmountIsCorrect()
		{
			var builder = GetBuilder();

			var RefundRequest = builder.CreateRefundRequest("145", "381a08c8-9189-4995-b07b-6c3821f70e35", 10.00M);
			Assert.AreEqual(1000, RefundRequest.Transaction.RefundAmount);
		}

		[TestMethod]
		public void RequestBuilder_RefundRequest_OriginalPaymentIdIsCorrect()
		{
			var builder = GetBuilder();

			var RefundRequest = builder.CreateRefundRequest("145", "381a08c8-9189-4995-b07b-6c3821f70e35", 10.00M);
			Assert.AreEqual("381a08c8-9189-4995-b07b-6c3821f70e35", RefundRequest.Transaction.OriginalPaymentId);
		}

		[TestMethod]
		public void RequestBuilder_RefundRequest_UsesDefault_UserAgent()
		{
			var builder = GetBuilder();

			var RefundRequest = builder.CreateRefundRequest("145", "381a08c8-9189-4995-b07b-6c3821f70e35", 10.00M);
			Assert.AreEqual("MyUserAgent", RefundRequest.Transaction.UserAgent);
		}

		[TestMethod]
		public void RequestBuilder_RefundRequest_UsesDefault_UserIP()
		{
			var builder = GetBuilder();

			var RefundRequest = builder.CreateRefundRequest("145", "381a08c8-9189-4995-b07b-6c3821f70e35", 10.00M);
			Assert.AreEqual("192.168.1.10", RefundRequest.Transaction.UserIPAddress);
		}

		[TestMethod]
		public void RequestBuilder_RefundRequest_UsesDefault_MerchantId()
		{
			var builder = GetBuilder();

			var RefundRequest = builder.CreateRefundRequest("145", "381a08c8-9189-4995-b07b-6c3821f70e35", 10.00M);
			Assert.AreEqual("Merch-1234", RefundRequest.Merchant.MerchantIdCode);
		}

		[TestMethod]
		public void RequestBuilder_RefundRequest_BuildsTemplatedRefundReason()
		{
			var builder = GetBuilder();

			var RefundRequest = builder.CreateRefundRequest("145", "381a08c8-9189-4995-b07b-6c3821f70e35", 10.00M);
			Assert.AreEqual("Refund from order 145", RefundRequest.Transaction.RefundReason);
		}

		[TestMethod]
		public void RequestBuilder_RefundRequest_HandlesEmptyRefundReason()
		{
			var builder = GetBuilder();
			builder.RefundReasonTemplate = String.Empty;

			var RefundRequest = builder.CreateRefundRequest("145", "381a08c8-9189-4995-b07b-6c3821f70e35", 10.00M);
			Assert.IsNull(RefundRequest.Transaction.RefundReason);
		}

		[TestMethod]
		public void RequestBuilder_RefundRequest_HandlesNonTemplatedRefundReason()
		{
			var builder = GetBuilder();
			builder.RefundReasonTemplate = "We're paying you!";

			var RefundRequest = builder.CreateRefundRequest("145", "381a08c8-9189-4995-b07b-6c3821f70e35", 10.00M);
			Assert.AreEqual("We're paying you!", RefundRequest.Transaction.RefundReason);
		}

		[TestMethod]
		public void RequestBuilder_RefundRequest_HandlesNullReturnReasonTemplate()
		{
			var builder = GetBuilder();
			builder.RefundReasonTemplate = null;

			var RefundRequest = builder.CreateRefundRequest("145", "381a08c8-9189-4995-b07b-6c3821f70e35", 10.00M);
			Assert.IsNull(RefundRequest.Transaction.RefundReason);
		}

		[TestMethod]
		public void RequestBuilder_RefundRequest_HandlesNullOrderIdForTemplatedRefundReason()
		{
			var builder = GetBuilder();

			var RefundRequest = builder.CreateRefundRequest(null, "381a08c8-9189-4995-b07b-6c3821f70e35", 10.00M);
			Assert.IsNull(RefundRequest.Transaction.RefundReason);
		}

		[TestMethod]
		public void RequestBuilder_RefundRequest_HandlesEmptyOrderIdForTemplatedRefundReason()
		{
			var builder = GetBuilder();

			var RefundRequest = builder.CreateRefundRequest(String.Empty, "381a08c8-9189-4995-b07b-6c3821f70e35", 10.00M);
			Assert.IsNull(RefundRequest.Transaction.RefundReason);
		}

		#endregion

		private OnlineEftposRequestBuilder GetBuilder()
		{
			return new OnlineEftposRequestBuilder()
			{
				CallbackUrlTemplate = "https://www.mycoolsite.com/payments/callback?reference={orderId}",
				DefaultCurrency = "NZD",
				DefaultMerchantIdCode = "Merch-1234",
				DefaultCurrencyMultiplier = 100,
				DefaultMerchantUrl = new Uri("www.mycoolsite.com", UriKind.Relative),
				DefaultUserAgent = "MyUserAgent",
				DefaultUserIP = "192.168.1.10",
				PurchaseDescriptionTemplate = "Order {orderId}",
				RefundReasonTemplate = "Refund from order {orderId}"
			};
		}
	}
}