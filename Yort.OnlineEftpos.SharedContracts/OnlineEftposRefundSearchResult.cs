using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Represents the results of a successful <see cref="IOnlineEftposClient.RefundSearch(OnlineEftposRefundSearchOptions)"/> request.
	/// </summary>
	public class OnlineEftposRefundSearchResult
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
		/// An <see cref="IEnumerable{T}"/> of <see cref="OnlineEftposRefundStatus"/> instances representing each found refund in the current page.
		/// </summary>
		[JsonProperty("refunds")]
		public IEnumerable<OnlineEftposRefundStatus> Refunds { get; set; }

		#endregion

		#region Private Methods

		private Uri GetLinkByRelationship(string relationshipName)
		{
			var links = this.Links;
			if (links == null) return null;

			var link = (from l in links where String.Compare(relationshipName, l.Rel, StringComparison.OrdinalIgnoreCase) == 0 select l).FirstOrDefault();
			if (link == null) return null;
				
			Uri.TryCreate(link.Href, UriKind.Absolute, out var uri);

			return uri;
		}

		#endregion

	}
}