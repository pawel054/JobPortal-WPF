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
    public class DatabaseProfile
    {
        readonly private static string dbpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JobPortalDatabase.db");
        public static List<Profile> GetProfileByID(int id)
        {
            List<Profile> profile = new List<Profile>();

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "SELECT * FROM profil WHERE user_id=@ID";
                insertCommand.Parameters.AddWithValue("@ID", id);
                using (SqliteDataReader reader = insertCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                                int profilID = reader.GetInt32(0);
                                int userID = reader.GetInt32(1);
                                string imie = reader.GetString(2);
                                string nazwisko = reader.GetString(3);
                                DateTime dataUrodzenia = reader.GetDateTime(4);
                                string numerTelefonu = reader.GetString(5);
                                string zdjecieProfilowe = reader.GetString(6);
                                string adres = reader.GetString(7);
                                string aktualneStanowisko = reader.GetString(8);
                                string aktualneStanowiskoOpis = reader.GetString(9);
                                string podsumowanieZawodowe = reader.GetString(10);


                                Profile readProfile = new Profile(profilID, userID, imie, nazwisko, dataUrodzenia, numerTelefonu, zdjecieProfilowe, adres, aktualneStanowisko, aktualneStanowiskoOpis, podsumowanieZawodowe);
                                profile.Add(readProfile);
                        }
                    }
                    else
                    {
                        CreateProfile(id);
                        return GetProfileByID(id);
                    }
                    return profile;
                }
            }
        }

        public static void UpdateProfile(Profile profile)
        {
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "UPDATE profil SET imie = @Name, nazwisko = @Surname, data_urodzenia = @Date, numer_telefonu = @Phone, zdjecie_profilowe = @Img, adres_zamieszkania = @Adress, aktualne_stanowisko = @Position, aktualne_stanowisko_opis = @PositionInfo, podsumowanie_zawodowe = @Summary WHERE profil_id = @ID;";
                insertCommand.Parameters.AddWithValue("@Name", profile.Name);
                insertCommand.Parameters.AddWithValue("@Surname", profile.Surname);
                insertCommand.Parameters.AddWithValue("@Date", profile.BirthDate);
                insertCommand.Parameters.AddWithValue("@Phone", profile.PhoneNumber);
                insertCommand.Parameters.AddWithValue("@Img", profile.ProfilePictureSrc);
                insertCommand.Parameters.AddWithValue("@Adress", profile.Adress);
                insertCommand.Parameters.AddWithValue("@Position", profile.WorkPosition);
                insertCommand.Parameters.AddWithValue("@PositionInfo", profile.WorkPositionDescription);
                insertCommand.Parameters.AddWithValue("@Summary", profile.CareerSummary);
                insertCommand.Parameters.AddWithValue("@ID", profile.ProfileID);
                insertCommand.ExecuteReader();
            }
        }

        public static void CreateProfile(int userId)
        {
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "INSERT INTO profil VALUES(NULL, @PID, @Empty, @Empty, @Date, @Empty, @Empty, @Empty, @Empty, @Empty, @Empty);";
                insertCommand.Parameters.AddWithValue("@PID", userId);
                insertCommand.Parameters.AddWithValue("@Empty", "brak informacji");
                insertCommand.Parameters.AddWithValue("@Date",new DateTime(1900, 1, 1));
                insertCommand.ExecuteReader();
            }
        }
    }
}
