using JobPortal.AppWindows;
using JobPortal.Class;
using JobPortal.Database;
using JobPortal.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JobPortal
{
    /// <summary>
    /// Logika interakcji dla klasy LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private Window mainWindow;
        private Frame mainFrame;
        private int userID;
        private string email;
        public LoginWindow(Window mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            loginPanel.Visibility = Visibility.Visible;
            userPanel.Visibility = Visibility.Collapsed;
        }

        public LoginWindow(Frame mainFrame, Window mainWindow, int userID, string email, bool isAdmin)
        {
            InitializeComponent();
            this.mainFrame = mainFrame;
            this.mainWindow = mainWindow;
            this.userID = userID;
            this.email = email;
            loginPanel.Visibility = Visibility.Collapsed;
            userPanel.Visibility = Visibility.Visible;

            if (isAdmin) btnAdminPanel.Visibility = Visibility.Visible;
        }

        private void CloseWindowButton(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = TimeSpan.FromSeconds(0.4)
            };

            animation.Completed += (s, _) => Close();

            BeginAnimation(OpacityProperty, animation);
        }

        private void LoginToAccountButton(object sender, MouseButtonEventArgs e)
        {
            registerBtn.Visibility = Visibility.Collapsed;
            loginBtn.Visibility = Visibility.Visible;
            windowTitle.Text = "Zaloguj się";
            run1.Text = "Nie masz jeszcze konta?";
            run2.Text = "Utwórz konto";
            run2.MouseLeftButtonDown += new MouseButtonEventHandler(CreateAccountButton);
        }

        private void CreateAccountButton(object sender, MouseButtonEventArgs e)
        {
            registerBtn.Visibility = Visibility.Visible;
            loginBtn.Visibility = Visibility.Collapsed;
            windowTitle.Text = "Utwórz konto";
            run1.Text = "Masz już konto?";
            run2.Text = "Zaloguj się";
            run2.MouseLeftButtonDown += new MouseButtonEventHandler(LoginToAccountButton);
        }

        private void RegisterButton(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string pass = txtPassword.Password;

            bool status_ok = true;

            if (string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Wypełnij wszystkie pola!");
                status_ok = false;
            }

            if (pass.Length < 3 || pass.Length > 60)
            {
                MessageBox.Show("Hasło może zawierać od 3 do 60 znaów!");
                status_ok = false;
            }

            if (!Regex.IsMatch(email, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"))
            {
                status_ok = false;
                MessageBox.Show("E-mail ma niepoprawny format!");
            }
            if (status_ok)
            {
                DatabaseCreator.AddNewUser(new User(email, HashPassword(pass)));
            }
        }

        private void LoginButton(object sender, RoutedEventArgs e)
        {
            DatabaseCreator.LogIn(new User(txtEmail.Text, txtPassword.Password), mainWindow);

        }

        private void HidePasswordPlaceholder(object sender, RoutedEventArgs e)
        {
            placeholder_password.Visibility = Visibility.Hidden;
            if (txtPassword.Password == "")
            {
                placeholder_password.Visibility = Visibility.Visible;
            }
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            txtPassword.Focus();
        }

        private string HashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return hashedPassword;
        }

        private void ProfileButton(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new ProfilePage(userID, email);
            this.Close();
        }

        private void LogoutButton(object sender, RoutedEventArgs e)
        {
            MainWindow newWindow = new MainWindow();
            newWindow.Show();
            mainWindow.Close();
        }

        private void ProfileAdminButton(object sender, RoutedEventArgs e)
        {
           AdminWindow adminWindow = new AdminWindow(userID, email);
           adminWindow.Show();
            mainWindow.Close();
           this.Close();
        }

        private void ProfilOffersButton(object sender, RoutedEventArgs e)
        {

        }
    }
}
