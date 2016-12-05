using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Core class for communication with the PayMark Online Eftpos API. Provides methods for performing transactions such as payments and refunds.
	/// </summary>
	public sealed class OnlineEftposClient : IOnlineEftposClient, IDisposable
	{

		#region Fields & Constants

		private readonly OnlineEftposRequestAuthoriser _RequestAuthoriser;
		private readonly OnlineEftposApiRouter _ApiRouter;
		private HttpClient _HttpClient;
		private bool _HttpClientIsOwned;

		private const string OnlineEftposContentType = "application/vnd.paymark_api+json";

		#endregion

		#region Constructors

		/// <summary>
		/// Minium constructor.
		/// </summary>
		/// <param name="credentialProvider">A <see cref="IOnlineEftposCredentialProvider"/> instance that can provide credentials used to obtain a token from the API for subsequent calls. See <see cref="OnlineEftposCredentialsProvider"/> for default implementations, or create your own.</param>
		/// <param name="apiVersion">A value from the <see cref="ApiVersion"/> enum specifying the version of the Online Eftpos API this client connects to.</param>
		/// <param name="apiEnvironment">A value from the <see cref="ApiEnvironment"/> enum specifying either a test or live environment for this client to connect to.</param>
		/// <seealso cref="ApiVersion"/>
		/// <seealso cref="ApiEnvironment"/>
		public OnlineEftposClient(IOnlineEftposCredentialProvider credentialProvider, OnlineEftposApiVersion apiVersion, OnlineEftposApiEnvironment apiEnvironment) : this(credentialProvider, apiVersion, apiEnvironment, CreateDefaultHttpClient())
		{
			_HttpClientIsOwned = true;
		}

		/// <summary>
		/// Full constructor.
		/// </summary>
		/// <param name="credentialProvider">A <see cref="IOnlineEftposCredentialProvider"/> instance that can provide credentials used to obtain a token from the API for subsequent calls. See SecureStringOnlineEftposCredentialProvider (if available) or <see cref="OnlineEftposCredentialsProvider"/> for default implementations, or create your own.</param>
		/// <param name="apiVersion">A value from the <see cref="ApiVersion"/> enum specifying the version of the Online Eftpos API this client connects to.</param>
		/// <param name="apiEnvironment">A value from the <see cref="ApiEnvironment"/> enum specifying either a test or live environment for this client to connect to.</param>
		/// <param name="httpClient">A instance of <see cref="HttpClient"/> that will be used by this instance to perform all HTTP communication with the Online Eftpos API.</param>
		/// <seealso cref="ApiVersion"/>
		/// <seealso cref="ApiEnvironment"/>
		public OnlineEftposClient(IOnlineEftposCredentialProvider credentialProvider, OnlineEftposApiVersion apiVersion, OnlineEftposApiEnvironment apiEnvironment, HttpClient httpClient)
		{
			credentialProvider.GuardNull(nameof(credentialProvider));
			httpClient.GuardNull(nameof(httpClient));

			_HttpClient = httpClient;
			_ApiRouter = new OnlineEftposApiRouter(apiEnvironment, apiVersion);
			_RequestAuthoriser = new OnlineEftposRequestAuthoriser(credentialProvider, _HttpClient, _ApiRouter);
		}

		#endregion

		#region IOnlineEftposClient Members

		/// <summary>
		/// Returns a value indicating the version of the Online Eftpos API this client will connect to.
		/// </summary>
		/// <remarks>
		/// <para>This property is read only, the value is provided when the object is instantiated via the constructor.</para>
		/// </remarks>
		/// <seealso cref="OnlineEftposClient(IOnlineEftposCredentialProvider, OnlineEftposApiVersion, OnlineEftposApiEnvironment)"/>
		/// <seealso cref="OnlineEftposClient(IOnlineEftposCredentialProvider, OnlineEftposApiVersion, OnlineEftposApiEnvironment, HttpClient)"/>
		public OnlineEftposApiVersion ApiVersion
		{
			get
			{
				return _ApiRouter.Version;
			}
		}

		/// <summary>
		/// Returns a value indicating the sandbox or production environment of the Online Eftpos API this client will connect to.
		/// </summary>
		/// <remarks>
		/// <para>This property is read only, the value is provided when the object is instantiated via the constructor.</para>
		/// </remarks>
		/// <seealso cref="OnlineEftposClient(IOnlineEftposCredentialProvider, OnlineEftposApiVersion, OnlineEftposApiEnvironment)"/>
		/// <seealso cref="OnlineEftposClient(IOnlineEftposCredentialProvider, OnlineEftposApiVersion, OnlineEftposApiEnvironment, HttpClient)"/>
		public OnlineEftposApiEnvironment ApiEnvironment
		{
			get
			{
				return _ApiRouter.Environment;
			}
		}

		#region Payment Members

		/// <summary>
		/// Starts a payment transaction. Sends a request for payment to the API, which will be forwarded to the payer for approval.
		/// </summary>
		/// <remarks>
		/// <para>Use the <seealso cref="CheckPaymentStatus(string)"/> method to obtain updated status information after the request has been submitted. Usually this is done in response to an HTTP callback from the API.</para>
		/// </remarks>
		/// <param name="paymentRequest">An instance of the <seealso cref="OnlineEftposPaymentRequest"/> class that provides details of the requested payment.</param>
		/// <returns>A task whose result is an instance of <seealso cref="OnlineEftposPaymentStatus"/> which describes the result of a successfully submitted payment.</returns>
		/// <exception cref="OnlineEftposAuthenticationException">Thrown if a token cannot be obtained from the API. Usually this indicates incorrect credentials.</exception>
		/// <exception cref="OnlineEftposException">Thrown if an exception occurs or error information is returned from the API.</exception>
		/// <seealso cref="OnlineEftposPaymentRequest"/>
		/// <seealso cref="OnlineEftposPaymentStatus"/>
		/// <seealso cref="CheckPaymentStatus(string)"/>
		public async Task<OnlineEftposPaymentStatus> RequestPayment(OnlineEftposPaymentRequest paymentRequest)
		{
			paymentRequest.GuardNull(nameof(paymentRequest));
			paymentRequest.EnsureValid();

			return await SendApiRequest<OnlineEftposPaymentRequest, OnlineEftposPaymentStatus>(paymentRequest, "transaction/oepayment", HttpMethod.Post, System.Net.HttpStatusCode.Created).ConfigureAwait(false);
		}

		/// <summary>
		/// Returns updated status information for a previously submitted payment request.
		/// </summary>
		/// <param name="transactionId">The unique id of a payment to retrieve the status of.</param>
		/// <returns>A task whose result is an instance of <seealso cref="OnlineEftposPaymentStatus"/> which describes the result of a successfully submitted payment.</returns>
		/// <seealso cref="RequestPayment(OnlineEftposPaymentRequest)"/>
		/// <exception cref="OnlineEftposAuthenticationException">Thrown if a token cannot be obtained from the API. Usually this indicates incorrect credentials.</exception>
		/// <exception cref="OnlineEftposException">Thrown if an exception occurs or error information is returned from the API.</exception>
		/// <seealso cref="OnlineEftposPaymentStatus"/>
		public async Task<OnlineEftposPaymentStatus> CheckPaymentStatus(string transactionId)
		{
			transactionId.GuardNullEmptyOrWhitespace(nameof(transactionId));

			var safeTransactionId = Uri.EscapeDataString(transactionId);

			return await SendApiRequest<OnlineEftposPaymentStatus>($"transaction/oepayment/{safeTransactionId}").ConfigureAwait(false);
		}

		/// <summary>
		/// Searches for payments based on one or more provided criteria and returns a <see cref="OnlineEftposPaymentSearchResult"/> containing any found transactions.
		/// </summary>
		/// <remarks>
		/// <para>The <see cref="OnlineEftposPaymentSearchResult"/> returned also contains information such as pagination url's for searches with many results.</para>
		/// </remarks>
		/// <param name="options">A <see cref="OnlineEftposPaymentSearchOptions"/> instance containing the search criteria and options for the search.</param>
		/// <returns>An <see cref="OnlineEftposPaymentSearchResult"/> instance containing the initial search results and any related meta-data.</returns>
		public async Task<OnlineEftposPaymentSearchResult> PaymentSearch(OnlineEftposPaymentSearchOptions options)
		{
			options.GuardNull(nameof(options));

			Uri requestUri = options.PaginationUri;
			if (requestUri == null)
			{
				var queryStr = options.BuildSearchQueryString();
				if (String.IsNullOrEmpty(queryStr))
					throw new ArgumentException("No search criteria specified.", nameof(options));
				requestUri = _ApiRouter.GetUrl($"transaction/oepayment/?{queryStr}");
			}

			var requestMessage = new HttpRequestMessage()
			{
				Method = HttpMethod.Get,
				RequestUri = requestUri,
			};
			var result = await SendApiRequest<OnlineEftposPaymentSearchResult>(HttpStatusCode.OK, requestMessage).ConfigureAwait(false);

			result.Links = result.Links ?? new HateoasLink[] { };
			result.Payments = result.Payments ?? new OnlineEftposPaymentStatus[] { };
			return result;
		}

		#endregion

		#region Refund Members

		/// <summary>
		/// Sends a refund for a previous payment to the original payer.
		/// </summary>
		/// <param name="refundRequest">An instance of the <seealso cref="OnlineEftposRefundRequest"/> class that provides details of the refund to issue.</param>
		/// <returns>A task whose result is an instance of <seealso cref="OnlineEftposRefundRequest"/> which describes the result of a successfull submitted refund.</returns>
		/// <seealso cref="RequestPayment(OnlineEftposPaymentRequest)"/>
		/// <exception cref="OnlineEftposAuthenticationException">Thrown if a token cannot be obtained from the API. Usually this indicates incorrect credentials.</exception>
		/// <exception cref="OnlineEftposException">Thrown if an exception occurs or error information is returned from the API.</exception>
		/// <seealso cref="OnlineEftposRefundStatus"/>
		/// <seealso cref="CheckRefundStatus(string)"/>
		public async Task<OnlineEftposRefundStatus> SendRefund(OnlineEftposRefundRequest refundRequest)
		{
			refundRequest.GuardNull(nameof(refundRequest));
			refundRequest.EnsureValid();

			return await SendApiRequest<OnlineEftposRefundRequest, OnlineEftposRefundStatus>(refundRequest, "transaction/oerefund", HttpMethod.Post, System.Net.HttpStatusCode.Created).ConfigureAwait(false);
		}

		/// <summary>
		/// Returns updated status information for a previously submitted refund.
		/// </summary>
		/// <param name="transactionId">The unique id of a refund to retrieve the status of.</param>
		/// <returns>A task whose result is an instance of <seealso cref="OnlineEftposRefundStatus"/> which describes the result of a successfully submitted refund.</returns>
		/// <seealso cref="SendRefund(OnlineEftposRefundRequest)"/>
		/// <exception cref="OnlineEftposAuthenticationException">Thrown if a token cannot be obtained from the API. Usually this indicates incorrect credentials.</exception>
		/// <exception cref="OnlineEftposException">Thrown if an exception occurs or error information is returned from the API.</exception>
		/// <seealso cref="OnlineEftposRefundStatus"/>
		public async Task<OnlineEftposRefundStatus> CheckRefundStatus(string transactionId)
		{
			transactionId.GuardNullEmptyOrWhitespace(nameof(transactionId));

			var safeTransactionId = Uri.EscapeDataString(transactionId);

			return await SendApiRequest<OnlineEftposRefundStatus>($"transaction/oerefund/{safeTransactionId}").ConfigureAwait(false);
		}

		#endregion

		#endregion

		#region Private Members

		private static HttpClient CreateDefaultHttpClient()
		{
			var retVal = new HttpClient();

			retVal.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("Yort.OnlineEftpos", OnlineEftposGlobals.GetVersionString()));
			retVal.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue(OnlineEftposGlobals.UserAgentComment));

			return retVal;
		}

		private async Task<RS> SendApiRequest<RS>(string endpointRelativePath)
		{
			var requestMessage = new HttpRequestMessage()
			{
				Method = HttpMethod.Get,
				RequestUri = _ApiRouter.GetUrl(endpointRelativePath),
			};
			requestMessage.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(OnlineEftposContentType));

			await _RequestAuthoriser.AuthoriseRequest(requestMessage).ConfigureAwait(false);

			var result = await _HttpClient.SendAsync(requestMessage).ConfigureAwait(false);

			if (result.StatusCode == System.Net.HttpStatusCode.OK)
				return JsonConvert.DeserializeObject<RS>(await result.Content.ReadAsStringAsync().ConfigureAwait(false));
			else
			{
				var apiError = JsonConvert.DeserializeObject<OnlineEftposApiError>(await result.Content.ReadAsStringAsync().ConfigureAwait(false));
				throw new OnlineEftposApiException(result.StatusCode, result.ReasonPhrase, apiError);
			}
		}

		private async Task<RS> SendApiRequest<RQ, RS>(RQ requestContent, string endpointRelativePath, HttpMethod endpointHttpMethod, HttpStatusCode expectedResponseStatus)
		{
			var requestMessage = new HttpRequestMessage()
			{
				Content = new System.Net.Http.StringContent(JsonConvert.SerializeObject(requestContent), null, OnlineEftposContentType),
				Method = endpointHttpMethod,
				RequestUri = _ApiRouter.GetUrl(endpointRelativePath),
			};
			return await SendApiRequest<RS>(expectedResponseStatus, requestMessage);
		}

		private async Task<RS> SendApiRequest<RS>(HttpStatusCode expectedResponseStatus, HttpRequestMessage requestMessage)
		{
			requestMessage.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(OnlineEftposContentType));
			//At time of writingt Paymark will fail the request if the charset is included, it seems
			//the content type must be an exact string match to the OnlineEftposContentType constant.
			//Clear the char set here to prevent 415 errors.
			if (requestMessage.Content?.Headers?.ContentType != null)
				requestMessage.Content.Headers.ContentType.CharSet = null;

			await _RequestAuthoriser.AuthoriseRequest(requestMessage).ConfigureAwait(false);

			var result = await _HttpClient.SendAsync(requestMessage).ConfigureAwait(false);

			if (result.StatusCode == expectedResponseStatus)
				return JsonConvert.DeserializeObject<RS>(await result.Content.ReadAsStringAsync().ConfigureAwait(false));
			else
			{
				var apiError = JsonConvert.DeserializeObject<OnlineEftposApiError>(await result.Content.ReadAsStringAsync().ConfigureAwait(false));
				throw new OnlineEftposApiException(result.StatusCode, result.ReasonPhrase, apiError);
			}
		}

		#endregion

		#region IDisposable Members

		/// <summary>
		/// Disposes this instance and all internal resources, except the HttpClient provided in the constructor.
		/// </summary>
		public void Dispose()
		{
			if (_RequestAuthoriser != null)
				_RequestAuthoriser.Dispose();

			if (_HttpClientIsOwned)
				_HttpClient.Dispose();
		}

		#endregion

	}
}