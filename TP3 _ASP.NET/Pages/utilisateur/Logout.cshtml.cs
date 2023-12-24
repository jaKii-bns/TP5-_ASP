using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TP3__ASP.NET.Pages.utilisateur
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
			HttpContext.Session.Clear();
			Response.Redirect("/utilisateur/Login");
		}
    }
}
