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
		private const string SandboxUrl = "https://apitest.paymark.nz";
		private const string LiveRootUrl = "https://api.paymark.nz";

		[TestMethod]
		public void OnlineEftposApiRouter_ConstructsFor_UatTest_Latest()
		{
			TestConstructionAndPath(OnlineEftposApiEnvironment.Uat, OnlineEftposApiVersion.None, UatRootUrl +"/");
		}

		[TestMethod]
		public void OnlineEftposApiRouter_ConstructsRelativeUrlWithoutLeadingSlash()
		{
#pragma warning disable CS0618 // Type or member is obsolete
			var router = TestConstructionAndPath(OnlineEftposApiEnvironment.Uat, OnlineEftposApiVersion.V1P1, UatRootUrl + "/v1.1/");
#pragma warning restore CS0618 // Type or member is obsolete
			Assert.AreEqual("https://apitest.uat.paymark.nz/v1.1/oempayment", router.GetUrl("oempayment").ToString());
		}

		[TestMethod]
		public void OnlineEftposApiRouter_ConstructsFor_UatTest_V1P1()
		{
#pragma warning disable CS0618 // Type or member is obsolete
			TestConstructionAndPath(OnlineEftposApiEnvironment.Uat, OnlineEftposApiVersion.V1P1, UatRootUrl + "/v1.1/");
#pragma warning restore CS0618 // Type or member is obsolete
		}

		[TestMethod]
		public void OnlineEftposApiRouter_ConstructsFor_Sandbox_V1P1()
		{
#pragma warning disable CS0618 // Type or member is obsolete
			TestConstructionAndPath(OnlineEftposApiEnvironment.Sandbox, OnlineEftposApiVersion.V1P1, SandboxUrl + "/v1.1/");
#pragma warning restore CS0618 // Type or member is obsolete
		}

		[TestMethod]
		public void OnlineEftposApiRouter_ConstructsFor_Live_V1P1()
		{
#pragma warning disable CS0618 // Type or member is obsolete
			TestConstructionAndPath(OnlineEftposApiEnvironment.Live, OnlineEftposApiVersion.V1P1, LiveRootUrl + "/v1.1/");
#pragma warning restore CS0618 // Type or member is obsolete
		}

		private static OnlineEftposApiRouter TestConstructionAndPath(OnlineEftposApiEnvironment environment, OnlineEftposApiVersion version, string expectedRootUrl)
		{
			var apiRouter = new OnlineEftposApiRouter(environment, version);
			Assert.AreEqual(environment, apiRouter.Environment);
			Assert.AreEqual(version, apiRouter.Version);
			Assert.AreEqual(expectedRootUrl, apiRouter.GetRootUrl().ToString());
			return apiRouter;
		}
	}
}