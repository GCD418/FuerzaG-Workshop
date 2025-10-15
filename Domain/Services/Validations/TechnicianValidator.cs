using FuerzaG.Models;

namespace FuerzaG.Domain.Services.Validations
{
    public class TechnicianValidator : IValidator<Technician>
    {
        private readonly List<string> _errors = [];
        public Result Validate(Technician entity)
        {
            _errors.Clear();
            ValidateName(entity.Name);
            return _errors.Count == 0
                ? Result.Success()
                : Result.Failure(_errors);
        }
        private void ValidateName(string name)
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
        }
    }
}
