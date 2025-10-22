using System.Text.RegularExpressions;
using FuerzaG.Domain.Entities;

namespace FuerzaG.Domain.Services.Validations;

public class ServiceValidator : IValidator<Service>
{
    private readonly List<string> _errors = new();

    public Result Validate(Service entity)
    {
        _errors.Clear();

        // Normalizar entradas
        entity.Name = entity.Name?.Trim() ?? string.Empty;
        entity.Type = entity.Type?.Trim() ?? string.Empty;
        entity.Description = entity.Description?.Trim() ?? string.Empty;

        // Validaciones
        ValidateName(entity.Name);
        ValidateType(entity.Type);
        ValidatePrice(entity.Price);
        ValidateDescription(entity.Description);

        return _errors.Count == 0
            ? Result.Success()
            : Result.Failure(_errors);
    }

    private void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            _errors.Add("El nombre del servicio es obligatorio");
            return;
        }

        if (name.Length < 3)
            _errors.Add("El nombre debe tener al menos 3 caracteres");

        if (name.Length > 100)
            _errors.Add("El nombre no puede superar los 100 caracteres");

        // Permitir letras, números y espacios
        if (!Regex.IsMatch(name, @"^[\p{L}\d\s]+$"))
            _errors.Add("El nombre solo puede contener letras, números y espacios");
    }

    private void ValidateType(string type)
    {
        if (string.IsNullOrWhiteSpace(type))
        {
            _errors.Add("El tipo de servicio es obligatorio");
            return;
        }

        if (type.Length < 3)
            _errors.Add("El tipo debe tener al menos 3 caracteres");

        if (type.Length > 50)
            _errors.Add("El tipo no puede superar los 50 caracteres");
    }

    private void ValidatePrice(decimal price)
    {
        if (price < 0)
        {
            _errors.Add("El precio no puede ser negativo");
            return;
        }
        if (price == 0)
        {
            _errors.Add("El precio es obligatorio");
            return;
        }

        if (decimal.Round(price, 2) != price)
        {
            _errors.Add("El precio solo puede tener hasta dos decimales");
            return;
        }
            
    }

    
    private void ValidateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            _errors.Add("La descripción es obligatoria");
            return;
        }
        if (description.Length > 500)
            _errors.Add("La descripción no puede superar los 500 caracteres");
    }
}
