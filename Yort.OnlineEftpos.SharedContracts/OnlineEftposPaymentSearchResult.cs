using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Represents the results of a successful <see cref="IOnlineEftposClient.PaymentSearch(OnlineEftposPaymentSearchOptions)"/> request.
	/// </summary>
	public class OnlineEftposPaymentSearchResult
	{

		#region Fields

		private Uri _NextPageUri;
		private Uri _PrevPageUri;

		#endregion

		#region Public Properties

		/// <summary>
		/// Returns the list of HATEOAS links included in the search results.
		/// </summary>
		[JsonProperty("links")]
		public IEnumerable<HateoasLink> Links { get; set; }

		/// <summary>
		/// A <see cref="Uri"/> that can be used to retrieve the next page of search results.
		/// </summary>
		/// <remarks>
		/// <para>Null if there is no next page.</para>
		/// </remarks>
		public Uri NextPageUri
		{
			get
			{
				if (_NextPageUri == null)
					_NextPageUri = GetLinkByRelationship("next");

				return _NextPageUri;
			}
		}

		/// <summary>
		/// A <see cref="Uri"/> that can be used to retrieve the previous page of search results.
		/// </summary>
		/// <remarks>
		/// <para>Null if there is no previous page.</para>
		/// </remarks>
		public Uri PreviousPageUri
		{
			get
			{
				if (_PrevPageUri == null)
					_PrevPageUri = GetLinkByRelationship("prev");

				return _PrevPageUri;
			}
		}

		/// <summary>
		/// An <see cref="IEnumerable{T}"/> of <see cref="OnlineEftposPaymentStatus"/> instances representing each found payment in the current page.
		/// </summary>
		[JsonProperty("payments")]
		public IEnumerable<OnlineEftposPaymentStatus> Payments { get; set; }

		#endregion

		#region Private Methods

		private Uri GetLinkByRelationship(string relationshipName)
		{
			var links = this.Links;
			if (links == null) return null;

			var link = (from l in links where String.Compare(relationshipName, l.Rel, StringComparison.OrdinalIgnoreCase) == 0 select l).FirstOrDefault();
			if (link == null) return null;
				
			Uri uri = null;
			Uri.TryCreate(link.Href, UriKind.Absolute, out uri);

			return uri;
		}

		#endregion

	}

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