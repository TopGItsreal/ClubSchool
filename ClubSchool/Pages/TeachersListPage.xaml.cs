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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClubSchool.Pages
{
    /// <summary>
    /// Interaction logic for TeachersListPage.xaml
    /// </summary>
    public partial class TeachersListPage : Page
    {
        public List<Teacher> Teachers { get; set; }

        public TeachersListPage()
        {
            InitializeComponent();

            Teachers = DataAccess.GetTeachers();
            DataAccess.AddNewItemEvent += DataAccess_AddNewItemEvent;

            this.DataContext = this;
        }

        private void DataAccess_AddNewItemEvent()
        {
            Teachers = DataAccess.GetTeachers();
            lvTeachers.ItemsSource = Teachers;
            lvTeachers.Items.Refresh();
        }

        private void btnNewTeacher_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TeacherPage(new Teacher()));
        }

        private void lvTeachers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var teacher = lvTeachers.SelectedItem as Teacher;

            if (teacher != null)
                NavigationService.Navigate(new TeacherPage(teacher));
        }

        private void SearchText(object sender, TextChangedEventArgs e)
        {
            var lastName = tbLastName.Text.ToLower();
            var firstName = tbFirstName.Text.ToLower();

            lvTeachers.ItemsSource = Teachers.FindAll(x => x.FirstName.ToLower().Contains(lastName) ||
                                                           x.LastName.ToLower().Contains(firstName));
        }
    }
}
