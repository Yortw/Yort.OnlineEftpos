using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Yort.OnlineEftpos;

namespace Yort.OnlineEftpos.Net40.Tests
{
	[TestClass]
	public class PaymentSerialisationTests
	{
		[TestMethod]
		public void Deserialise_SamplePaymentRequest()
		{
			var sampleRequestJson = @"{   ""bank"": {     ""payerId"": ""0215551234"",     ""bankId"": ""ASB"",     ""payerIdType"": ""MOBILE""   },   ""merchant"": {     ""merchantIdCode"": ""301234567"",     ""merchantUrl"": ""www.widgets.co.nz"",     ""callbackUrl"": ""www.widgets.co.nz/callback?order=145""   },   ""transaction"": {     ""amount"": 1000,     ""currency"": ""NZD"",     ""description"": ""Widgets"",     ""orderId"": ""145"",     ""userAgent"": ""Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36"",     ""userIpAddress"": ""192.168.0.1""   } }";

			var result = JsonConvert.DeserializeObject<OnlineEftposPaymentRequest>(sampleRequestJson);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Bank);
			Assert.IsNotNull(result.Merchant);
			Assert.IsNotNull(result.Transaction);

			Assert.AreEqual("0215551234", result.Bank.PayerId);
			Assert.AreEqual("ASB", result.Bank.BankId);
			Assert.AreEqual("MOBILE", result.Bank.PayerIdType);

			Assert.AreEqual(new Uri("www.widgets.co.nz/callback?order=145", UriKind.Relative), result.Merchant.CallbackUrl);
			Assert.AreEqual("301234567", result.Merchant.MerchantIdCode);
			Assert.AreEqual(new Uri("www.widgets.co.nz", UriKind.Relative), result.Merchant.MerchantUrl);

			Assert.AreEqual(1000, result.Transaction.Amount);
			Assert.AreEqual("NZD", result.Transaction.Currency);
			Assert.AreEqual("Widgets", result.Transaction.Description);
			Assert.AreEqual("145", result.Transaction.OrderId);
			Assert.AreEqual("Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36", result.Transaction.UserAgent);
			Assert.AreEqual("192.168.0.1", result.Transaction.UserIPAddress);
		}

		[TestMethod]
		public void Serialise_SamplePaymentRequest()
		{
			var sampleRequest = new OnlineEftposPaymentRequest()
			{
				Bank = new BankDetails()
				{
					PayerId = "0215551234"
				},
				Merchant = new MerchantDetails()
				{
					MerchantIdCode = "02d48cb1-784e-41aa-a05f-5d3e9698ce47",
					CallbackUrl = new Uri("www.widgets.co.nz/callback?order=145", UriKind.Relative),
					MerchantUrl = new Uri("www.widgets.co.nz", UriKind.Relative)
				},
				Transaction = new PaymentDetails()
				{
					Amount = 1000,
					Currency = "NZD",
					Description= "Widgets",
					OrderId= "145",
					UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_2) AppleWebKit/5 37.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36",
					UserIPAddress = "192.168.0.1"
				}
			};

			var result = JsonConvert.SerializeObject(sampleRequest, Formatting.None);

			var unformattedSampleRequestJson = "{\"bank\":{\"payerId\":\"0215551234\",},\"merchant\":{\"merchantId\":\"02d48cb1-784e-41aa-a05f-5d3e9698ce47\",\"merchantUrl\":\"www.widgets.co.nz\",\"callbackUrl\":\"www.widgets.co.nz/callback?order=145\"},\"transaction\":{\"amount\":1000,\"currency\":\"NZD\",\"description\":\"Widgets\",\"orderId\":\"145\",\"userAgent\":\"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_2) AppleWebKit/5 37.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36\",\"userIpAddress\":\"192.168.0.1\"}}";

			Assert.IsNotNull(unformattedSampleRequestJson, result);
		}

		[TestMethod]
		public void Deserialise_SamplePaymentResponse()
		{
			var sampleResponseJson = @"{   ""id"": ""381a08c8-9189-4995-b07b-6c3821f70e35"",   ""status"": ""SUBMITTED"",   ""bank"": {     ""payerId"": ""0215551234"",     ""bankId"": ""ASB"",     ""payerIdType"": ""MOBILE""   },   ""merchant"": {     ""merchantIdCode"": ""301234567"",     ""callbackUrl"": ""www.widgets.co.nz/callback?order=145""   },   ""transaction"": {     ""amount"": 1000,     ""currency"": ""NZD"",     ""description"": ""Widgets"",     ""orderId"": ""145""   },   ""creationTime"": ""2016-01-01T11:59:59Z"",   ""modificationTime"": ""2016-01-01T12:01:02Z"",   ""links"": [     {       ""href"": ""https://apitest.uat.paymark.nz/v1.1/transaction/oepayment/381a08c8-91894995-b07b-6c3821f70e35"",       ""rel"": ""self""     }   ] }";

			var result = JsonConvert.DeserializeObject<OnlineEftposPaymentStatus>(sampleResponseJson);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Bank);
			Assert.IsNotNull(result.Merchant);
			Assert.IsNotNull(result.Transaction);

			Assert.AreEqual("381a08c8-9189-4995-b07b-6c3821f70e35", result.Id);
			Assert.AreEqual("SUBMITTED", result.Status);

			Assert.AreEqual("0215551234", result.Bank.PayerId);
			Assert.AreEqual("ASB", result.Bank.BankId);
			Assert.AreEqual("MOBILE", result.Bank.PayerIdType);

			Assert.AreEqual(new Uri("www.widgets.co.nz/callback?order=145", UriKind.Relative), result.Merchant.CallbackUrl);
			Assert.AreEqual("301234567", result.Merchant.MerchantIdCode);

			Assert.AreEqual(1000, result.Transaction.Amount);
			Assert.AreEqual("NZD", result.Transaction.Currency);
			Assert.AreEqual("Widgets", result.Transaction.Description);
			Assert.AreEqual("145", result.Transaction.OrderId);
			Assert.AreEqual(DateTime.Parse("2016-01-01T12:01:02Z"), result.ModificationTime);
			Assert.AreEqual(DateTime.Parse("2016-01-01T11:59:59Z"), result.CreationTime);
		}

	}
}