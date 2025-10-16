# 📧 Ejemplo de Uso del SmtpEmailSender

## Configuración en appsettings.json ✅

```json
{
  "EmailSettings": {
    "SmtpHost": "smtp.mailersend.net",
    "SmtpPort": 587,
    "SmtpUser": "MS_vra0u0@test-zxk54v81mvpljy6v.mlsender.net",
    "SmtpPass": "mssp.qt2mmD5.3vz9dlekpj7lkj50.xrmqWei",
    "FromName": "FuerzaG",
    "FromEmail": "test-zxk54v81mvpljy6v.mlsender.net"
  }
}
```

## Uso en un PageModel

```csharp
using FuerzaG.Domain.Ports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages.Examples;

public class SendEmailModel : PageModel
{
    private readonly IMailSender _mailSender;

    public SendEmailModel(IMailSender mailSender)
    {
        _mailSender = mailSender;
    }

    public IActionResult OnPost(string email, string name)
    {
        try
        {
            string subject = "Bienvenido a FuerzaG";
            string htmlMessage = $@"
                <html>
                <body>
                    <h2>Hola {name}!</h2>
                    <p>Gracias por registrarte en FuerzaG.</p>
                    <p>Tu cuenta ha sido creada exitosamente.</p>
                    <br>
                    <p>Saludos,<br>El equipo de FuerzaG</p>
                </body>
                </html>
            ";

            _mailSender.SendEmail(email, subject, htmlMessage);
            
            TempData["Success"] = "Correo enviado exitosamente";
            return RedirectToPage();
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error al enviar correo: {ex.Message}";
            return Page();
        }
    }
}
```

## Uso en un Servicio de Aplicación

```csharp
using FuerzaG.Domain.Ports;
using FuerzaG.Domain.Entities;

namespace FuerzaG.Application.Services;

public class UserAccountService
{
    private readonly IMailSender _mailSender;

    public UserAccountService(IMailSender mailSender)
    {
        _mailSender = mailSender;
    }

    public void SendWelcomeEmail(UserAccount account)
    {
        string subject = "Bienvenido a FuerzaG";
        string body = $@"
            <h1>Hola {account.Name}!</h1>
            <p>Tu nombre de usuario es: <strong>{account.UserName}</strong></p>
            <p>Ya puedes iniciar sesión en el sistema.</p>
        ";

        _mailSender.SendEmail(account.Email, subject, body);
    }

    public void SendPasswordResetEmail(string email, string resetToken)
    {
        string subject = "Recuperación de contraseña";
        string body = $@"
            <h2>Recuperación de contraseña</h2>
            <p>Has solicitado restablecer tu contraseña.</p>
            <p>Tu código de verificación es: <strong>{resetToken}</strong></p>
            <p>Este código expira en 15 minutos.</p>
        ";

        _mailSender.SendEmail(email, subject, body);
    }
}
```

## Características implementadas

✅ **Envío síncrono** (sin async/await)
✅ **Manejo de errores** con excepciones descriptivas
✅ **HTML en el cuerpo del mensaje**
✅ **SSL habilitado** para seguridad
✅ **Configuración desde appsettings.json**
✅ **Inyección de dependencias** configurada en Program.cs
✅ **Arquitectura hexagonal** (IMailSender en Domain/Ports, implementación en Infrastructure)

## Notas importantes

- El método `SendEmail` es **bloqueante** (síncrono)
- Si el envío falla, lanza una `InvalidOperationException`
- El cuerpo del mensaje soporta **HTML**
- La configuración se lee automáticamente desde `appsettings.json`
