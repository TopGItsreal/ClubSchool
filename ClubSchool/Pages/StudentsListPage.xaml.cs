﻿using System;
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
        public List<Class> Classes { get; set; }

        public StudentsListPage()
        {
            InitializeComponent();
            Classes = DataAccess.GetClasses();
            Classes.Insert(0, new Class { Name = "Все классы" });

            Students = DataAccess.GetStudents().OrderBy(x => x.Class.Name).ThenBy(y => y.LastName).ToList();
            DataContext = this;
        }

        private void LvStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var student = (sender as ListView).SelectedItem as Student;
            NavigationService.Navigate(new ClubsListPage(student));
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = tbSearch.Text.ToLower();
            lvStudents.ItemsSource = Students.FindAll(x => x.LastName.ToLower().Contains(text));
        }

        private void cbClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var text = tbSearch.Text.ToLower();
            var students = Students.FindAll(x => x.LastName.ToLower().Contains(text));
            if ((cbClass.SelectedItem as Class).Name != "Все классы")
            {
                students = students.FindAll(x => x.Class == cbClass.SelectedItem as Class);
            }

            lvStudents.ItemsSource = students;
        }
    }
}
