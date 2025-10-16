using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FuerzaG.Domain.Entities;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;
using FuerzaG.Domain.Services.Validations;

namespace FuerzaG.Pages.Services;

public class EditModel : PageModel
{
    private readonly ServiceRepositoryCreator _creator;
    private readonly IValidator<Service> _validator;

    public EditModel(IDbConnectionFactory connectionFactory, IValidator<Service> validator)
    {
        _creator = new ServiceRepositoryCreator(connectionFactory);
        _validator = validator;
    }

    [BindProperty] public int Id { get; set; }
    [BindProperty] public string Name { get; set; } = string.Empty;
    [BindProperty] public string Type { get; set; } = string.Empty;
    [BindProperty] public decimal Price { get; set; }
    [BindProperty] public string Description { get; set; } = string.Empty;
    [BindProperty] public bool IsActive { get; set; }

    
    public List<string> ValidationErrors { get; } = new();

    public IActionResult OnGet(int id)
    {
        var repo = _creator.GetRepository<Service>();
        var s = repo.GetById(id);
        if (s is null) return RedirectToPage("/Services/ServicePage");

        Id = s.Id;
        Name = s.Name;
        Type = s.Type;
        Price = s.Price;
        Description = s.Description;
        IsActive = s.IsActive;

        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        ValidationErrors.Clear();

        var updated = new Service
        {
            Id = Id,
            Name = Name,
            Type = Type,
            Price = Price,
            Description = Description,
            IsActive = IsActive
        };

        var validation = _validator.Validate(updated);
        if (!validation.IsSuccess)
        {
            ValidationErrors.AddRange(validation.Errors);
            return Page();
        }

        var repo = _creator.GetRepository<Service>();
        var ok = repo.Update(updated);

        if (!ok)
        {
            ValidationErrors.Add("No se pudo actualizar el servicio.");
            return Page();
        }

        return RedirectToPage("/Services/ServicePage");
    }
}
