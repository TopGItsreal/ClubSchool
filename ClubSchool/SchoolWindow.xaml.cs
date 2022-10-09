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
using ClubSchool.Pages;

namespace ClubSchool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SchoolWindow : Window
    {
        public string pageTitle { get; set; }
        public SchoolWindow()
        {
            InitializeComponent();

            frame.Navigated += Frame_Navigated;
            frame.NavigationService.Navigate(new AuthorizationPage());
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            var pageContent = frame.Content;

            pageTitle = (pageContent as Page).Title;

            spButtons.Visibility = pageContent is AuthorizationPage ? Visibility.Hidden : Visibility.Visible;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (frame.CanGoBack)
                frame.GoBack();
        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {
            if (frame.CanGoForward)
                frame.GoForward();
        }

        private void btnClubs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSchedule_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new SchedulePage());
        }

        private void btnNewClub_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new ClubPage());
        }
    }
}
