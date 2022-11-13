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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClubSchool.Pages
{
    /// <summary>
    /// Interaction logic for TeacherPage.xaml
    /// </summary>
    public partial class TeacherPage : Page
    {
        public Teacher Teacher { get; set; }

        public TeacherPage(Teacher teacher)
        {
            InitializeComponent();

            Teacher = teacher;

            this.DataContext = Teacher;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataAccess.SaveTeacher(Teacher);
                NavigationService.GoBack();
            }
            catch
            {
                MessageBox.Show("Ну удалось сохранить учителя", "Ошибка");
            }
        }
    }
}
