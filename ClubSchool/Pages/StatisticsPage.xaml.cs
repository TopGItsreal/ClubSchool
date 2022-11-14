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
            StudentStatistics = new List<StudentStatistic>();
            ClubStatistics = new List<ClubStatistic>();

            foreach (var student in DataAccess.GetStudents())
            {
                var allLessons = 0;
                var visitedLessons = 0;

                foreach (var group in student.StudentGroups)
                {
                    foreach (var lesson in group.Journals)
                    {
                        allLessons++;
                        if (lesson.IsVisited)
                            visitedLessons++;
                    }
                }

                StudentStatistics.Add(new StudentStatistic
                { 
                    Student = student, 
                    Attendance = allLessons != 0 ? (100 * visitedLessons / allLessons).ToString() + " %" :
                                                   "Не было занятий",
                    AttendanceValue = allLessons != 0 ? 100 * visitedLessons / allLessons : 0,
                });
            }

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

            StudentStatistics = StudentStatistics.OrderBy(x => x.AttendanceValue).ThenByDescending(x => x.Attendance).Reverse().ToList();
            ClubStatistics = ClubStatistics.OrderBy(x => x.AttendanceValue).ThenByDescending(x => x.Attendance).Reverse().ToList();

            DataContext = this;
        }

        public class StudentStatistic
        {
            public Student Student { get; set; }
            public string Attendance { get; set; }
            public double AttendanceValue { get; set; }
        }
        public class ClubStatistic
        {
            public Club Club { get; set; }
            public string Attendance { get; set; }
            public double AttendanceValue { get; set; }
        }
    }
}
