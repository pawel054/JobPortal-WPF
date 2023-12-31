﻿using JobPortal.Class;
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
    /// Logika interakcji dla klasy OfferPage.xaml
    /// </summary>
    public partial class OfferPage : Page
    {
        private Frame mainFrame;
        private int offeriD;
        private int userID;
        public OfferPage(Frame mainFrame, int offeriD, int userID)
        {
            InitializeComponent();
            this.mainFrame = mainFrame;
            this.offeriD = offeriD;
            this.userID = userID;
            LoadDataFromDatabase(offeriD);
        }

        private void LoadDataFromDatabase(int offerId)
        {
            Offer offer = DatabaseOffer.GetOfferByID(offerId).FirstOrDefault();
            txtStanowisko.Content = offer.NazwaStanowiska;
            txtFirma.Content = offer.Company.Name;
            offerImg.Source = new BitmapImage(new Uri(offer.SciezkaObraz, UriKind.RelativeOrAbsolute));

            detailPoziom.Text = offer.PoziomStanowiska;
            detailUmowa.Text = offer.RodzajUmowy;
            detailWymiar.Text = offer.WymiarZatrudnienia;
            detailRodzajPracy.Text = offer.RodzajPracy;
            detailDniPracy.Text = offer.DniPracy;
            detailGodzinyPracy.Text = offer.GodzinyPracy;
            detailWynagrodzenie.Text = offer.Wynagrodzenie;
            detailDataWaznosci.Text = offer.DataWaznosci.ToString("dd.MM.yyyy");

            moreAdres.Text = offer.Company.Adress;
            moreOpis.Text = offer.Company.Description;

            itemsControlObowiazki.DataContext = DatabaseOffer.GetOfferDutiesByID(offerId);
            itemsControlWymagania.DataContext = DatabaseOffer.GetOfferRequirementsByID(offerId);
            itemsControlBenefity.DataContext = DatabaseOffer.GetOfferBenefitsByID(offerId);
        }

        private void AddApplication(object sender, RoutedEventArgs e)
        {
            Offer offer = DatabaseOffer.GetOfferByID(offeriD).FirstOrDefault();
            User user = DatabaseCreator.GetUserByID(userID).FirstOrDefault();
            DatabaseOffer.InsertApplication(new UserApplication(user, offer, "Oczekuje"));
            MessageBox.Show("Złożono aplikacje");
        }
    }
}
