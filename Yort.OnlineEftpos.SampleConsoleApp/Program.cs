using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yort.OnlineEftpos.SampleConsoleApp
{
	class Program
	{
		private static OnlineEftposClient _Client;
		private static OnlineEftposRequestBuilder _RequestBuilder;

		private static int _OrderId = 1;

		static void Main(string[] args)
		{
			_RequestBuilder = new OnlineEftposRequestBuilder()
			{
				CallbackUrlTemplate = "https://www.mycoolsite.com/payments/callback?reference={orderId}",
				DefaultCurrency = "NZD",
				DefaultMerchantIdCode = "02d48cb1-784e-41aa-a05f-5d3e9698ce47",
				DefaultCurrencyMultiplier = 100,
				DefaultMerchantUrl = new Uri("www.mycoolsite.com", UriKind.Relative),
				DefaultUserAgent = "MyUserAgent",
				DefaultUserIP = "192.168.1.10",
				PurchaseDescriptionTemplate = "Order {orderId}",
				RefundReasonTemplate = "Refund from order {orderId}"
			};

			var creds = new OnlineEftposCredentials("yourkey", "yoursecret");
			_Client = new OnlineEftposClient(new OnlineEftposCredentialsProvider(creds), OnlineEftposApiVersion.Latest, OnlineEftposApiEnvironment.Uat);

			var t = StartLoopAsync();
			Console.WriteLine("Press enter to quit.");
			Console.ReadLine();
		}

		private static async Task StartLoopAsync()
		{
			var payerIdType = new NZMobilePayerIdType();
			while (true)
			{
				Console.WriteLine("Enter to quit, or enter payer id:");
				var payerId = Console.ReadLine();

				if (String.IsNullOrEmpty(payerId)) break;

				try
				{
					payerId = payerIdType.Normalize(payerId);
					Console.WriteLine("Normalised Id is: " + payerId);

					await RequestPaymentAmount(payerId);
				}
				catch (OnlineEftposInvalidDataException)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Invalid payer id.");
					Console.ForegroundColor = ConsoleColor.Gray;
				}
			}
		}

		private static async Task RequestPaymentAmount(string payerId)
		{
			try
			{
				Console.WriteLine("Enter payment amount: ");
				var amountStr = Console.ReadLine();
				decimal amount = 0;
				amount = Decimal.Parse(amountStr, System.Globalization.NumberStyles.Currency);

				var request = _RequestBuilder.CreatePaymentRequest(payerId, "MOBILE", "ASB", _OrderId.ToString(), amount);
				var result = await _Client.RequestPayment(request);

				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine(result.Id);
				Console.WriteLine(result.Status);
				Console.WriteLine(result.CreationTime);
				Console.WriteLine(result.ModificationTime);
				Console.ForegroundColor = ConsoleColor.Gray;
			}
			catch (Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Error: " + ex.ToString());
				Console.ForegroundColor = ConsoleColor.Gray;
			}
		}
	}
}