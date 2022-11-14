using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class StatisticService
    {
        public static List<StudentStatistic> GetStudentStatistics()
        {
            List<StudentStatistic> Statistics = new List<StudentStatistic>();

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

                Statistics.Add(new StudentStatistic
                {
                    Student = student,
                    Attendance = allLessons != 0 ? (100 * visitedLessons / allLessons).ToString() + " %" :
                                                   "Не было занятий",
                    AttendanceValue = allLessons != 0 ? 100 * visitedLessons / allLessons : 0,
                });
            }
            
            return Statistics.OrderBy(x => x.AttendanceValue).ThenByDescending(x => x.Attendance).Reverse().ToList();
        }

        public static List<ClubStatistic> GetClubStatistics()
        {
            var ClubStatistics = new List<ClubStatistic>();

            foreach (var club in DataAccess.GetClubs())
            {
                var allLessons = 0;
                var visitedLessons = 0;

                foreach (var group in club.Groups)
                {
                    foreach (var studentGroup in group.StudentGroups)
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
            return ClubStatistics.OrderBy(x => x.AttendanceValue).ThenByDescending(x => x.Attendance).Reverse().ToList();
        }
    }
}
