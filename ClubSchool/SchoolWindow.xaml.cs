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

namespace ClubSchool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SchoolWindow : Window
    {
        public SchoolWindow()
        {
            InitializeComponent();

            frame.NavigationService.Navigate(new Pages.AuthorizationPage());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
