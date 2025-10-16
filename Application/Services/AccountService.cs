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
        return _dataRepositoryFactory.GetRepository<Account>().Create(account);
    }

    public bool Update(Account account)
    {
        return _dataRepositoryFactory.GetRepository<Account>().Update(account);
    }

    public bool DeleteById(int id)
    {
        return _dataRepositoryFactory.GetRepository<Account>().DeleteById(id);
    }