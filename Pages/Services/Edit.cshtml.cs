using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using FuerzaG.Domain.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;
using Microsoft.AspNetCore.Authorization;

namespace FuerzaG.Pages.Services;


[Authorize(Roles = UserRoles.Manager)]
public class EditModel : PageModel
{
    private readonly ServiceRepositoryCreator _creator;
    private readonly IDataProtector _protector;

    public EditModel(IDbConnectionFactory connectionFactory, IDataProtectionProvider provider)
    {
        _creator = new ServiceRepositoryCreator(connectionFactory);
        _protector = provider.CreateProtector("ServiceProtector");
    }

    [BindProperty] public string EncryptedId { get; set; } = string.Empty;
    [BindProperty] public int Id { get; set; }
    [BindProperty] public string Name { get; set; } = string.Empty;
    [BindProperty] public string Type { get; set; } = string.Empty;
    [BindProperty] public decimal Price { get; set; }
    [BindProperty] public string Description { get; set; } = string.Empty;
    [BindProperty] public bool IsActive { get; set; }

    public IActionResult OnGet(string id)
    {
        var decryptedId = int.Parse(_protector.Unprotect(id));
        var repo = _creator.GetRepository<Service>();
        var s = repo.GetById(decryptedId);
        if (s is null) return RedirectToPage("/Services/ServicePage");

        EncryptedId = id;
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

        var repo = _creator.GetRepository<Service>();
        var ok = repo.Update(new Service
        {
            Id = Id,
            Name = Name,
            Type = Type,
            Price = Price,
            Description = Description,
            IsActive = IsActive
        });

        if (!ok)
        {
            ModelState.AddModelError(string.Empty, "No se pudo actualizar el servicio.");
            return Page();
        }

        return RedirectToPage("/Services/ServicePage");
    }
}
