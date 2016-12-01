using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Yort.OnlineEftpos.Net40.Tests
{
	[TestClass]
	public class SecureStringOnlineEftposCredentialProviderTests
	{
		[TestMethod]
		public void SecureStringOnlineEftposCredentialProvider_ConstructsOk()
		{
			var key = GetRandomSecureString();
			var secret = GetRandomSecureString();
			using (var provider = new SecureStringOnlineEftposCredentialProvider(key, secret))
			{
			}
		}

		[TestMethod]
		public void SecureStringOnlineEftposCredentialProvider_ReturnsCredentialsProvided()
		{
			var key = GetRandomSecureString();
			var secret = GetRandomSecureString();
			using (var provider = new SecureStringOnlineEftposCredentialProvider(key, secret))
			{
				var returnedCreds = provider.GetCredentials();
				Assert.IsNotNull(returnedCreds);
				Assert.AreEqual(ToInsecureString(key), returnedCreds.ConsumerKey);
				Assert.AreEqual(ToInsecureString(secret), returnedCreds.ConsumerSecret);
			}
		}

		[TestMethod]
		public void SecureStringOnlineEftposCredentialProvider_DisposesCreds()
		{
			var key = GetRandomSecureString();
			var secret = GetRandomSecureString();
			using (var provider = new SecureStringOnlineEftposCredentialProvider(key, secret))
			{
			}
			Assert.IsTrue(String.IsNullOrWhiteSpace(ToInsecureString(key)));
			Assert.IsTrue(String.IsNullOrWhiteSpace(ToInsecureString(secret)));
		}

		private System.Security.SecureString GetRandomSecureString()
		{
			var retVal = new System.Security.SecureString();
			var value = Guid.NewGuid().ToByteArray();
			foreach (var b in value)
			{
				retVal.AppendChar((char)b);
			}
			return retVal;
		}

		private static string ToInsecureString(SecureString secureString)
		{
			string plainString;
			IntPtr bstr = IntPtr.Zero;

			if (secureString == null || secureString.Length == 0)
				return String.Empty;

			try
			{
				bstr = Marshal.SecureStringToBSTR(secureString);
				plainString = Marshal.PtrToStringBSTR(bstr);
			}
			finally
			{
				if (bstr != IntPtr.Zero)
					Marshal.ZeroFreeBSTR(bstr);
			}
			return plainString;
		}
	}
}