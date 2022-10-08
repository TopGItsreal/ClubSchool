using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Linq;
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
    /// Interaction logic for StudentsListPage.xaml
    /// </summary>
    public partial class StudentsListPage : Page
    {
        public IEnumerable<Student> Students { get; set; }
        public StudentsListPage()
        {
            InitializeComponent();
            Students = DataAccess.GetStudents();
            DataContext = this;
        }
        public StudentsListPage(IEnumerable<Student> students)
        {
            InitializeComponent();
            Students = students;
            DataContext = this;
        }

        private void LvStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var student = (sender as ListView).SelectedItem as Student;
            var clubs = student.StudentClubs.Select(x => x.Club);
            NavigationService.Navigate(new ClubsListPage(clubs));
        }

        private void LvStudents_MouseLeave(object sender, MouseEventArgs e)
        {
            gvStudents.Columns[2].Width = 0;
        }

        private void LvStudents_MouseEnter(object sender, MouseEventArgs e)
        {
            gvStudents.Columns[2].Width = 200;
        }
    }
}
