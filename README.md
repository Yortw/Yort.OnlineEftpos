# Yort.OnlineEFTPOS
An **unofficial**, light-weight, .Net wrapper around the [Paymark](https://www.paymark.co.nz/) [Online EFTPOS API](http://docs.dev.paymark.nz/oe). Makes it easy to integrate to the API from .Net applications and services. 

## What does it do?
Amongst other things;

* Allows requesting payment, sending refunds, checking transaction status by id, searching for transaction status.
* Performs digital signature verification for notifications (not available on WinRT/UWP).
* Provides models that (de)serialise from/to the correct json for requests/responses.
* Handles OAuth 2.0 authentication/token refresh etc (though credential management is still your problem).
* Turns error responses into exceptions.
* Normalises & validates payer ids.
* Makes it easy to switch code between sandbox/UAT/production environments for accurate testing.
* Uses interfaces for main components allowing for unit testing of calling code.
* Uses injectable HttpClient for controllable HTTP pipeline and setup.

[![GitHub license](https://img.shields.io/github/license/mashape/apistatus.svg)](https://github.com/Yortw/Yort.OnlineEFTPOS/blob/master/LICENSE.md) 

## What Doesn't it do?
Obviously lots of things, but specifically this library is *NOT* designed to;

* Provide credential management.
* Eliminate the need to read docs, understand the API and how to use it.
* Provide a high level design that ensures a robust payment integration, i.e correctly implementing rechecks, handling accepted/declined states and implementing auto-refund if necessary should be handled at the application level.

## Supported Platforms
Currently;

* .Net Framework 4.0
* .Net Framework 4.5+
* .Net Standard 1.3 (supports ASP.Net Core)
* Xamarin.iOS
* Xamarin.Android
* WinRT/UWP  

## Build Status
[![Build status](https://ci.appveyor.com/api/projects/status/ul7cdbsb5jqhv2nj?svg=true)](https://ci.appveyor.com/project/Yortw/yort-onlineeftpos)

## How do I use Yort.OnlineEFTPOS?
There is a very basic sample console application included in the repository, though you'll need to plugin your merchant id and API credentials to run it. 
For details on what the API does and how it works, see the [Online EFTPOS API documentation](http://docs.dev.paymark.nz/oe).

There is also [reference documentation for this library's API](https://yortw.github.io/Yort.OnlineEftpos/reference/index.html)

### Quick start

Install the Nuget package like this;

```powershell
    PM> Install-Package Yort.OnlineEFTPOS
```

[![NuGet Badge](https://buildstats.info/nuget/Yort.OnlineEFTPOS)](https://yortw.github.io/Yort.OnlineEftpos/reference/api/Yort.OnlineEftpos.html)

Create a credential provider initialising it with your API credentials;

*although the difference in security is marginal, use the secure credential provider on platforms that support it. 
This sample uses the non-secure version for simplicity.*

```C#
var credentialProvider = new OnlineEftposCredentialsProvider(new OnlineEftposCredentials("yourkey", "yoursecret"));
```

Then create a client, specifying the credential provider, target API version and API environment;
```C#
IOnlineEftposClient client = new OnlineEftposClient(credentialProvider, 
    OnlineEftposApiVersion.Latest, 
    OnlineEftposApiEnvironment.Sandbox
);
```

Now you can request a payment. The simple but long winded way involves passing every value on every request;
```C#
    try
    {
        OnlineEftposPaymentStatus result = await client.RequestPayment(new OnlineEftposPaymentRequest()
        {
            Bank = new BankDetails()
            {
                BankId = "ASB",
                PayerIdType = "MOBILE",
                PayerId = "021555123"
            },
            Merchant = new MerchantDetails()
            {
                CallbackUrl = new Uri("https://www.mycoolsite.com/Payments/Notification"),
                MerchantIdCode = "yourmechantid"
                MerchantUrl = new Uri("http://www.mycoolsite.co.nz")
            },
            Transaction = new PaymentDetails()
            {
                Amount = 1000,
                Currency = "NZD",
                Description = "Test Tran",
                OrderId = "Order1",
                UserAgent = "users user agent string",
                UserIPAddress = "users ip address"
            }
        });

        //Result contains the stauts of the requested payment
        //Handle the result/poll for status updates if neccesary here.
    }
    catch (OnlineEftposInvalidDataException idex)
    {
        // An error was detected with the request prior to sending it to the server
        // either edit & retry or treat as declined.
    }
    catch (OnlineEftposApiError apiex)
    {
        // An error response was received from the API
        // Confirm status via recheck, obtain error details from apiex.
    }
```

If you're making multiple requests over time with mostly the same criteria, you can use the request builder to pre-load most of the request details.
Note the request builder also takes care of converting decimal amounts into the integer version required by the API for you.

```C#
//Create and store a reference to the request builder somewhere (maybe your IoC)
_RequestBuilder = new OnlineEftposRequestBuilder()
{
    CallbackUrlTemplate = "https://www.mycoolsite.com/payments/callback?reference={orderId}",
    DefaultCurrency = "NZD",
    DefaultMerchantIdCode = "02d48cb1-784e-41aa-a05f-5d3e9698ce47",
    DefaultCurrencyMultiplier = 100,
    DefaultMerchantUrl = new Uri("www.mycoolsite.com", UriKind.Relative),
    DefaultUserAgent = "MyUserAgent",
    DefaultUserIP = "192.168.1.10",
    PurchaseDescriptionTemplate = "Order {orderId}",
    RefundReasonTemplate = "Refund from order {orderId}"
};

//Use the request builder to create a payment request and send it via the client
var request = _RequestBuilder.CreatePaymentRequest("021555123", "MOBILE", "ASB", "Order1", 10.00M);
var result = await client.RequestPayment(request);
```

Refunds work similarly, use CreateRefundRequest/SendRefund instead of CreatePaymentRequest/RequestPayment.
Methods for status checking and transaction search should be self-explanatory after reading the API documentation.

### Signature verification

You can validate that a notification actually came from the Paymark API rather than a malicious actor by using the notification verifier.

```C#
// Create a notification using the (string) content sent to the end point.
var notification = new OnlineEftposNotification(content);

// Create a verifier with the appropriate public key for the API environment, the notification came from. 
// This could be reused for efficiency if loaded in an IoC container etc.
// The relevant public keys can be obtained from Paymark. The key is passed in as base64 encoded string.
var verifier = new Sha512WithRsaVerifier(PublicKey);

// Now verify the notification			
if (!verifier.Verify(notification))
    throw new System.Security.SecurityException("Notification did not pass verification.");
```
