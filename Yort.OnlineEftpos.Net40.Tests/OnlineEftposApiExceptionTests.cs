using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yort.OnlineEftpos.Net40.Tests
{
	[TestClass]
	public class OnlineEftposApiExceptionTests
	{
		[TestMethod]
		public void OnlineEftposApiException_Constructor_ConstructsWithNoArgs()
		{
			var ex = new OnlineEftposApiException();
			Assert.IsNull(ex.ErrorContent);
			Assert.IsNull(ex.InnerException);
			Assert.IsTrue(String.IsNullOrEmpty(ex.ReasonPhrase));
			Assert.AreEqual((System.Net.HttpStatusCode)0, ex.StatusCode);
		}

		[TestMethod]
		public void OnlineEftposApiException_Constructor_ConstructsFullArgs()
		{
			var ex = new OnlineEftposApiException(System.Net.HttpStatusCode.NotFound, 
				"Payment not found.", 
				new OnlineEftposApiError()
				{
					Error = "Payment not found.",
					Messages = new ApiErrorMessage[]
					{
						new ApiErrorMessage()
						{
							Field = "OrderId",
							Message = "Not found."
						}
					},
					Reference = "123"
				}
			);

			Assert.AreEqual("Error: (404) Payment not found.\r\nPayment not found.: Not found.", ex.Message);
			Assert.AreEqual(ex.ReasonPhrase, "Payment not found.");
			Assert.AreEqual(ex.StatusCode, System.Net.HttpStatusCode.NotFound);
			Assert.AreEqual(1, ex.ErrorContent.Messages.Count());
			Assert.AreEqual("123", ex.ErrorContent.Reference);
			Assert.AreEqual("OrderId", ex.ErrorContent.Messages.First().Field);
			Assert.AreEqual("Not found.", ex.ErrorContent.Messages.First().Message);
		}

	}
}