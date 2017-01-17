using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Yort.OnlineEftpos.Net40.Tests
{
	[TestClass]
	public class OnlineEftposRequestAuthoriserTests
	{

		[TestMethod]
		public async Task OnlineEftposRequestAuthoriser_SignsRequest()
		{
			var credProvider = new OnlineEftposCredentialsProvider(new OnlineEftposCredentials("mykey", "mysecret"));
#pragma warning disable CS0618 // Type or member is obsolete
			var router = new OnlineEftposApiRouter(OnlineEftposApiEnvironment.Uat, OnlineEftposApiVersion.V1P1);
#pragma warning restore CS0618 // Type or member is obsolete
			var handler = GetMockHandler(TimeSpan.FromHours(1));
			var client = new System.Net.Http.HttpClient(handler);
			
			var signer = new OnlineEftposRequestAuthoriser(credProvider, client, router);
			var request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, router.GetUrl("oempayment/3c6682fd-4532-499c-b9fc-890943e82a66"));

			await signer.AuthoriseRequest(request);

			Assert.IsNotNull(request.Headers.Authorization);
			Assert.AreEqual("Bearer", request.Headers.Authorization.Scheme);
			Assert.AreEqual("AAAA%2FAAA%3DAAAAAAAA", request.Headers.Authorization.Parameter);
			Assert.AreEqual(1, handler.Requests.Count);
		}

		[TestMethod]
		public async Task OnlineEftposRequestAuthoriser_DoesNotObtainTokenEveryRequest()
		{
			var credProvider = new OnlineEftposCredentialsProvider(new OnlineEftposCredentials("mykey", "mysecret"));
#pragma warning disable CS0618 // Type or member is obsolete
			var router = new OnlineEftposApiRouter(OnlineEftposApiEnvironment.Uat, OnlineEftposApiVersion.V1P1);
#pragma warning restore CS0618 // Type or member is obsolete
			var handler = GetMockHandler(TimeSpan.FromHours(1));
			var client = new System.Net.Http.HttpClient(handler);

			var signer = new OnlineEftposRequestAuthoriser(credProvider, client, router);
			var request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, router.GetUrl("oempayment/3c6682fd-4532-499c-b9fc-890943e82a66"));

			await signer.AuthoriseRequest(request);
			System.Threading.Thread.Sleep(2000);

			request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, router.GetUrl("oempayment/3c6682fd-4532-499c-b9fc-890943e82a66"));
			await signer.AuthoriseRequest(request);

			Assert.IsNotNull(request.Headers.Authorization);
			Assert.AreEqual("Bearer", request.Headers.Authorization.Scheme);
			Assert.AreEqual("AAAA%2FAAA%3DAAAAAAAA", request.Headers.Authorization.Parameter);
			Assert.AreEqual(1, handler.Requests.Count);
		}

		[TestMethod]
		public async Task OnlineEftposRequestAuthoriser_UpdatesExpiredToken()
		{
			var credProvider = new OnlineEftposCredentialsProvider(new OnlineEftposCredentials("mykey", "mysecret"));
#pragma warning disable CS0618 // Type or member is obsolete
			var router = new OnlineEftposApiRouter(OnlineEftposApiEnvironment.Uat, OnlineEftposApiVersion.V1P1);
#pragma warning restore CS0618 // Type or member is obsolete
			var handler = GetMockHandler(TimeSpan.FromSeconds(5));
			var client = new System.Net.Http.HttpClient(handler);

			var signer = new OnlineEftposRequestAuthoriser(credProvider, client, router);
			var request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, router.GetUrl("oempayment/3c6682fd-4532-499c-b9fc-890943e82a66"));

			await signer.AuthoriseRequest(request);
			System.Threading.Thread.Sleep(2000);

			request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, router.GetUrl("oempayment/3c6682fd-4532-499c-b9fc-890943e82a66"));
			await signer.AuthoriseRequest(request);

			Assert.IsNotNull(request.Headers.Authorization);
			Assert.AreEqual("Bearer", request.Headers.Authorization.Scheme);
			Assert.AreEqual("AAAA%2FAAA%3DAAAAAAAA", request.Headers.Authorization.Parameter);
			Assert.AreEqual(2, handler.Requests.Count);
		}

		private MockHttpClientHandler GetMockHandler(TimeSpan tokenLifetime)
		{
			var retVal = new MockHttpClientHandler();

			retVal.DynamicResponses.Add(new MockHttpClientHandler.DynamicResponse()
			{
				ShouldHandle = (request) => request.RequestUri.ToString() == "https://apitest.uat.paymark.nz/bearer" 
					&& request.Headers.Authorization.Scheme == "Basic" 
					&& request.Headers.Authorization.Parameter == "bXlrZXk6bXlzZWNyZXQ=", 
				GetResponse = (request) =>
				{
					return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
					{
						Content = new StringContent("{\"token_type\":\"bearer\",\"access_token\":\"AAAA%2FAAA%3DAAAAAAAA\", \"expires_in\":\"" + tokenLifetime.TotalSeconds.ToString() + "\"}",
							System.Text.UTF8Encoding.UTF32, "application/json")
					};				
				}
			});


			return retVal;
		}
	}
}