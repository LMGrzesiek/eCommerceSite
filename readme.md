# To Run The Application



## SendGrid Configuration


This application requires that a SendGrid API key be set before it can be run.  You'll need to provision an API key from your SendGrid account prior to setting this.  Enter the API key in place of the {YOUR KEY VALUE HERE} placeholder in the command below:



```
dotnet user-secrets set "SendGrid:Key" "{YOUR KEY VALUE HERE}"
```