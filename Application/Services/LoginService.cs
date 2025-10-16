using System.Diagnostics;
using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Ports;
using FuerzaG.Infrastructure.Security;
using FuerzaG.Pages;

namespace FuerzaG.Application.Services;

public class LoginService
{
    private readonly ILoginRepository _loginRepository;
    private readonly UserAccountService _userAccountService;
    private readonly IPasswordService _passwordService;
    private readonly ILogger<IndexModel> _logger;
    private readonly IMailSender _mailSender;
    
    public LoginService(ILoginRepository loginRepository,  
        UserAccountService userAccountService, 
        IPasswordService passwordService,
        ILogger<IndexModel> logger,
        IMailSender mailSender)
    {
        _loginRepository = loginRepository;
        _userAccountService = userAccountService;
        _passwordService = passwordService;
        _logger = logger;
        _mailSender = mailSender;
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
        
        //TODO SEND CREDENTIALS
        SendEmail(userAccount.Name, userAccount.UserName, userAccount.Email, password);
        _logger.LogInformation("Creating user account with password {password}", password);
        return _userAccountService.Create(userAccount) > 0;
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