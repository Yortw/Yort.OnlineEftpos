using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yort.OnlineEftpos.Net40.Tests
{
	[TestClass]
	public class OnlineEftposCredentialsProviderTests
	{
		[TestMethod]
		public void OnlineEftposCredentialsProvider_ConstructsOk()
		{
			var creds = new OnlineEftposCredentials(System.Guid.NewGuid().ToString(), System.Guid.NewGuid().ToString());
			using (var provider = new OnlineEftposCredentialsProvider(creds))
			{
			}
		}

		[TestMethod]
		public void OnlineEftposCredentialsProvider_ReturnsCredentialsProvided()
		{
			var creds = new OnlineEftposCredentials(System.Guid.NewGuid().ToString(), System.Guid.NewGuid().ToString());
			using (var provider = new OnlineEftposCredentialsProvider(creds))
			{
				var returnedCreds = provider.GetCredentials();
				Assert.IsNotNull(returnedCreds);
				Assert.AreEqual(creds.ConsumerKey, returnedCreds.ConsumerKey);
				Assert.AreEqual(creds.ConsumerSecret, returnedCreds.ConsumerSecret);
			}
		}

		[TestMethod]
		public void OnlineEftposCredentialsProvider_DisposesCreds()
		{
			var creds = new OnlineEftposCredentials(System.Guid.NewGuid().ToString(), System.Guid.NewGuid().ToString());
			using (var provider = new OnlineEftposCredentialsProvider(creds))
			{
			}
			Assert.IsTrue(String.IsNullOrWhiteSpace(creds.ConsumerKey));
			Assert.IsTrue(String.IsNullOrWhiteSpace(creds.ConsumerSecret));
		}
	}
}