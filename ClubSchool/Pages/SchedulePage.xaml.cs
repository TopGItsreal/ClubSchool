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
        public List<string> DayNames { get; set; }

        public bool IsAll { get; set; }

        public SchedulePage(bool isAll = true)
        {
            InitializeComponent();

            IsAll = isAll;
            Schedules = IsAll ? DataAccess.GetSchedules() : DataAccess.GetSchedules(App.Teacher);
            var day = App.Culture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek);
            DayNames = App.Culture.DateTimeFormat.DayNames.Select(x => char.ToUpper(x[0]) + x.Substring(1)).ToList();
            DayNames.Insert(0, "Все дни");
            
            cbDay.SelectedItem = char.ToUpper(day[0]) + day.Substring(1);
            dpDateStart.SelectedDate = new DateTime(DateTime.Today.Year, 1, 1);

            btnNewSchedule.Visibility = DataAccess.IsAdmin(App.Teacher.User) ?
                                        Visibility.Visible : Visibility.Collapsed;
            DataAccess.AddNewItemEvent += DataAccess_AddNewItemEvent;
            DataContext = this;
        }

        private void DataAccess_AddNewItemEvent()
        {
            Schedules = IsAll ? DataAccess.GetSchedules() : DataAccess.GetSchedules(App.Teacher);
            ApplyFilters();
        }

        private void lvSchedules_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var schedule = (sender as ListView).SelectedItem as Schedule;
            if (schedule != null)
                NavigationService.Navigate(new JournalPage(schedule));
        }

        private void cbDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void dpDateStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        public void ApplyFilters()
        {
            var date = dpDateStart.SelectedDate;
            var day = cbDay.SelectedItem;
            if (DateTime.MinValue == date || day == null)
                return;

            lvSchedules.ItemsSource = day == "Все дни" ?
                                     Schedules.FindAll(x => x.Date.Date > date) :
                                     Schedules.FindAll(x => App.Culture.DateTimeFormat.GetDayName(x.Date.DayOfWeek)== day.ToString().ToLower()
                                     && x.Date.Date > date);

            lvSchedules.Items.Refresh();
        }

        private void btnNewSchedule_Click(object sender, RoutedEventArgs e)
        {
            new Windows.SchedulePlanWindow().ShowDialog();
        }
    }
}
