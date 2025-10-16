using System.Text.RegularExpressions;
using FuerzaG.Domain.Entities;

namespace FuerzaG.Domain.Services.Validations;

public class ServiceValidator : IValidator<Service>
{
    private readonly List<string> _errors = [];

    public Result Validate(Service entity)
    {
        _errors.Clear();

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
            _errors.Add("El nombre del servicio es requerido");
            return;
        }

        if (name.Length < 3)
            _errors.Add("El nombre debe tener al menos 3 caracteres");

        if (name.Length > 100)
            _errors.Add("El nombre no puede superar los 100 caracteres");

        if (!char.IsLetter(name[0]))
            _errors.Add("El nombre debe comenzar con una letra");

        var prohibited = new[] { '<', '>', '/', '\\', '|' };
        if (name.Any(c => prohibited.Contains(c)))
            _errors.Add("El nombre contiene caracteres no permitidos");
    }

    
    private void ValidateType(string type)
    {
        if (string.IsNullOrWhiteSpace(type))
        {
            _errors.Add("El tipo de servicio es requerido");
            return;
        }

        var t = type.Trim();

        var allowed = new[] { "Preventivo", "Correctivo" };

        if (!allowed.Contains(t))
            _errors.Add("El tipo debe ser Preventivo o Correctivo");
    }



    private void ValidatePrice(decimal price)
    {
        if (price < 0)
        {
            _errors.Add("El precio no puede ser negativo");
            return;
        }

        // Verifica hasta 2 decimales
        if (decimal.Round(price, 2) != price)
            _errors.Add("El precio solo puede tener hasta dos decimales");

        if (price > 1000000)
        {
            _errors.Add("El precio no puede superar 1.000.000");
        }
        
    }
    

    


    private void ValidateDescription(string? description)
    {
        if (string.IsNullOrEmpty(description))
            return;

        if (description.Length > 500)
            _errors.Add("La descripción no puede superar los 500 caracteres");

        // Evita ciertos caracteres peligrosos (igual que en Name)
        var prohibited = new[] { '<', '>', '/', '\\', '|' };
        if (description.Any(c => prohibited.Contains(c)))
            _errors.Add("La descripción contiene caracteres no permitidos");
    }
}
