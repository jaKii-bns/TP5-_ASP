using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using static TP3__ASP.NET.Pages.demandeModel;

namespace TP3__ASP.NET.Pages.Couvreplancher
{
    public class DeleteModel : PageModel
    {
        public string messageErreur = "";
        public Couvre_Plancher cvrdelete = new Couvre_Plancher();

        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=db_plancheur;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM couvrePlanche WHERE id_couvre=@id;";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cvrdelete.id = reader.GetInt32(0);
                                cvrdelete.Type = reader.GetString(1);
                                cvrdelete.Materiaux = (float)reader.GetDouble(2);
                                cvrdelete.MainOeuvre = (float)reader.GetDouble(3);
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
            String id = Request.Query["id"];
            try
            {
                string connectionString = "Data Source=.;Initial Catalog=PlancherExpert;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "DELETE FROM couvrePlanche WHERE id_couvre=@id;";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
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