using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class DataAccess
    {
        public delegate void AddNewItem();
        public static event AddNewItem AddNewItemEvent;

        public static List<Student> GetStudents() => ClubSchoolEntities.GetContext().Students.ToList();
        public static List<Teacher> GetTeachers() => ClubSchoolEntities.GetContext().Teachers.ToList();
        public static List<Class> GetClasses() => ClubSchoolEntities.GetContext().Classes.ToList();
        public static List<Schedule> GetSchedules() => ClubSchoolEntities.GetContext().Schedules.ToList();
        public static List<Room> GetRooms() => ClubSchoolEntities.GetContext().Rooms.ToList();
        public static List<TeacherClub> GetTeacherClubs() => ClubSchoolEntities.GetContext().TeacherClubs.ToList();
        public static List<Club> GetClubs() => ClubSchoolEntities.GetContext().Clubs.ToList();
        public static List<Journal> GetJournals() => ClubSchoolEntities.GetContext().Journals.ToList();
        public static List<User> GetUsers() => ClubSchoolEntities.GetContext().Users.ToList();

        public static void SaveJournal(Journal journal)
        {
            if (journal.Id == 0)
                ClubSchoolEntities.GetContext().Journals.Add(journal);

            ClubSchoolEntities.GetContext().SaveChanges();
            AddNewItemEvent?.Invoke();
        }

        public static void SaveTeacher(Teacher teacher)
        {
            if (teacher.Id == 0)
                ClubSchoolEntities.GetContext().Teachers.Add(teacher);

            ClubSchoolEntities.GetContext().SaveChanges();
            AddNewItemEvent?.Invoke();
        }

        public static void RemoveTeacher(Teacher teacher)
        {
            teacher.IsDeleted = true;
            SaveTeacher(teacher);
        }

        public static void SaveClub(Club club)
        {
            if (club.Id == 0)
                ClubSchoolEntities.GetContext().Clubs.Add(club);

            ClubSchoolEntities.GetContext().SaveChanges();
            AddNewItemEvent?.Invoke();
        }

        public static void RemoveClub(Club club)
        {
            club.IsDeleted = true;
            SaveClub(club);
        }

        public static User GetUser(string login, string password) => GetUsers().FirstOrDefault(x => x.Login == login && x.Password == password);

        public static Teacher LoginTeacher(string login, string password)
        {
            return GetTeachers().FirstOrDefault(x => (x.User.Login == login ||
                                                      x.User.Email == login ||
                                                      x.User.PhoneNumber == login) &&
                                                      x.User.Password == password);
        }
        public static void SaveJournals(ICollection<Journal> journals, bool isNew = false)
        {
            if (isNew)
                ClubSchoolEntities.GetContext().Journals.AddRange(journals);
            ClubSchoolEntities.GetContext().SaveChanges();
            AddNewItemEvent?.Invoke();
        }


        public static bool IsAdmin(User user) => user.Login == user.Password && user.Login == "admin";
    }
}
