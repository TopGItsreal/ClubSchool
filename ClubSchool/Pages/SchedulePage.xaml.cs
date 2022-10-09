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
using Core;

namespace ClubSchool.Pages
{
    /// <summary>
    /// Interaction logic for SchedulePage.xaml
    /// </summary>
    public partial class SchedulePage : Page
    {
        public List<Schedule> Schedules { get; set; }
        public SchedulePage()
        {
            InitializeComponent();
            Schedules = DataAccess.GetSchedules();
            DataContext = this;

        }

        private void lvShedules_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var club = ((sender as ListView).SelectedItem as Schedule).TeacherClub.Club;
            NavigationService.Navigate(new ClubPage(club));
        }
    }
}
