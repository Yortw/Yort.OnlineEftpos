using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Search criteria and options holder for <see cref="IOnlineEftposClient.PaymentSearch(OnlineEftposPaymentSearchOptions)"/>.
	/// </summary>
	public class OnlineEftposPaymentSearchOptions
	{

		#region Public Properties

		/// <summary>
		/// The merchant's original order id (<see cref="PaymentDetails.OrderId"/>) for the payment.
		/// </summary>
		public string OrderId { get; set; }
		/// <summary>
		/// The code that uniquley identifies the merchant who requested payment.
		/// </summary>
		public string MerchantIdCode { get; set; }
		/// <summary>
		/// The id of the payer that request was made to.
		/// </summary>
		public string PayerId { get; set; }
		/// <summary>
		/// The date and time at which the payment was requested.
		/// </summary>
		public DateTime? CreationTime { get; set; }
		/// <summary>
		/// The current status of the payment.
		/// </summary>
		public string Status { get; set; }

		/// <summary>
		/// A list of field names to return. If empty, all fields are returned.
		/// </summary>
		/// <remarks>
		/// <para>If requesting sub-fields, each sub-field must be listed separately, i.e transaction.Amount, transaction.userIpAddress.</para>
		/// </remarks>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public IList<string> Fields { get; private set; } = new List<string>();
		
		/// <summary>
		/// The number of items to return 'per page'.
		/// </summary>
		public uint Limit { get; set; }

		/// <summary>
		/// The pagination offset to use, for when results span multiple 'pages'.
		/// </summary>
		/// <remarks>
		/// <para>Use of this field is not recommended. Instead we recommend using <see cref="PaginationUri"/>.</para>
		/// </remarks>
		public string Offset { get; set; }

		/// <summary>
		/// A Uri returned by a previous call to payment search, which is used to retrieve another page of results. Preferred over <see cref="Offset"/>.
		/// </summary>
		public Uri PaginationUri { get; set; }

		#endregion

		#region Public Methods

		/// <summary>
		/// Converts the properties of this instance into a url query string.
		/// </summary>
		/// <returns></returns>
		public string BuildSearchQueryString()
		{
			if (this.PaginationUri != null) throw new InvalidOperationException("Use PaginationUri instead.");

			var sb = new System.Text.StringBuilder();

			if (this.CreationTime != null)
			{
				var value = CreationTime.Value;
				if (CreationTime.Value.Kind != DateTimeKind.Utc)
					value = value.ToUniversalTime();

				AppendKeyValuePair(sb, "creationTime", value.ToString("O"));
			}

			if (this.Limit != 0)
				AppendKeyValuePair(sb, "limit", this.Limit.ToString(System.Globalization.CultureInfo.InvariantCulture));

			AppendKeyValuePairIfValueNotEmpty(sb, "merchantIdCode", MerchantIdCode);

			AppendKeyValuePairIfValueNotEmpty(sb, "offset", Offset);

			AppendKeyValuePairIfValueNotEmpty(sb, "orderId", OrderId);

			AppendKeyValuePairIfValueNotEmpty(sb, "payerId", PayerId);

			AppendKeyValuePairIfValueNotEmpty(sb, "status", Status);

			AppendFieldListIfNotEmpty(sb);

			return sb.ToString();
		}

		#endregion

		#region Private Methods

		private void AppendFieldListIfNotEmpty(StringBuilder sb)
		{
			if (Fields.Count > 0)
			{
				sb.Append("&fields=");

				bool doneOne = false;
				foreach (var fieldName in Fields)
				{
					if (!doneOne)
						sb.Append(",");

					sb.Append(Uri.EscapeDataString(fieldName));

					doneOne = true;
				}
			}
		}

		private static void AppendKeyValuePairIfValueNotEmpty(StringBuilder sb, string key, string value)
		{
			if (!String.IsNullOrEmpty(value))
				AppendKeyValuePair(sb, key, value);
		}

		private static void AppendKeyValuePair(StringBuilder sb, string key, string value)
		{
			if (sb.Length > 0)
				sb.Append("&");

			sb.Append(Uri.EscapeDataString(key));
			sb.Append("=");
			sb.Append(Uri.EscapeDataString(value));
		}

		#endregion

	}
}