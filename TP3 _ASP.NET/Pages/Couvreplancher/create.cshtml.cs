using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using static TP3__ASP.NET.Pages.demandeModel;

namespace TP3__ASP.NET.Pages.Couvreplancher
{
    public class createModel : PageModel
    {
        public Couvre_Plancher cvrcreate = new Couvre_Plancher();
        public string messageErreur = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            cvrcreate.id = Convert.ToInt32(Request.Form["id"]);
            cvrcreate.Type = Request.Form["Type"];
            cvrcreate.Materiaux = float.Parse(Request.Form["Materiaux"]);
            cvrcreate.MainOeuvre = float.Parse(Request.Form["MainOeuvre"]);

            try
            {
                string connectionString = "Data Source=.;Initial Catalog=PlancherExpert;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO couvrePlanche(id_couvre, type_couvre, Mat_couvre, Main_couvre)" +
                        "VALUES(@id, @Type, @Materiaux, @MainOeuvre);";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", cvrcreate.id);
                        cmd.Parameters.AddWithValue("@Type", cvrcreate.Type);
                        cmd.Parameters.AddWithValue("@Materiaux", Convert.ToString(cvrcreate.Materiaux));
                        cmd.Parameters.AddWithValue("@MainOeuvre", Convert.ToString(cvrcreate.MainOeuvre));
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

            cvrcreate.id = 0;
            cvrcreate.Type = "";
            cvrcreate.Materiaux = 0.00F;
            cvrcreate.MainOeuvre = 0.00F;

            Response.Redirect("/Couvreplancher/Index");
        }
    }
}