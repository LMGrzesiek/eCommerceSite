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
This application requires that a Braintree API key be set before it can be run.  You'll need to provision an API key from your own Braingree account prior to 

```
dotnet user-secrets set "Braintre:Environment"
