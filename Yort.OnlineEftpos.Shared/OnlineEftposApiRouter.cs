using System;
using System.Collections.Generic;
using System.Text;

namespace Yort.OnlineEftpos
{
	internal class OnlineEftposApiRouter
	{
		private readonly OnlineEftposApiEnvironment _Environment;
		private readonly OnlineEftposApiVersion _Version;

		private const string RootSandboxUrl = "https://apitest.paymark.nz/";
		private const string RootUatUrl = "https://apitest.uat.paymark.nz/";
		private const string RootLiveUrl = "TODO: This.";

		private Uri _RootUrl;

		internal OnlineEftposApiRouter(OnlineEftposApiEnvironment environment, OnlineEftposApiVersion version)
		{
			_Environment = environment;
			_Version = version;
		}

		public OnlineEftposApiEnvironment Environment
		{
			get
			{
				return _Environment;
			}
		}

		public OnlineEftposApiVersion Version
		{
			get
			{
				return _Version;
			}
		}

		public Uri GetRootUrl()
		{
			if (_RootUrl == null)
				_RootUrl = new Uri(new Uri(GetEnvironmentRootUrl(), UriKind.Absolute), GetVersionPath() + "/");

			return _RootUrl;
		}

		public Uri GetUrl(string relativePath)
		{
			//HACK: Better way of handling non-versioned paths? Is bearer the only one?
			if (relativePath == "bearer")
				return new Uri(new Uri(GetEnvironmentRootUrl()), relativePath);
			else
				return new Uri(GetRootUrl(), relativePath);
		}

		private string GetEnvironmentRootUrl()
		{
			switch (_Environment)
			{
				case OnlineEftposApiEnvironment.Sandbox:
					return RootSandboxUrl;

				case OnlineEftposApiEnvironment.Uat:
					return RootUatUrl;

				case OnlineEftposApiEnvironment.Live:
					return RootLiveUrl;

				default:
					throw new InvalidOperationException("Invalid/unknown API environment specified.");
			}
		}

		private string GetVersionPath()
		{
			switch (_Version)
			{
				case OnlineEftposApiVersion.Latest:
				case OnlineEftposApiVersion.V1P1:
					return "v1.1";

				default:
					throw new InvalidOperationException("Invalid/unknown API version specified.");
			}
		}

	}
}