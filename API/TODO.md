# opret appsettings.json file

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "ConnectionString": "Your Connection String"
  },
  "AllowedHosts": "*",
  "JwtSettings": {
    "SecretKey": "Your secret",
    "Issuer": "MyApp",
    "Audience": "MyAppUsers",
    "ExpiryMinutes": 60
  }
}

Inkluder aldrig følsomme oplysninger som forbindelsesstrenge eller hemmelige nøgler i din kodebase.
Gem dem i stedet et sikker sted, som i dine miljøvariabler eller i en hemmelig nøgle manager.