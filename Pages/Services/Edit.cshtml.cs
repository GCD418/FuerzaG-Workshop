using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Services.Validations;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;
using Microsoft.AspNetCore.Authorization;

using System.ComponentModel.DataAnnotations;
namespace FuerzaG.Pages.Services;
using System.Globalization;
using System.Text.RegularExpressions;



[Authorize(Roles = UserRoles.Manager)]
public class EditModel : PageModel
{
    private readonly ServiceRepositoryCreator _creator;
    private readonly IDataProtector _protector;
    private readonly IValidator<Service> _validator;

    public List<string> ValidationErrors { get; set; } = [];

    public EditModel(IDbConnectionFactory connectionFactory, IDataProtectionProvider provider, IValidator<Service> validator)
    {
        _creator = new ServiceRepositoryCreator(connectionFactory);
        _protector = provider.CreateProtector("ServiceProtector");
        _validator = validator;
    }

    [BindProperty] public string EncryptedId { get; set; } = string.Empty;
    [BindProperty] public int ServiceId { get; set; }
    [BindProperty] public string Name { get; set; } = string.Empty;
    [BindProperty] public string Type { get; set; } = string.Empty;
    [BindProperty, Display(Name = "Precio")] public decimal Price { get; set; }
    [BindProperty] public bool IsActive { get; set; }

    [BindProperty, Display(Name = "Descripción")]
    [Required(ErrorMessage = "La descripción es obligatoria")]
    public string Description { get; set; } = string.Empty;

    public IActionResult OnGet(string id)
    {
        var decryptedId = int.Parse(_protector.Unprotect(id));
        var repo = _creator.GetRepository<Service>();
        var service = repo.GetById(decryptedId);
        if (service is null)
            return RedirectToPage("/Services/ServicePage");

        EncryptedId = id;
        ServiceId = service.Id;
        Name = service.Name;
        Type = service.Type;
        Price = service.Price;
        Description = service.Description;
        IsActive = service.IsActive;

        return Page();
    }

    public IActionResult OnPost()
    {
        // Validación de binding
        
        var rawPrice = (Request.Form["Price"].ToString() ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(rawPrice))
        {
            ValidationErrors = new() { "El precio es obligatorio." };
            return Page();
        }

        rawPrice = new string(rawPrice.Where(c => char.IsDigit(c) || c == ',' || c == '.' || c == '-').ToArray());

        decimal parsedPrice;
        if (!decimal.TryParse(rawPrice, NumberStyles.Number, CultureInfo.GetCultureInfo("es-BO"), out parsedPrice) &&
            !decimal.TryParse(rawPrice, NumberStyles.Number, CultureInfo.GetCultureInfo("es-ES"), out parsedPrice) &&
            !decimal.TryParse(rawPrice, NumberStyles.Number, CultureInfo.InvariantCulture, out parsedPrice))
        {
            
            ValidationErrors = new() { "El precio SOLO puede contener números y separador decimal." };
            return Page();
        }
        // if (!ModelState.IsValid)
        // {
        //     ValidationErrors = ModelState.Values
        //         .SelectMany(v => v.Errors)
        //         .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage)
        //             ? "Entrada inválida."
        //             : e.ErrorMessage)
        //         .ToList();
        //     return Page();
        // }

        Price = parsedPrice;

        var candidate = new Service
        {
            Id = ServiceId,
            Name = Name?.Trim() ?? string.Empty,
            Type = Type?.Trim() ?? string.Empty,
            Price = Price,
            Description = Description?.Trim() ?? string.Empty,
            IsActive = IsActive
        };

        var result = _validator.Validate(candidate);
        if (result.IsFailure)
        {
            ValidationErrors = result.Errors;
            return Page();
        }

        var repo = _creator.GetRepository<Service>();
        var ok = repo.Update(candidate);

        if (!ok)
        {
            var current = repo.GetById(candidate.Id);
            if (current is not null &&
                current.Name == candidate.Name &&
                current.Type == candidate.Type &&
                current.Price == candidate.Price &&
                current.Description == candidate.Description &&
                current.IsActive == candidate.IsActive)
            {
                return RedirectToPage("/Services/ServicePage");
            }

            ValidationErrors = new() { "No se pudo actualizar el servicio." };
            return Page();
        }

        return RedirectToPage("/Services/ServicePage");
    }
}