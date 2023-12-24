using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;

namespace TP3__ASP.NET.Pages.utilisateur

{
	public class LoginModel : PageModel
	{
		public Utilisateur utilisateurData = new Utilisateur();
		public string msgError = "";

		public void OnGet()
		{
			HttpContext.Session.Remove("userNotExist");
			HttpContext.Session.Remove("userPwError");
		}

		public void OnPost()
		{
			utilisateurData.userLogin = Request.Form["email"];
			utilisateurData.userPw = Request.Form["pw"];

			if (utilisateurData.userLogin == "" || utilisateurData.userPw == "")
			{
				msgError = "Veuillez saisir Votre login et votre mot de passe";
				return;
			}

			if (!utilisateurData.VerifierUtilisateurExiste())
			{
				HttpContext.Session.SetString("userNotExist", "Cet utilisateur n'existe pas");
				return;
			}

			if (!utilisateurData.VerifierPw())
			{
				HttpContext.Session.SetString("userPwError", "Le mot de passe saisi est incorrect");
				return;
			}
			else
			{
				HttpContext.Session.SetString("userAuthenticated", "true");
				HttpContext.Session.SetString("userId", Convert.ToString(utilisateurData.id));
				HttpContext.Session.SetString("userFirstName", Convert.ToString(utilisateurData.firstName));
				HttpContext.Session.SetString("userLastName", Convert.ToString(utilisateurData.lastName));
				HttpContext.Session.SetString("userLogin", Convert.ToString(utilisateurData.userLogin));
				HttpContext.Session.SetString("userType", Convert.ToString(utilisateurData.userType));
				Response.Redirect("/Produit/Index");
			}
		}
	}

	public class Utilisateur
	{
		public int id;
		public string firstName = "";
		public string lastName = "";
		public string userLogin = "";
		public string userPw = "";
		public string userType = "";
		public bool VerifierUtilisateurExiste()
		{
			try
			{
				string connectionString = "Data Source=.;Initial Catalog=PlancherExpert;Integrated Security=True;Encrypt=False";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM users WHERE userLogin=@Login";
					using (SqlCommand cmd = new SqlCommand(sql, connection))
					{
						cmd.Parameters.AddWithValue("@Login", this.userLogin);
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							if (reader.Read())
								return true;
						}
					}
				}
				return false;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.ToString());
				return false;
			}
		}
		public bool VerifierPw()
		{
			try
			{
				string connectionString = "Data Source=.;Initial Catalog=PlancherExpert;Integrated Security=True;Encrypt=False";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM users WHERE userLogin=@Login AND userPw = @Pw";

					using (SqlCommand cmd = new SqlCommand(sql, connection))
					{
						cmd.Parameters.AddWithValue("@Login", this.userLogin);
						cmd.Parameters.AddWithValue("@Pw", this.userPw);
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							if (reader.Read())
							{
								this.id = reader.GetInt32(0);
								this.firstName = reader.GetString(1);
								this.lastName = reader.GetString(2);
								this.userType = reader.GetString(5);
								return true;
							}
						}
					}
				}
				return false;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.ToString());
				return false;
			}
		}
		public bool AjouterUtilisateur()
		{
			try
			{
				string connectionString = "Data Source=.;Initial Catalog=PlancherExpert;Integrated Security=True;Encrypt=False";
                

                using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "INSERT INTO users (firstName,lastName,userLogin,userPw,userType) values(@firstName,@lastName,@userLogin,@userPw,@userType);";
					using (SqlCommand cmd = new SqlCommand(sql, connection))
					{
						cmd.Parameters.AddWithValue("@firstName", this.firstName);
						cmd.Parameters.AddWithValue("@lastName", this.lastName);
						cmd.Parameters.AddWithValue("@userLogin", this.userLogin);
						cmd.Parameters.AddWithValue("@userPw", this.userPw);
						cmd.Parameters.AddWithValue("@userType", "user");
						cmd.ExecuteNonQuery();
						return true;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.ToString());
				return false;
			}
		}
	}
}