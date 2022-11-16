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
using System.Windows.Shapes;
using Core;

namespace ClubSchool.Windows
{
    /// <summary>
    /// Interaction logic for SchedulePlanWindow.xaml
    /// </summary>
    public partial class SchedulePlanWindow : Window
    {
        public List<Schedule> Schedules { get; set; }
        public List<Club> Clubs { get; set; }
        public List<Group> Groups { get; set; }
        public List<Room> Rooms { get; set; }
        public SchedulePlanWindow()
        {
            InitializeComponent();
            Schedules = DataAccess.GetSchedules();
            Clubs = DataAccess.GetNotDeletedClubs();
            Groups = DataAccess.GetNotDeletedGroups();
            Rooms = DataAccess.GetRooms();
            cdClubsDays.SelectedDate = DateTime.Now;

            lvSchedules.ItemsSource = Schedules.FindAll(x => x.Date.Date == cdClubsDays.SelectedDate.Value.Date);
            DataAccess.AddNewItemEvent += RefreshList;

            DataContext = this;
        }
        private void RefreshList()
        {
            Schedules = DataAccess.GetSchedules();
            lvSchedules.ItemsSource = Schedules.FindAll(x => x.Date.Date == cdClubsDays.SelectedDate.Value.Date);
            lvSchedules.Items.Refresh();
        }

        private void cdClubsDays_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            lvSchedules.ItemsSource = Schedules.FindAll(x=> x.Date.Date == ((DateTime)cdClubsDays.SelectedDate).Date);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var schedule = (sender as MenuItem).DataContext as Schedule;

            cdClubsDays.SelectedDate = schedule.Date;
            cbClubs.SelectedItem = schedule.Group.Club;
            cbRooms.SelectedItem = schedule.Room;
            cbGroups.SelectedItem = schedule.Group;
            tpClubTime.SelectedTime = schedule.Date;

            var enable = schedule.Date > DateTime.Now;

            cbClubs.IsEnabled = enable;
            cbRooms.IsEnabled = enable;
            cbGroups.IsEnabled = enable;
            tpClubTime.IsEnabled = enable;
            btnAddClub.IsEnabled = enable;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var schedule = (sender as MenuItem).DataContext as Schedule;

            if (schedule.Date > DateTime.Now)
            {
                MessageBox.Show("Нельзя удалить прошедшее занятие", "Ошибка");
                return;
            }

            var result = MessageBox.Show("Вы точно хотите удалить данное занятие?",
                                         "Предупреждение",
                                         MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
                DataAccess.RemoveSchedule(schedule);
        }

        private void btnAddClub_Click(object sender, RoutedEventArgs e)
        {
            var errorMessage = new StringBuilder();
            if (cdClubsDays.SelectedDate == null)
                errorMessage.AppendLine("Выберите дату");
            if (cbGroups.SelectedItem == null)
                errorMessage.AppendLine("Выберите группу");
            if (cbRooms.SelectedItem == null)
                errorMessage.AppendLine("Выберите кабинет");
            if (tpClubTime.SelectedTime == null 
                || tpClubTime.SelectedTime.Value.TimeOfDay < TimeSpan.FromHours(6)
                || tpClubTime.SelectedTime.Value.TimeOfDay > TimeSpan.FromHours(22))
                errorMessage.AppendLine("Выберите корректное время");

            var schedule = new Schedule
            {
                Date = ((DateTime)cdClubsDays.SelectedDate).Date + tpClubTime.SelectedTime.Value.TimeOfDay,
                Group = cbGroups.SelectedItem as Group,
                Room = cbRooms.SelectedItem as Room,
            };

            if (schedule.Date < DateTime.Now)
                errorMessage.AppendLine("Выберите корректную дату");

            if (Schedules.Any(x => Math.Abs((x.Date - schedule.Date).TotalHours) < 1
                && x.Group.Teacher == schedule.Group.Teacher))
                errorMessage.AppendLine("Учитель в это время занят");

            if (Schedules.Any(x => Math.Abs((x.Date - schedule.Date).TotalHours) < 1
                && x.Room == schedule.Room))
                errorMessage.AppendLine("Кабинет в это время занят");

            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage.ToString(), "Ошибка");
                return;
            }


            DataAccess.SaveSchedule(schedule);
        }

        private void cbClubs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbGroups.ItemsSource = (cbClubs.SelectedItem as Club).NotDeletedGroups;
        }

        private void cbGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void cbRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
