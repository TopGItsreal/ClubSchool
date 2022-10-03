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



    }
}
