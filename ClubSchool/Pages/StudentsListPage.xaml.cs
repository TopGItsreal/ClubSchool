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
        public List<Student> Students { get; set; }

        public StudentsListPage()
        {
            InitializeComponent();
            Students = DataAccess.GetStudents();
            DataContext = this;
        }

        private void LvStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var student = (sender as ListView).SelectedItem as Student;
            NavigationService.Navigate(new ClubsListPage(student));
        }
    }
}
