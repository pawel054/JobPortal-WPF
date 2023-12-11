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
    /// Logika interakcji dla klasy AdminMain.xaml
    /// </summary>
    public partial class AdminMain : Page
    {
        private Frame mainFrame;
        public AdminMain(Frame mainFrame)
        {
            InitializeComponent();
            this.mainFrame = mainFrame;
        }
    }
}
