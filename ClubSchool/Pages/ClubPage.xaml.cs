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
using System.Text.RegularExpressions;
using Core;

namespace ClubSchool.Pages
{
    /// <summary>
    /// Interaction logic for ClubPage.xaml
    /// </summary>
    public partial class ClubPage : Page
    {
        public Club Club { get; set; }
        public List<Club> Clubs { get; set; }
        public IEnumerable<Schedule> Schedules { get; set; }

        public ClubPage()
        {
            InitializeComponent();
            Club = new Club();
            Clubs = DataAccess.GetClubs();
            Schedules = DataAccess.GetSchedules().Where(x=> x.TeacherClub.ClubId == Club.Id);

            if (Schedules.Count() == 0)
                lvSchedule.Visibility = Visibility.Hidden;

            DataContext = this;
        }
        public ClubPage(Club club)
        {
            InitializeComponent();
            Club = club;
            Clubs = DataAccess.GetClubs();
            Schedules = DataAccess.GetSchedules().Where(x => x.TeacherClub.ClubId == Club.Id);

            if (Schedules.Count() == 0)
                lvSchedule.Visibility = Visibility.Hidden;

            DataContext = this;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var t = cbClubs.Text;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
