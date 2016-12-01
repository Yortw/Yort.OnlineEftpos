using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yort.OnlineEftpos.Net40.Tests
{
	[TestClass]
	public class RefundStatusTests
	{

		[TestMethod]
		public void RefundStatus_Declined_MatchesString_CaseInsensitive()
		{
			Assert.IsTrue(RefundStatus.Declined == "Declined");
			Assert.IsTrue(RefundStatus.Declined == "DECLINED");

			Assert.IsTrue("Declined" == RefundStatus.Declined);
			Assert.IsTrue("DECLINED" == RefundStatus.Declined);
		}

		[TestMethod]
		public void RefundStatus_Error_MatchesString_CaseInsensitive()
		{
			Assert.IsTrue(RefundStatus.Error == "Error");
			Assert.IsTrue(RefundStatus.Error == "ERROR");

			Assert.IsTrue("Error" == RefundStatus.Error);
			Assert.IsTrue("ERROR" == RefundStatus.Error);
		}

		[TestMethod]
		public void RefundStatus_Unsubmitted_MatchesString_CaseInsensitive()
		{
			Assert.IsTrue(RefundStatus.Unsubmitted == "Unsubmitted");
			Assert.IsTrue(RefundStatus.Unsubmitted == "UNSUBMITTED");

			Assert.IsTrue("Unsubmitted" == RefundStatus.Unsubmitted);
			Assert.IsTrue("UNSUBMITTED" == RefundStatus.Unsubmitted);
		}

		[TestMethod]
		public void RefundStatus_Refunded_MatchesString_CaseInsensitive()
		{
			Assert.IsTrue(RefundStatus.Refunded == "Refunded");
			Assert.IsTrue(RefundStatus.Refunded == "REFUNDED");

			Assert.IsTrue("Refunded" == RefundStatus.Refunded);
			Assert.IsTrue("Refunded" == RefundStatus.Refunded);
		}

		[TestMethod]
		public void RefundStatus_Declined_IsFinal()
		{
			Assert.AreEqual(true, RefundStatus.Declined.IsTerminal);
		}

		[TestMethod]
		public void RefundStatus_Error_IsFinal()
		{
			Assert.AreEqual(true, RefundStatus.Error.IsTerminal);
		}

		[TestMethod]
		public void RefundStatus_Refunded_IsNotFinal()
		{
			Assert.AreEqual(false, RefundStatus.Refunded.IsTerminal);
		}

	}
}