using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Represents a string that is pinned in memory and can be overwritten when finished with, limiting the time window where the stored value can be obtained.
	/// </summary>
	/// <remarks>
	/// <para>In theory this improves (but does not guarantee) security by reducing the amount of time an unencrypted credential is available in memory. No guarantees or promises are made about the security provided by this class.</para>
	/// <para>Note, failure to explicitly dispose this class may reduce/eliminate it's effectiveness. <see cref="Dispose"/> should be called as early as possible, but no earlier.</para>
	/// </remarks>
	public sealed class ErasableString : IDisposable
	{

		#region Fields

		private readonly string _Value;
		private object _Synchroniser;
		private GCHandle _ValuePinnedHandle;
		private bool _IsDisposed;
		private bool _IsCleared;

		#endregion

		#region Constructors

		/// <summary>
		/// Full constructor.
		/// </summary>
		/// <param name="value">The .Net string to manage, used as the return value for the <see cref="Value"/> property.</param>
		public ErasableString(string value)
		{
			_Synchroniser = new object();
			_Value = value;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns true if the <see cref="Dispose"/> method has been called.
		/// </summary>
		public bool IsDisposed
		{
			get { return _IsDisposed; }
		}

		/// <summary>
		/// Returns true if the <see cref="Clear"/> method has been called.
		/// </summary>
		public bool IsCleared
		{
			get { return _IsCleared; }
		}

		/// <summary>
		/// Returns the value of the string. This will be a string of character zero, for the length of the original string, if it has been cleared.
		/// </summary>
		public string Value
		{
			get
			{
				if (IsDisposed) throw new ObjectDisposedException(nameof(ErasableString));

				return _Value;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Overwrites the string value contained by this instance with a series of character zero values.
		/// </summary>
		[System.Security.SecurityCritical]
		public void Clear()
		{
			if (IsDisposed) throw new ObjectDisposedException(nameof(ErasableString));
			if (IsCleared) return;

			if (!String.IsNullOrEmpty(_Value))
			{
				lock (_Synchroniser)
				{
					if (!_IsCleared)
					{
						unsafe
						{
							fixed (char* p = Value)
							{
								for (int cnt = 0; cnt < Value.Length; cnt++)
								{
									char* p1 = p + cnt;
									*p1 = (Char)0;
								}
							}
						}
					}
				}
			}

			_IsCleared = true;
		}

		#endregion

		#region IDisposable Members

		/// <summary>
		/// Disposes this instance.
		/// </summary>
		/// <remarks>
		/// <para>Clears the string if the <see cref="Clear"/> method has not already been called, and unpins the wrapped string in memory.</para>
		/// </remarks>
		public void Dispose()
		{
			if (!_IsDisposed)
			{
				try
				{
					try
					{
						Clear();
					}
					finally
					{
						if (_ValuePinnedHandle.IsAllocated)
							_ValuePinnedHandle.Free();
					}
				}
				finally
				{
					_IsDisposed = true;
				}
			}
		}

		#endregion

	}
}