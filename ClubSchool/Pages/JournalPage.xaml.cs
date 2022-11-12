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
    /// Interaction logic for JournalPage.xaml
    /// </summary>
    public partial class JournalPage : Page
    {
        public ICollection<Journal> Journals { get; set; }

        public JournalPage(Schedule schedule)
        {
            InitializeComponent();

            var dateEquals = schedule.Date.Date == DateTime.Today.Date;

            if (schedule.TeacherClub.Teacher.User.Id != App.User.Id)
                spMain.IsEnabled = false;

            if (schedule.Journals.Count() != 0)
                Journals = schedule.Journals;
            else
            {
                if (!dateEquals || schedule.TeacherClub.Teacher.User.Id != App.User.Id)
                    return;

                Journals = new List<Journal>();
                foreach (var StudentTeacherClub in schedule.TeacherClub.StudentTeacherClubs)
                {
                    Journals.Add(new Journal
                    {
                        Schedule = schedule,
                        StudentTeacherClub = StudentTeacherClub
                    });
                }

                DataAccess.SaveJournals(Journals, true);
            }

            this.DataContext = this;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DataAccess.SaveJournals(Journals);
            NavigationService.GoBack();
        }
    }
}
