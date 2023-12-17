using JobPortal.Pages;
using JobPortal.Pages.Admin;
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
using System.Windows.Shapes;

namespace JobPortal.AppWindows
{
    /// <summary>
    /// Logika interakcji dla klasy AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private int userId;
        private string email;

        public AdminWindow()
        {
            InitializeComponent();
            adminFrame.Content = new AdminMain(adminFrame);
        }
        public AdminWindow(int userId, string email)
        {
            InitializeComponent();
            this.userId = userId;
            this.email = email;
            adminFrame.Content = new AdminMain(adminFrame);
        }

        private void AdminMainClicked(object sender, MouseButtonEventArgs e)
        {
            adminFrame.Content = new AdminMain(adminFrame);
            pageTitle.Text = "Panel główny";
        }

        private void AdminOfferClicked(object sender, MouseButtonEventArgs e)
        {
            adminFrame.Content = new AdminOffer();
            pageTitle.Text = "Ogłoszenia";
        }

        private void AdminCompanyClicked(object sender, MouseButtonEventArgs e)
        {

        }

        private void ViewChangeButton(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(1, "1", true);
            mainWindow.Show();
            this.Close();
        }
    }
}
