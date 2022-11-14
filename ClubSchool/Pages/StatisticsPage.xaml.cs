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
        public List<Statistic> Statistics { get; set; }

        public StatisticsPage()
        {
            InitializeComponent();
            Statistics = new List<Statistic>();

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

                Statistics.Add(new Statistic
                { 
                    Student = student, 
                    Attendance = allLessons != 0 ? (100 * visitedLessons / allLessons).ToString() + " %" :
                                                   "Не было занятий",
                    AttendanceValue = allLessons != 0 ? 100 * visitedLessons / allLessons : 0,
                });
            }
            Statistics = Statistics.OrderBy(x => x.AttendanceValue).Reverse().ToList();

            DataContext = this;
        }

        public class Statistic 
        {
            public Student Student { get; set; }
            public string Attendance { get; set; }
            public double AttendanceValue { get; set; }
        }
    }
}
