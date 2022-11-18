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
    /// Interaction logic for GroupsListPage.xaml
    /// </summary>
    public partial class GroupsListPage : Page
    {
        public List<Group> Groups { get; set; }
        public GroupsListPage()
        {
            InitializeComponent();

            Groups = DataAccess.GetNotDeletedGroups();
            if (!DataAccess.IsAdmin(App.Teacher.User))
                Groups = Groups.FindAll(x => x.Teacher == App.Teacher);
            DataAccess.AddNewItemEvent += DataAccess_AddNewItemEvent;

            btnNewGroup.Visibility = DataAccess.IsAdmin(App.Teacher.User) ?
                                        Visibility.Visible : Visibility.Collapsed;

            this.DataContext = this;
        }

        private void DataAccess_AddNewItemEvent()
        {
            Groups = DataAccess.GetNotDeletedGroups();
            if (!DataAccess.IsAdmin(App.Teacher.User))
                Groups = Groups.FindAll(x => x.Teacher == App.Teacher);
            lvGroups.ItemsSource = Groups;
            lvGroups.Items.Refresh();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = tbSearch.Text.ToLower();
            lvGroups.ItemsSource = Groups.FindAll(x => x.Name.ToLower().Contains(text));
        }

        private void btnNewGroup_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GroupPage(new Group(), true));
        }

        private void lvGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var group = lvGroups.SelectedItem as Group;

            if (group != null)
                NavigationService.Navigate(new GroupPage(group));
        }
    }
}
