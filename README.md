# 🧪 Prueba Técnica
 
Aplicación Blazor con autenticación JWT, seguridad de endpoints y notificaciones por correo.
 
---
 
## ⚙️ Configuración
 
Crea el archivo `appsettings.json` en la raíz del proyecto con el siguiente contenido:
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
    "Password": "Tu clave de aplicación en Gmail",
    "From": "example@gmail.com"
  },
  "AllowedHosts": "*"
}
```

> [!NOTE]
> Para generar la clave JWT en base64 puedes apoyarte de <a href="https://www.base64encode.org/" target="_blank">encode</a> o puedes usar:
> ```bash
> openssl rand -base64 32
> ```
> Para la contraseña de correo, genera una <a href="https://myaccount.google.com/apppasswords" target="_blank">clave de aplicación en Gmail</a>.
 
---
 
## 🚀 Pasos para ejecutar
 
### 1. Instalar dependencias
```bash
dotnet restore
```
 
### 2. Crear un usuario
```bash
dotnet run --register-user
```
 
### 3. Iniciar la aplicación
```bash
dotnet run
```
 
---

## ✨ Características
 
- 🔐 **Seguridad de endpoints** — La ruta `/profile` es una página protegida; redirige al login si no hay sesión activa.
- 🔑 **Hash de contraseñas** — Se utiliza **Argon2** para hashear las contraseñas. Al iniciar sesión, se hashea el valor ingresado y se compara con el hash almacenado en la base de datos (sin revertir en ningún momento).
- 📧 **Notificaciones por correo** — Se envía un correo automático cuando el usuario supera el número máximo de intentos fallidos al iniciar sesión.
