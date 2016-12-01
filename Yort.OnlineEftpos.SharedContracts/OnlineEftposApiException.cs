using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Represent an error returned from the Online Eftpos API.
	/// </summary>
	/// <remarks>
	/// <para>See the <see cref="System.Exception.Data"/> property for more details, if available.</para>
	/// </remarks>
#if SUPPORTS_SERIALISEDEXCEPTIONS
	[Serializable]
#endif
	public class OnlineEftposApiException : OnlineEftposException
	{

		/// <summary>
		/// Runtime required constructor.
		/// </summary>
		public OnlineEftposApiException() : this("Unknown API error.")
		{
		}

		/// <summary>
		/// Basic constructor. Specifies an error message but no other details.
		/// </summary>
		/// <param name="message">The error message for the exception.</param>
		public OnlineEftposApiException(string message) : base(message)
		{
		}

		/// <summary>
		/// Partial constructor. Specifies an error message and inner exception but no other details.
		/// </summary>
		/// <param name="innerException">The original exception that caused this one.</param>
		/// <param name="message">The error message for the exception.</param>
		public OnlineEftposApiException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>
		/// Preferred constructor, accepts detailed information returned from the API about what went wrong.
		/// </summary>
		/// <param name="statusCode">The HTTP status code returned from the API.</param>
		/// <param name="reasonPhase">The reason phrase returned by the API HTTP response.</param>
		/// <param name="errorContent">The content returned by the API deserialised as a <see cref="OnlineEftposApiError"/> instance.</param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Tested, Data property is set to a valid instance before our constructor code runs.")]
		internal OnlineEftposApiException(HttpStatusCode statusCode, string reasonPhase, OnlineEftposApiError errorContent) : this(BuildErrorMessage(statusCode, reasonPhase, errorContent))
		{
			this.Data.Add("StatusCode", statusCode);
			this.Data.Add("ReasonPhrase", reasonPhase);

			if (errorContent != null)
			{
				this.Data.Add("ErrorMessage", errorContent.Error);
				if (!String.IsNullOrEmpty(errorContent.Reference))
					this.Data.Add("TransactionReference", errorContent.Reference);

				if (errorContent.Messages != null)
				{
					foreach (var message in errorContent.Messages)
					{
						this.Data.Add(message.Field + "Error", message.Message);
					}
				}
			}
		}

#if SUPPORTS_SERIALISEDEXCEPTIONS
		/// <summary>
		/// Runtime required constructor for serialisation.
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected OnlineEftposApiException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
#endif

		/// <summary>
		/// Returns the <see cref="System.Net.HttpStatusCode"/> from the API response.
		/// </summary>
		public HttpStatusCode StatusCode
		{
			get { return (HttpStatusCode)this.Data["Status"]; }
		}

		/// <summary>
		/// Returns the reason phrase from the API response.
		/// </summary>
		public string ReasonPhrase
		{
			get { return (string)this.Data["ReasonPhrase"]; }
		}

		/// <summary>
		/// Returns the error message from the API response body.
		/// </summary>
		public string ErrorMessage
		{
			get
			{
				if (this.Data.Contains("ErrorMessage"))
					return (string)this.Data["ErrorMessage"];
				else
					return this.ReasonPhrase;
			}
		}

		private static string BuildErrorMessage(HttpStatusCode statusCode, string reasonPhase, OnlineEftposApiError errorContent)
		{
			return $"Error: {statusCode.ToString()} - {reasonPhase}\r\n{errorContent.Error}";
		}
	}
}