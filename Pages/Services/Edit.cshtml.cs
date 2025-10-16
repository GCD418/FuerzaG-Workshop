using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Services.Validations;

namespace FuerzaG.Pages.Services;

public class EditModel : PageModel
{
    private readonly ServiceRepositoryCreator _creator;
    private readonly IDataProtector _protector;
    private readonly ServiceService _serviceService;
    private readonly IValidator<Service> _validator;

    public List<string> ValidationErrors { get; set; } = [];

    public EditModel(IDbConnectionFactory connectionFactory, IDataProtectionProvider provider)
    {
        _creator = new ServiceRepositoryCreator(connectionFactory);
        _protector = provider.CreateProtector("ServiceProtector");
    }

    [BindProperty] public string EncryptedId { get; set; } = string.Empty;
    public EditModel(ServiceService serviceService, IValidator<Service> validator)
    {
        _serviceService = serviceService;
        _validator = validator;
    }


    [BindProperty] public int Id { get; set; }
    [BindProperty] public string Name { get; set; } = string.Empty;
    [BindProperty] public string Type { get; set; } = string.Empty;
    [BindProperty] public decimal Price { get; set; }
    [BindProperty] public string Description { get; set; } = string.Empty;

    public IActionResult OnGet(string id)
    {
        var decryptedId = int.Parse(_protector.Unprotect(id));
        var repo = _creator.GetRepository<Service>();
        var s = repo.GetById(decryptedId);
        if (s is null) return RedirectToPage("/Services/ServicePage");
        var existing = _serviceService.GetById(id);
        if (existing is null)
            return RedirectToPage("/Services/ServicePage");

        EncryptedId = id;
        Id = s.Id;
        Name = s.Name;
        Type = s.Type;
        Price = s.Price;
        Description = s.Description;
        IsActive = s.IsActive;
        // Cargar datos existentes
        Id = existing.Id;
        Name = existing.Name;
        Type = existing.Type;
        Price = existing.Price;
        Description = existing.Description;

        return Page();
    }

    public IActionResult OnPost()
    {
      
        var service = new Service
        {
            Id = Id,
            Name = Name,
            Type = Type,
            Price = Price,
            Description = Description,
            IsActive = true
        };

        var validationResult = _validator.Validate(service);
        if (validationResult.IsFailure)
        {
            ValidationErrors = validationResult.Errors;
            foreach (var error in validationResult.Errors)
                ModelState.AddModelError(string.Empty, error);
            return Page();
        }

        
        var success = _serviceService.Update(service);
        if (!success)
        {
            ModelState.AddModelError(string.Empty, "No se pudo actualizar el servicio.");
            return Page();
        }

        return RedirectToPage("/Services/ServicePage");
    }
}
