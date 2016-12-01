using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Base type for exceptions thrown by this library. These indicate deliberately thrown exceptions rather than bugs within the library itself.
	/// </summary>
#if SUPPORTS_SERIALISEDEXCEPTIONS
	[Serializable]
#endif
	public class OnlineEftposException : Exception
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public OnlineEftposException() : this("Unspecified library error.") { }
		/// <summary>
		/// Partial constructor.
		/// </summary>
		/// <param name="message">The error message for the exception.</param>
		public OnlineEftposException(string message) : base(message) { }
		/// <summary>
		/// Full constructor.
		/// </summary>
		/// <param name="message">The error message for the exception.</param>
		/// <param name="inner">The original exception wrapped by this instance, if any.</param>
		public OnlineEftposException(string message, Exception inner) : base(message, inner) { }

#if SUPPORTS_SERIALISEDEXCEPTIONS
		/// <summary>
		/// Runtime required constructor for serialisation.
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected OnlineEftposException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
#endif
	}
}