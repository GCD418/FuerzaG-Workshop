using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Ports;
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
        // Generaci�n autom�tica del username antes de guardar
        user.UserName = GenerateUserName(user.Name, user.FirstLastName, user.Ci);
        // Generaci�n de una contrase�a temporal
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

    // --- M�todos auxiliares ---
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

        // Primera letra del nombre, primer apellido y �ltimos 3 d�gitos del CI
        var firstLetter = name.Trim()[0].ToString().ToUpper();
        var lastNamePart = firstLastName.Trim().ToLower();
        var ciPart = ci.Length >= 3 ? ci[^3..] : ci; // �ltimos 3 d�gitos o el n�mero completo si es m�s corto

        return $"{firstLetter}.{lastNamePart}.{ciPart}";
    }
}