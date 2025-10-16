using System.Diagnostics;
using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Ports;
using FuerzaG.Infrastructure.Security;

namespace FuerzaG.Application.Services;

public class LoginService
{
    private readonly ILoginRepository _loginRepository;
    private readonly UserAccountService _userAccountService;
    private readonly IPasswordService _passwordService;
    
    public LoginService(ILoginRepository loginRepository,  UserAccountService userAccountService, IPasswordService passwordService)
    {
        _loginRepository = loginRepository;
        _userAccountService = userAccountService;
        _passwordService = passwordService;
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
        userAccount.Password = password;
        //TODO SEND CREDENTIALS
        Trace.WriteLine(password);
        return _userAccountService.Create(userAccount) > 0;
    }
}