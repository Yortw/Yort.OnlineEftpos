using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Reduces the effort/code needed to construct a <see cref="OnlineEftposPaymentRequest"/> or <see cref="OnlineEftposRefundRequest"/> by using default &amp; templated values for properties that do not change or have a predictable pattern.
	/// </summary>
	/// <remarks>
	/// <para>Note: Values neither provided to the builder nor the resulting request objects are validated. To ensure a request is valid make sure to call the requests EnsureValid method.</para>
	/// </remarks>
	/// <seealso cref="PaymentDetails"/>
	/// <seealso cref="RefundDetails"/>
	/// <seealso cref="PaymentDetails.EnsureValid"/>
	/// <seealso cref="RefundDetails.EnsureValid"/>
	public class OnlineEftposRequestBuilder
	{

		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public OnlineEftposRequestBuilder()
		{
			this.DefaultCurrencyMultiplier = 100;
			this.DefaultUserAgent = OnlineEftposGlobals.UserAgentComment;
			this.PurchaseDescriptionTemplate = "{orderId}";
		}

		#endregion

		#region Properties

		/// <summary>
		/// The default value for the <see cref="PaymentDetails.Currency"/> property.
		/// </summary>
		public string DefaultCurrency { get; set; }
		/// <summary>
		/// Specifies the multipler to convert a <see cref="System.Decimal"/> value into a valid API amount (integer).
		/// </summary>
		/// <remarks>
		/// <para>The API requires amounts to be provided as an integer representing the total value in the minimum unit of the currency being used. For example, in the US, Australia and New Zealand the minimum unit is 'cents', at 0.01 for a single cent. If you want to charge $10.00 then the API requires you to provide an amount of 1000. The multiplier in this case would be 100 (10 * 100 = 1000).</para>
		/// <para>The default value for this property is 100.</para>
		/// </remarks>
		public int DefaultCurrencyMultiplier { get; set; }

		/// <summary>
		/// The template string to use for default purchase descriptions.
		/// </summary>
		/// <remarks>
		/// <para>The following tags can be used and will be runtime replaced;</para>
		/// <list type="Bullet">
		/// {orderId}
		/// </list>
		/// </remarks>
		public string PurchaseDescriptionTemplate
		{
			get; set;
		}

		/// <summary>
		/// The template string to use for default refund reasons.
		/// </summary>
		/// <remarks>
		/// <para>The following tags can be used and will be runtime replaced;</para>
		/// <list type="Bullet">
		/// {orderId}
		/// </list>
		/// </remarks>
		public string RefundReasonTemplate
		{
			get; set;
		}

		/// <summary>
		/// For systems that do not interact with a user via HTTP, this is the fake value passed for the <see cref="TransactionRequestDetails.UserAgent"/> property.
		/// </summary>
		public string DefaultUserAgent { get; set; }
		/// <summary>
		/// For systems that do not interact with a user via HTTP, this is the fake value passed for the <see cref="TransactionRequestDetails.UserIPAddress"/> property.
		/// </summary>
		public string DefaultUserIP { get; set; }

		/// <summary>
		/// The default value for the <see cref="MerchantDetails.MerchantUrl"/> property.
		/// </summary>
		public Uri DefaultMerchantUrl { get; set; }
		/// <summary>
		/// The default value for the <see cref="MerchantDetails.MerchantIdCode"/> property.
		/// </summary>
		public string DefaultMerchantIdCode { get; set; }

		/// <summary>
		/// A template string for creating callback urls.
		/// </summary>
		/// <remarks>
		/// <para>You can use {orderId} to indicate your transaction reference within the template.</para>
		/// <para>e.g. services.mycoolsite.com/payments/callback?orderId={orderId}</para>
		/// </remarks>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification ="Not an actual URI, just a template. Value may not be suitable for the Uri data type.")]
		public string CallbackUrlTemplate { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// Create a new (unvalidated) payment request using default and supplied values.
		/// </summary>
		/// <param name="payerId">The id of the entity to request payment from.</param>
		/// <param name="payerIdType">The type id provided in the <paramref name="payerId"/> argument. Usually "MOBILE".</param>
		/// <param name="bankId">The unique ID of the bank to which the payer is registered with/using for this request.</param>
		/// <param name="orderId">Your (merchant) reference for the transaction.</param>
		/// <param name="amount">The amount of the transaction as a decimal value.</param>
		/// <returns>An <see cref="OnlineEftposPaymentRequest"/> pre-populated with appropriate values, but unvalidated. Call the <see cref="OnlineEftposPaymentRequest.EnsureValid"/> method to confirm validity.</returns>
		public OnlineEftposPaymentRequest CreatePaymentRequest(string payerId, string payerIdType, string bankId, string orderId, decimal amount)
		{
			return CreatePaymentRequest(payerId, payerIdType, bankId, orderId, amount, this.DefaultUserIP, this.DefaultUserAgent);
		}

		/// <summary>
		/// Create a new (unvalidated) payment request using default and supplied values.
		/// </summary>
		/// <param name="payerId">The id of the entity to request payment from.</param>
		/// <param name="payerIdType">The type id provided in the <paramref name="payerId"/> argument. Usually "MOBILE".</param>
		/// <param name="bankId">The unique ID of the bank to which the payer is registered with/using for this request.</param>
		/// <param name="orderId">Your (merchant) reference for the transaction.</param>
		/// <param name="amount">The amount of the transaction as a decimal value.</param> 
		/// <param name="userAgent">The user agent string of the requesting client.</param>
		/// <param name="userIP">The IP address of the requesting client.</param>
		/// <returns>An <see cref="OnlineEftposPaymentRequest"/> pre-populated with appropriate values, but unvalidated. Call the <see cref="OnlineEftposPaymentRequest.EnsureValid"/> method to confirm validity.</returns>
		public OnlineEftposPaymentRequest CreatePaymentRequest(string payerId, string payerIdType, string bankId, string orderId, decimal amount, string userIP, string userAgent)
		{
			return new OnlineEftposPaymentRequest()
			{
				Bank = new BankDetails()
				{
					BankId = bankId,
					PayerId = payerId,
					PayerIdType = payerIdType,
				}
				,
				Merchant = new MerchantDetails()
				{
					CallbackUrl = BuildCallbackUrl(orderId),
					MerchantIdCode = this.DefaultMerchantIdCode,
					MerchantUrl = this.DefaultMerchantUrl
				},
				Transaction = new PaymentDetails()
				{
					Amount = DecimalToApiAmount(amount),
					Currency = DefaultCurrency,
					Description = BuildPurchaseDescription(orderId),
					OrderId = orderId,
					UserAgent = userAgent,
					UserIPAddress = userIP
				}
			};
		}

		/// <summary>
		/// Create a new (unvalidated) refund request using default and supplied values.
		/// </summary>
		/// <param name="orderId">Your (merchant) reference for the transaction.</param>
		/// <param name="originalPaymentId">The id of the original payment being refunded.</param>
		/// <param name="amount">The amount of the transaction as a decimal value.</param>
		/// <returns>An <see cref="OnlineEftposRefundRequest"/> pre-populated with appropriate values, but unvalidated. Call the <see cref="OnlineEftposRefundRequest.EnsureValid"/> method to confirm validity.</returns>
		public OnlineEftposRefundRequest CreateRefundRequest(string orderId, string originalPaymentId, decimal amount)
		{
			return CreateRefundRequest(orderId, originalPaymentId, amount, DefaultUserIP, DefaultUserAgent);
		}

		/// <summary>
		/// Create a new (unvalidated) refund request using default and supplied values.
		/// </summary>
		/// <param name="orderId">Your (merchant) reference for the transaction.</param>
		/// <param name="originalPaymentId">The id of the original payment being refunded.</param>
		/// <param name="amount">The amount of the transaction as a decimal value.</param>
		/// <param name="userAgent">The user agent string of the requesting client.</param>
		/// <param name="userIP">The IP address of the requesting client.</param>
		/// <returns>An <see cref="OnlineEftposRefundRequest"/> pre-populated with appropriate values, but unvalidated. Call the <see cref="OnlineEftposRefundRequest.EnsureValid"/> method to confirm validity.</returns>
		public OnlineEftposRefundRequest CreateRefundRequest(string orderId, string originalPaymentId, decimal amount, string userIP, string userAgent)
		{
			return new OnlineEftposRefundRequest()
			{
				Merchant = new MerchantDetails()
				{
					MerchantIdCode = this.DefaultMerchantIdCode,
				},
				Transaction = new RefundDetails()
				{
					RefundAmount = DecimalToApiAmount(amount),
					RefundReason = BuildRefundReason(orderId),
					OriginalPaymentId = originalPaymentId,
					UserAgent = userAgent,
					UserIPAddress = userIP
				}
			};
		}

		#endregion

		#region Private Methods

		private string BuildPurchaseDescription(string orderId)
		{
			if (String.IsNullOrEmpty(this.PurchaseDescriptionTemplate)) return null;
			if (String.IsNullOrEmpty(orderId)) return null;

			return this.PurchaseDescriptionTemplate.Replace("{orderId}", orderId);
		}

		private string BuildRefundReason(string orderId)
		{
			if (String.IsNullOrEmpty(this.RefundReasonTemplate)) return null;
			if (String.IsNullOrEmpty(orderId)) return null;

			return this.RefundReasonTemplate.Replace("{orderId}", orderId);
		}

		private int DecimalToApiAmount(decimal amount)
		{
			return Convert.ToInt32(amount * this.DefaultCurrencyMultiplier);
		}

		private Uri BuildCallbackUrl(string orderId)
		{
			if (String.IsNullOrEmpty(this.CallbackUrlTemplate)) return null;
			if (String.IsNullOrEmpty(orderId)) return null;

			return new Uri(this.CallbackUrlTemplate.Replace("{orderId}", Uri.EscapeUriString(orderId)), UriKind.RelativeOrAbsolute);
		}

		#endregion

	}
}