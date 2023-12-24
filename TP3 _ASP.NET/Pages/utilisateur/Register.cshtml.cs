using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TP3__ASP.NET.Pages.utilisateur;

namespace TP3__ASP.NET.Pages.utilisateur
{
	public class RegisterModel : PageModel
	{
		public Utilisateur utilisateurData = new Utilisateur();
		public string msgError = "";
		public void OnGet() { }
		public void OnPost()
		{
			utilisateurData.firstName = Request.Form["firstName"];
			utilisateurData.lastName = Request.Form["lastName"];
			utilisateurData.userLogin = Request.Form["email"];
			utilisateurData.userPw = Request.Form["pw"];

			if (utilisateurData.firstName == "" || utilisateurData.lastName == "" ||
			 utilisateurData.userLogin == "" || utilisateurData.userPw == "")
			{
				msgError = "Veuillez saisir vos données";
				return;
			}
			else
			{
				Response.Redirect("/utilisateur/Login");
			}

			if (!utilisateurData.AjouterUtilisateur())
			{
				HttpContext.Session.SetString("userNotAdded", "Erreur d'inscription, Essayer plus tard");
				Response.Redirect("/utilisateur/Logout");
			}
			else
			{
				Response.Redirect("/utilisateur/Login");
			}
		}
	}
}