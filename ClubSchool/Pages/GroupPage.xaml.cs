using Core;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClubSchool.Pages
{
    /// <summary>
    /// Interaction logic for GroupPage.xaml
    /// </summary>
    public partial class GroupPage : Page
    {
        public Group Group { get; set; }
        public List<Club> Clubs { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Student> Students { get; set; }

        public GroupPage(Group group, bool isNew = false)
        {
            InitializeComponent();

            Group = group;
            Clubs = DataAccess.GetNotDeletedClubs();
            Teachers = DataAccess.GetNotDeletedTeachers();
            Students = DataAccess.GetStudents();

            ChangeEditing();
            if (isNew)
            {
                Title = $"Новая {Title}";
                btnDelete.IsEnabled = false;
            }
            else
                if (Group != null)
                    Title = $"{Title} {Group.Id}";

            this.DataContext = this;
        }

        public void ChangeEditing()
        {
            if (DataAccess.IsAdmin(App.Teacher.User))
                return;

            if (Group.Teacher == App.Teacher)
            {
                cbClub.IsEnabled = false;
                cbTeacher.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }
            else
            {
                btnSave.IsEnabled = false;
                tbName.IsEnabled = false;
                cbClub.IsEnabled = false;
                cbTeacher.IsEnabled = false;
                cbStudents.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }
        }

        private void cbTeacher_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Group.Teacher = cbTeacher.SelectedItem as Teacher;
        }

        private void cbClub_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Group.Club = cbClub.SelectedItem as Club;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var message = MessageBox.Show($"Вы точно хотите удалить данный кружок?",
                                          "Предупреждение",
                                          MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

            if (message != MessageBoxResult.Yes)
                return;
            DataAccess.RemoveGroup(Group);
            NavigationService.GoBack();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var errorMessage = new StringBuilder();
            try
            {
                if (string.IsNullOrWhiteSpace(Group.Name))
                    errorMessage.AppendLine("Имя группы обязательно для заполнения");
                if (Group.Club == null)
                    errorMessage.AppendLine("Не выбран кружок группы");
                if (Group.Teacher == null)
                    errorMessage.AppendLine("Не выбран учитель группы");
                if (errorMessage.Length > 0)
                    throw new Exception();

                DataAccess.SaveGroup(Group);
                NavigationService.GoBack();
            }
            catch
            {
                MessageBox.Show(errorMessage.ToString(), "Ошибка");
            }
        }

        private void lvStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Group.Teacher != App.Teacher)
                return;

            var studentGroup = lvStudents.SelectedItem as StudentGroup;

            if (studentGroup == null)
                return;

            var message = MessageBox.Show($"Вы точно хотите отчислить {studentGroup.Student.LastName} {studentGroup.Student.FirstName}?",
                                          "Предупреждение",
                                          MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

            if (message == MessageBoxResult.Yes)
            {
                studentGroup.IsDeleted = true;
                lvStudents.ItemsSource = Group.NotDeletedStudentGroups;
                lvStudents.Items.Refresh();
            }
        }

        private void cbStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var student = cbStudents.SelectedItem as Student;

            if (student == null || Group.StudentGroups.Any(x => x.Student == student && x.Group == Group && !x.IsDeleted))
                return;


            if(Group.Club == null || Group.StudentGroups.Count >= Group.Club.MaxStudentCount)
            {
                MessageBox.Show("Превышено максимальное количество учеников", "Ошибка");
                return;
            }

            var studentGroup = Group.StudentGroups.FirstOrDefault(x => x.Student == student && x.Group == Group && x.IsDeleted);
            if (studentGroup == null)
            {
                Group.StudentGroups.Add(new StudentGroup
                {
                    Student = student,
                    Group = Group
                });
            }
            else
                studentGroup.IsDeleted = false;

            lvStudents.ItemsSource = Group.NotDeletedStudentGroups;
            lvStudents.Items.Refresh();
        }
    }
}
