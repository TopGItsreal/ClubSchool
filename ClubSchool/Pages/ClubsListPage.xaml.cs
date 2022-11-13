using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using Core;

namespace ClubSchool.Pages
{
    /// <summary>
    /// Interaction logic for ClubsListPage.xaml
    /// </summary>
    public partial class ClubsListPage : Page
    {
        public IEnumerable<Club> Clubs { get; set; }
        public IEnumerable<StudentTeacherClub> StudentTeacherClubs { get; set; }

        public ClubsListPage()
        {
            InitializeComponent();
            Clubs = DataAccess.GetClubs();
            StudentTeacherClubs = new List<StudentTeacherClub>();

            lvStudentGroups.Visibility = Visibility.Collapsed;
            btnNewClub.Visibility = DataAccess.IsAdmin(App.Teacher.User) ? Visibility.Visible : Visibility.Collapsed;

            DataContext = this;
        }

        public ClubsListPage(Student student)
        {
            InitializeComponent();
            StudentTeacherClubs = student.StudentTeacherClubs;
            Clubs = new List<Club>();

            lvClubs.Visibility = Visibility.Collapsed;
            btnNewClub.Visibility = DataAccess.IsAdmin(App.Teacher.User) ? Visibility.Visible : Visibility.Collapsed;

            DataContext = this;
        }

        private void btnStudents_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StudentsListPage());
        }

        private void lvClubs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var club = (sender as ListView).SelectedItem as Club;
            if (club == null)
                return;
            NavigationService.Navigate(new ClubPage(club));
        }

        private void btnNewClub_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClubPage(new Club()));
        }
    }
}
