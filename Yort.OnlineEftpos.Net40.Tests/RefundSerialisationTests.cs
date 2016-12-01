using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yort.OnlineEftpos.Net40.Tests
{
	[TestClass]
	public class RefundSerialisationTests
	{

		[TestMethod]
		public void Deserialise_SampleRefundRequest()
		{
			var sampleRequestJson = @"{   ""merchant"": {     ""merchantIdCode"": ""301234567""   },   ""transaction"": {     ""refundAmount"": 1000,     ""refundReason"": ""Defective goods"",     ""refundId"": ""145"",     ""originalPaymentId"": ""381a08c8-9189-b07b-6c3821f70e35"",     ""userAgent"": ""Mozilla/5.0(Macintosh; Intel Mac OS X 10_11_2) AppleWebKit/537.36(KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36"",     ""userIpAddress"": ""192.168.0.1""   } }";

			var result = JsonConvert.DeserializeObject<OnlineEftposRefundRequest>(sampleRequestJson);

			Assert.IsNotNull(result.Merchant);
			Assert.IsNotNull(result.Transaction);

			Assert.AreEqual("301234567", result.Merchant.MerchantIdCode);

			Assert.AreEqual(1000, result.Transaction.RefundAmount);
			Assert.AreEqual("Defective goods", result.Transaction.RefundReason);
			Assert.AreEqual("381a08c8-9189-b07b-6c3821f70e35", result.Transaction.OriginalPaymentId);
			Assert.AreEqual("Mozilla/5.0(Macintosh; Intel Mac OS X 10_11_2) AppleWebKit/537.36(KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36", result.Transaction.UserAgent);
			Assert.AreEqual("192.168.0.1", result.Transaction.UserIPAddress);
		}

		[TestMethod]
		public void Serialise_SampleRefundRequest()
		{
			var sampleRequest = new OnlineEftposRefundRequest()
			{
				Merchant = new MerchantDetails()
				{
					MerchantIdCode = "02d48cb1-784e-41aa-a05f-5d3e9698ce47",
				},
				Transaction = new RefundDetails()
				{
					RefundAmount = 1000,
					RefundReason = "Defective goods",
					OriginalPaymentId = "381a08c8-9189-4995-b07b-6c3821f70e35",
					UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_2) AppleWebKit/5 37.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36",
					UserIPAddress = "192.168.0.1"
				}
			};

			var result = JsonConvert.SerializeObject(sampleRequest, Formatting.None);

			var unformattedSampleRequestJson = "{\"merchant\":{\"merchantId\":\"02d48cb1-784e-41aa-a05f-5d3e9698ce47\"},\"transaction\":{\"refundAmount\":1000,\"refundReason\":\"Defective goods\",\"originalPaymentId\":\"381a08c8-9189-4995-b07b-6c3821f70e35\",\"userAgent\":\"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_2) AppleWebKit/5 37.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36\",\"userIpAddress\":\"192.168.0.1\"}}";

			Assert.IsNotNull(unformattedSampleRequestJson, result);
		}

		[TestMethod]
		public void Deserialise_SampleRefundResponse()
		{
			var sampleResponseJson = @"{   ""id"": ""3c6682fd-4532-499c-b9fc-890943e82a66"",   ""status"": ""REFUNDED"",   ""bank"": {     ""payerId"": ""0215551234"",   },   ""merchant"": {     ""merchantIdCode"": ""301234567"",   },   ""transaction"": {     ""refundAmount"": 1000,     ""refundReason"": ""Defective goods"",     ""refundId"": ""145"",     ""originalPaymentId"": ""381a08c8-9189-4995-b07b-6c3821f70e35"",   },   ""creationTime"": ""2016-01-02T11:59:59Z"",   ""modificationTime"": ""2016-01-02T12:00:01Z"",   ""links"": [     {       ""href"": ""https://apitest.uat.paymark.nz/v1.1/transaction/oerefund/3c6682fd-4532499c-b9fc-890943e82a66"",       ""rel"": ""self""     }   ] }";

			var result = JsonConvert.DeserializeObject<OnlineEftposRefundStatus>(sampleResponseJson);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Merchant);
			Assert.IsNotNull(result.Transaction);

			Assert.AreEqual("3c6682fd-4532-499c-b9fc-890943e82a66", result.Id);
			Assert.AreEqual("REFUNDED", result.Status);

			Assert.AreEqual("301234567", result.Merchant.MerchantIdCode);

			Assert.AreEqual(1000, result.Transaction.RefundAmount);
			Assert.AreEqual("Defective goods", result.Transaction.RefundReason);
			Assert.AreEqual("381a08c8-9189-4995-b07b-6c3821f70e35", result.Transaction.OriginalPaymentId);

			Assert.AreEqual(DateTime.Parse("2016-01-02T12:00:01Z"), result.ModificationTime);
			Assert.AreEqual(DateTime.Parse("2016-01-02T11:59:59Z"), result.CreationTime);
		}

	}
}