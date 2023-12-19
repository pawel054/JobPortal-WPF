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

        public static void UpdateOffer(Offer offer, bool updateImg)
        {
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                if (updateImg) { insertCommand.CommandText = "UPDATE oferta SET firma_id = @CompanyID, kategoria_id = @CategoryID, nazwa_stanowiska = @PositionName, poziom_stanowiska = @PositionLevel, rodzaj_umowy = @ContractType, rodzaj_pracy = @JobType, wymiar_zatrudnienia = @WorkingTime, wynagrodzenie = @Salary, dni_pracy = @WorkingDays, godziny_pracy = @WorkingHours, data_waznosci = @ExpirationDate, obraz_src = @ImageSrc WHERE oferta_id = @ID;"; insertCommand.Parameters.AddWithValue("@ImageSrc", offer.SciezkaObraz); }
                else insertCommand.CommandText = "UPDATE oferta SET firma_id = @CompanyID, kategoria_id = @CategoryID, nazwa_stanowiska = @PositionName, poziom_stanowiska = @PositionLevel, rodzaj_umowy = @ContractType, rodzaj_pracy = @JobType, wymiar_zatrudnienia = @WorkingTime, wynagrodzenie = @Salary, dni_pracy = @WorkingDays, godziny_pracy = @WorkingHours, data_waznosci = @ExpirationDate WHERE oferta_id = @ID;";
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
                File.Delete(filePath);
            }
        }

        public static void InsertOfferTables(string tableName, string value, int offerID)
        {
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "INSERT INTO @Table VALUES(NULL, @Value, @ID);";
                insertCommand.Parameters.AddWithValue("@Table", tableName);
                insertCommand.Parameters.AddWithValue("@@Value", value);
                insertCommand.Parameters.AddWithValue("@ID", offerID);
                insertCommand.ExecuteReader();
            }
        }

        public static void InsertCategory(Category category)
        {
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "INSERT INTO kategoria VALUES(NULL, @Name);";
                insertCommand.Parameters.AddWithValue("@Name", category.Name);
                insertCommand.ExecuteReader();
            }
        }

        public static void UpdateCategory(Category category)
        {
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "UPDATE kategoria SET nazwa = @Name WHERE kategoria_id=@ID;";
                insertCommand.Parameters.AddWithValue("@Name", category.Name);
                insertCommand.Parameters.AddWithValue("@ID", category.CategoryID);
                insertCommand.ExecuteReader();
            }
        }

        public static void RemoveCategory(int Id)
        {
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "DELETE FROM kategoria WHERE kategoria_id=@ID;";
                insertCommand.Parameters.AddWithValue("@ID", Id);
                insertCommand.ExecuteReader();
            }
        }

        public static void InsertCompany(Company company)
        {
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "INSERT INTO firma VALUES(NULL, @Name, @Adress, @Info);";
                insertCommand.Parameters.AddWithValue("@Name", company.Name);
                insertCommand.Parameters.AddWithValue("@Adress", company.Adress);
                insertCommand.Parameters.AddWithValue("@Info", company.Description);
                insertCommand.ExecuteReader();
            }
        }

        public static void UpdateCompany(Company company)
        {
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "UPDATE firma SET nazwa = @Name, adres = @Adress, informacja = @Info WHERE firma_id=@ID;";
                insertCommand.Parameters.AddWithValue("@Name", company.Name);
                insertCommand.Parameters.AddWithValue("@Adress", company.Adress);
                insertCommand.Parameters.AddWithValue("@Info", company.Description);
                insertCommand.Parameters.AddWithValue("@ID", company.CompanyID);
                insertCommand.ExecuteReader();
            }
        }

        public static void RemoveCompany(int Id)
        {
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "DELETE FROM firma WHERE firma_id=@ID;";
                insertCommand.Parameters.AddWithValue("@ID", Id);
                insertCommand.ExecuteReader();
            }
        }

        public static List<Category> GetCategoryByID(int id)
        {
            List<Category> categories = new List<Category>();

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "SELECT * FROM kategoria WHERE kategoria_id=@ID";
                insertCommand.Parameters.AddWithValue("@ID", id);
                using (SqliteDataReader reader = insertCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int categoryID = reader.GetInt32(0);
                        string name = reader.GetString(1);

                        Category readCategory = new Category(categoryID, name);
                        categories.Add(readCategory);
                    }
                    return categories;
                }
            }
        }

        public static List<Company> GetCompanyByID(int id)
        {
            List<Company> companies = new List<Company>();

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "SELECT * FROM firma WHERE firma_id=@ID";
                insertCommand.Parameters.AddWithValue("@ID", id);
                using (SqliteDataReader reader = insertCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int companyID = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string adress = reader.GetString(2);
                        string information = reader.GetString(3);

                        Company company = new Company(companyID, name, adress, information);
                        companies.Add(company);
                    }
                    return companies;
                }
            }
        }

        private static string GetOfferImagePath(int iD)
        {
            string path = null;
            string ImageFullPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Imgs\\Upload\\"));
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
                        string imgPath = Path.Combine(ImageFullPath + reader.GetString(0));
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
                        string Adres = reader.GetString(2);
                        string Opis = reader.GetString(3);

                        Company company = new Company(firmaID, Nazwa, Adres, Opis);
                        companies.Add(company);
                    }
                    return companies;
                }
            }
        }

        public static List<Company> GetLatestRecords_Company()
        {
            List<Company> companies = new List<Company>();

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "SELECT * FROM firma LIMIT 6;";
                using (SqliteDataReader reader = insertCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int firmaID = reader.GetInt32(0);
                        string Nazwa = reader.GetString(1);
                        string Adres = reader.GetString(2);
                        string Opis = reader.GetString(3);

                        Company company = new Company(firmaID, Nazwa, Adres, Opis);
                        companies.Add(company);
                    }
                    return companies;
                }
            }
        }

        public static List<Offer> GetLatestRecords_Offer()
        {
            List<Offer> offers = new List<Offer>();

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "SELECT * FROM oferta INNER JOIN firma USING(firma_id) LIMIT 6;";
                using (SqliteDataReader reader = insertCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int offerID = reader.GetInt32(0);
                        string Nazwa = reader.GetString(3);
                        string nazwaFirmy = reader.GetString(13);

                        Company company = new Company(nazwaFirmy);

                        Offer offer = new Offer(offerID, company, Nazwa);
                        offers.Add(offer);
                    }
                    return offers;
                }
            }
        }

        public static int GetCountOfRecords(string tableName)
        {
            int count = 0;
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = $"SELECT COUNT(*) FROM {tableName}";
                using (SqliteDataReader reader = insertCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int result = reader.GetInt32(0);
                        count = result;
                    }
                    return count;
                }
            }
        }

    }
}
