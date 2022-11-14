﻿using Core;
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

            if (isNew)
                Title = $"Новая {Title}";
            else
                Title = $"{Title} {Group.Id}";

            this.DataContext = this;
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
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataAccess.SaveGroup(Group);
                NavigationService.GoBack();
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить группу", "Ошибка");
            }
        }

        private void lvStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var studentGroup = lvStudents.SelectedItem as StudentGroup;

            if (studentGroup == null)
                return;

            var message = MessageBox.Show($"Вы точно хотите отчислить {studentGroup.Student.LastName} {studentGroup.Student.FirstName}?",
                                          "Предупреждение",
                                          MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

            if (message == MessageBoxResult.Yes)
            {
                Group.StudentGroups.Remove(studentGroup);
                lvStudents.ItemsSource = Group.StudentGroups;
                lvStudents.Items.Refresh();
            }
        }

        private void cbStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var student = cbStudents.SelectedItem as Student;

            if (student == null)
                return;

            Group.StudentGroups.Add(new StudentGroup
            {
                Student = student,
                Group = Group
            });

            lvStudents.ItemsSource = Group.StudentGroups;
            lvStudents.Items.Refresh();
        }
    }
}
