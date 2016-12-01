using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Yort.OnlineEftpos
{
	internal static class OnlineEftposGlobals
	{
		internal static string UserAgentComment
		{
			get
			{
				return "(WinRT)";
      }
		}

		internal static string GetVersionString()
		{
			return typeof(OnlineEftposGlobals).GetTypeInfo().Assembly.GetName().Version.ToString();
		}
	}
}