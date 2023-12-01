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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JobPortal
{
    /// <summary>
    /// Logika interakcji dla klasy LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
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

        private void LoginToAccountButton(object sender, MouseButtonEventArgs e)
        {
            registerBtn.Visibility = Visibility.Collapsed;
            loginBtn.Visibility = Visibility.Visible;
            windowTitle.Text = "Zaloguj się";
            run1.Text = "Nie masz jeszcze konta?";
            run2.Text = "Utwórz konto";
            run2.MouseLeftButtonDown += new MouseButtonEventHandler(CreateAccountButton);
        }

        private void CreateAccountButton(object sender, MouseButtonEventArgs e)
        {
            registerBtn.Visibility = Visibility.Visible;
            loginBtn.Visibility = Visibility.Collapsed;
            windowTitle.Text = "Utwórz konto";
            run1.Text = "Masz już konto?";
            run2.Text = "Zaloguj się";
            run2.MouseLeftButtonDown += new MouseButtonEventHandler(LoginToAccountButton);
        }
    }
}
