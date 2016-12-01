using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yort.OnlineEftpos.Net40.Tests
{
	[TestClass]
	public class PaymentStatusTests
	{

		[TestMethod]
		public void PaymentStatus_Authorised_MatchesString_CaseInsensitive()
		{
			Assert.IsTrue(PaymentStatus.Authorised == "Authorised");
			Assert.IsTrue(PaymentStatus.Authorised == "AUTHORISED");

			Assert.IsTrue("Authorised" == PaymentStatus.Authorised);
			Assert.IsTrue("AUTHORISED" == PaymentStatus.Authorised);
		}

		[TestMethod]
		public void PaymentStatus_Declined_MatchesString_CaseInsensitive()
		{
			Assert.IsTrue(PaymentStatus.Declined == "Declined");
			Assert.IsTrue(PaymentStatus.Declined == "DECLINED");

			Assert.IsTrue("Declined" == PaymentStatus.Declined);
			Assert.IsTrue("DECLINED" == PaymentStatus.Declined);
		}

		[TestMethod]
		public void PaymentStatus_Error_MatchesString_CaseInsensitive()
		{
			Assert.IsTrue(PaymentStatus.Error == "Error");
			Assert.IsTrue(PaymentStatus.Error == "ERROR");

			Assert.IsTrue("Error" == PaymentStatus.Error);
			Assert.IsTrue("ERROR" == PaymentStatus.Error);
		}

		[TestMethod]
		public void PaymentStatus_Expired_MatchesString_CaseInsensitive()
		{
			Assert.IsTrue(PaymentStatus.Expired == "Expired");
			Assert.IsTrue(PaymentStatus.Expired == "EXPIRED");

			Assert.IsTrue("Expired" == PaymentStatus.Expired);
			Assert.IsTrue("EXPIRED" == PaymentStatus.Expired);
		}

		[TestMethod]
		public void PaymentStatus_New_MatchesString_CaseInsensitive()
		{
			Assert.IsTrue(PaymentStatus.New == "New");
			Assert.IsTrue(PaymentStatus.New == "NEW");

			Assert.IsTrue("New" == PaymentStatus.New);
			Assert.IsTrue("NEW" == PaymentStatus.New);
		}

		[TestMethod]
		public void PaymentStatus_Refunded_MatchesString_CaseInsensitive()
		{
			Assert.IsTrue(PaymentStatus.Refunded == "Refunded");
			Assert.IsTrue(PaymentStatus.Refunded == "REFUNDED");

			Assert.IsTrue("Refunded" == PaymentStatus.Refunded);
			Assert.IsTrue("Refunded" == PaymentStatus.Refunded);
		}

		[TestMethod]
		public void PaymentStatus_Submitted_MatchesString_CaseInsensitive()
		{
			Assert.IsTrue(PaymentStatus.Submitted == "Submitted");
			Assert.IsTrue(PaymentStatus.Submitted == "SUBMITTED");
			
			Assert.IsTrue("Submitted" == PaymentStatus.Submitted);
			Assert.IsTrue("SUBMITTED" == PaymentStatus.Submitted);
		}


		[TestMethod]
		public void PaymentStatus_Authorised_IsNotFinal()
		{
			Assert.AreEqual(false, PaymentStatus.Submitted.IsTerminal);
		}

		[TestMethod]
		public void PaymentStatus_Declined_IsFinal()
		{
			Assert.AreEqual(true, PaymentStatus.Declined.IsTerminal);
		}

		[TestMethod]
		public void PaymentStatus_Error_IsFinal()
		{
			Assert.AreEqual(true, PaymentStatus.Error.IsTerminal);
		}

		[TestMethod]
		public void PaymentStatus_Expired_IsFinal()
		{
			Assert.AreEqual(true, PaymentStatus.Expired.IsTerminal);
		}

		[TestMethod]
		public void PaymentStatus_New_IsNotFinal()
		{
			Assert.AreEqual(false, PaymentStatus.New.IsTerminal);
		}

		[TestMethod]
		public void PaymentStatus_Refunded_IsNotFinal()
		{
			Assert.AreEqual(false, PaymentStatus.Refunded.IsTerminal);
		}

		[TestMethod]
		public void PaymentStatus_Submitted_IsNotFinal()
		{
			Assert.AreEqual(false, PaymentStatus.Submitted.IsTerminal);
		}

	}
}