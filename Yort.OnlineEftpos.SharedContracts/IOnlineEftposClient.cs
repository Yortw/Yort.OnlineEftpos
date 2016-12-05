using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Primary interface for the <see cref="OnlineEftposClient"/>.
	/// </summary>
	/// <remarks>
	/// <para>Use of this interface makes it possible to mock/stub/fake out the <see cref="OnlineEftposClient"/> instance in application code for testing purposes.</para>
	/// </remarks>
	public interface IOnlineEftposClient : IDisposable
	{
		/// <summary>
		/// Returns a value indicating the version of the Online Eftpos API this client will connect to.
		/// </summary>
		/// <remarks>
		/// <para>This property is read only, the value is provided when the object is instantiated via the constructor.</para>
		/// </remarks>
		OnlineEftposApiVersion ApiVersion
		{
			get;
		}

		/// <summary>
		/// Returns a value indicating the sandbox or production environment of the Online Eftpos API this client will connect to.
		/// </summary>
		/// <remarks>
		/// <para>This property is read only, the value is provided when the object is instantiated via the constructor.</para>
		/// </remarks>
		OnlineEftposApiEnvironment ApiEnvironment
		{
			get;
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
		Task<OnlineEftposPaymentStatus> RequestPayment(OnlineEftposPaymentRequest paymentRequest);

		/// <summary>
		/// Returns updated status information for a previously submitted payment request.
		/// </summary>
		/// <param name="transactionId">The unique id of a payment to retrieve the status of.</param>
		/// <returns>A task whose result is an instance of <seealso cref="OnlineEftposPaymentStatus"/> which describes the result of a successfully submitted payment.</returns>
		/// <seealso cref="RequestPayment(OnlineEftposPaymentRequest)"/>
		/// <exception cref="OnlineEftposAuthenticationException">Thrown if a token cannot be obtained from the API. Usually this indicates incorrect credentials.</exception>
		/// <exception cref="OnlineEftposException">Thrown if an exception occurs or error information is returned from the API.</exception>
		/// <seealso cref="OnlineEftposPaymentStatus"/>
		Task<OnlineEftposPaymentStatus> CheckPaymentStatus(string transactionId);

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
		Task<OnlineEftposRefundStatus> SendRefund(OnlineEftposRefundRequest refundRequest);

		/// <summary>
		/// Returns updated status information for a previously submitted refund.
		/// </summary>
		/// <param name="transactionId">The unique id of a refund to retrieve the status of.</param>
		/// <returns>A task whose result is an instance of <seealso cref="OnlineEftposRefundStatus"/> which describes the result of a successfully submitted refund.</returns>
		/// <seealso cref="SendRefund(OnlineEftposRefundRequest)"/>
		/// <exception cref="OnlineEftposAuthenticationException">Thrown if a token cannot be obtained from the API. Usually this indicates incorrect credentials.</exception>
		/// <exception cref="OnlineEftposException">Thrown if an exception occurs or error information is returned from the API.</exception>
		/// <seealso cref="OnlineEftposRefundStatus"/>
		Task<OnlineEftposRefundStatus> CheckRefundStatus(string transactionId);

		/// <summary>
		/// Searches for payments based on one or more provided criteria and returns a <see cref="OnlineEftposPaymentSearchResult"/> containing any found transactions.
		/// </summary>
		/// <remarks>
		/// <para>The <see cref="OnlineEftposPaymentSearchResult"/> returned also contains information such as pagination url's for searches with many results.</para>
		/// </remarks>
		/// <param name="options">A <see cref="OnlineEftposPaymentSearchOptions"/> instance containing the search criteria and options for the search.</param>
		/// <returns>An <see cref="OnlineEftposPaymentSearchResult"/> instance containing the initial search results and any related meta-data.</returns>
		Task<OnlineEftposPaymentSearchResult> PaymentSearch(OnlineEftposPaymentSearchOptions options);

		/// <summary>
		/// Searches for refunds based on one or more provided criteria and returns a <see cref="OnlineEftposRefundSearchResult"/> containing any found transactions.
		/// </summary>
		/// <remarks>
		/// <para>The <see cref="OnlineEftposRefundSearchResult"/> returned also contains information such as pagination url's for searches with many results.</para>
		/// </remarks>
		/// <param name="options">A <see cref="OnlineEftposRefundSearchOptions"/> instance containing the search criteria and options for the search.</param>
		/// <returns>An <see cref="OnlineEftposRefundSearchResult"/> instance containing the initial search results and any related meta-data.</returns>
		Task<OnlineEftposRefundSearchResult> RefundSearch(OnlineEftposRefundSearchOptions options);

	}
}