using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using static TP3__ASP.NET.Pages.demandeModel;

namespace TP3__ASP.NET.Pages.Couvreplancher
{
    public class editModel : PageModel
    {
        public Couvre_Plancher cvrmodifi = new Couvre_Plancher();
        public string messageErreur = "";

        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                string connectionString = "Data Source=.;Initial Catalog=PlancherExpert;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM couvrePlanche WHERE id=@id;";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cvrmodifi.id = reader.GetInt32(0);
                                cvrmodifi.Type = reader.GetString(1);
                                cvrmodifi.Materiaux = (float)reader.GetDouble(2);
                                cvrmodifi.MainOeuvre = (float)reader.GetDouble(3);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }

        public void OnPost()
        {
            cvrmodifi.id = Convert.ToInt32(Request.Form["id"]);
            cvrmodifi.Type = Request.Form["Type"];
            cvrmodifi.Materiaux = float.Parse(Request.Form["Materiaux"]);
            cvrmodifi.MainOeuvre = float.Parse(Request.Form["MainOeuvre"]);

            if (string.IsNullOrEmpty(cvrmodifi.Type) || cvrmodifi.Materiaux == 0 || cvrmodifi.MainOeuvre == 0)
            {
                messageErreur = "Veuillez saisir le type de couvre et les valeurs de couvre plancher valide";
                return;
            }

            try
            {
                string connectionString = "Data Source=.;Initial Catalog=PlancherExpert;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE couvrePlanche SET type_couvre=@Type, Mat_couvre=@Materiaux, Main_couvre=@MainOeuvre WHERE id_couvre=@id;";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        Console.WriteLine("Modifier le produit n: " + Convert.ToString(cvrmodifi.id));
                        cmd.Parameters.AddWithValue("@id", Convert.ToString(cvrmodifi.id));

                        if (!string.IsNullOrEmpty(cvrmodifi.Type))
                        {
                            cmd.Parameters.AddWithValue("@Type", cvrmodifi.Type);
                        }
                        else
                        {
                            messageErreur = "La valeur de 'Type' est manquante.";
                            return;
                        }

                        cmd.Parameters.AddWithValue("@Materiaux", Convert.ToString(cvrmodifi.Materiaux));
                        cmd.Parameters.AddWithValue("@MainOeuvre", Convert.ToString(cvrmodifi.MainOeuvre));
                        Console.WriteLine("preparer la requêtes");

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
            Response.Redirect("/Couvreplancher/Index");
        }
    }
}