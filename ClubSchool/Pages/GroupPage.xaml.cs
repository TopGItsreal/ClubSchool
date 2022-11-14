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
    /// Interaction logic for GroupPage.xaml
    /// </summary>
    public partial class GroupPage : Page
    {
        public Group Group { get; set; }
        public List<Club> Clubs { get; set; }
        public List<Teacher> Teachers { get; set; }

        public GroupPage(Group group, bool isNew = false)
        {
            InitializeComponent();

            Group = group;
            Clubs = DataAccess.GetNotDeletedClubs();
            Teachers = DataAccess.GetNotDeletedTeachers();

            if (isNew)
                Title = $"Новая {Title}";
            else
                Title = $"{Title} {Group.Id}";

            this.DataContext = this;
        }

        private void cbTeacher_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbClub_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
