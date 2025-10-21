using System.Diagnostics;
using System.Security.Claims;
using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Ports;
using FuerzaG.Infrastructure.Security;
using FuerzaG.Pages;
using Microsoft.AspNetCore.Authentication;

namespace FuerzaG.Application.Services;

public class LoginService
{
    private readonly ILoginRepository _loginRepository;
    private readonly UserAccountService _userAccountService;
    private readonly IPasswordService _passwordService;
    private readonly IMailSender _mailSender;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public LoginService(ILoginRepository loginRepository,  
        UserAccountService userAccountService, 
        IPasswordService passwordService,
        IMailSender mailSender,
        IHttpContextAccessor httpContextAccessor)
    {
        _loginRepository = loginRepository;
        _userAccountService = userAccountService;
        _passwordService = passwordService;
        _mailSender = mailSender;
        _httpContextAccessor = httpContextAccessor;
    }

    public bool IsUserNameUsed(string userName)
    {
        return _loginRepository.IsUserNameUsed(userName);
    }
    
    public UserAccount? GetByUserName(string userName)
    {
        return _loginRepository.GetByUserName(userName);
    }

    public bool CreateUserAccount(UserAccount userAccount)
    {
        userAccount.UserName = _userAccountService.GenerateUserName(userAccount);
        if (IsUserNameUsed(userAccount.UserName))
        {
            return false;
        }
        var password = _passwordService.GenerateRandomPassword();
        userAccount.Password = _passwordService.HashPassword(password);
        
        SendEmail(userAccount.Name, userAccount.UserName, userAccount.Email, password);
        return _userAccountService.Create(userAccount) > 0;
    }

    public async Task<bool> LogIn(string username, string password)
    {
        UserAccount ? userAccount = GetByUserName(username);
        if (!VerifyCredentials(userAccount, password))
        {
            return false;
        }
        
        await SetupAuthentication(userAccount);
        
        return true;
    }

    private bool VerifyCredentials(UserAccount userAccount, string password)
    {
        if (userAccount == null || !_passwordService.VerifyPassword(password, userAccount.Password))
        {
            return false;
        }
        return true;
    }

    public UserSessionData GetCurrentUser()
    {
        var user = _httpContextAccessor.HttpContext.User;
        if (user?.Identity?.IsAuthenticated != true)
        {
            return null;
        }

        var userData = new UserSessionData
        {
            UserId = Int32.Parse(user.FindFirst(ClaimTypes.Sid).Value),
            Username = user.FindFirst(ClaimTypes.NameIdentifier).Value,
            Role = user.FindFirst(ClaimTypes.Role).Value,
        };
        return userData;
    }

    private async Task SetupAuthentication(UserAccount userAccount)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userAccount.UserName),
            new Claim(ClaimTypes.Sid, userAccount.Id.ToString()),
            new Claim(ClaimTypes.Role, userAccount.Role),
        };

        var identity = new ClaimsIdentity(claims, "GForceAuth");
        var principal = new  ClaimsPrincipal(identity);

        await _httpContextAccessor.HttpContext.SignInAsync(
            "GForceAuth",
            principal,
            new AuthenticationProperties { IsPersistent = false }
        );
    }

    private void SendEmail(string name, string username, string email, string password)
    {
        string subject = "Bienvenido a FuerzaG";
        string body = $@"
            <h1>Hola {name}!</h1>
            <p>Tu nombre de usuario es: <strong>{username}</strong></p>
            <p>Tu contraseña es: <strong>{password}</strong></p>
            <p>Ya puedes iniciar sesión en el sistema. Recuerda cuidarla como las llaves de tu casa</p>
        ";
        _mailSender.SendEmail(email, subject, body);

    }
}