using JobPortal.Pages;
using JobPortal.Class;
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

namespace JobPortal
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Database.CreateDb();
            mainFrame.Content = new MainPage(mainFrame);
            btnZaloguj.Visibility = Visibility.Visible;
            txtLoggedIn.Visibility = Visibility.Collapsed;
        }

        public MainWindow(string email)
        {
            InitializeComponent();
            btnZaloguj.Visibility = Visibility.Collapsed;
            txtLoggedIn.Visibility = Visibility.Visible;
            txtLoggedIn.Text = email;
            mainFrame.Content = new MainPage(mainFrame);
        }

        private void ProfileTest(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new ProfilePage(mainFrame);
        }

        private void TestLogin(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            loginWindow.Owner = Application.Current.MainWindow;
            this.Opacity = 0.5;
            loginWindow.ShowDialog();
            this.Opacity = 1;
        }
    }
}
