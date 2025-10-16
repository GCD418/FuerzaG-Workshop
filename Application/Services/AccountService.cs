using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Ports;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;

namespace FuerzaG.Application.Services;

public class AccountService
{
    private readonly DataRepositoryFactory _dataRepositoryFactory;

    public AccountService(IDbConnectionFactory connectionFactory)
    {
        _dataRepositoryFactory = new AccountRepositoryCreator(connectionFactory);
    }

    public List<UserAccount> GetAll()
    {
        return _dataRepositoryFactory.GetRepository<UserAccount>().GetAll();
    }

    public UserAccount? GetById(int id)
    {
        return _dataRepositoryFactory.GetRepository<UserAccount>().GetById(id);
    }

    public int Create(UserAccount userAccount)
    {
        SanitizeAccountFields(userAccount);

        userAccount.UserName = GenerateUserName(userAccount);
        userAccount.Password = GeneratePassword(userAccount);

        return _dataRepositoryFactory.GetRepository<UserAccount>().Create(userAccount);
    }


    public bool Update(UserAccount userAccount)
    {
        SanitizeAccountFields(userAccount);

        return _dataRepositoryFactory.GetRepository<UserAccount>().Update(userAccount);
    }

    public bool DeleteById(int id)
    {
        return _dataRepositoryFactory.GetRepository<UserAccount>().DeleteById(id);
    }
    private string GenerateUserName(UserAccount userAccount)
    {
        var firstName = userAccount.Name.Split(' ')[0];
        var firstLetter = firstName[0]; 
        var firstLastName = userAccount.FirstLastName;
        var docNumber = userAccount.DocumentNumber;
        var last3 = docNumber[^3..];

        return $"{firstLetter}{firstLastName}.{last3}";
    }

    private string GeneratePassword(UserAccount userAccount)
    {
        return userAccount.DocumentNumber;
    }

    private void SanitizeAccountFields(UserAccount userAccount)
    {
        userAccount.Name = userAccount.Name.Trim();
        userAccount.FirstLastName = userAccount.FirstLastName.Trim();
        userAccount.SecondLastName = userAccount.SecondLastName?.Trim();
        userAccount.Email = userAccount.Email.Trim();
        userAccount.DocumentNumber = userAccount.DocumentNumber.Trim();
    }
}