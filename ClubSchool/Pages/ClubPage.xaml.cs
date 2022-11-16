using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using System.Text.RegularExpressions;
using Core;

namespace ClubSchool.Pages
{
    /// <summary>
    /// Interaction logic for ClubPage.xaml
    /// </summary>
    public partial class ClubPage : Page
    {
        public Club Club { get; set; }
        public List<Club> Clubs { get; set; }
        public List<Core.Group> Groups { get; set; }

        public ClubPage(Club club)
        {
            InitializeComponent();
            Club = club;
            Clubs = DataAccess.GetClubs();
            Groups = DataAccess.GetNotDeletedGroups().FindAll(x => x.ClubId == Club.Id);

            if (Groups.Count() == 0)
                spGroup.Visibility = Visibility.Collapsed;

            if (!DataAccess.IsAdmin(App.Teacher.User))
                gridMain.IsEnabled = false;

            DataContext = this;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Сделать проверку на корректное название
            try
            {
                if (string.IsNullOrWhiteSpace(Club.Name))
                    throw new Exception();
                DataAccess.SaveClub(Club);
                NavigationService.GoBack();
            }
            catch
            {
                MessageBox.Show("Сохранение не удалось", "Ошибка");
            }
        }
    }
}
