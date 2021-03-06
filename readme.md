# To Run The Application

## Build and Restore

This application is built using DotNetCore 2.2, and Visual Studio 2017.  To build and run this application from the command line, please run the following:

```
dotnet restore
```
```
dotnet build
```
```
dotnet ef database update
```
```
dotnet run
```

## SendGrid Configuration
This application requires that a SendGrid API key be set before it can be run.  You'll need to provision an API key from your SendGrid account prior to setting this.  Enter the API key in place of the {YOUR KEY VALUE HERE} placeholder in the command below:

```
dotnet user-secrets set "SendGrid:Key" "{YOUR KEY VALUE HERE}"
```

## Braintree Configuration
This application requires that a Braintree API key be set before it can be run.  You'll need to provision an API key from your own Braingree account prior to running.

```
dotnet user-secrets set "Braintree:Environment" "sandbox"
dotnet user-secrets set "Braintree:MerchantId" "st8...."
dotnet user-secrets set "Braintree:PublicKey" "8os8...."
dotnet user-secrets set "Braintree:PrivateKey" "gd5b5....."
```

## SmartyStreets Configuration
This application requires that a SmartyStreets Token and ID be set before it can be run.  You'll need to provision your own Token and ID from SmartyStreets.
```
dotnet user-secrets set "SmartyStreets:AuthID" "st8...."
dotnet user-secrets set "SmartyStreets:AuthToken" "8os8...."
```

