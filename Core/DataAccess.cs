using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class DataAccess
    {
        private static ClubSchoolEntities ClubSchool = new ClubSchoolEntities();

        public static List<Student> GetStudents() => ClubSchool.GetContext().Students.ToList();

        public static List<Teacher> GetTeachers() => ClubSchool.GetContext().Teachers.ToList();

        public static List<Class> GetClasses() => ClubSchool.GetContext().Classes.ToList();

        public static List<Group> GetGroups() => ClubSchool.GetContext().Groups.ToList();

        public static List<Schedule> GetSchedules() => ClubSchool.GetContext().Schedules.ToList();

        public static List<Room> GetRooms() => ClubSchool.GetContext().Rooms.ToList();

        public static List<StudentGroup> GetStudentGroups() => ClubSchool.GetContext().StudentGroups.ToList();

        public static List<StudentClub> GetStudentClubs() => ClubSchool.GetContext().StudentClubs.ToList();

        public static List<TeacherClub> GetTeacherClubs() => ClubSchool.GetContext().TeacherClubs.ToList();

        public static List<Club> GetClubs() => ClubSchool.GetContext().Clubs.ToList();

        public static List<Journal> GetJournals() => ClubSchool.GetContext().Journals.ToList();

        public static List<User> GetUsers() => ClubSchool.GetContext().Users.ToList();

        public static List<UserInfo> GetUsersInfo() => ClubSchool.GetContext().UserInfoes.ToList();

        public static bool SaveJournal(Journal journal)
        {
            if (journal.Id == 0)
                ClubSchool.GetContext().Journals.Add(journal);

            return Convert.ToBoolean(ClubSchool.GetContext().SaveChanges());
        }
        public static bool SaveTeacher(Teacher teacher)
        {
            if (teacher.Id == 0)
                ClubSchool.GetContext().Teachers.Add(teacher);

            return Convert.ToBoolean(ClubSchool.GetContext().SaveChanges());
        }

        public static bool SaveClub(Club club)
        {
            if (club.Id == 0)
                ClubSchool.GetContext().Clubs.Add(club);

            return Convert.ToBoolean(ClubSchool.GetContext().SaveChanges());
        }

        public static bool RemoveClub(Club club)
        {
            club.IsDeleted = true;
            return SaveClub(club);
        }

        public static bool RemoveTeacher(Teacher teacher)
        {
            teacher.IsDeleted = true;
            return SaveTeacher(teacher);
        }

        public static bool SaveStudentClub(StudentClub studentClub)
        {
            if (studentClub.Id == 0)
                ClubSchool.GetContext().StudentClubs.Add(studentClub);

            return Convert.ToBoolean(ClubSchool.GetContext().SaveChanges());
        }

        public static bool RemoveStudentClub(StudentClub studentClub)
        {
            studentClub.IsDeleted = true;
            return SaveStudentClub(studentClub);
        }

        public static bool IsPasswordCorrect(string login, string password) => GetUsers().Any(x => x.Login == login && x.Password == password);

        public static User GetUser(string login, string password) => GetUsers().FirstOrDefault(x => x.Login == login && x.Password == password);



    }
}
