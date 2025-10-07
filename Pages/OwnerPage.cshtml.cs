using FuerzaG.Factories;
using FuerzaG.Factories.ConcreteCreators;
using FuerzaG.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages;

public class OwnerPage : PageModel
{
    public List<Owner> Owners { get; set; } = [];
    private readonly DataRepositoryFactory _dataRepositoryFactory;

    public OwnerPage(IDbConnectionFactory connectionFactory)
    {
        _dataRepositoryFactory = new OwnerRepositoryCreator(connectionFactory);
    }
    public void OnGet()
    {
        Owners = _dataRepositoryFactory.GetRepository<Owner>().GetAll();
    }
}