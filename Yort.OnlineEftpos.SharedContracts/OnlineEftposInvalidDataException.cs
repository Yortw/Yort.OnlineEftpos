using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Represents an exception thrown when a request contains invalid or missing data, validated prior to being sumitted to the API.
	/// </summary>
#if SUPPORTS_SERIALISEDEXCEPTIONS
	[Serializable]
#endif
	public class OnlineEftposInvalidDataException : OnlineEftposException
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public OnlineEftposInvalidDataException() : this("Unspecified data error.") { }
		/// <summary>
		/// Partial constructor.
		/// </summary>
		/// <param name="message">The error message for the exception.</param>
		public OnlineEftposInvalidDataException(string message) : base(message) { }
		/// <summary>
		/// Full constructor.
		/// </summary>
		/// <param name="message">The error message for the exception.</param>
		/// <param name="inner">The original exception wrapped by this instance, if any.</param>
		public OnlineEftposInvalidDataException(string message, Exception inner) : base(message, inner) { }

#if SUPPORTS_SERIALISEDEXCEPTIONS
		/// <summary>
		/// Runtime required constructor for serialisation.
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected OnlineEftposInvalidDataException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
#endif

		internal OnlineEftposInvalidDataException(ArgumentException innerException) : base(innerException.Message, innerException)
		{
		}
	}
}