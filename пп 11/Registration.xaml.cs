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
using System.Windows.Shapes;
using пп_11.Data;
using пп_11.Models;

namespace пп_11
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>

    public partial class Registration : Window
    {
        private ContextDB db;
        private List<string> roles = new List<string>() {
            "Администратор",
            "Оператор"
        };

        public Registration()
        {
            InitializeComponent();
            db  = new ContextDB();
            RoleComboBox.ItemsSource = roles;
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {

            string error = "";
            if (RegistrationTextBox.Text == "") error += "Заполните поле \"Логин\"\n";
            if (RegistrationPasswordBox.Password == "") error += "Заполните поле \"Пароль\"\n";
            if (BirchsdayDatePicker.Text == "") error += "Выберите Дату рождения\n";
            if (RoleComboBox.Text == "") error += "Выберите Роль\n";
            if (error != "")
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); return;
            }
            try
            {
                var user = db.Users.FirstOrDefault(x => x.Name == RegistrationTextBox.Text);
                if (user != null)
                {
                    ErrorRegisterLabel.Content = "Пользователь с таким Логином уже существует"; return;
                }

                User user1 = new User(RegistrationTextBox.Text, RegistrationPasswordBox.Password,true, DateTime.Parse(BirchsdayDatePicker.Text), RoleComboBox.Text);

                db.Users.Add(user1);
                db.SaveChanges();
                MessageBox.Show("Вы зарегистрировались!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Login loginWindow = new Login();
                loginWindow.Show(); this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        

        private void EnterInLofinLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Login login = new Login();
            login.Show(); this.Close();
        }

        private void EnterInLofinLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            RegistrationButton.Foreground = Brushes.Black;
        }

        private void EnterInLofinLabel_MouseLeave(object sender, MouseEventArgs e)
        {
            RegistrationButton.Foreground = Brushes.White;
        }
    }
}
