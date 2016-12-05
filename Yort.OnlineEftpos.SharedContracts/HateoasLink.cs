using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Reperesents a "Hypermedia As The Engine Of Application State" link in a response from the server. 
	/// </summary>
	public class HateoasLink
	{
		/// <summary>
		/// The address the link points to.
		/// </summary>
		public string Href { get; set; }
		/// <summary>
		/// The relationship of this link to parent data.
		/// </summary>
		public string Rel { get; set; }
		/// <summary>
		/// The title of the link.
		/// </summary>
		public string Title { get; set; }
	}
}