﻿using System;
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
    /// Interaction logic for AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var login = tbLogin.Text;
            var password = pbPassword.Password;

            if ((App.Teacher = DataAccess.LoginTeacher(login, password)) == null)
            {
                MessageBox.Show("Неверный логин и/или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            NavigationService.Navigate(new ClubsListPage());
        }
    }
}
