using JobPortal.Class;
using JobPortal.Database;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private int categoryID;
        private int companyID;
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
            btnFile.Content = offer.SciezkaObraz;
            SetComboboxSelectedItem(cmbPositionLevel, offer.PoziomStanowiska);
            SetComboboxSelectedItem(cmbContract, offer.RodzajUmowy);
            SetComboboxSelectedItem(cmbEtat, offer.WymiarZatrudnienia);
            SetComboboxSelectedItem(cmbWorkingType, offer.RodzajPracy);
            SetComboboxSelectedItem(cmbCategory, offer.Category.Name);
            SetComboboxSelectedItem(cmbCompany, offer.Company.Name);

            AddFromDatabaseToList(offerID);

            btnAdd.Visibility = Visibility.Collapsed;
            btnEdit.Visibility = Visibility.Visible;
        }

        public FormWindow(bool isCategory)
        {
            InitializeComponent();
            if (isCategory)
            {
                windowTitle.Text = "Dodaj kategorię";
                borderWindow.Height = 350;
                btnAdd.Visibility = Visibility.Collapsed;
                btnAddCategory.Visibility = Visibility.Visible;
                offerPanel.Visibility = Visibility.Collapsed;
                categoryPanel.Visibility = Visibility.Visible;
            }
            else
            {
                windowTitle.Text = "Dodaj firmę";
                borderWindow.Height = 590;
                btnAdd.Visibility = Visibility.Collapsed;
                btnAddCompany.Visibility = Visibility.Visible;
                offerPanel.Visibility = Visibility.Collapsed;
                companyPanel.Visibility = Visibility.Visible;
            }
        }

        public FormWindow(bool isCategory, int id)
        {
            InitializeComponent();
            if (isCategory)
            {
                categoryID = id;
                windowTitle.Text = "Edytuj kategorię";
                Category category = DatabaseAdmin.GetCategoryByID(categoryID).FirstOrDefault();
                borderWindow.Height = 350;
                txtCategoryName.Text = category.Name;
                btnAdd.Visibility = Visibility.Collapsed;
                btnEditCategory.Visibility = Visibility.Visible;
                offerPanel.Visibility = Visibility.Collapsed;
                categoryPanel.Visibility = Visibility.Visible;
            }
            else
            {
                companyID = id;
                windowTitle.Text = "Edytuj firmę";
                Company company = DatabaseAdmin.GetCompanyByID(companyID).FirstOrDefault();
                borderWindow.Height = 590;
                txtCompanyName.Text = company.Name;
                txtCompanyAdress.Text = company.Adress;
                txtCompanyInfo.Text = company.Description;
                btnAdd.Visibility = Visibility.Collapsed;
                btnEditCompany.Visibility = Visibility.Visible;
                offerPanel.Visibility = Visibility.Collapsed;
                companyPanel.Visibility = Visibility.Visible;
            }
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
                if (selectedFilePath != null)
                {
                    string ImagePath = CreateFilePath(true);
                    var SelectedItemCategory = cmbCategory.SelectedItem as Category;
                    var SelectedItemCompany = cmbCompany.SelectedItem as Company;
                    AddOfferDetails(DatabaseAdmin.InsertOffer(new Offer(SelectedItemCompany, SelectedItemCategory, txtPosition.Text, cmbPositionLevel.Text, cmbContract.Text, cmbWorkingType.Text, cmbEtat.Text, txtSalary.Text, txtWorkingDays.Text, txtWorkingHours.Text, DateTime.Parse(datePicker.Text), ImagePath)));
                    System.IO.File.Copy(selectedFilePath, ImagePath, true);
                    this.Close();
                }
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
                if (selectedFilePath != null)
                {
                    string destinationFolderPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Imgs\\Upload"));
                    string fileName = "offerImg_" + Guid.NewGuid().ToString();
                    string fileExtension = System.IO.Path.GetExtension(selectedFilePath);
                    string fileNameWithExtention = fileName + fileExtension;
                    string destinationFilePath = System.IO.Path.Combine(destinationFolderPath, fileNameWithExtention);

                    var SelectedItemCategory = cmbCategory.SelectedItem as Category;
                    var SelectedItemCompany = cmbCompany.SelectedItem as Company;
                    try
                    {
                        DatabaseAdmin.UpdateOffer(new Offer(offerID, SelectedItemCompany, SelectedItemCategory, txtPosition.Text, cmbPositionLevel.Text, cmbContract.Text, cmbWorkingType.Text, cmbEtat.Text, txtSalary.Text, txtWorkingDays.Text, txtWorkingHours.Text, DateTime.Parse(datePicker.Text), fileNameWithExtention), true);
                        System.IO.File.Copy(selectedFilePath, destinationFilePath, true);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Wstapił błąd podczas edycji: " + ex.Message);
                    }
                }
                else
                {
                    var SelectedItemCategory = cmbCategory.SelectedItem as Category;
                    var SelectedItemCompany = cmbCompany.SelectedItem as Company;
                    DatabaseAdmin.UpdateOffer(new Offer(offerID, SelectedItemCompany, SelectedItemCategory, txtPosition.Text, cmbPositionLevel.Text, cmbContract.Text, cmbWorkingType.Text, cmbEtat.Text, txtSalary.Text, txtWorkingDays.Text, txtWorkingHours.Text, DateTime.Parse(datePicker.Text)), false);
                    this.Close();
                }
            }
        }

        private bool IsValid()
        {
            bool isOk = true;
            if (string.IsNullOrWhiteSpace(txtPosition.Text) || string.IsNullOrWhiteSpace(txtSalary.Text) || string.IsNullOrWhiteSpace(txtWorkingDays.Text) || string.IsNullOrWhiteSpace(txtWorkingHours.Text) || cmbPositionLevel.SelectedIndex == 0 || cmbContract.SelectedIndex == 0 || cmbEtat.SelectedIndex == 0 || cmbWorkingType.SelectedIndex == 0)
            {
                isOk = false;
                MessageBox.Show("Pola nie mogą być puste!");
            }
            else
            {
                if (!Regex.IsMatch(txtSalary.Text, @"^\d+(?:\s*-\s*\d+)?$")) { isOk = false; MessageBox.Show("Niepoprawna wartość dla pola wynagrodzenie!"); }
                if (!Regex.IsMatch(txtWorkingHours.Text, @"^\d{1,2}:\d{2}(?:\s*-\s*\d{1,2}:\d{2})?$")) { isOk = false; MessageBox.Show("Niepoprawna wartość dla pola godziny pracy!"); }
            }
            
            return isOk;
        }


        private string CreateFilePath(bool returnPath)
        {
            if(selectedFilePath != null)
            {
                string destinationFolderPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Imgs\\Upload"));
                string fileName = "offerImg_" + Guid.NewGuid().ToString();
                string fileExtension = System.IO.Path.GetExtension(selectedFilePath);
                string destinationFilePath = System.IO.Path.Combine(destinationFolderPath, fileName + fileExtension);

                if (returnPath)
                    return destinationFilePath;
                else
                    return fileName + fileExtension;
            }
            return "0";
        }

        private void SetComboboxSelectedItem(ComboBox comboBox, string value)
        {
            foreach (var item in comboBox.Items)
            {
                if (item is ComboBoxItem comboBoxItem && comboBoxItem.Content.ToString() == value)
                {
                    comboBox.SelectedItem = comboBoxItem;
                    break;
                }
                else if (item is Category category && category.Name == value)
                {
                    comboBox.SelectedItem = category;
                    break;
                }
                else if (item is Company company && company.Name == value)
                {
                    comboBox.SelectedItem = company;
                    break;
                }
            }
        }

        private void AddButtonCategory(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCategoryName.Text))
            {
                DatabaseAdmin.InsertCategory(new Category(txtCategoryName.Text));
                this.Close();
            }
            else
                MessageBox.Show("Pole nie może być puste!");
        }
        private void EditButtonCategory(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCategoryName.Text))
            {
                DatabaseAdmin.UpdateCategory(new Category(categoryID, txtCategoryName.Text));
                this.Close();
            }
            else
                MessageBox.Show("Pole nie może być puste!");
        }

        private void AddButtonCompany(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCompanyName.Text) && !string.IsNullOrEmpty(txtCompanyAdress.Text) && !string.IsNullOrEmpty(txtCompanyInfo.Text))
            {
                DatabaseAdmin.InsertCompany(new Company(txtCompanyName.Text, txtCompanyAdress.Text, txtCompanyInfo.Text));
                this.Close();
            }
            else
                MessageBox.Show("Pola nie mogą być puste!");
        }

        private void EditButtonCompany(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCompanyName.Text) && !string.IsNullOrEmpty(txtCompanyAdress.Text) && !string.IsNullOrEmpty(txtCompanyInfo.Text))
            {
                DatabaseAdmin.UpdateCompany(new Company(companyID, txtCompanyName.Text, txtCompanyAdress.Text, txtCompanyInfo.Text));
                this.Close();
            }
            else
                MessageBox.Show("Pola nie mogą być puste!");
        }

        private void AddDutiesBtn(object sender, RoutedEventArgs e)
        {
            offerDetailsListBox.Items.Add(new TextBox { Height = 40, Width = 350, FontSize = 18, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Style = (Style)this.FindResource("PlaceholderTextBoxStyle"), Tag = "Obowiązek" });
        }


        private void AddOfferDetails(int ID)
        {
            foreach (var item in offerDetailsListBox.Items)
            {
                if (item is TextBox textBox)
                {
                    if(!string.IsNullOrEmpty(textBox.Text))
                    DatabaseAdmin.InsertOfferTables("oferta_obowiazki", textBox.Text, ID);
                }
            }
            foreach (var item in offerDetailsListBox2.Items)
            {
                if (item is TextBox textBox)
                {
                    if (!string.IsNullOrEmpty(textBox.Text))
                        DatabaseAdmin.InsertOfferTables("oferta_wymagania", textBox.Text, ID);
                }
            }
            foreach (var item in offerDetailsListBox3.Items)
            {
                if (item is TextBox textBox)
                {
                    if (!string.IsNullOrEmpty(textBox.Text))
                        DatabaseAdmin.InsertOfferTables("oferta_benefity", textBox.Text, ID);
                }
            }
        }

        private void AddFromDatabaseToList(int offerID)
        {
            List<string> listBenefits = DatabaseAdmin.ReadDataBenefits("oferta_benefity", offerID);
            foreach (string value in listBenefits)
            {
                offerDetailsListBox3.Items.Add(new TextBox { Text = value, Height = 40, Width = 350, FontSize = 18, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Style = (Style)this.FindResource("PlaceholderTextBoxStyle"), Tag = "Benefit" });
            }

            List<string> listDuties = DatabaseAdmin.ReadDataBenefits("oferta_obowiazki", offerID);
            foreach (string value in listDuties)
            {
                offerDetailsListBox.Items.Add(new TextBox { Text = value, Height = 40, Width = 350, FontSize = 18, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Style = (Style)this.FindResource("PlaceholderTextBoxStyle"), Tag = "Benefit" });
            }

            List<string> listRequirements = DatabaseAdmin.ReadDataBenefits("oferta_wymagania", offerID);
            foreach (string value in listRequirements)
            {
                offerDetailsListBox2.Items.Add(new TextBox { Text = value, Height = 40, Width = 350, FontSize = 18, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Style = (Style)this.FindResource("PlaceholderTextBoxStyle"), Tag = "Benefit" });
            }
        }

        private void AddRequiremenmtsBtn(object sender, RoutedEventArgs e)
        {
            offerDetailsListBox2.Items.Add(new TextBox { Height = 40, Width = 350, FontSize = 18, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Style = (Style)this.FindResource("PlaceholderTextBoxStyle"), Tag = "Wymóg" });
        }

        private void AddBenefitsBtn(object sender, RoutedEventArgs e)
        {
            offerDetailsListBox3.Items.Add(new TextBox { Height = 40, Width = 350, FontSize = 18, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Style = (Style)this.FindResource("PlaceholderTextBoxStyle"), Tag = "Benefit" });
        }
    }
}
