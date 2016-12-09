using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yort.OnlineEftpos
{
	internal static class ExceptionHelper
	{

		internal static void ThrowYoureDoingItWrong()
		{
			throw new InvalidOperationException("You've reference the wrong assembly. The portable assembly should never be loaded at runtime, and should only be referenced from other portable class libraries. Please reference the platform specific library relevant to your application instead.");
		}
	}
}