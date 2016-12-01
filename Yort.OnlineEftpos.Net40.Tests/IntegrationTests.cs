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
			//TODO: Constants for payerIdType?
			//TODO: Constants for BankId?

			System.Net.ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => { return true; };
			System.Net.ServicePointManager.Expect100Continue = false;
			System.Net.ServicePointManager.UseNagleAlgorithm = false;

			var credProvider = new SecureStringOnlineEftposCredentialProvider(
				SecureStringFromString(Environment.GetEnvironmentVariable("PaymarkUatKey")),
				SecureStringFromString(Environment.GetEnvironmentVariable("PaymarkUatSecret"))
			);

			IOnlineEftposClient client = new OnlineEftpos.OnlineEftposClient(credProvider, OnlineEftposApiVersion.Latest, OnlineEftposApiEnvironment.Sandbox);
			var result = await client.RequestPayment(new OnlineEftpos.OnlineEftposPaymentRequest()
			{
				Bank = new BankDetails()
				{
					BankId = "ASB",
					PayerIdType = "MOBILE",
					PayerId = "021588167"
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
					OrderId = System.Guid.NewGuid().ToString(),
					UserAgent = "Yort.OnlineEftpos.Tests",
					UserIPAddress = "10.10.10.100"
				}
			}).ConfigureAwait(false);

			Assert.IsNotNull(result);
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