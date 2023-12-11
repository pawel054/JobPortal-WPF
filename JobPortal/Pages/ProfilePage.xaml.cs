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

namespace JobPortal.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage(int userID, string email)
        {
            InitializeComponent();
            LoadDataFromDatabase(userID, email);
        }

        private void LoadDataFromDatabase(int userID, string email)
        {
            Profile profile = DatabaseProfile.GetProfileByID(userID).FirstOrDefault();
            profilePicture.Source = new BitmapImage(new Uri(profile.ProfilePictureSrc, UriKind.Absolute));
            txtNameSurname.Text = profile.Name + " " + profile.Surname;
            txtBirthDate.Text = profile.BirthDate.ToString();
            txtEmail.Text = email;
            txtPhoneNumber.Text = profile.PhoneNumber;
            txtAdress.Text = profile.Adress;

            txtCurrentPosition.Text = profile.WorkPosition;
            txtCurrentPositionDescription.Text = profile.WorkPositionDescription;
            txtSummary.Text = profile.CareerSummary;
        }
    }
}
