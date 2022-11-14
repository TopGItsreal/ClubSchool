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
using Core;

namespace ClubSchool.Pages
{
    /// <summary>
    /// Interaction logic for StatisticsPage.xaml
    /// </summary>
    public partial class StatisticsPage : Page
    {
        public List<StudentStatistic> StudentStatistics { get; set; }
        public List<ClubStatistic> ClubStatistics { get; set; }

        public StatisticsPage()
        {
            InitializeComponent();
            Statistics = StatisticService.GetStudentStatistics();
            ClubStatistics = new List<ClubStatistic>();


            foreach(var club in DataAccess.GetClubs())
            {
                var allLessons = 0;
                var visitedLessons = 0;

                foreach(var group in club.Groups)
                {
                    foreach(var studentGroup in group.StudentGroups)
                    {
                        foreach (var lesson in studentGroup.Journals)
                        {
                            allLessons++;
                            if (lesson.IsVisited)
                                visitedLessons++;
                        }
                    }
                }

                ClubStatistics.Add(new ClubStatistic
                {
                    Club = club,
                    Attendance = allLessons != 0 ? (100 * visitedLessons / allLessons).ToString() + " %" :
                                                   "Не было занятий",
                    AttendanceValue = allLessons != 0 ? 100 * visitedLessons / allLessons : 0,

                });
            }

            ClubStatistics = ClubStatistics.OrderBy(x => x.AttendanceValue).ThenByDescending(x => x.Attendance).Reverse().ToList();

            DataContext = this;
        }
    }
}
