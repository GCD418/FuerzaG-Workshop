using FuerzaG.Factories;
using FuerzaG.Factories.ConcreteCreators;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages;

public class Owner : PageModel
{
    public List<Owner> Owners { get; set; } = [];
    private readonly DataRepositoryFactory _dataRepositoryFactory;

    public Owner(IDbConnectionFactory connectionFactory)
    {
        _dataRepositoryFactory = new OwnerRepositoryCreator(connectionFactory);
    }
    public void OnGet()
    {
        Owners = _dataRepositoryFactory.GetRepository<Owner>().GetAll();
    }
}