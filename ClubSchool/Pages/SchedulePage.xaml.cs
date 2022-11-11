using System;
using System.Linq;
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
using System.Globalization;
using Core;

namespace ClubSchool.Pages
{
    /// <summary>
    /// Interaction logic for SchedulePage.xaml
    /// </summary>
    public partial class SchedulePage : Page
    {
        public List<Schedule> Schedules { get; set; }
        public IEnumerable<string> DayNames { get; set; }
        public SchedulePage(List<Schedule> schedules)
        {
            InitializeComponent();
            Schedules = schedules;
            var day = App.Culture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek);
            DayNames = App.Culture.DateTimeFormat.DayNames.Select(x => char.ToUpper(x[0]) + x.Substring(1));

            DataContext = this;
        }

        private void lvShedules_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var club = ((sender as ListView).SelectedItem as Schedule).TeacherClub.Club;
            //NavigationService.Navigate(new ClubPage(club));

            var club = ((sender as ListView).SelectedItem as Schedule).TeacherClub.Club;
            var students = club.StudentClubs.Select(x => x.Student);
            NavigationService.Navigate(new StudentsListPage(students));
        }

        private void cbDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvShedules.ItemsSource = Schedules.FindAll(x => App.Culture.DateTimeFormat.GetDayName(x.Date.DayOfWeek) == cbDay.SelectedItem.ToString().ToLower());
        }
    }
}
