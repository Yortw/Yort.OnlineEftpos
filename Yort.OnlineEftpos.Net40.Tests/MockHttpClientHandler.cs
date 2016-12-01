using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Yort.OnlineEftpos.Net40.Tests
{
	public class MockHttpClientHandler : System.Net.Http.HttpClientHandler
	{

		private readonly Dictionary<string, HttpResponseMessage> _FixedResponses;
		private readonly List<DynamicResponse> _DynamicResponses;

		private readonly Stack<HttpRequestMessage> _Requests;

		public MockHttpClientHandler() : base()
		{
			_FixedResponses = new Dictionary<string, HttpResponseMessage>();
			_DynamicResponses = new List<DynamicResponse>();
			_Requests = new Stack<HttpRequestMessage>();
		}

		public Dictionary<string, HttpResponseMessage> FixedResponses
		{
			get
			{
				return _FixedResponses;
			}
		}

		public List<DynamicResponse> DynamicResponses
		{
			get
			{
				return _DynamicResponses;
			}
		}

		public Stack<HttpRequestMessage> Requests
		{
			get
			{
				return _Requests;
			}
		}

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			Requests.Push(request);

			if (FixedResponses.ContainsKey(request.RequestUri.ToString())) return Task.Factory.StartNew<HttpResponseMessage>(() => FixedResponses[request.RequestUri.ToString()]);
			
			foreach (var item in DynamicResponses)
			{
				if (item.ShouldHandle(request)) return Task.Factory.StartNew<HttpResponseMessage>(() => item.GetResponse(request));
			}

			return Task.Factory.StartNew<HttpResponseMessage>(() => new HttpResponseMessage(System.Net.HttpStatusCode.NotFound));
		}

		public class DynamicResponse
		{
			public Func<HttpRequestMessage, bool> ShouldHandle;

			public Func<HttpRequestMessage, HttpResponseMessage> GetResponse;
		}
	}
}