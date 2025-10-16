using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages.Shared;

public class SecurePageModel : PageModel
{
    protected bool ValidateSession(out string role)
    {
        var user = HttpContext.Session.GetString("userName");
        role = HttpContext.Session.GetString("role")!;

        if (string.IsNullOrEmpty(user))
        {
            HttpContext.Response.Redirect("/Login");
            return false;
        }

        return true;
    }
}