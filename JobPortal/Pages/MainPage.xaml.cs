﻿using System;
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
        public MainPage(Frame mainFrame)
        {
            InitializeComponent();
            this.mainFrame = mainFrame;
        }

        private void OfferBoxClicked(object sender, MouseButtonEventArgs e)
        {
            mainFrame.Content = new OfferPage(mainFrame);
        }
    }
}
