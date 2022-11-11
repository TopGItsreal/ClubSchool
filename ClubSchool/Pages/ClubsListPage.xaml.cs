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
        public ClubsListPage()
        {
            InitializeComponent();
            Clubs = DataAccess.GetClubs();

            DataContext = this;
        }
        public ClubsListPage(IEnumerable<Club> clubs)
        {
            InitializeComponent();
            Clubs = clubs;

            DataContext = this;
        }

        private void btnStudents_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StudentsListPage());
        }

        private void lvClubs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var club = (sender as ListView).SelectedItem as Club;
            NavigationService.Navigate(new ClubPage(club));

            //var club = lvClubs.SelectedItem as Club;
            //var students = club.StudentClubs.Select(x => x.Student);
            //NavigationService.Navigate(new StudentsListPage(students));
        }
    }
}
