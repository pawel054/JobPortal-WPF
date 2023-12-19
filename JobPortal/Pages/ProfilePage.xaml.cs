using JobPortal.Class;
using JobPortal.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JobPortal.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        private int userID;
        private string email;
        private int profileID;
        public ProfilePage(int userID, string email)
        {
            InitializeComponent();
            this.userID = userID;
            this.email = email;
            LoadDataFromDatabase(userID, email);
        }

        private void LoadDataFromDatabase(int userID, string email)
        {
            Profile profile = DatabaseProfile.GetProfileByID(userID).FirstOrDefault();
            //profilePicture.Source = new BitmapImage(new Uri(profile.ProfilePictureSrc, UriKind.Absolute));
            txtNameSurname.Text = profile.Name + " " + profile.Surname;
            txtEmail.Text = email;
            txtPhoneNumber.Text = profile.PhoneNumber;
            txtAdress.Text = profile.Adress;

            if (profile.Name == "brak informacji") txtNameSurname.Text = profile.Name;
            else txtNameSurname.Text = profile.Name + " " + profile.Surname;

            if (profile.BirthDate == new DateTime(1900, 1, 1)) txtBirthDate.Text = "brak informacji";
            else txtBirthDate.Text = profile.BirthDate.ToString();

            if (profile.BirthDate == new DateTime(1900, 1, 1)) txtBirthDate.Text = "brak informacji";
            else txtBirthDate.Text = profile.BirthDate.ToString();

            txtCurrentPosition.Text = profile.WorkPosition;
            txtCurrentPositionDescription.Text = profile.WorkPositionDescription;
            txtSummary.Text = profile.CareerSummary;
        }

        private void LoadDataFromDatabaseEdit(int userID, string email)
        {
            Profile profile = DatabaseProfile.GetProfileByID(userID).FirstOrDefault();
            //profilePicture.Source = new BitmapImage(new Uri(profile.ProfilePictureSrc, UriKind.Absolute));
            txtNameSurnameEdit.Text = profile.Name + " " + profile.Surname;
            txtEmailEdit.Text = email;
            txtPhoneNumberEdit.Text = profile.PhoneNumber;
            txtAdressEdit.Text = profile.Adress;

            if (profile.Name == "brak informacji") txtNameSurnameEdit.Text = profile.Name;
            else txtNameSurnameEdit.Text = profile.Name + " " + profile.Surname;

            if (profile.BirthDate == new DateTime(1900, 1, 1)) txtBirthDateEdit.Text = "brak informacji";
            else txtBirthDateEdit.Text = profile.BirthDate.ToString();

            txtCurrentPositionEdit.Text = profile.WorkPosition;
            txtCurrentPositionDescriptionEdit.Text = profile.WorkPositionDescription;
            txtSummaryEdit.Text = profile.CareerSummary;
            profileID = profile.ProfileID;
        }

        private void InfoSaveButton(object sender, RoutedEventArgs e)
        {
            InsertInfoPositionSummary();
            gridInfo.Visibility = Visibility.Visible;
            gridInfo_Edit.Visibility = Visibility.Collapsed;
        }

        private void EditInfoButton(object sender, RoutedEventArgs e)
        {
            LoadDataFromDatabaseEdit(userID, email);
            gridInfo.Visibility = Visibility.Collapsed;
            gridInfo_Edit.Visibility = Visibility.Visible;
        }

        private void EditPositionButton(object sender, RoutedEventArgs e)
        {
            LoadDataFromDatabaseEdit(userID, email);
            gridPosition.Visibility = Visibility.Collapsed;
            gridPositionEdit.Visibility = Visibility.Visible;
        }

        private void PositionSaveButton(object sender, RoutedEventArgs e)
        {
            InsertInfoPositionSummary();
            gridPosition.Visibility = Visibility.Collapsed;
            gridPositionEdit.Visibility = Visibility.Visible;
        }

        private void InsertInfoPositionSummary()
        {
            string date = null;
            string inputText = txtNameSurnameEdit.Text;
            string[] parts = inputText.Split(' ');
            string firstName = parts[0];
            string lastName = string.Join(" ", parts.Skip(1));


            if (txtBirthDateEdit.Text == "brak informacji") date = new DateTime(1900, 1, 1).ToString();

            DatabaseProfile.UpdateProfile(new Profile(profileID, firstName, lastName, DateTime.Parse(date), txtPhoneNumberEdit.Text, profilePictureEdit.Source.ToString(), txtAdressEdit.Text, txtCurrentPositionEdit.Text, txtCurrentPositionDescriptionEdit.Text, txtSummaryEdit.Text));
            LoadDataFromDatabase(userID, email);
        }

        private void SummarySaveButton(object sender, RoutedEventArgs e)
        {
            InsertInfoPositionSummary();
            gridSummary.Visibility = Visibility.Visible;
            gridSummaryEdit.Visibility = Visibility.Collapsed;
        }

        private void EditSummaryButton(object sender, RoutedEventArgs e)
        {
            LoadDataFromDatabaseEdit(userID, email);
            gridSummary.Visibility = Visibility.Collapsed;
            gridSummaryEdit.Visibility = Visibility.Visible;
        }
    }
}
