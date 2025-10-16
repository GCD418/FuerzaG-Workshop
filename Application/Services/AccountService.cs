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

    public List<Account> GetAll()
    {
        return _dataRepositoryFactory.GetRepository<Account>().GetAll();
    }

    public Account? GetById(int id)
    {
        return _dataRepositoryFactory.GetRepository<Account>().GetById(id);
    }

    public int Create(Account account)
    {
        SanitizeAccountFields(account);

        account.UserName = GenerateUserName(account);
        account.Password = GeneratePassword(account);

        return _dataRepositoryFactory.GetRepository<Account>().Create(account);
    }


    public bool Update(Account account)
    {
        SanitizeAccountFields(account);

        return _dataRepositoryFactory.GetRepository<Account>().Update(account);
    }

    public bool DeleteById(int id)
    {
        return _dataRepositoryFactory.GetRepository<Account>().DeleteById(id);
    }
    private string GenerateUserName(Account account)
    {
        var firstName = account.Name.Split(' ')[0];
        var firstLetter = firstName[0]; 
        var firstLastName = account.FirstLastName;
        var docNumber = account.DocumentNumber;
        var last3 = docNumber[^3..];

        return $"{firstLetter}{firstLastName}.{last3}";
    }

    private string GeneratePassword(Account account)
    {
        return account.DocumentNumber;
    }

    private void SanitizeAccountFields(Account account)
    {
        account.Name = account.Name.Trim();
        account.FirstLastName = account.FirstLastName.Trim();
        account.SecondLastName = account.SecondLastName?.Trim();
        account.Email = account.Email.Trim();
        account.DocumentNumber = account.DocumentNumber.Trim();
    }
}