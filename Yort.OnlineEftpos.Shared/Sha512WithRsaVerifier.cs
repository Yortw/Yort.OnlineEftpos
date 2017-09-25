#if !WINRT

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
#if NETSTANDARD
using RSACryptoServiceProvider = System.Security.Cryptography.RSA;
#endif

namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Default verifier for Online EFTPOS notifications, using SHA512withRSA signature verification.
	/// </summary>
	/// <remarks>
	/// <para>This class is not available in WinRT/UWP projects.</para>
	/// </remarks>
	public sealed class Sha512WithRsaVerifier : NotificationVerifierBase, IDisposable
	{

		#region Fields

		private static readonly byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };

		private RSACryptoServiceProvider _Rsa;

		#endregion

		#region Constructors

		/// <summary>
		/// Constructs a new instance using a public key encoded as as base 64 string.
		/// </summary>
		/// <remarks>
		/// <para>If the string provided is in the common format using a header an footer to mark the key extent, these headers are removed before the key is parsed, i.e;
		/// 
		///-----BEGIN PUBLIC KEY-----
		///xxx
		///-----END PUBLIC KEY-----
		///
		/// will have the begin/end public key lines removed.
		/// </para>
		/// </remarks>
		/// <param name="base64PublicKey">A string containing the base 64 encoded public key to use for verification.</param>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="base64PublicKey"/> is null.</exception>
		/// <exception cref="System.ArgumentException">Thrown if <paramref name="base64PublicKey"/> is empty or not a valid base 64 string.</exception>
		public Sha512WithRsaVerifier(string base64PublicKey) : base(base64PublicKey)
		{
		}

		/// <summary>
		/// Constructs a new instance using the raw binary data provided as the public key.
		/// </summary>
		/// <param name="publicKey">A byte array containing the bytes representing the public key.</param>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="publicKey"/> is null.</exception>
		/// <exception cref="System.ArgumentException">Thrown if <paramref name="publicKey"/> is zero length.</exception>
		public Sha512WithRsaVerifier(byte[] publicKey) : base(publicKey)
		{
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Returns true if the specified <see cref="OnlineEftposNotification"/> is verified valid and authentic, otherwise false.
		/// </summary>
		/// <param name="notification">The <see cref="OnlineEftposNotification"/> to verify.</param>
		/// <returns>A boolean indicating if <paramref name="notification"/> passes verification.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="notification"/> is null.</exception>
		public override bool Verify(OnlineEftposNotification notification)
		{
			notification.GuardNull(nameof(notification));

			bool retVal = false;
			if (_Rsa == null) _Rsa = CreateRsa(GetPublicKey());

			byte[] bytesToVerify = System.Text.UTF8Encoding.UTF8.GetBytes(notification.SignedData);
			byte[] signedBytes = System.Convert.FromBase64String(notification.Signature);
#if NETSTANDARD
			retVal = _Rsa.VerifyData(bytesToVerify, signedBytes, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1);
#else
			retVal = _Rsa.VerifyData(bytesToVerify, CryptoConfig.MapNameToOID("SHA512"), signedBytes);
#endif
			return retVal;
		}

		#endregion

		#region Private Methods

		/*
		"CreateRsa" below is modified from http://www.jensign.com/JavaScience/dotnet/pempublic/
		Original license below applies.

		Copyright(c)  2006 - 2014   JavaScience Consulting, Michel Gallant

		Permission is hereby granted, free of charge, to any person obtaining a copy
		of this software and associated documentation files(the "Software"), to deal
		in the Software without restriction, including without limitation the rights
		to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
		copies of the Software, and to permit persons to whom the Software is
		furnished to do so, subject to the following conditions:

		The above copyright notice and this permission notice shall be included in
		all copies or substantial portions of the Software.

		THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
		IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
		FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
		AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
		LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
		OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
		THE SOFTWARE.
		*/
		private static RSACryptoServiceProvider CreateRsa(byte[] publicKey)
		{
			using (BinaryReader reader = new BinaryReader(new System.IO.MemoryStream(publicKey)))
			{
				byte bt = 0;
				ushort twobytes = 0;

				twobytes = reader.ReadUInt16();
				if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
					reader.ReadByte();  //advance 1 byte
				else if (twobytes == 0x8230)
					reader.ReadInt16(); //advance 2 bytes
				else
					throw new System.Security.Cryptography.CryptographicException("Could not decode public key.");

				var seq = reader.ReadBytes(15);   //read the Sequence OID
				if (!CompareBytearrays(seq, SeqOID))  //make sure Sequence for OID is correct
					throw new System.Security.Cryptography.CryptographicException("Could not decode public key.");

				twobytes = reader.ReadUInt16();
				if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)
					reader.ReadByte();  //advance 1 byte
				else if (twobytes == 0x8203)
					reader.ReadInt16(); //advance 2 bytes
				else
					throw new System.Security.Cryptography.CryptographicException("Could not decode public key.");

				bt = reader.ReadByte();
				if (bt != 0x00)   //expect null byte next
					throw new System.Security.Cryptography.CryptographicException("Could not decode public key.");

				twobytes = reader.ReadUInt16();
				if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
					reader.ReadByte();  //advance 1 byte
				else if (twobytes == 0x8230)
					reader.ReadInt16(); //advance 2 bytes
				else
					throw new System.Security.Cryptography.CryptographicException("Could not decode public key.");

				twobytes = reader.ReadUInt16();
				byte lowbyte = 0x00;
				byte highbyte = 0x00;

				if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
					lowbyte = reader.ReadByte();  // read next bytes which is bytes in modulus
				else if (twobytes == 0x8202)
				{
					highbyte = reader.ReadByte(); //advance 2 bytes
					lowbyte = reader.ReadByte();
				}
				else
					throw new System.Security.Cryptography.CryptographicException("Could not decode public key.");

				byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order
				int modsize = BitConverter.ToInt32(modint, 0);

				int firstbyte = reader.PeekChar();
				if (firstbyte == 0x00)
				{ //if first byte (highest order) of modulus is zero, don't include it
					reader.ReadByte();  //skip this null byte
					modsize -= 1; //reduce modulus buffer size by 1
				}

				byte[] modulus = reader.ReadBytes(modsize); //read the modulus bytes

				if (reader.ReadByte() != 0x02)      //expect an Integer for the exponent data
					throw new System.Security.Cryptography.CryptographicException("Could not decode public key.");

				int expbytes = (int)reader.ReadByte();    // should only need one byte for actual exponent data (for all useful values)
				byte[] exponent = reader.ReadBytes(expbytes);

				// ------- create RSACryptoServiceProvider instance and initialize with public key -----
#if NETSTANDARD
				var rsa = System.Security.Cryptography.RSA.Create();
#else
				RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
				rsa.PersistKeyInCsp = false;
#endif
				RSAParameters RSAKeyInfo = new RSAParameters();
				RSAKeyInfo.Modulus = modulus;
				RSAKeyInfo.Exponent = exponent;

				rsa.ImportParameters(RSAKeyInfo);

				return rsa;
			}
		}

		private static bool CompareBytearrays(byte[] a, byte[] b)
		{
			if (a.Length != b.Length)
				return false;
			int i = 0;
			foreach (byte c in a)
			{
				if (c != b[i])
					return false;
				i++;
			}
			return true;
		}

		#endregion

		#region IDisposable Members

		/// <summary>
		/// Disposes this instance and all internal resources.
		/// </summary>
		public void Dispose()
		{
			_Rsa?.Dispose();
		}

		#endregion

	}
}

#endif