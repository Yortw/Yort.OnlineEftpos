using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yort.OnlineEftpos.Net40.Tests
{
	[TestClass]
	public class MerchantDetailsTests
	{
		[TestMethod]
		public void MerchantDetails_ValidatesWithCorrectValues()
		{
			var merchantDetails = CreateMerchantDetails();

			merchantDetails.EnsureValid();
		}

		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		[TestMethod]
		public void MerchantDetails_CallbackUrl_ThrowsOnNullCallbackUrl()
		{
			var merchantDetails = CreateMerchantDetails();
			merchantDetails.CallbackUrl = null;

			merchantDetails.EnsureValid();
		}

		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		[TestMethod]
		public void MerchantDetails_MerchantIdCode_ThrowsOnNullMerchantId()
		{
			var merchantDetails = CreateMerchantDetails();
			merchantDetails.MerchantIdCode = null;

			merchantDetails.EnsureValid();
		}

		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		[TestMethod]
		public void MerchantDetails_MerchantIdCode_ThrowsOnEmptyMerchantId()
		{
			var merchantDetails = CreateMerchantDetails();
			merchantDetails.MerchantIdCode = String.Empty;

			merchantDetails.EnsureValid();
		}

		[ExpectedException(typeof(OnlineEftposInvalidDataException))]
		[TestMethod]
		public void MerchantDetails_MerchantIdCode_ThrowsOnWhitespaceOnlyMerchantId()
		{
			var merchantDetails = CreateMerchantDetails();
			merchantDetails.MerchantIdCode = "    ";

			merchantDetails.EnsureValid();
		}

		[TestMethod]
		public void MerchantDetails_MerchantUrl_IsOptional()
		{
			var merchantDetails = CreateMerchantDetails();
			merchantDetails.MerchantUrl = null;

			merchantDetails.EnsureValid();
		}

		private MerchantDetails CreateMerchantDetails()
		{
			return new MerchantDetails()
			{
				CallbackUrl = new Uri("www.widgets.co.nz/callback?order=145", UriKind.Relative),
				MerchantUrl = new Uri("www.widgets.co.nz", UriKind.Relative),
				MerchantIdCode = "02d48cb1-784e-41aa-a05f-5d3e9698ce47"
			};
		}
	}
}