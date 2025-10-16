using FuerzaG.Domain.Entities;

namespace FuerzaG.Domain.Services.Validations;

public class AccountValidator : IValidator<UserAccount>
{
    private readonly List<string> _errors = new();

    public Result Validate(UserAccount entity)
    {
        _errors.Clear();

        SanitizeAccountFields(entity);

        ValidateName(entity.Name);
        ValidateFirstLastName(entity.FirstLastName);
        ValidateSecondLastName(entity.SecondLastName);
        ValidateDocumentNumber(entity.DocumentNumber);
        ValidateEmail(entity.Email);

        return _errors.Count == 0
            ? Result.Success()
            : Result.Failure(_errors);
    }

    public void SanitizeAccountFields(UserAccount account)
    {
        account.Name = account.Name.Trim();
        account.FirstLastName = account.FirstLastName.Trim();
        account.SecondLastName = account.SecondLastName?.Trim();
        account.Email = account.Email?.Trim();
        account.DocumentNumber = account.DocumentNumber.Trim();
    }

    public void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            _errors.Add("El nombre es requerido");
            return;
        }

        if (name.Length < 3)
        {
            _errors.Add("El nombre debe tener al menos 3 caracteres");
        }

        if (name.Length > 100)
        {
            _errors.Add("El nombre no puede superar los 100 caracteres");
        }

        if (!char.IsLetter(name[0]))
        {
            _errors.Add("El nombre debe comenzar con una letra");
        }

        var prohibitedCharacters = new[] { '<', '>', '/', '\\', '|' };
        if (name.Any(c => prohibitedCharacters.Contains(c)))
        {
            _errors.Add("El nombre contiene caracteres no permitidos");
        }

        if (name.Any(char.IsDigit))
        {
            _errors.Add("El nombre no puede contener números");
        }
    }

    public void ValidateFirstLastName(string firstLastName)
    {
        if (string.IsNullOrWhiteSpace(firstLastName))
            _errors.Add("El primer apellido es requerido");
        var prohibitedCharacters = new[] { '<', '>', '/', '\\', '|', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        if (firstLastName.Any(c => prohibitedCharacters.Contains(c)))
        {
            _errors.Add("El segundo apellido no puede contener números ni caracteres especiales");
        }
    }

    public void ValidateSecondLastName(string? secondLastName)
    {
        if (string.IsNullOrWhiteSpace(secondLastName))
            return;

        var prohibitedCharacters = new[] { '<', '>', '/', '\\', '|', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        if (secondLastName.Any(c => prohibitedCharacters.Contains(c)))
        {
            _errors.Add("El segundo apellido no puede contener números ni caracteres especiales");
        }
    }

    public void ValidateDocumentNumber(string documentNumber)
    {
        if (string.IsNullOrWhiteSpace(documentNumber))
        {
            _errors.Add("El número de documento es requerido");
            return;
        }

        if (documentNumber.Length < 8 || documentNumber.Length > 14)
        {
            _errors.Add("El número de documento debe tener entre 8 y 14 caracteres");
        }
    }

    public void ValidateEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return; // Opcional

        if (!email.Contains("@"))
        {
            _errors.Add("El correo electrónico debe contener '@'");
        }
    }

    public bool HasPermission(UserAccount account, string requiredRole)
    {
        if (string.IsNullOrEmpty(account.Role))
            return false;

        return string.Equals(account.Role.Trim(), requiredRole.Trim(), StringComparison.OrdinalIgnoreCase);
    }
}
