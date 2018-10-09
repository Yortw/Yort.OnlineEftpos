using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yort.OnlineEftpos.Net40.Tests
{
	[TestClass]
	public class TrustStatusTests
	{

		[TestMethod]
		public void TrustStatus_Active_ReturnsInstance()
		{
			Assert.IsNotNull(TrustStatus.Active);
		}

		[TestMethod]
		public void TrustStatus_Cancelled_ReturnsInstance()
		{
			Assert.IsNotNull(TrustStatus.Cancelled);
		}


		[TestMethod]
		public void TrustStatus_Pending_ReturnsInstance()
		{
			Assert.IsNotNull(TrustStatus.Pending);
		}

		[TestMethod]
		public void TrustStatus_Active_EqualsStringRepresentation()
		{
			Assert.IsTrue("ACTIVE" == TrustStatus.Active);
			Assert.IsTrue((TrustStatus)"active" == TrustStatus.Active);
		}

		[TestMethod]
		public void TrustStatus_Cancelled_EqualsStringRepresentation_CaseInsensitive()
		{
			Assert.IsTrue("CANCELLED" == TrustStatus.Cancelled);
			Assert.IsTrue((TrustStatus)"cancelled" == TrustStatus.Cancelled);
		}

		[TestMethod]
		public void TrustStatus_Pending_EqualsStringRepresentation_CaseInsensitive()
		{
			Assert.IsTrue("PENDING" == TrustStatus.Pending);
			Assert.IsTrue((TrustStatus)"pending" == TrustStatus.Pending);
		}

		[TestMethod]
		public void TrustStatus_Active_Is_Not_Cancelled()
		{
			Assert.AreNotEqual<TrustStatus>(TrustStatus.Active, TrustStatus.Cancelled);
		}

		[TestMethod]
		public void TrustStatus_Active_Is_Not_Pending()
		{
			Assert.AreNotEqual<TrustStatus>(TrustStatus.Active, TrustStatus.Pending);
		}

		[TestMethod]
		public void TrustStatus_Cancelled_Is_Not_Pending()
		{
			Assert.AreNotEqual<TrustStatus>(TrustStatus.Cancelled, TrustStatus.Pending);
		}

		[TestMethod]
		public void TrustStatus_Active_Is_EqualTo_Itself()
		{
			TrustStatus status = "active";
			Assert.AreEqual<TrustStatus>(TrustStatus.Active, status);
		}

		[TestMethod]
		public void TrustStatus_Cancelled_Is_EqualTo_Itself()
		{
			TrustStatus status = "cancelled";
			Assert.AreEqual<TrustStatus>(TrustStatus.Cancelled, status);
		}

		[TestMethod]
		public void TrustStatus_Pending_Is_EqualTo_Itself()
		{
			TrustStatus status = "pending";
			Assert.AreEqual<TrustStatus>(TrustStatus.Pending, status);
		}

		[TestMethod]
		public void TrustStatus_Active_And_Cancelled_Have_Different_Hashcodes()
		{
			Assert.AreNotEqual<int>(TrustStatus.Cancelled.GetHashCode(), TrustStatus.Active.GetHashCode());
		}

		[TestMethod]
		public void TrustStatus_Active_And_Pending_Have_Different_Hashcodes()
		{
			Assert.AreNotEqual<int>(TrustStatus.Pending.GetHashCode(), TrustStatus.Active.GetHashCode());
		}

		[TestMethod]
		public void TrustStatus_Cancelled_And_Pending_Have_Different_Hashcodes()
		{
			Assert.AreNotEqual<int>(TrustStatus.Pending.GetHashCode(), TrustStatus.Cancelled.GetHashCode());
		}

		[TestMethod]
		public void TrustStatus_Active_ToString_Returns_Active()
		{
			Assert.AreEqual<string>("ACTIVE", TrustStatus.Active.ToString());
		}

		[TestMethod]
		public void TrustStatus_Cancelled_ToString_Returns_Cancelled()
		{
			Assert.AreEqual<string>("CANCELLED", TrustStatus.Cancelled.ToString());
		}

		[TestMethod]
		public void TrustStatus_Pending_ToString_Returns_Pending()
		{
			Assert.AreEqual<string>("PENDING", TrustStatus.Pending.ToString());
		}

	}
}