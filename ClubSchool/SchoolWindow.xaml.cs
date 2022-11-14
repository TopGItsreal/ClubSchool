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
using Core;

namespace ClubSchool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SchoolWindow : Window
    {
        public string PageTitle { get; set; }
        public SchoolWindow()
        {
            InitializeComponent();

            frame.Navigated += Frame_Navigated;
            frame.NavigationService.Navigate(new AuthorizationPage());
            DataContext = this;
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            var pageContent = frame.Content;

            PageTitle = (pageContent as Page).Title;
            tbTitle.Text = PageTitle;

            if (pageContent is AuthorizationPage)
            {
                bordContent.Background = new SolidColorBrush(Colors.Transparent);
                App.Teacher = null;
            }    
            else
                bordContent.Background = new SolidColorBrush(Colors.White);

            if (App.Teacher != null && DataAccess.IsAdmin(App.Teacher.User))
            {
                btnMySchedule.Visibility = Visibility.Collapsed;
                spAdminButtons.Visibility = Visibility.Visible;
            }
            else
            {
                btnMySchedule.Visibility = Visibility.Visible;
                spAdminButtons.Visibility = Visibility.Collapsed;
            }

            var buttonsVisibility = pageContent is AuthorizationPage ? Visibility.Collapsed : Visibility.Visible;
            spButtons.Visibility = buttonsVisibility;
            spMenuButtons.Visibility = buttonsVisibility;
            
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
            frame.NavigationService.Navigate(new ClubsListPage());
        }

        private void btnSchedule_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new SchedulePage());
        }

        private void btnMySchedule_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new SchedulePage(false));
        }

        private void btnTeachers_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new TeachersListPage());
        }

        private void btnStat_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new StatisticsPage());
        }

        private void btnGroups_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new GroupsListPage());
        }
    }
}
