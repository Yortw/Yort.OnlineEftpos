using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Core class for communication with the PayMark Online Eftpos API. Provides methods for performing transactions such as payments and refunds.
	/// </summary>
	public sealed class OnlineEftposClient : IOnlineEftposClient
	{
		/// <summary>
		/// Minium constructor.
		/// </summary>
		/// <param name="credentialProvider">A <see cref="IOnlineEftposCredentialProvider"/> instance that can provide credentials used to obtain a token from the API for subsequent calls. See <see cref="OnlineEftposCredentialsProvider"/> for default implementations, or create your own.</param>
		/// <param name="apiVersion">A value from the <see cref="ApiVersion"/> enum specifying the version of the Online Eftpos API this client connects to.</param>
		/// <param name="apiEnvironment">A value from the <see cref="ApiEnvironment"/> enum specifying either a test or live environment for this client to connect to.</param>
		/// <seealso cref="ApiVersion"/>
		/// <seealso cref="ApiEnvironment"/>
		public OnlineEftposClient(IOnlineEftposCredentialProvider credentialProvider, OnlineEftposApiVersion apiVersion, OnlineEftposApiEnvironment apiEnvironment)
		{
			ExceptionHelper.ThrowYoureDoingItWrong();
		}

		/// <summary>
		/// Full constructor.
		/// </summary>
		/// <param name="credentialProvider">A <see cref="IOnlineEftposCredentialProvider"/> instance that can provide credentials used to obtain a token from the API for subsequent calls. See SecureStringOnlineEftposCredentialProvider (if available) or <see cref="OnlineEftposCredentialsProvider"/> for default implementations, or create your own.</param>
		/// <param name="apiVersion">A value from the <see cref="ApiVersion"/> enum specifying the version of the Online Eftpos API this client connects to.</param>
		/// <param name="apiEnvironment">A value from the <see cref="ApiEnvironment"/> enum specifying either a test or live environment for this client to connect to.</param>
		/// <param name="httpClient">A instance of <see cref="System.Net.Http.HttpClient"/> that will be used by this instance to perform all HTTP communication with the Online Eftpos API.</param>
		/// <seealso cref="ApiVersion"/>
		/// <seealso cref="ApiEnvironment"/>
		public OnlineEftposClient(IOnlineEftposCredentialProvider credentialProvider, OnlineEftposApiVersion apiVersion, OnlineEftposApiEnvironment apiEnvironment, HttpClient httpClient)
		{
			ExceptionHelper.ThrowYoureDoingItWrong();
		}

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
				ExceptionHelper.ThrowYoureDoingItWrong();
				return OnlineEftposApiVersion.None; //Work around compiler warning.
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
				ExceptionHelper.ThrowYoureDoingItWrong();
				return OnlineEftposApiEnvironment.Uat; //Work around compiler warning.
			}
		}

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
		public Task<OnlineEftposPaymentStatus> RequestPayment(OnlineEftposPaymentRequest paymentRequest)
		{
			ExceptionHelper.ThrowYoureDoingItWrong();
			return null; //Avoid compiler warning.
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
		public Task<OnlineEftposPaymentStatus> CheckPaymentStatus(string transactionId)
		{
			ExceptionHelper.ThrowYoureDoingItWrong();
			return null; //Avoid compiler warning.
		}


		/// <summary>
		/// Searches for payments based on one or more provided criteria and returns a <see cref="OnlineEftposPaymentSearchResult"/> containing any found transactions.
		/// </summary>
		/// <remarks>
		/// <para>The <see cref="OnlineEftposPaymentSearchResult"/> returned also contains information such as pagination url's for searches with many results.</para>
		/// </remarks>
		/// <param name="options">A <see cref="OnlineEftposPaymentSearchOptions"/> instance containing the search criteria and options for the search.</param>
		/// <returns>An <see cref="OnlineEftposPaymentSearchResult"/> instance containing the initial search results and any related meta-data.</returns>
		public Task<OnlineEftposPaymentSearchResult> PaymentSearch(OnlineEftposPaymentSearchOptions options)
		{
			ExceptionHelper.ThrowYoureDoingItWrong();
			return null;
		}

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
		public Task<OnlineEftposRefundStatus> SendRefund(OnlineEftposRefundRequest refundRequest)
		{
			ExceptionHelper.ThrowYoureDoingItWrong();
			return null; //Avoid compiler warning.
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
		public Task<OnlineEftposRefundStatus> CheckRefundStatus(string transactionId)
		{
			ExceptionHelper.ThrowYoureDoingItWrong();
			return null; //Avoid compiler warning.
		}

		/// <summary>
		/// Searches for refunds based on one or more provided criteria and returns a <see cref="OnlineEftposRefundSearchResult"/> containing any found transactions.
		/// </summary>
		/// <remarks>
		/// <para>The <see cref="OnlineEftposRefundSearchResult"/> returned also contains information such as pagination url's for searches with many results.</para>
		/// </remarks>
		/// <param name="options">A <see cref="OnlineEftposRefundSearchOptions"/> instance containing the search criteria and options for the search.</param>
		/// <returns>An <see cref="OnlineEftposRefundSearchResult"/> instance containing the initial search results and any related meta-data.</returns>
		public Task<OnlineEftposRefundSearchResult> RefundSearch(OnlineEftposRefundSearchOptions options)
		{
			ExceptionHelper.ThrowYoureDoingItWrong();
			return null;
		}

		/// <summary>
		/// Disposes this instance and all internal resources, except the HttpClient provided in the constructor.
		/// </summary>
		public void Dispose()
		{
			ExceptionHelper.ThrowYoureDoingItWrong();
		}

		/// <summary>
		/// Deletes a trust/autopay relationship previously established via <see cref="RequestPayment(OnlineEftposPaymentRequest)"/>.
		/// </summary>
		/// <param name="options">The details of the trust/autopay relationship to delete.</param>
		/// <returns>An <see cref="OnlineEftposDeleteTrustResult"/> instance containing details about the result of the request.</returns>
		public Task<OnlineEftposDeleteTrustResult> DeleteTrust(OnlineEftposDeleteTrustOptions options)
		{
			throw new NotImplementedException();
		}
	}
}