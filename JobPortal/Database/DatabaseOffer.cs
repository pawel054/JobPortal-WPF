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
                        string img_src = reader.GetString(12);

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
                        string img_src = reader.GetString(12);

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

    }
}
