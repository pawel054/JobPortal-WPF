﻿using JobPortal.Database;
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
    /// Logika interakcji dla klasy SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        private Frame mainFrame;
        private int userID;
        public SearchPage(Frame mainFrame, string positionName, string companyName, string categoryName, int userID)
        {
            InitializeComponent();
            this.mainFrame = mainFrame;
            itemsControlOffer.DataContext = DatabaseOffer.GetOfferBySearch(positionName, companyName, categoryName);
        }

        private void OfferClicked(object sender, MouseButtonEventArgs e)
        {
            var button = (Border)sender;
            int offerID = (int)button.Tag;
            mainFrame.Content = new OfferPage(mainFrame, offerID, userID);
        }
    }
}
