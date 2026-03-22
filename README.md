# Prueba técnica

Crea el archivo `appsettings.json`:
```JSON
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;database=test;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "Tu palabra clave en base 64",
    "Issuer": "BlazorApp",
    "Audience": "BlazorApp",
    "ExpirationHours": 24
  },
  "Email": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "Username": "example@gmail.com",
    "Password": "Tu clave de aplicación",
    "From": "example@gmail.com"
  },
  "AllowedHosts": "*"
}
```

### Pasos
- Crear nuevo usuario: ```dotnet run --register-user```.
- Descargar dependencias: ```dotnet restore```.
- Iniciar: ```dotnet run```.
