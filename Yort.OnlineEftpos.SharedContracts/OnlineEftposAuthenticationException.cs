using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Represents an exception thrown when a failure occurs obtaining a token from the Online Eftpos API.
	/// </summary>
#if SUPPORTS_SERIALISEDEXCEPTIONS
	[Serializable]
#endif
	public class OnlineEftposAuthenticationException : OnlineEftposException
	{

		/// <summary>
		/// Runtime required constructor.
		/// </summary>
		public OnlineEftposAuthenticationException() : this("Unknown API authentication error.")
		{
		}

		/// <summary>
		/// Basic constructor. Specifies an error message but no other details.
		/// </summary>
		/// <param name="message">The error message for the exception.</param>
		public OnlineEftposAuthenticationException(string message) : base(message)
		{
		}

		/// <summary>
		/// Partial constructor. Specifies an error message and inner exception but no other details.
		/// </summary>
		/// <param name="innerException">The original exception that caused this one.</param>
		/// <param name="message">The error message for the exception.</param>
		public OnlineEftposAuthenticationException(string message, Exception innerException) : base(message, innerException)
		{
		}

#if SUPPORTS_SERIALISEDEXCEPTIONS
		/// <summary>
		/// Runtime required constructor for serialisation.
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected OnlineEftposAuthenticationException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
#endif

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification="Tested, Data property is set to a valid instance before our constructor code runs.")]
		internal OnlineEftposAuthenticationException(string error, string errorDescription, string errorUrl, HttpStatusCode statusCode, string reasonPhrase) : base(BuildErrorMessage(error, errorDescription, errorUrl))
		{
			this.Data.Add("Error", error);
			this.Data.Add("ErrorDescription", errorDescription);
			this.Data.Add("ErrorUrl", errorUrl);
			this.Data.Add("StatusCode", statusCode);
			this.Data.Add("ReasonPhrase", reasonPhrase);
		}

		/// <summary>
		/// Returns the error message from the API, if any.
		/// </summary>
		public string Error
		{
			get { return (string)this.Data["Error"]; }
		}

		/// <summary>
		/// Returns a human readable description of the error, if any was provided by the API.
		/// </summary>
		public string ErrorDescription
		{
			get { return (string)this.Data["ErrorDescription"]; }
		}

		/// <summary>
		/// Returns a string containing a url to a web page with more information about the error, if any was provided by the API.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification="Deseriable to return value provided by API even if it can't be parsed to a System.Uri type.")]
		public string ErrorUrl
		{
			get { return (string)this.Data["ErrorUrl"]; }
		}

		/// <summary>
		/// Returns the <see cref="HttpStatusCode"/> from the API response.
		/// </summary>
		public HttpStatusCode StatusCode
		{
			get { return (HttpStatusCode)this.Data["StatusCode"]; }
		}

		/// <summary>
		/// Returns the reason phrase from the API response, if any.
		/// </summary>
		public string ReasonPhrase
		{
			get { return (string)this.Data["ReasonPhrase"]; }
		}

		private static string BuildErrorMessage(string error, string errorDescription, string errorUrl)
		{
			var retVal = $"Error obtaining token; {error}";

			if (!String.IsNullOrEmpty(errorUrl))
				retVal += $"\r\nSee {errorUrl} for more details.";
			if (!String.IsNullOrEmpty(errorDescription))
				retVal += $"\r\n{errorDescription}";

			return retVal;
    }
	}
}
