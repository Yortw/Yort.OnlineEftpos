using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yort.OnlineEftpos;

namespace Yort.OnlineEftpos.Net40.Tests
{
	[TestClass]
	public class OnlineEftposApiRouterTests
	{

		private const string UatRootUrl = "https://apitest.uat.paymark.nz";
		private const string LiveRootUrl = "TODO: This.";

		[TestMethod]
		public void OnlineEftposApiRouter_ConstructsFor_UatTest_Latest()
		{
			TestConstructionAndPath(OnlineEftposApiEnvironment.Uat, OnlineEftposApiVersion.Latest, UatRootUrl +"/v1.1/");
		}

		[TestMethod]
		public void OnlineEftposApiRouter_ConstructsRelativeUrlWithoutLeadingSlash()
		{
			var router = TestConstructionAndPath(OnlineEftposApiEnvironment.Uat, OnlineEftposApiVersion.V1P1, UatRootUrl + "/v1.1/");
			Assert.AreEqual("https://apitest.uat.paymark.nz/v1.1/oempayment", router.GetUrl("oempayment").ToString());
		}

		[TestMethod]
		public void OnlineEftposApiRouter_ConstructsFor_UatTest_V1P1()
		{
			TestConstructionAndPath(OnlineEftposApiEnvironment.Uat, OnlineEftposApiVersion.V1P1, UatRootUrl + "/v1.1/");
		}

		private static OnlineEftposApiRouter TestConstructionAndPath(OnlineEftposApiEnvironment environment, OnlineEftposApiVersion version, string expectedRootUrl)
		{
			var apiRouter = new OnlineEftposApiRouter(OnlineEftposApiEnvironment.Uat, version);
			Assert.AreEqual(environment, apiRouter.Environment);
			Assert.AreEqual(version, apiRouter.Version);
			Assert.AreEqual(expectedRootUrl, apiRouter.GetRootUrl().ToString());
			return apiRouter;
		}
	}
}