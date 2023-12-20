using JobPortal.Class;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JobPortal.Database
{
    public class DatabaseOffer
    {
        readonly private static string dbpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JobPortalDatabase.db");

        public static List<Offer> GetLatestAddedOffers()
        {
            List<Offer> offers = new List<Offer>();
            string ImageFullPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Imgs\\Upload"));

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "SELECT * FROM oferta INNER JOIN firma USING(firma_id) INNER JOIN kategoria USING(kategoria_id) ORDER BY oferta_id DESC LIMIT 9;";
                using (SqliteDataReader reader = insertCommand.ExecuteReader())
                {
                    while(reader.Read())
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
                        string img_src = Path.Combine(ImageFullPath, reader.GetString(12));

                        string firmaNazwa = reader.GetString(13);
                        string firmaAdres = reader.GetString(14);
                        string firmaInformacja = reader.GetString(15);

                        string kategoriaNazwa = reader.GetString(16);

                        Company company = new Company(firmaID, firmaNazwa, firmaAdres, firmaInformacja);
                        Category category = new Category(kategoriaID, kategoriaNazwa);

                        Offer offer = new Offer(ofertaID, company, category, nazwaStanowiska, poziomStanowiska, rodzajUmowy, rodzajPracy, wymiarZatrudnienia, wynagrodzenie, dniPracy, godzinyPracy, dataWaznosci, img_src);
                        offers.Add(offer);
                    }
                    return offers;
                }
            }
        }

        public static List<Offer> GetAllOffers()
        {
            List<Offer> offers = new List<Offer>();
            string ImageFullPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Imgs\\Upload"));

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "SELECT * FROM oferta INNER JOIN firma USING(firma_id) INNER JOIN kategoria USING(kategoria_id)";
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
                        string img_src = Path.Combine(ImageFullPath, reader.GetString(12));

                        string firmaNazwa = reader.GetString(13);
                        string firmaAdres = reader.GetString(14);
                        string firmaInformacja = reader.GetString(15);

                        string kategoriaNazwa = reader.GetString(16);

                        Company company = new Company(firmaID, firmaNazwa, firmaAdres, firmaInformacja);
                        Category category = new Category(kategoriaID, kategoriaNazwa);

                        Offer offer = new Offer(ofertaID, company, category, nazwaStanowiska, poziomStanowiska, rodzajUmowy, rodzajPracy, wymiarZatrudnienia, wynagrodzenie, dniPracy, godzinyPracy, dataWaznosci, img_src);
                        offers.Add(offer);
                    }
                    return offers;
                }
            }
        }

        public static List<Offer> GetOfferByID(int id)
        {
            List<Offer> offer = new List<Offer>();
            string ImageFullPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Imgs\\Upload"));

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
                        string img_src = Path.Combine(ImageFullPath, reader.GetString(12));

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

        public static List<Offer> GetOfferBySearch(string positionName, string companyName, string categoryName)
        {
            List<Offer> offer = new List<Offer>();
            string ImageFullPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Imgs\\Upload"));

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var sqlCommand = new StringBuilder("SELECT * FROM oferta INNER JOIN firma USING(firma_id) INNER JOIN kategoria USING(kategoria_id) WHERE 1 = 1");

                if (!string.IsNullOrEmpty(positionName))
                {
                    sqlCommand.Append(" AND nazwa_stanowiska = @Name");
                }

                if (!string.IsNullOrEmpty(categoryName))
                {
                    sqlCommand.Append(" AND kategoria.nazwa = @CategoryName");
                }

                if (!string.IsNullOrEmpty(companyName))
                {
                    sqlCommand.Append(" AND firma.nazwa = @CompanyName");
                }

                var insertCommand = new SqliteCommand(sqlCommand.ToString(), db);

                if (!string.IsNullOrEmpty(positionName))
                {
                    insertCommand.Parameters.AddWithValue("@Name", positionName);
                }

                if (!string.IsNullOrEmpty(categoryName))
                {
                    insertCommand.Parameters.AddWithValue("@CategoryName", categoryName);
                }

                if (!string.IsNullOrEmpty(companyName))
                {
                    insertCommand.Parameters.AddWithValue("@CompanyName", companyName);
                }
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
                        string img_src = Path.Combine(ImageFullPath, reader.GetString(12));

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

        public static List<OfferTables> GetOfferRequirementsByID(int id)
        {
            List<OfferTables> offer = new List<OfferTables>();

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "SELECT * FROM oferta_wymagania WHERE oferta_id=@ID";
                insertCommand.Parameters.AddWithValue("@ID", id);
                using (SqliteDataReader reader = insertCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int wymogId = reader.GetInt32(0);
                        string wymog = reader.GetString(1);
                        int ofertaID = reader.GetInt32(2);

                        OfferTables readOfferTable = new OfferTables(id, wymog, ofertaID);
                        offer.Add(readOfferTable);
                    }
                    return offer;
                }
            }
        }

        public static List<OfferTables> GetOfferBenefitsByID(int id)
        {
            List<OfferTables> offer = new List<OfferTables>();

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "SELECT * FROM oferta_benefity WHERE oferta_id=@ID";
                insertCommand.Parameters.AddWithValue("@ID", id);
                using (SqliteDataReader reader = insertCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int wymogId = reader.GetInt32(0);
                        string benefit = reader.GetString(1);
                        int ofertaID = reader.GetInt32(2);

                        OfferTables readOfferTable = new OfferTables(id, benefit, ofertaID);
                        offer.Add(readOfferTable);
                    }
                    return offer;
                }
            }
        }

        public static List<OfferTables> GetOfferDutiesByID(int id)
        {
            List<OfferTables> offer = new List<OfferTables>();

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "SELECT * FROM oferta_obowiazki WHERE oferta_id=@ID";
                insertCommand.Parameters.AddWithValue("@ID", id);
                using (SqliteDataReader reader = insertCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int wymogId = reader.GetInt32(0);
                        string obowiazek = reader.GetString(1);
                        int ofertaID = reader.GetInt32(2);

                        OfferTables readOfferTable = new OfferTables(id, obowiazek, ofertaID);
                        offer.Add(readOfferTable);
                    }
                    return offer;
                }
            }
        }

        public static void InsertApplication(UserApplication application)
        {
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "INSERT INTO uzytkownik_aplikacje VALUES(NULL, @UserID, @OfferID, @Status);";
                insertCommand.Parameters.AddWithValue("@UserID", application.user.UserID);
                insertCommand.Parameters.AddWithValue("@OfferID", application.offer.OfferID);
                insertCommand.Parameters.AddWithValue("@Status", application.Status);
                insertCommand.ExecuteReader();
            }
        }

        public static List<UserApplication> GetApplicationByID(int id)
        {
            List<UserApplication> applications = new List<UserApplication>();

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "SELECT * FROM uzytkownik_aplikacje INNER JOIN uzytkownik USING(user_id) INNER JOIN oferta USING(oferta_id) WHERE user_id=@ID";
                insertCommand.Parameters.AddWithValue("@ID", id);
                using (SqliteDataReader reader = insertCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int appliactionId = reader.GetInt32(0);
                        int userId = reader.GetInt32(1);
                        int offerID = reader.GetInt32(2);
                        string status = reader.GetString(3);

                        string email = reader.GetString(4);
                        string passoword = reader.GetString(5);
                        bool isAdmin = reader.GetBoolean(6);

                        int companyID = reader.GetInt32(7);
                        int categoryID = reader.GetInt32(8);
                        string positionName = reader.GetString(9);

                        User user = new User(email, passoword, isAdmin);
                        Offer offer = new Offer(new Company(DatabaseAdmin.GetCompanyName(companyID)), positionName);

                        UserApplication userApplication = new UserApplication(user, offer, status);
                        applications.Add(userApplication);
                    }
                    return applications;
                }
            }
        }

        public static void DeleteApplication(int id)
        {
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "DELETE FROM uzytkownik_aplikacje WHERE user_id=@ID";
                insertCommand.Parameters.AddWithValue("@ID", id);
                insertCommand.ExecuteReader();
            }
        }

    }
}
