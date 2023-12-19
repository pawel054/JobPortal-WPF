using JobPortal.AppWindows.CustomWindows;
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

namespace JobPortal.Pages.Admin
{
    /// <summary>
    /// Logika interakcji dla klasy AdminOffer.xaml
    /// </summary>
    public partial class AdminOffer : Page
    {
        public AdminOffer()
        {
            InitializeComponent();
            itemsControl.DataContext = DatabaseOffer.GetAllOffers();
        }

        public AdminOffer(bool isCategory)
        {
            InitializeComponent();

            if(isCategory)
            {
                offerView.Visibility = Visibility.Collapsed;
                CategoryView.Visibility = Visibility.Visible;
                itemsControlCategory.DataContext = DatabaseAdmin.GetAllCategories();
            }
            else
            {
                offerView.Visibility = Visibility.Collapsed;
                CompanyView.Visibility = Visibility.Visible;
                itemsControlCompany.DataContext = DatabaseAdmin.GetAllCompanies();
            }
        }

        private void BtnDodajClicked(object sender, RoutedEventArgs e)
        {
            FormWindow formWindow = new FormWindow();
            formWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            formWindow.Owner = Application.Current.MainWindow;
            this.Opacity = 0.5;
            formWindow.ShowDialog();
            this.Opacity = 1;
            itemsControl.DataContext = DatabaseOffer.GetAllOffers();
        }

        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            int offerID = (int)button.CommandParameter;
            DatabaseAdmin.RemoveOffer(offerID);
            itemsControl.DataContext = DatabaseOffer.GetAllOffers();
        }

        private void EditButton(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            int offerID = (int)button.CommandParameter;

            FormWindow formWindow = new FormWindow(offerID);
            formWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            formWindow.Owner = Application.Current.MainWindow;
            this.Opacity = 0.5;
            formWindow.ShowDialog();
            this.Opacity = 1;
            itemsControl.DataContext = DatabaseOffer.GetAllOffers();
        }

        private void BtnDodajClickedCategory(object sender, RoutedEventArgs e)
        {
            FormWindow formWindow = new FormWindow(true);
            formWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            formWindow.Owner = Application.Current.MainWindow;
            formWindow.Height = 400;
            formWindow.Width = 640;
            this.Opacity = 0.5;
            formWindow.ShowDialog();
            this.Opacity = 1;
            itemsControlCategory.DataContext = DatabaseAdmin.GetAllCategories();
        }

        private void DeleteButtonCategory(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            int categoryID = (int)button.CommandParameter;
            DatabaseAdmin.RemoveCategory(categoryID);
            itemsControlCategory.DataContext = DatabaseAdmin.GetAllCategories();
        }

        private void EditButtonCategory(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            int categoryID = (int)button.CommandParameter;

            FormWindow formWindow = new FormWindow(true, categoryID);
            formWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            formWindow.Owner = Application.Current.MainWindow;
            formWindow.Height = 400;
            this.Opacity = 0.5;
            formWindow.ShowDialog();
            this.Opacity = 1;
            itemsControlCategory.DataContext = DatabaseAdmin.GetAllCategories();
        }

        private void BtnDodajClickedCompany(object sender, RoutedEventArgs e)
        {
            FormWindow formWindow = new FormWindow(false);
            formWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            formWindow.Owner = Application.Current.MainWindow;
            formWindow.Height = 690;
            this.Opacity = 0.5;
            formWindow.ShowDialog();
            this.Opacity = 1;
            itemsControlCompany.DataContext = DatabaseAdmin.GetAllCompanies();
        }

        private void EditButtonCompany(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            int companyID = (int)button.CommandParameter;

            FormWindow formWindow = new FormWindow(false, companyID);
            formWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            formWindow.Owner = Application.Current.MainWindow;
            formWindow.Height = 690;
            this.Opacity = 0.5;
            formWindow.ShowDialog();
            this.Opacity = 1;
            itemsControlCompany.DataContext = DatabaseAdmin.GetAllCompanies();
        }

        private void DeleteButtonCompany(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            int companyID = (int)button.CommandParameter;
            DatabaseAdmin.RemoveCompany(companyID);
            itemsControlCompany.DataContext = DatabaseAdmin.GetAllCompanies();
        }
    }
}
