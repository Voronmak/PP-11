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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorLabel.Content = "Ошибка!";
        }

        private void NoLoginRegisterLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show(); this.Close();
        }

        private void NoRegisterLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            LoginButton.Foreground = Brushes.Black;
        }

        private void NoRegisterLabel_MouseLeave(object sender, MouseEventArgs e)
        {
            LoginButton.Foreground = Brushes.White;
        }
    }
}
