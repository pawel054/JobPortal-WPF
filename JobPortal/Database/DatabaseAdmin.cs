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
    public class DatabaseAdmin
    {
        readonly private static string dbpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JobPortalDatabase.db");

        public static void InsertOffer(Offer offer)
        {
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "INSERT INTO oferta VALUES(NULL, @CompanyID, @CategoryID, @PositionName, @PositionLevel, @ContractType, @JobType, @WorkingTime, @Salary, @WorkingDays, @WorkingHours, @ExpirationDate, @ImageSrc);";
                insertCommand.Parameters.AddWithValue("@CompanyID", offer.Company.CompanyID);
                insertCommand.Parameters.AddWithValue("@CategoryID", offer.Category.CategoryID);
                insertCommand.Parameters.AddWithValue("@PositionName", offer.NazwaStanowiska);
                insertCommand.Parameters.AddWithValue("@PositionLevel", offer.PoziomStanowiska);
                insertCommand.Parameters.AddWithValue("@ContractType", offer.RodzajUmowy);
                insertCommand.Parameters.AddWithValue("@JobType", offer.RodzajPracy);
                insertCommand.Parameters.AddWithValue("@WorkingTime", offer.WymiarZatrudnienia);
                insertCommand.Parameters.AddWithValue("@Salary", offer.Wynagrodzenie);
                insertCommand.Parameters.AddWithValue("@WorkingDays", offer.DniPracy);
                insertCommand.Parameters.AddWithValue("@WorkingHours", offer.GodzinyPracy);
                insertCommand.Parameters.AddWithValue("@ExpirationDate", offer.DataWaznosci);
                insertCommand.Parameters.AddWithValue("@ImageSrc", offer.SciezkaObraz);
                insertCommand.ExecuteReader();
            }
        }

        public static void UpdateOffer(Offer offer)
        {
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "UPDATE oferta SET firma_id = @CompanyID, kategoria_id = @CategoryID, nazwa_stanowiska = @PositionName, poziom_stanowiska = @PositionLevel, rodzaj_umowy = @ContractType, rodzaj_pracy = @JobType, wymiar_zatrudnienia = @WorkingTime, wynagrodzenie = @Salary, dni_pracy = @WorkingDays, godziny_pracy = @WorkingHours, data_waznosci = @ExpirationDate, obraz_src = @ImageSrc WHERE oferta_id = @ID;";
                insertCommand.Parameters.AddWithValue("@ID", offer.OfferID);
                insertCommand.Parameters.AddWithValue("@CompanyID", offer.Company.CompanyID);
                insertCommand.Parameters.AddWithValue("@CategoryID", offer.Category.CategoryID);
                insertCommand.Parameters.AddWithValue("@PositionName", offer.NazwaStanowiska);
                insertCommand.Parameters.AddWithValue("@PositionLevel", offer.PoziomStanowiska);
                insertCommand.Parameters.AddWithValue("@ContractType", offer.RodzajUmowy);
                insertCommand.Parameters.AddWithValue("@JobType", offer.RodzajPracy);
                insertCommand.Parameters.AddWithValue("@WorkingTime", offer.WymiarZatrudnienia);
                insertCommand.Parameters.AddWithValue("@Salary", offer.Wynagrodzenie);
                insertCommand.Parameters.AddWithValue("@WorkingDays", offer.DniPracy);
                insertCommand.Parameters.AddWithValue("@WorkingHours", offer.GodzinyPracy);
                insertCommand.Parameters.AddWithValue("@ExpirationDate", offer.DataWaznosci);
                insertCommand.Parameters.AddWithValue("@ImageSrc", offer.SciezkaObraz);
                insertCommand.ExecuteReader();
            }
        }

        public static void RemoveOffer(int Id)
        {
            string filePath = GetOfferImagePath(Id);
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "DELETE FROM oferta WHERE oferta_id=@ID;";
                insertCommand.Parameters.AddWithValue("@ID", Id);
                insertCommand.ExecuteReader();
            }
            if (filePath != null)
            {
                File.SetAttributes(filePath, FileAttributes.Normal);
                File.Delete(filePath);
            }
        }

        private static string GetOfferImagePath(int iD)
        {
            string path = null;
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "SELECT obraz_src FROM oferta WHERE oferta_id=@ID";
                insertCommand.Parameters.AddWithValue("@ID", iD);
                using (SqliteDataReader reader = insertCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string imgPath = reader.GetString(0);
                        path = imgPath;
                    }
                    return path;
                }
            }
        }

        public static int GetLatesOfferId()
        {
            int Id = 0;
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "SELECT oferta_id FROM oferta ORDER BY oferta_id DESC LIMIT 1;";
                using (SqliteDataReader reader = insertCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int kategoriaID = reader.GetInt32(0);
                        Id = kategoriaID;
                    }
                    return Id+1;
                }
            }
        }



        public static List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "SELECT * FROM kategoria";
                using (SqliteDataReader reader = insertCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int kategoriaID = reader.GetInt32(0);
                        string Nazwa = reader.GetString(1);

                        Category category = new Category(kategoriaID, Nazwa);
                        categories.Add(category);
                    }
                    return categories;
                }
            }
        }

        public static List<Company> GetAllCompanies()
        {
            List<Company> companies = new List<Company>();

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "SELECT * FROM firma";
                using (SqliteDataReader reader = insertCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int firmaID = reader.GetInt32(0);
                        string Nazwa = reader.GetString(1);
                        string Adres = reader.GetString(1);
                        string Opis = reader.GetString(2);

                        Company company = new Company(firmaID, Nazwa, Adres, Opis);
                        companies.Add(company);
                    }
                    return companies;
                }
            }
        }

    }
}
