using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yort.OnlineEftpos
{
	internal static class OnlineEftposGlobals
	{
		internal static string UserAgentComment
		{
			get
			{
				return $"(iOS;{Environment.OSVersion.VersionString})";
      }
		}


		internal static string GetVersionString()
		{
			return typeof(OnlineEftposGlobals).Assembly.GetName().Version.ToString();
		}
	}
}