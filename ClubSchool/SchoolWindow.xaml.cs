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
                bordContent.Background = new SolidColorBrush(Colors.Transparent);
            else
                bordContent.Background = new SolidColorBrush(Colors.White);
            var visibility = pageContent is AuthorizationPage ? Visibility.Collapsed : Visibility.Visible;
            spButtons.Visibility = visibility;
            spMenuButtons.Visibility = visibility;
            
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
            frame.NavigationService.Navigate(new SchedulePage(DataAccess.GetSchedules()));
        }

        private void btnNewClub_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new ClubPage(new Club()));
        }

        private void btnMySchedule_Click(object sender, RoutedEventArgs e)
        {
            var schedules = new List<Schedule>();
            var teacher = App.User.Teachers.FirstOrDefault();
            if (teacher != null)
            {
                foreach (var schedule in teacher.TeacherClubs.Select(x => x.Schedules))
                {
                    schedules.AddRange(schedule);
                }
            }
            frame.NavigationService.Navigate(new SchedulePage(schedules));
        }
    }
}
