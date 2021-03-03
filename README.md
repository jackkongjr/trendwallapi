## Trendwallapi

This is a .NET 5.0 project.

It's a REST Service ahead of a MongoDB instance

Follows a picture from the front-end part

![immagine](https://user-images.githubusercontent.com/36534362/109786749-c6672680-7c0d-11eb-8023-ac9017c5cd9e.png)



* Here's the appsettings.json not included in this development environment commit
```
{
  "TrendsDatabaseSettings": {
    "TrendsCollectionName": "trends",
    "ConnectionString": "mongodb://user:password@MONGODB_IP_SERVER:27017",
    "DatabaseName": "SocialData"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
```
