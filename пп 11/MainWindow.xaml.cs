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
using пп_11.Enums;

namespace пп_11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadComboBoxes();
        }

        private void LoadComboBoxes()
        {
            TypeOfGroundTextBoxGroundPlace.ItemsSource = Enum.GetValues(typeof(TypeOfGround));
            StatusTextBoxGroundPlace.ItemsSource = Enum.GetValues(typeof(Status));
        }

        #region ground
        private void AddButtonGroundPlace_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void DeleteButtonGroundPlace_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ChangeButtonGroundPlace_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion

        #region right
        private void AddButtonRight_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void DeleteButtonRight_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void EditButtonRight_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion

        private void RefreshButtonGroundPlace_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void AddButtonRole_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void DeleteButtonRole_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void EditButtonRole_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void AddButtonUser_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void DeleteButtonUser_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void EditButtonUser_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void EditButtonTypeOfRight_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void DeleteButtonTypeOfRight_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void AddButtonTypeOfRight_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void EditButtonObremenenia_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void DeleteButtonObremenenia_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void AddButtonObremenenia_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}