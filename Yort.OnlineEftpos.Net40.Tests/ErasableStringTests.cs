using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yort.OnlineEftpos.Net40.Tests
{
	[TestClass]
	public class ErasableStringTests
	{

		[TestMethod]
		public void ErasableString_ConstructsCorrectly()
		{
			var s1 = "My Test Value";

			using (var es = new ErasableString(s1))
			{
				Assert.AreEqual(s1, es.Value);
			}
		}

		[TestMethod]
		public void ErasableString_AllReferencesCleared()
		{
			var s1 = "My Test Value";
			var s2 = s1;
			var s3 = s1;

			var expectedClearedValue = new String((char)0, s1.Length);

			using (var es = new ErasableString(s1))
			{
				Assert.AreEqual(s1, es.Value);
				es.Clear();

				Assert.AreEqual(expectedClearedValue, es.Value);
				Assert.AreEqual(expectedClearedValue, s1);
				Assert.AreEqual(expectedClearedValue, s2);
				Assert.AreEqual(expectedClearedValue, s3);
			}
		}

		[TestMethod]
		public void ErasableString_IsClearedSet()
		{
			using (var es = new ErasableString("My Test Value"))
			{
				Assert.AreEqual(false, es.IsCleared);
				es.Clear();
				Assert.AreEqual(true, es.IsCleared);
			}
		}

		[TestMethod]
		public void ErasableString_IsDisposedSet()
		{
			ErasableString es = null;
			try
			{
				es = new ErasableString("My Test Value");
				Assert.AreEqual(false, es.IsDisposed);
				es.Dispose();
				Assert.AreEqual(true, es.IsDisposed);
			}
			finally
			{
				if (es != null && !es.IsDisposed)
					es.Dispose();
			}
		}

	}
}
