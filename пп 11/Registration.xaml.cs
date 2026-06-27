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

namespace пп_11
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {

        private List<string> roles = new List<string>() {
            "Администратор",
            "Оператор"
        };

        public Registration()
        {
            InitializeComponent();
            RoleComboBox.ItemsSource = roles;
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorRegisterLabel.Content = "Ошибка!";
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
