using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yort.OnlineEftpos.Net40.Tests
{
	[TestClass]
	public class OnlineEftposNotificationTests
	{

		#region Constructor Tests

		[ExpectedException(typeof(System.ArgumentNullException))]
		[TestMethod]
		public void Notification_Constructor_ThrowsOnNullData()
		{
			var n = new OnlineEftposNotification(null);
		}

		[ExpectedException(typeof(System.ArgumentException))]
		[TestMethod]
		public void Notification_Constructor_ThrowsOnEmptyData()
		{
			var n = new OnlineEftposNotification(String.Empty);
		}

		[ExpectedException(typeof(System.ArgumentException))]
		[TestMethod]
		public void Notification_Constructor_ThrowsOnWhitespaceData()
		{
			var n = new OnlineEftposNotification("    ");
		}

		[ExpectedException(typeof(System.ArgumentException))]
		[TestMethod]
		public void Notification_Constructor_ThrowsOnNonFormUrlEncodedData()
		{
			var n = new OnlineEftposNotification("Not Valid Data");
		}

		[ExpectedException(typeof(System.ArgumentException))]
		[TestMethod]
		public void Notification_Constructor_ThrowsOnMissingSignature()
		{
			var n = new OnlineEftposNotification("merchantOrderId=115fb597-7033-4a94-8fd2-3d20d4f3d545&status=AUTHORISED");
		}

		[ExpectedException(typeof(System.ArgumentException))]
		[TestMethod]
		public void Notification_Constructor_ThrowsOnMissingMerchantOrderId()
		{
			var n = new OnlineEftposNotification("status=AUTHORISED&signature=VqgXoSaxlW494ftrYJoZAC8t%2FBuRIqmcP9ZOX%2FPcGDZYWtc91mmx9%2BUXfMqMoi9UlyVI2unSESezIsBM8yZ02Zn8NKsxKg7usiAbTnvmLB%2F8UrDEHmsZau6GrnLyI%2BxbvNQ0j%2BDSq%2B3%2BfRZxe3HjSywLZD8%2F5k695ms%2FlNjb2BgYvcZ0TjPPKwX9rKjoPJlOzMVNESjZaaSCb229Rs2VEshVCbivO8OsFPiR3nH4DrfmJL2DpDDdSmjnJ0IuvzLUNg6LJeomHRf4RsGs9JwPsBxKLl9ZIhXjz7t5rCCG4GuoVqScUcwoJuZmP0%2B2lTvDB9VAbziuhKrMR1FuzZbaRVwBNRV0G%2FwKWaD%2BG8tDnLtB8qBj1pwHPdmzOY8d%2FA5BpjvvGlIaD7ktnQpDrcUddgKPBljkHVMqoV97dKtLOvwaI5xRlaXHVLD1Kjx23puXBwcj5lmF0vBnbzbk5u5zELyRyWpW5cvJrTk8c6s%2FlGnXPvu%2F0zlGIQwPb2kE5CySeB%2Brj0h2ejU3eynEoeb5NdOKlRNFnIn5OHJUeJZOtfa0CHNF37%2BXaJaqCxHLB4smXSab84xbFXstSz0do3WW8eH7Xghyx9U4mnh%2FISXiXtyPTPn1tD0nlIJR7tgr7X%2F%2FmykGqpSVVY10%2FDWUE2Mm2KYXSlH1Ywt%2FfImnKxWvbyc%3D");
		}

		[ExpectedException(typeof(System.ArgumentException))]
		[TestMethod]
		public void Notification_Constructor_ThrowsOnMissingStatus()
		{
			var n = new OnlineEftposNotification("merchantOrderId=115fb597-7033-4a94-8fd2-3d20d4f3d545&signature=VqgXoSaxlW494ftrYJoZAC8t%2FBuRIqmcP9ZOX%2FPcGDZYWtc91mmx9%2BUXfMqMoi9UlyVI2unSESezIsBM8yZ02Zn8NKsxKg7usiAbTnvmLB%2F8UrDEHmsZau6GrnLyI%2BxbvNQ0j%2BDSq%2B3%2BfRZxe3HjSywLZD8%2F5k695ms%2FlNjb2BgYvcZ0TjPPKwX9rKjoPJlOzMVNESjZaaSCb229Rs2VEshVCbivO8OsFPiR3nH4DrfmJL2DpDDdSmjnJ0IuvzLUNg6LJeomHRf4RsGs9JwPsBxKLl9ZIhXjz7t5rCCG4GuoVqScUcwoJuZmP0%2B2lTvDB9VAbziuhKrMR1FuzZbaRVwBNRV0G%2FwKWaD%2BG8tDnLtB8qBj1pwHPdmzOY8d%2FA5BpjvvGlIaD7ktnQpDrcUddgKPBljkHVMqoV97dKtLOvwaI5xRlaXHVLD1Kjx23puXBwcj5lmF0vBnbzbk5u5zELyRyWpW5cvJrTk8c6s%2FlGnXPvu%2F0zlGIQwPb2kE5CySeB%2Brj0h2ejU3eynEoeb5NdOKlRNFnIn5OHJUeJZOtfa0CHNF37%2BXaJaqCxHLB4smXSab84xbFXstSz0do3WW8eH7Xghyx9U4mnh%2FISXiXtyPTPn1tD0nlIJR7tgr7X%2F%2FmykGqpSVVY10%2FDWUE2Mm2KYXSlH1Ywt%2FfImnKxWvbyc%3D");
		}

		[TestMethod]
		public void Notification_Constructor_ParsesValidNotificationDataWithoutTransactionId()
		{
			var n = new OnlineEftposNotification("merchantOrderId=115fb597-7033-4a94-8fd2-3d20d4f3d545&status=AUTHORISED&signature=VqgXoSaxlW494ftrYJoZAC8t%2FBuRIqmcP9ZOX%2FPcGDZYWtc91mmx9%2BUXfMqMoi9UlyVI2unSESezIsBM8yZ02Zn8NKsxKg7usiAbTnvmLB%2F8UrDEHmsZau6GrnLyI%2BxbvNQ0j%2BDSq%2B3%2BfRZxe3HjSywLZD8%2F5k695ms%2FlNjb2BgYvcZ0TjPPKwX9rKjoPJlOzMVNESjZaaSCb229Rs2VEshVCbivO8OsFPiR3nH4DrfmJL2DpDDdSmjnJ0IuvzLUNg6LJeomHRf4RsGs9JwPsBxKLl9ZIhXjz7t5rCCG4GuoVqScUcwoJuZmP0%2B2lTvDB9VAbziuhKrMR1FuzZbaRVwBNRV0G%2FwKWaD%2BG8tDnLtB8qBj1pwHPdmzOY8d%2FA5BpjvvGlIaD7ktnQpDrcUddgKPBljkHVMqoV97dKtLOvwaI5xRlaXHVLD1Kjx23puXBwcj5lmF0vBnbzbk5u5zELyRyWpW5cvJrTk8c6s%2FlGnXPvu%2F0zlGIQwPb2kE5CySeB%2Brj0h2ejU3eynEoeb5NdOKlRNFnIn5OHJUeJZOtfa0CHNF37%2BXaJaqCxHLB4smXSab84xbFXstSz0do3WW8eH7Xghyx9U4mnh%2FISXiXtyPTPn1tD0nlIJR7tgr7X%2F%2FmykGqpSVVY10%2FDWUE2Mm2KYXSlH1Ywt%2FfImnKxWvbyc%3D");

			Assert.AreEqual("115fb597-7033-4a94-8fd2-3d20d4f3d545", n.MerchantOrderId);
			Assert.AreEqual("AUTHORISED", n.Status);
			Assert.IsTrue(String.IsNullOrEmpty(n.TransactionId));
			Assert.AreEqual("VqgXoSaxlW494ftrYJoZAC8t/BuRIqmcP9ZOX/PcGDZYWtc91mmx9+UXfMqMoi9UlyVI2unSESezIsBM8yZ02Zn8NKsxKg7usiAbTnvmLB/8UrDEHmsZau6GrnLyI+xbvNQ0j+DSq+3+fRZxe3HjSywLZD8/5k695ms/lNjb2BgYvcZ0TjPPKwX9rKjoPJlOzMVNESjZaaSCb229Rs2VEshVCbivO8OsFPiR3nH4DrfmJL2DpDDdSmjnJ0IuvzLUNg6LJeomHRf4RsGs9JwPsBxKLl9ZIhXjz7t5rCCG4GuoVqScUcwoJuZmP0+2lTvDB9VAbziuhKrMR1FuzZbaRVwBNRV0G/wKWaD+G8tDnLtB8qBj1pwHPdmzOY8d/A5BpjvvGlIaD7ktnQpDrcUddgKPBljkHVMqoV97dKtLOvwaI5xRlaXHVLD1Kjx23puXBwcj5lmF0vBnbzbk5u5zELyRyWpW5cvJrTk8c6s/lGnXPvu/0zlGIQwPb2kE5CySeB+rj0h2ejU3eynEoeb5NdOKlRNFnIn5OHJUeJZOtfa0CHNF37+XaJaqCxHLB4smXSab84xbFXstSz0do3WW8eH7Xghyx9U4mnh/ISXiXtyPTPn1tD0nlIJR7tgr7X//mykGqpSVVY10/DWUE2Mm2KYXSlH1Ywt/fImnKxWvbyc=", n.Signature);
		}

		[TestMethod]
		public void Notification_Constructor_ParsesValidNotificationDataWithTransactionId()
		{
			var n = new OnlineEftposNotification("merchantOrderId=d387fd32-ca20-467c-8693-1c73dea9806e&status=AUTHORISED&transactionId=f2b22ebf-f2b4-490d-a537-a3a745b95a8c&signature=KN4C8fZQYY9opQI9NxUl%2BA9HyEh%2F5f%2Feid7Q00Iv815c%2FqugR3%2BjR6braSKUEacJLOJV0hojXAQ59mmjfw%2BtZKCCeYVQd%2Bqt%2FI%2Fn28ZNUEwr%2Fdb4UEiblIf0bz2aPJiGp6L%2FxGdIO7xD47JVR%2FWSy43wv%2FweLgWfrjwaiDJJD8K5Bv8dwPVC%2Ft%2BgmRW4CN2rk547Xja7aKOwxVze1kwtwGvaVtNsUQOWlKZhkcQLDmTgT1gAOCCHa6DCJUSZHzwiHvkltaMtgDqeFZv5AmayX14iiueOrUf24dHYi68YLuu9czCK%2Bc4FIZ4RHlir21hYUcCTzsqokAL1UpO%2BzGu%2BefWZi53CHQdlthNSrBWEVpmgi8d4zAq4FTvioPkosj%2BOefMaXTBCDKRus%2F3UrOWFWWIcEo%2F74yUotiUUkdhG%2FVnillyc7KvT98oIG0bh04byeVFE377WK%2Bur2kFHTZofb%2B8SK%2FRS8oEn%2FOSEK25MJDLHrOcCy3l4BkX3%2FW9lmS8ysUdnoHtzlvWrcJlfcjOCXxyl1LSUkZbTxou8xyUX02Vyn54nG8yOiNNZd10VxJ%2BZALcS0YgTUb73gfcNJWe6u7JcIuRDr9SJvvrfVuKR34Vq3rF%2FUheEBH%2B7Mzu5N%2BJyKJ%2BsUPSgczQRZM9%2B5KhR0DeM2hLWTlATCsY%2FzuK7CtA%3D");

			Assert.AreEqual("d387fd32-ca20-467c-8693-1c73dea9806e", n.MerchantOrderId);
			Assert.AreEqual("f2b22ebf-f2b4-490d-a537-a3a745b95a8c", n.TransactionId);
			Assert.AreEqual("AUTHORISED", n.Status);
			Assert.AreEqual(Uri.UnescapeDataString("KN4C8fZQYY9opQI9NxUl%2BA9HyEh%2F5f%2Feid7Q00Iv815c%2FqugR3%2BjR6braSKUEacJLOJV0hojXAQ59mmjfw%2BtZKCCeYVQd%2Bqt%2FI%2Fn28ZNUEwr%2Fdb4UEiblIf0bz2aPJiGp6L%2FxGdIO7xD47JVR%2FWSy43wv%2FweLgWfrjwaiDJJD8K5Bv8dwPVC%2Ft%2BgmRW4CN2rk547Xja7aKOwxVze1kwtwGvaVtNsUQOWlKZhkcQLDmTgT1gAOCCHa6DCJUSZHzwiHvkltaMtgDqeFZv5AmayX14iiueOrUf24dHYi68YLuu9czCK%2Bc4FIZ4RHlir21hYUcCTzsqokAL1UpO%2BzGu%2BefWZi53CHQdlthNSrBWEVpmgi8d4zAq4FTvioPkosj%2BOefMaXTBCDKRus%2F3UrOWFWWIcEo%2F74yUotiUUkdhG%2FVnillyc7KvT98oIG0bh04byeVFE377WK%2Bur2kFHTZofb%2B8SK%2FRS8oEn%2FOSEK25MJDLHrOcCy3l4BkX3%2FW9lmS8ysUdnoHtzlvWrcJlfcjOCXxyl1LSUkZbTxou8xyUX02Vyn54nG8yOiNNZd10VxJ%2BZALcS0YgTUb73gfcNJWe6u7JcIuRDr9SJvvrfVuKR34Vq3rF%2FUheEBH%2B7Mzu5N%2BJyKJ%2BsUPSgczQRZM9%2B5KhR0DeM2hLWTlATCsY%2FzuK7CtA%3D"), n.Signature);
		}

		#endregion

	}
}
