using JobPortal.Class;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal
{
    public class Database
    {
        readonly private static string dbpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JobPortalDatabase.db");
        public static void CreateDb()
        {

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                string tableCommand =
                    @"CREATE TABLE IF NOT EXISTS firma (firma_id INTEGER PRIMARY KEY AUTOINCREMENT, nazwa TEXT, adres TEXT, informacja TEXT);" +
                    "CREATE TABLE IF NOT EXISTS kategoria (kategoria_id INTEGER PRIMARY KEY AUTOINCREMENT, nazwa TEXT);" +
                    "CREATE TABLE IF NOT EXISTS uzytkownik (user_id INTEGER PRIMARY KEY AUTOINCREMENT, email TEXT, haslo TEXT, isadmin INTEGER);" +
                    "CREATE TABLE IF NOT EXISTS oferta (oferta_id INTEGER PRIMARY KEY AUTOINCREMENT, firma_id INTEGER, kategoria_id INTEGER, nazwa_stanowiska TEXT, poziom_stanowiska TEXT, rodzaj_umowy TEXT, rodzaj_pracy TEXT, wymiar_zatrudnienia TEXT, wynagrodzenie TEXT, dni_pracy TEXT, godziny_pracy TEXT, data_waznosci DATE, FOREIGN KEY (firma_id) REFERENCES firma(firma_id) ON UPDATE CASCADE ON DELETE CASCADE, FOREIGN KEY (kategoria_id) REFERENCES kategoria(kategoria_id) ON UPDATE CASCADE ON DELETE CASCADE); " +
                    "CREATE TABLE IF NOT EXISTS profil (profil_id INTEGER PRIMARY KEY AUTOINCREMENT, user_id INTEGER, imie TEXT, nazwisko TEXT, data_urodzenia DATE, numer_telefonu TEXT, zdjecie_profilowe TEXT, adres_zamieszkania TEXT, aktualne_stanowisko TEXT, aktualne_stanowisko_opis TEXT, podsumowanie_zawodowe TEXT, FOREIGN KEY (user_id) REFERENCES uzytkownik(user_id) ON UPDATE CASCADE ON DELETE CASCADE);" +
                    "CREATE TABLE IF NOT EXISTS uzytkownik_aplikacje (aplikacja_id INTEGER PRIMARY KEY AUTOINCREMENT, user_id INTEGER, oferta_id INTEGER, status TEXT, FOREIGN KEY (oferta_id) REFERENCES oferta(oferta_id) ON UPDATE CASCADE ON DELETE NO ACTION, FOREIGN KEY (user_id) REFERENCES uzytkownik(user_id) ON UPDATE CASCADE ON DELETE NO ACTION);" +
                    "CREATE TABLE IF NOT EXISTS uzytkownik_ulubione (ulubione_id INTEGER PRIMARY KEY AUTOINCREMENT, user_id INTEGER, oferta_id INTEGER, FOREIGN KEY (oferta_id) REFERENCES oferta(oferta_id) ON UPDATE CASCADE ON DELETE NO ACTION, FOREIGN KEY (user_id) REFERENCES uzytkownik(user_id) ON UPDATE CASCADE ON DELETE NO ACTION);" +
                    "CREATE TABLE IF NOT EXISTS oferta_benefity (benefit_id INTEGER PRIMARY KEY AUTOINCREMENT, benefit TEXT, oferta_id INTEGER, FOREIGN KEY (oferta_id) REFERENCES oferta(oferta_id) ON UPDATE CASCADE ON DELETE CASCADE);" +
                    "CREATE TABLE IF NOT EXISTS oferta_wymagania (wymog_id INTEGER PRIMARY KEY AUTOINCREMENT, wymog TEXT, oferta_id INTEGER, FOREIGN KEY (oferta_id) REFERENCES oferta(oferta_id) ON UPDATE CASCADE ON DELETE CASCADE);" +
                    "CREATE TABLE IF NOT EXISTS oferta_obowiazki (obowiazek_id INTEGER PRIMARY KEY AUTOINCREMENT, obowiazek TEXT, oferta_id INTEGER, FOREIGN KEY (oferta_id) REFERENCES oferta(oferta_id) ON UPDATE CASCADE ON DELETE CASCADE);" +
                    "CREATE TABLE IF NOT EXISTS profil_certyfikaty (certyfikat_id INTEGER PRIMARY KEY AUTOINCREMENT, profil_id INTEGER, certyfikat_nazwa TEXT, organizator TEXT, certyfikat_data DATE, FOREIGN KEY (profil_id) REFERENCES profil(profil_id) ON UPDATE CASCADE ON DELETE CASCADE);" +
                    "CREATE TABLE IF NOT EXISTS profil_wyksztalcenie (wyksztalcenie_id INTEGER PRIMARY KEY AUTOINCREMENT, profil_id INTEGER, nazwa_szkoly TEXT, lokalizacja_szkoly TEXT, poziom_wyksztalcenia TEXT, kierunek TEXT, okres_od DATE, okres_do DATE, FOREIGN KEY (profil_id) REFERENCES profil(profil_id) ON UPDATE CASCADE ON DELETE CASCADE);" +
                    "CREATE TABLE IF NOT EXISTS profil_doswiadczenie (doswiadczenie_id INTEGER PRIMARY KEY AUTOINCREMENT, profil_id INTEGER, stanowisko TEXT, nazwa_firmy TEXT, lokalizacja TEXT, okres_od DATE, okres_do DATE, FOREIGN KEY (profil_id) REFERENCES profil(profil_id) ON UPDATE CASCADE ON DELETE CASCADE);" +
                    "CREATE TABLE IF NOT EXISTS profil_jezyki (jezyk_id INTEGER PRIMARY KEY AUTOINCREMENT, profil_id INTEGER, jezyk TEXT, jezyk_poziom TEXT, FOREIGN KEY (profil_id) REFERENCES profil(profil_id) ON UPDATE CASCADE ON DELETE CASCADE);" +
                    "CREATE TABLE IF NOT EXISTS profil_umiejetnosci (umiejetnosc_id INTEGER PRIMARY KEY AUTOINCREMENT, profil_id INTEGER, umiejetnosc TEXT, FOREIGN KEY (profil_id) REFERENCES profil(profil_id) ON UPDATE CASCADE ON DELETE CASCADE);" +
                    "CREATE TABLE IF NOT EXISTS profil_urls (url_id INTEGER PRIMARY KEY AUTOINCREMENT, profil_id INTEGER, url_strona TEXT, url TEXT, FOREIGN KEY (profil_id) REFERENCES profil(profil_id) ON UPDATE CASCADE ON DELETE CASCADE);";

                var createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
            }
        }

        public static void AddCategory(Category category)
        {
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "INSERT INTO kategoria VALUES(NULL, @Name);";
                insertCommand.Parameters.AddWithValue("@Name", category.CategoryName);
                insertCommand.ExecuteReader();
            }
        }
    }
}
