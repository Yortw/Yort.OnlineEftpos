using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yort.OnlineEftpos.Net40.Tests
{
	[TestClass]
	public class KnownBanksTests
	{
		[TestMethod]
		public void KnownBanks_LoadsDefaultList()
		{
			Assert.IsTrue(KnownBanks.All.Count() > 0);
		}

		[TestMethod]
		public void KnownBanks_LoadsAsbBank()
		{
			Assert.IsNotNull(KnownBanks.GetBank("ASB"));
		}

		[TestMethod]
		public void KnownBanks_IdLookupIsCaseInsensitive()
		{
			Assert.IsNotNull(KnownBanks.GetBank("ASB"));
			Assert.IsNotNull(KnownBanks.GetBank("Asb"));
			Assert.IsNotNull(KnownBanks.GetBank("aSB"));
			Assert.IsNotNull(KnownBanks.GetBank("aSb"));
		}

		#region AddBank Tests

		[TestMethod]
		public void KnownBanks_AddBank_AddsValidBankDefinition()
		{
			var id = System.Guid.NewGuid().ToString();
			var newBank = new BankDefinition(id, new List<string>(1) { "Mobile" });

			var oldCount = KnownBanks.All.Count();
			Assert.IsTrue(KnownBanks.AddBank(newBank));
			Assert.AreEqual(oldCount + 1, KnownBanks.All.Count());
			Assert.IsNotNull(KnownBanks.GetBank(id));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void KnownBanks_AddBank_ThrownOnNullBank()
		{
			KnownBanks.AddBank(null);
		}

		#endregion

		#region AddBank Tests

		[TestMethod]
		public void KnownBanks_RemoveBank_RemovesBankById()
		{
			var id = System.Guid.NewGuid().ToString();
			var newBank = new BankDefinition(id, new List<string>(1) { "Mobile" });
			KnownBanks.AddBank(newBank);

			var oldCount = KnownBanks.All.Count();
			Assert.IsTrue(KnownBanks.RemoveBank(id));

			Assert.AreEqual(oldCount - 1, KnownBanks.All.Count());
			Assert.IsNull(KnownBanks.GetBank(id));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void KnownBanks_AddBank_ThrownOnNullBankId()
		{
			KnownBanks.RemoveBank(null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void KnownBanks_AddBank_ThrownOnEmptyBankId()
		{
			KnownBanks.RemoveBank(String.Empty);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void KnownBanks_AddBank_ThrownOnWhitespaceBankId()
		{
			KnownBanks.RemoveBank("   ");
		}

		#endregion

	}
}