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
            DataAccess.AddNewItemEvent += DataAccess_AddNewItemEvent;

            this.DataContext = this;
        }

        private void DataAccess_AddNewItemEvent()
        {
            Groups = DataAccess.GetNotDeletedGroups();
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
            NavigationService.Navigate(new GroupPage(lvGroups.SelectedItem as Group));
        }
    }
}
