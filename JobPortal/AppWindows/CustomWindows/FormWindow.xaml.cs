using JobPortal.Class;
using JobPortal.Database;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace JobPortal.AppWindows.CustomWindows
{
    /// <summary>
    /// Logika interakcji dla klasy FormWindow.xaml
    /// </summary>
    public partial class FormWindow : Window
    {
        private string selectedFilePath = null;
        private int offerID;
        public FormWindow()
        {
            InitializeComponent();
            cmbCategory.DisplayMemberPath = "Name";
            cmbCategory.SelectedIndex = 0;
            cmbCompany.DisplayMemberPath = "Name";
            cmbCompany.SelectedIndex = 0;
            cmbCategory.ItemsSource = DatabaseAdmin.GetAllCategories();
            cmbCompany.ItemsSource = DatabaseAdmin.GetAllCompanies();
        }

        public FormWindow(int offerID)
        {
            InitializeComponent();
            cmbCategory.DisplayMemberPath = "Name";
            cmbCategory.SelectedIndex = 0;
            cmbCompany.DisplayMemberPath = "Name";
            cmbCompany.SelectedIndex = 0;
            cmbCategory.ItemsSource = DatabaseAdmin.GetAllCategories();
            cmbCompany.ItemsSource = DatabaseAdmin.GetAllCompanies();

            Offer offer = DatabaseOffer.GetOfferByID(offerID).FirstOrDefault();
            this.offerID = offerID;
            windowTitle.Text = "Edytuj ofertę";

            txtPosition.Text = offer.NazwaStanowiska;
            txtSalary.Text = offer.Wynagrodzenie;
            txtWorkingDays.Text = offer.DniPracy;
            txtWorkingHours.Text = offer.GodzinyPracy;
            datePicker.Text = offer.DataWaznosci.ToString();
            cmbPositionLevel.SelectedValue = offer.PoziomStanowiska;
            cmbContract.SelectedItem = offer.RodzajUmowy;
            cmbEtat.SelectedItem = offer.WymiarZatrudnienia;
            cmbWorkingType.SelectedItem = offer.RodzajPracy;
            cmbCategory.SelectedItem = offer.Category;
            cmbCompany.SelectedItem = offer.Company;

            btnAdd.Visibility = Visibility.Collapsed;
            btnEdit.Visibility = Visibility.Visible;
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

        private void SelectFileButton(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pliki JPEG (*.jpg)|*.jpg|Pliki PNG (*.png)|*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                selectedFilePath = openFileDialog.FileName;
                btnFile.Content = System.IO.Path.GetFileName(selectedFilePath);
            }
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                string ImagePath = CreateFilePath();
                var SelectedItemCategory = cmbCategory.SelectedItem as Category;
                var SelectedItemCompany = cmbCompany.SelectedItem as Company;
                DatabaseAdmin.InsertOffer(new Offer(SelectedItemCompany, SelectedItemCategory, txtPosition.Text, cmbPositionLevel.Text, cmbContract.Text, cmbWorkingType.Text, cmbEtat.Text, txtSalary.Text, txtWorkingDays.Text, txtWorkingHours.Text, DateTime.Parse(datePicker.Text), ImagePath));
                System.IO.File.Copy(selectedFilePath, ImagePath, true);
            }
            else
            {
                MessageBox.Show("niepoprawne dane");
            }
        }

        private void EditButton(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                if(CreateFilePath() != "0")
                {
                    string ImagePath = CreateFilePath();
                    var SelectedItemCategory = cmbCategory.SelectedItem as Category;
                    var SelectedItemCompany = cmbCompany.SelectedItem as Company;
                    DatabaseAdmin.UpdateOffer(new Offer(offerID, SelectedItemCompany, SelectedItemCategory, txtPosition.Text, cmbPositionLevel.Text, cmbContract.Text, cmbWorkingType.Text, cmbEtat.Text, txtSalary.Text, txtWorkingDays.Text, txtWorkingHours.Text, DateTime.Parse(datePicker.Text), ImagePath));
                    System.IO.File.Copy(selectedFilePath, ImagePath, true);
                }
            }
        }

        private bool IsValid()
        {
            bool isOk = true;
            if (string.IsNullOrWhiteSpace(txtPosition.Text) || string.IsNullOrWhiteSpace(txtSalary.Text) || string.IsNullOrWhiteSpace(txtWorkingDays.Text) || string.IsNullOrWhiteSpace(txtWorkingHours.Text) || cmbPositionLevel.SelectedIndex == 0 || cmbContract.SelectedIndex == 0 || cmbEtat.SelectedIndex == 0 || cmbWorkingType.SelectedIndex == 0)
            {
                isOk = false;
            }
            else
            {
                if (!Regex.IsMatch(txtSalary.Text, @"^\d+(?:\s*-\s*\d+)?$")) { isOk = false; MessageBox.Show("!23"); }
                if (!Regex.IsMatch(txtWorkingHours.Text, @"^\d{1,2}:\d{2}(?:\s*-\s*\d{1,2}:\d{2})?$")) { isOk = false; MessageBox.Show("!23"); }
            }
            
            return isOk;
        }

        private string CreateFilePath()
        {
            if(selectedFilePath != null)
            {
                string destinationFolderPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Imgs\\Upload"));
                string fileName = "offerImg_" + DatabaseAdmin.GetLatesOfferId();
                string fileExtension = System.IO.Path.GetExtension(selectedFilePath);
                string destinationFilePath = System.IO.Path.Combine(destinationFolderPath, fileName + fileExtension);

                return destinationFilePath;
            }
            return "0";
        }
    }
}
