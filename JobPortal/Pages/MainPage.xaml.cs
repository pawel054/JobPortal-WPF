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
    /// Logika interakcji dla klasy MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private Frame mainFrame;
        private int userID;
        public MainPage(Frame mainFrame, int userID)
        {
            InitializeComponent();
            this.mainFrame = mainFrame;
            this.userID = userID;
            itemsControl.DataContext = DatabaseOffer.GetLatestAddedOffers();
            cmbCompany.DisplayMemberPath = "Name";
            cmbCategory.DisplayMemberPath = "Name";
            cmbCompany.ItemsSource = DatabaseAdmin.GetAllCompanies();
            cmbCategory.ItemsSource = DatabaseAdmin.GetAllCategories();
        }

        private void OfferBoxClicked(object sender, MouseButtonEventArgs e)
        {
            var test = ((FrameworkElement)sender).DataContext;
            if(test is Offer offer)
            {
                mainFrame.Content = new OfferPage(mainFrame, offer.OfferID, userID);
            }
        }

        private void SearchButton(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new SearchPage(mainFrame, txtStanowisko.Text, cmbCompany.Text, cmbCategory.Text, userID);
        }
    }
}
