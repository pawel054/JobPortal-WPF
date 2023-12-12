using JobPortal.Class;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Database
{
    public class DatabaseSearch
    {
        readonly private static string dbpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JobPortalDatabase.db");

        public static List<Offer> GetOfferSearch(string name, )
        {
            List<Offer> offer = new List<Offer>();

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "SELECT * FROM oferta INNER JOIN firma USING(firma_id) INNER JOIN kategoria USING(kategoria_id) WHERE oferta_id=@ID";
                insertCommand.Parameters.AddWithValue("@ID", id);
                using (SqliteDataReader reader = insertCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int ofertaID = reader.GetInt32(0);
                        int firmaID = reader.GetInt32(1);
                        int kategoriaID = reader.GetInt32(2);
                        string nazwaStanowiska = reader.GetString(3);
                        string poziomStanowiska = reader.GetString(4);
                        string rodzajUmowy = reader.GetString(5);
                        string rodzajPracy = reader.GetString(6);
                        string wymiarZatrudnienia = reader.GetString(7);
                        string wynagrodzenie = reader.GetString(8);
                        string dniPracy = reader.GetString(9);
                        string godzinyPracy = reader.GetString(10);
                        DateTime dataWaznosci = reader.GetDateTime(11);
                        string img_src = reader.GetString(12);

                        string firmaNazwa = reader.GetString(13);
                        string firmaAdres = reader.GetString(14);
                        string firmaInformacja = reader.GetString(15);

                        string kategoriaNazwa = reader.GetString(16);

                        Company company = new Company(firmaID, firmaNazwa, firmaAdres, firmaInformacja);
                        Category category = new Category(kategoriaID, kategoriaNazwa);

                        Offer readOffer = new Offer(ofertaID, company, category, nazwaStanowiska, poziomStanowiska, rodzajUmowy, rodzajPracy, wymiarZatrudnienia, wynagrodzenie, dniPracy, godzinyPracy, dataWaznosci, img_src);
                        offer.Add(readOffer);
                    }
                    return offer;
                }
            }
        }
    }
}
