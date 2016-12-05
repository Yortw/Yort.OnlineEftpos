using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;

namespace Yort.OnlineEftpos.Net40.Tests
{
	[TestClass]
	public class IntegrationTests
	{

		[TestMethod]
		public async Task Integration_RequestPayment()
		{
			System.Net.ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => { return true; };
			System.Net.ServicePointManager.Expect100Continue = false;
			System.Net.ServicePointManager.UseNagleAlgorithm = false;

			var credProvider = new SecureStringOnlineEftposCredentialProvider(
				SecureStringFromString(Environment.GetEnvironmentVariable("PaymarkUatKey")),
				SecureStringFromString(Environment.GetEnvironmentVariable("PaymarkUatSecret"))
			);

			IOnlineEftposClient client = new OnlineEftpos.OnlineEftposClient(credProvider, OnlineEftposApiVersion.Latest, OnlineEftposApiEnvironment.Sandbox);
			var orderId = System.Guid.NewGuid().ToString();
			var result = await client.RequestPayment(new OnlineEftpos.OnlineEftposPaymentRequest()
			{
				Bank = new BankDetails()
				{
					BankId = "ASB",
					PayerIdType = "MOBILE",
					PayerId = "021555123"
				},
				Merchant = new MerchantDetails()
				{
					CallbackUrl = new Uri("https://connect.ontempo.ws/devtest1/store/v1/EPayment/Paymark/Notification"),
					MerchantIdCode = Environment.GetEnvironmentVariable("PaymarkMerchantId"), 
					MerchantUrl = new Uri("http://www.ontempo.co.nz")
				},
				Transaction = new PaymentDetails()
				{
					Amount = 1000,
					Currency = "NZD",
					Description = "Test Tran",
					OrderId = orderId,
					UserAgent = "Yort.OnlineEftpos.Tests",
					UserIPAddress = "10.10.10.100"
				}
			}).ConfigureAwait(false);

			Assert.IsNotNull(result);
			Assert.IsTrue(!String.IsNullOrWhiteSpace(result.Id), "Invalid (blank) id returned.");
			Assert.AreEqual(result.Bank.BankId, "ASB");
			Assert.AreEqual(result.Bank.PayerId, "021555123");
			Assert.AreEqual(result.Bank.PayerIdType, "MOBILE");
			Assert.AreEqual(result.Transaction.Amount, 1000);
			Assert.AreEqual(result.Transaction.Description, "Test Tran");
			Assert.AreEqual(result.Transaction.OrderId, orderId);
			Assert.AreEqual(result.Merchant.MerchantIdCode, Environment.GetEnvironmentVariable("PaymarkMerchantId"));
		}

		[TestMethod]
		public async Task Integration_CheckPaymentStatus()
		{

			#region Test Setup

			System.Net.ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => { return true; };
			System.Net.ServicePointManager.Expect100Continue = false;
			System.Net.ServicePointManager.UseNagleAlgorithm = false;

			var credProvider = new SecureStringOnlineEftposCredentialProvider(
				SecureStringFromString(Environment.GetEnvironmentVariable("PaymarkUatKey")),
				SecureStringFromString(Environment.GetEnvironmentVariable("PaymarkUatSecret"))
			);

			//Make a payment

			var paymentId = System.Guid.NewGuid().ToString();
			IOnlineEftposClient client = new OnlineEftpos.OnlineEftposClient(credProvider, OnlineEftposApiVersion.Latest, OnlineEftposApiEnvironment.Sandbox);
			var result = await client.RequestPayment(new OnlineEftpos.OnlineEftposPaymentRequest()
			{
				Bank = new BankDetails()
				{
					BankId = "ASB",
					PayerIdType = "MOBILE",
					PayerId = "021555123"
				},
				Merchant = new MerchantDetails()
				{
					CallbackUrl = new Uri("https://connect.ontempo.ws/devtest1/store/v1/EPayment/Paymark/Notification"),
					MerchantIdCode = Environment.GetEnvironmentVariable("PaymarkMerchantId"),
					MerchantUrl = new Uri("http://www.ontempo.co.nz")
				},
				Transaction = new PaymentDetails()
				{
					Amount = 1000,
					Currency = "NZD",
					Description = "Test Tran",
					OrderId = paymentId,
					UserAgent = "Yort.OnlineEftpos.Tests",
					UserIPAddress = "10.10.10.100"
				}
			}).ConfigureAwait(false);

			Assert.IsNotNull(result);
			Assert.IsTrue(!String.IsNullOrWhiteSpace(result.Id), "Invalid (blank) id returned.");

			#endregion

			var statusResult = await client.CheckPaymentStatus(result.Id).ConfigureAwait(false);

			Assert.AreEqual(paymentId, result.Transaction.OrderId);
			Assert.AreEqual(1000, result.Transaction.Amount);
			Assert.AreEqual("NZD", result.Transaction.Currency);
			Assert.AreEqual("ASB", result.Bank.BankId);
			Assert.AreEqual("MOBILE", result.Bank.PayerIdType);
			Assert.AreEqual("021555123", result.Bank.PayerId);
			Assert.AreEqual(Environment.GetEnvironmentVariable("PaymarkMerchantId"), result.Merchant.MerchantIdCode);
		}

		[TestMethod]
		public async Task Integration_CheckPaymentStatusByOrderId()
		{

			#region Test Setup

			System.Net.ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => { return true; };
			System.Net.ServicePointManager.Expect100Continue = false;
			System.Net.ServicePointManager.UseNagleAlgorithm = false;

			var credProvider = new SecureStringOnlineEftposCredentialProvider(
				SecureStringFromString(Environment.GetEnvironmentVariable("PaymarkUatKey")),
				SecureStringFromString(Environment.GetEnvironmentVariable("PaymarkUatSecret"))
			);

			//Make a payment

			var orderId = System.Guid.NewGuid().ToString();
			IOnlineEftposClient client = new OnlineEftpos.OnlineEftposClient(credProvider, OnlineEftposApiVersion.Latest, OnlineEftposApiEnvironment.Sandbox);
			var result = await client.RequestPayment(new OnlineEftpos.OnlineEftposPaymentRequest()
			{
				Bank = new BankDetails()
				{
					BankId = "ASB",
					PayerIdType = "MOBILE",
					PayerId = "021555123"
				},
				Merchant = new MerchantDetails()
				{
					CallbackUrl = new Uri("https://connect.ontempo.ws/devtest1/store/v1/EPayment/Paymark/Notification"),
					MerchantIdCode = Environment.GetEnvironmentVariable("PaymarkMerchantId"),
					MerchantUrl = new Uri("http://www.ontempo.co.nz")
				},
				Transaction = new PaymentDetails()
				{
					Amount = 1000,
					Currency = "NZD",
					Description = "Test Tran",
					OrderId = orderId,
					UserAgent = "Yort.OnlineEftpos.Tests",
					UserIPAddress = "10.10.10.100"
				}
			}).ConfigureAwait(false);

			Assert.IsNotNull(result);
			Assert.IsTrue(!String.IsNullOrWhiteSpace(result.Id), "Invalid (blank) id returned.");

			#endregion

			var searchResult = await client.PaymentSearch(new OnlineEftposPaymentSearchOptions()
			{
				OrderId = orderId
			}).ConfigureAwait(false);

			Assert.IsNotNull(searchResult);
			Assert.AreEqual(1, searchResult.Payments.Count());

			var paymentResult = searchResult.Payments.First();
			Assert.IsTrue(!String.IsNullOrWhiteSpace(paymentResult.Id), "Invalid (blank) id returned in search result.");
			Assert.AreEqual(orderId, paymentResult.Transaction.OrderId);
			Assert.AreEqual(1000, paymentResult.Transaction.Amount);
			Assert.AreEqual("NZD", paymentResult.Transaction.Currency);
			Assert.AreEqual("021555123", paymentResult.Bank.PayerId);
			Assert.AreEqual(Environment.GetEnvironmentVariable("PaymarkMerchantId"), paymentResult.Merchant.MerchantIdCode);
		}

		[TestMethod]
		public async Task Integration_CheckPaymentStatusSearchPagination()
		{

			#region Test Setup

			System.Net.ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => { return true; };
			System.Net.ServicePointManager.Expect100Continue = false;
			System.Net.ServicePointManager.UseNagleAlgorithm = false;

			var credProvider = new SecureStringOnlineEftposCredentialProvider(
				SecureStringFromString(Environment.GetEnvironmentVariable("PaymarkUatKey")),
				SecureStringFromString(Environment.GetEnvironmentVariable("PaymarkUatSecret"))
			);

			#endregion

			IOnlineEftposClient client = new OnlineEftpos.OnlineEftposClient(credProvider, OnlineEftposApiVersion.Latest, OnlineEftposApiEnvironment.Sandbox);

			var searchResult = await client.PaymentSearch(new OnlineEftposPaymentSearchOptions()
			{
				MerchantIdCode = Environment.GetEnvironmentVariable("PaymarkMerchantId"),
				Limit = 1
			}).ConfigureAwait(false);

			Assert.IsTrue(searchResult.Payments.Count() > 0, "No payments returned, expected some payments.");
			Assert.IsFalse(searchResult.Payments.Count() > 1, "Too many payments returned, limit not being applied.");

			Assert.IsNotNull(searchResult.NextPageUri, "No next page URI returned, expected next page with limit of 1 result per page.");
			Assert.IsNull(searchResult.PreviousPageUri, "Previous page URI returned, not expected on first search request.");

			var paymentId = searchResult.Payments.First().Id;

			searchResult = await client.PaymentSearch(new OnlineEftposPaymentSearchOptions()
			{
				PaginationUri = searchResult.NextPageUri
			}).ConfigureAwait(false);

			Assert.IsNotNull(searchResult.PreviousPageUri, "Previous page URI not returned, expected on second search request.");
			Assert.AreNotEqual(paymentId, searchResult.Payments.First().Id, "First and second search request returned same payment.");
		}

		private SecureString SecureStringFromString(string sourceText)
		{
			var retVal = new SecureString();
			foreach (var c in sourceText)
			{
				retVal.AppendChar(c);
			}
			return retVal;
		}
	}
}