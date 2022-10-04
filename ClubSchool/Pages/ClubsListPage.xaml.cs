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
using Core;

namespace ClubSchool.Pages
{
    /// <summary>
    /// Interaction logic for ClubsListPage.xaml
    /// </summary>
    public partial class ClubsListPage : Page
    {
        public List<Club> Clubs { get; set; }
        public ClubsListPage()
        {
            InitializeComponent();
            Clubs = DataAccess.GetClubs();

            DataContext = this;
        }
    }
}
