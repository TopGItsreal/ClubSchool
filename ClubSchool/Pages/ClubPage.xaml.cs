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
        public List<Schedule> Schedules { get; set; }

        public ClubPage(Club club)
        {
            InitializeComponent();
            Club = club;
            Clubs = DataAccess.GetClubs();
            Schedules = DataAccess.GetSchedules().FindAll(x => x.TeacherClub.ClubId == Club.Id);

            if (Schedules.Count() == 0)
                spSchedule.Visibility = Visibility.Collapsed;

            DataContext = this;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataAccess.SaveClub(Club);
                NavigationService.GoBack();
            }
            catch
            {
                MessageBox.Show("Сохранение не удалось", "Ошибка");
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
