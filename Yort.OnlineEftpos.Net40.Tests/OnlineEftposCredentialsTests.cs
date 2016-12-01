using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yort.OnlineEftpos.Net40.Tests
{
	[TestClass]
	public class OnlineEftposCredentials_Tests
	{
		public void OnlineEftposCredentials_ConstructsOk()
		{
			var key = System.Guid.NewGuid().ToString();
			var secret = System.Guid.NewGuid().ToString();

			using (var creds = new OnlineEftposCredentials(key, secret))
			{
				Assert.IsNotNull(creds);
				Assert.AreEqual(key, creds.ConsumerKey);
				Assert.AreEqual(secret, creds.ConsumerSecret);
			}
		}
	}
}