using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace TP3__ASP.NET.Pages
{
    public class demandeModel : PageModel
    {
        public string nom { get; set; } = "";
        public string email { get; set; } = "";
        public double Longueur { get; set; }
        public double Largeur { get; set; }
        public int NbType { get; set; }
        public bool HasData { get; set; }
        public int nbType { get; set; }
        public double superficie { get; set; }
        public double taux_installation, taux_materiaux;
        public double cout_installation, cout_materiaux;
        public double total_non_inclus, taxe_materiaux, taxe_installation, total_taxe_inclus;
        public List<Couvre_Plancher> ListCouvrePlancher = new List<Couvre_Plancher>();

        public demande clsdemande = new demande();
        public client newclient = new client();
        public string messageErreur = "";

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.;Initial Catalog=PlancherExpert;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM couvrePlanche";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            ListCouvrePlancher.Clear(); // Clear the list before adding new items

                            while (reader.Read())
                            {
                                Couvre_Plancher cvrp = new Couvre_Plancher();
                                cvrp.id = reader.GetInt32(0);
                                cvrp.Type = reader.GetString(1);
                                cvrp.Materiaux = (float)reader.GetDouble(2);
                                cvrp.MainOeuvre = (float)reader.GetDouble(3);
                                ListCouvrePlancher.Add(cvrp);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    
        public void OnPost()
        {
            newclient.Nom = Request.Form["Nom"];
            newclient.email = Request.Form["email"];

            if (float.TryParse(Request.Form["Larg"], out float valeurLargeur))
            {
                clsdemande.Largeur = valeurLargeur;
            }
            else
            {
                Console.WriteLine("La conversion de la chaîne en float a échoué.");
            }

            if (float.TryParse(Request.Form["Long"], out float valeurLongueur))
            {
                clsdemande.Longueur = valeurLongueur;
            }
            else
            {
                Console.WriteLine("La conversion de la chaîne en float a échoué.");
            }

            try
            {
                string connectionString = "Data Source=.;Initial Catalog=PlancherExpert;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Demande_client (Id_client, largeur, longueur) " +
                                 "VALUES ((SELECT Id_client FROM Client_couvre WHERE Nom_client = @Nom AND Email = @Email), @Largeur, @Longueur)";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@Nom", newclient.Nom);
                        cmd.Parameters.AddWithValue("@Email", newclient.email);
                        cmd.Parameters.AddWithValue("@Largeur", clsdemande.Largeur);
                        cmd.Parameters.AddWithValue("@Longueur", clsdemande.Longueur);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

            HasData = true;
            nom = Request.Form["Nom"];
            email = Request.Form["email"];

            if (double.TryParse(Request.Form["Long"], out double longueur))
            {
                Longueur = longueur;
            }
            else
            {
                // Handle the conversion failure if needed
            }

            if (double.TryParse(Request.Form["Larg"], out double largeur))
            {
                Largeur = largeur;
            }
            else
            {
                // Handle the conversion failure if needed
            }

            superficie = largeur * longueur;

            if (int.TryParse(Request.Form["NbType"], out int nbType))
            {
                foreach (var type in ListCouvrePlancher)
                {
                    if (nbType == type.id)
                    {
                        // Perform operations related to the selected type
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("La conversion de la chaîne en int a échoué.");
            }

            Console.WriteLine(nom);
            Console.WriteLine(email);
            Console.WriteLine(Longueur);
            Console.WriteLine(Largeur);
            Console.WriteLine(nbType);

            // Rest of your code...
        }

        public class Couvre_Plancher
        {
            public int id { get; set; }
            public string Type { get; set; } = "";
            public float Materiaux { get; set; }
            public float MainOeuvre { get; set; }
        }

        public class demande
        {
            public int Id_demande { get; set; }
            public int Id_client { get; set; }
            public float Largeur { get; set; }
            public float Longueur { get; set; }
            public int id { get; set; }
        }

        public class client : demande
        {
            public String Nom { get; set; } = "";
            public string email { get; set; } = "";
        }
    }
}

