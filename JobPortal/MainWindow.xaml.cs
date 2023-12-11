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
using JobPortal.Database;

namespace JobPortal
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int userId;
        private string email;
        public MainWindow()
        {
            InitializeComponent();
            DatabaseCreator.CreateDb();
            mainFrame.Content = new MainPage(mainFrame);
            btnZaloguj.Visibility = Visibility.Visible;
            btnKonto.Visibility = Visibility.Collapsed;
        }

        public MainWindow(int userId, string email)
        {
            InitializeComponent();
            btnZaloguj.Visibility = Visibility.Collapsed;
            btnKonto.Visibility = Visibility.Visible;
            mainFrame.Content = new MainPage(mainFrame);
            this.email = email;
            this.userId = userId;
        }

        private void TestLogin(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow(this);
            loginWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            loginWindow.Owner = Application.Current.MainWindow;
            this.Opacity = 0.5;
            loginWindow.ShowDialog();
            this.Opacity = 1;
        }

        private void MenuMainPage(object sender, MouseButtonEventArgs e)
        {
            mainFrame.Content = new MainPage(mainFrame);
        }

        private void AccountButton(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow(mainFrame, userId, email);
            loginWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            loginWindow.Owner = Application.Current.MainWindow;
            this.Opacity = 0.5;
            loginWindow.ShowDialog();
            this.Opacity = 1;
        }
    }
}
