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
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace ClubSchool.Pages
{
    /// <summary>
    /// Interaction logic for TeacherPage.xaml
    /// </summary>
    public partial class TeacherPage : Page
    {
        public Teacher Teacher { get; set; }

        public TeacherPage(Teacher teacher, bool isNew = false)
        {
            InitializeComponent();

            Teacher = teacher;
            if (isNew)
                Title = $"Новый {Title}";
            else
                Title = $"{Title} {Teacher.Id}";

            this.DataContext = Teacher;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errorMessage = new StringBuilder();
            try
            {
                if(!IsLoginFilled(Teacher.User.Login))
                    errorMessage.AppendLine("Логин не должен быть пустым.");
                if (!DataAccess.IsLoginUnique(Teacher.User.Login))
                    errorMessage.AppendLine("Логин должен быть уникальным.");
                if (!IsNameValid(Teacher.FirstName))
                    errorMessage.AppendLine("Некорректное имя.");
                if (!IsNameValid(Teacher.LastName))
                    errorMessage.AppendLine("Некорректная фамилия.");
                if (!string.IsNullOrWhiteSpace(Teacher.User.Email) && !IsEmailValid(Teacher.User.Email))
                    errorMessage.AppendLine("Некорректный email адрес.");
                if (!IsPasswordStrong(Teacher.User.Password))
                {
                    errorMessage.AppendLine("Пароль должен содержать минимум 6 сиволов и включать спецсимволы или цифры.");
                    throw new Exception();
                }

                DataAccess.SaveTeacher(Teacher);
                NavigationService.GoBack();
            }
            catch
            {
                MessageBox.Show(errorMessage.ToString(), "Ошибка");
            }
        }

        private bool IsLoginFilled(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                return false;

            return true;
        }

        private bool IsNameValid(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            foreach (char c in name)
            {
                if (char.IsDigit(c))
                    return false;
            }
            return true;
        }
        private bool IsEmailValid(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private bool IsPasswordStrong(string password)
        {
            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^])(?=.*[^a-zA-Z0-9])\S{6,16}$");


            return !string.IsNullOrWhiteSpace(password) && regex.IsMatch(password);
        }
    }
}
