using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Ports;
using FuerzaG.Factories.ConcreteCreators;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;

namespace FuerzaG.Application.Services;

public class UserService
{
    private readonly DataRepositoryFactory _dataRepositoryFactory;

    public UserService(IDbConnectionFactory connectionFactory)
    {
        _dataRepositoryFactory = new UserRepositoryCreator(connectionFactory);
    }

    public List<User> GetAll()
    {
        return _dataRepositoryFactory.GetRepository<User>().GetAll();
    }

    public User? GetById(int id)
    {
        return _dataRepositoryFactory.GetRepository<User>().GetById(id);
    }

    public int Create(User user)
    {
        // Generación automática del username antes de guardar
        user.UserName = GenerateUserName(user.Name, user.FirstLastName, user.Ci);
        // Generación de una contraseña temporal
        user.Password = GenerateTemporaryPassword();
        return _dataRepositoryFactory.GetRepository<User>().Create(user);
    }

    public bool Update(User user)
    {
        return _dataRepositoryFactory.GetRepository<User>().Update(user);
    }

    public bool DeleteById(int id)
    {
        return _dataRepositoryFactory.GetRepository<User>().DeleteById(id);
    }

    // --- Métodos auxiliares ---
    private string GenerateTemporaryPassword()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 8)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    private string GenerateUserName(string name, string firstLastName, string ci)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(firstLastName) || string.IsNullOrWhiteSpace(ci))
            throw new ArgumentException("Nombre, Primer apellido y CI son requeridos para generar el nombre de usuario.");

        // Primera letra del nombre, primer apellido y últimos 3 dígitos del CI
        var firstLetter = name.Trim()[0].ToString().ToUpper();
        var lastNamePart = firstLastName.Trim().ToLower();
        var ciPart = ci.Length >= 3 ? ci[^3..] : ci; // últimos 3 dígitos o el número completo si es más corto

        return $"{firstLetter}.{lastNamePart}.{ciPart}";
    }
}