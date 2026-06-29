using IronWord;
using IronWord.Models;
using Microsoft.Win32;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using пп_11.Data;
using пп_11.Enums;
using пп_11.Models;
using Color = IronWord.Models.Color;
using Paragraph = IronWord.Models.Paragraph;

namespace пп_11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private ContextDB db;
        public MainWindow()
        {
            InitializeComponent();
            db = new ContextDB();
            LoadAllData();
        }
        private void LoadAllData()
        {
            LoadDataGrids();
            LoadComboBoxes();
        }

        private void LoadDataGrids()
        {
            UserDataGrid.ItemsSource = db.Users.ToList();

        }

        private void LoadComboBoxes()
        {
            TypeOfGroundTextBoxGroundPlace.ItemsSource = Enum.GetValues(typeof(TypeOfGround));
            StatusTextBoxGroundPlace.ItemsSource = Enum.GetValues(typeof(Status));
            TypeOfObremeneniaComboBoxObremenenia.ItemsSource = Enum.GetValues(typeof(TypeOfObremenenia));
            YstanovlFaceTextBoxObremenenia.ItemsSource = Enum.GetValues(typeof(YstanovlFace));

            FreeGroundPlaceComboBoxPoluchitObremenenie.ItemsSource = (from g in db.GroundPlaces
                                                                      where g.Status == "Свободен"
                                                                      select g.KadastrNumber).ToList();
            TypeOfObremeneniaComboBoxObremenenia.ItemsSource = Enum.GetValues(typeof(TypeOfObremenenia));
            TypeRightComboBoxPoluchitObremenenie.ItemsSource = (from e in db.TypeOfRight
                                                               select e.Name).ToList();
            PravoobladatelComboBoxPoluchitObremenenie.ItemsSource = (from e in db.Pravoobladatelis
                                                                    select e.Name).ToList();
            YstanovlFaceComboBoxPoluchitObremenenie.ItemsSource = YstanovlFaceTextBoxObremenenia.ItemsSource = Enum.GetValues(typeof(YstanovlFace));

        }

        #region ground
        private void AddButtonGroundPlace_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string error = "";


            try
            {

                //GroundPlace groundPlace = new GroundPlace(KadastrNumberTextBoxGroundPlace.Text,);

                //db.GroundPlaces.Add(groundPlace);
                db.SaveChanges();

                LoadAllData();

                MessageBox.Show("Водитель добавлен", "Успех", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
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

        #region ground
        private void RefreshButtonGroundPlace_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion

        #region role
        private void AddButtonRole_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void DeleteButtonRole_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void EditButtonRole_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion

        #region user
        private void AddButtonUser_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void DeleteButtonUser_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void EditButtonUser_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion

        #region typeofright
        private void EditButtonTypeOfRight_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void DeleteButtonTypeOfRight_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void AddButtonTypeOfRight_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion

        #region obremenenia
        private void EditButtonObremenenia_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void DeleteButtonObremenenia_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void AddButtonObremenenia_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion

        #region pravoobladateli
        private void EditButtonPravoobladateli_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void DeleteButtonPravoobladateli_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void AddButtonPravoobladateli_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion

        #region typeofpravoobladateli
        private void EditButtonTypeOfPravoobladateli_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void DeleteButtonTypeOfPravoobladateli_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void AddButtonTypeOfPravoobladateli_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion

        private void SformirovatObremenenieButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Документ Word (*.docx)|*.docx";
            saveFileDialog.FileName = "МойДокумент.docx";

            var lastObremenenie = db.Obremenenia
                .OrderByDescending(o => o.Id)
                .FirstOrDefault();

            var prava = (Models.Right)(from p in db.Rights where lastObremenenie.IdRight == p.Id select p);
            string statusText = prava.StatusOfRight ? "Активно" : "Неактивно";
            var pravoobl = (Models.Pravoobladateli)(from pr in db.Pravoobladatelis where prava.IdPravoobladateli == pr.Id select pr);

            var ground = (Models.GroundPlace)(from gr in db.GroundPlaces where prava.IdGroundPlace == gr.Id select gr);
            
            if (saveFileDialog.ShowDialog() == true)
            {
                var doc = new WordDocument();
                var lines = new string[] { $"Правообладатель: {pravoobl.Name}", $"Документ-основание: {prava.DocumentOsnovanie}",$"Номер и дата регистрации: {lastObremenenie.NumberOfRegistration},{lastObremenenie.NumberOfRegistration}", $"Статус права: {statusText}", 
                    $"Выдано: Министерство сельского хозяйства Оренбургской области", $"Что выдано: {ground.KadastrNumber}" , $"Когда выдано: {prava.DateOfRegistration}"};

                foreach (string line in lines)
                {

                    var paragraph = new Paragraph();
                    var textRun = new IronWord.Models.Run(new TextContent(line));
                    textRun.Style = new TextStyle()
                    {
                        FontSize = 14,
                        Color = Color.Black,
                        TextFont = new Font() { FontFamily = "Arial" }
                    };

                    paragraph.AddChild(textRun);
                    doc.AddParagraph(paragraph);
                }

                doc.SaveAs(saveFileDialog.FileName);
                MessageBox.Show("Документ Word успешно сохранен");
            }
        }

        private void OformitPravoButtonType_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string error = "";
            if (FreeGroundPlaceComboBoxPoluchitObremenenie.Text == "") error += "Выберите свободный участок\n";
            if (TypeOfObremeneniaComboBoxObremenenia.Text == "") error += "Выберите тип обремениения\n";
            if (TypeRightComboBoxPoluchitObremenenie.Text == "") error += "Выберите вид права\n";
            if (PravoobladatelComboBoxPoluchitObremenenie.Text == "") error += "Выберите правообладателя\n";
            if (YstanovlFaceComboBoxPoluchitObremenenie.Text == "") error += "Выберите цстановленное лицо\n";

            if (!string.IsNullOrEmpty(error))
            { 
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                int lastRegistrationNumber2 = db.Rights.Any()
    ? db.Rights.Max(r => r.NumberOfRegistration)
    : 000000;

                int lastRegistrationNumber3 = db.Obremenenia.Any()
? db.Rights.Max(r => r.NumberOfRegistration)
: 0000000;
                var idground = from q in db.GroundPlaces
                                where q.KadastrNumber == Convert.ToInt32(FreeGroundPlaceComboBoxPoluchitObremenenie.Text)
                                select q.Id;
                var idpravoobl = from p in db.Pravoobladatelis 
                                 where p.Name == PravoobladatelComboBoxPoluchitObremenenie.Text
                                 select p.Id;
                var idtype = from t in db.TypeOfRight
                             where t.Name == TypeRightComboBoxPoluchitObremenenie.Text
                             select t.Id;
                Right right = new Right(lastRegistrationNumber2++, DateTime.Now, "Разрешение на использование земель",true, Convert.ToInt32(idground), Convert.ToInt32(idpravoobl), Convert.ToInt32(idtype));
                db.Rights.Add(right);
                db.SaveChanges();
                int idright = Convert.ToInt32(right.Id);
                Obremenenia obremenenia = new Obremenenia(TypeOfObremeneniaComboBoxPoluchitObremenenie.Text,lastRegistrationNumber3,DateTime.Now, DiscribeTextBoxPoluchitObremenenie.Text, YstanovlFaceComboBoxPoluchitObremenenie.Text,idright);
                db.Obremenenia.Add(obremenenia);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка - проверьте данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        
        }


        private void GroundPlaceDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GroundPlaceDataGrid.SelectedItem is GroundPlace selected)
            {
                KadastrNumberTextBoxGroundPlace.Text = (selected.KadastrNumber).ToString();
                AddresTextBoxGroundPlace.Text = selected.Addres;
                SquareTextBoxGroundPlace.Text = selected.Square.ToString();
                TypeOfGroundTextBoxGroundPlace.SelectedItem = selected.TypeOfGround;
                KadastrPriceTextBoxGroundPlace.Text = selected.KadastrPrice.ToString();
                StatusTextBoxGroundPlace.SelectedItem = selected.Status;
            }
        }

   
        private void RightDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RightDataGrid.SelectedItem is Right selected)
            {
                NumberOfRegistrationTextBoxRights.Text = (selected.NumberOfRegistration).ToString();
                DateOfRegistrationTextBoxRights.SelectedDate = selected.DateOfRegistration;
                DocumentOsnovanieTextBoxRights.Text = selected.DocumentOsnovanie;
                StatusOfRightTextBoxRights.IsChecked = selected.StatusOfRight;
                IdGroundPlaceTextBoxRights.Text = selected.IdGroundPlace.ToString();
                IdPravoobladateliTextBoxRights.Text = selected.IdPravoobladateli.ToString();
                IdTypeOfRightTextBoxRights.Text = selected.IdTypeOfRight.ToString();
            }
        }

 
        private void RoleDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RoleDataGrid.SelectedItem is Role selected)
            {
                NameTextBoxRole.Text = selected.Name;
                DiscribeTextBoxRole.Text = selected.Discribe;
            }
        }


        private void UserDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserDataGrid.SelectedItem is User selected)
            {
                LoginTextBoxUser.Text = selected.Name;
                PasswordPasswordBoxUser.Text = selected.Password;
                StatusCheckBoxUser.IsChecked = selected.Status;
                DataCreateTextBoxUser.SelectedDate = selected.DateCreate;
                IdRoleTextBoxUser.Text = selected.IdRole.ToString();
            }
        }


        private void TypeOfRightDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypeOfRightDataGrid.SelectedItem is TypeOfRight selected)
            {
                NameTextBoxTypeOfRight.Text = selected.Name;
                CodeNameTextBoxTypeOfRight.Text = (selected.CodeName).ToString();
                DiscribeTextBoxTypeOfRight.Text = selected.Dicribe;
            }
        }

        private void ObremeneneiaDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ObremeneneiaDataGrid.SelectedItem is Obremenenia selected)
            {
                TypeOfObremeneniaComboBoxObremenenia.SelectedItem = selected.TypeOfObremenenia;
                NumberOfRegistrationBoxObremenenia.Text = (selected.NumberOfRegistration).ToString();
                DataOfRegistrationDatePickerObremenenia.SelectedDate = selected.DataOfRegistration;
                DiscribeTextBoxObremenenia.Text = selected.Discribe;
                YstanovlFaceTextBoxObremenenia.Text = selected.YstanovlFace;
                PravoTextBoxObremenenia.Text = selected.IdRight.ToString();
            }
        }

        private void PravoobladateliDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PravoobladateliDataGrid.SelectedItem is Pravoobladateli selected)
            {
                NameTextBoxPravoobladateli.Text = selected.Name;
                INNTextBoxPravoobladateli.Text = (selected.INN).ToString();
                ORGNTextBoxPravoobladateli.Text = (selected.ORGN).ToString();
                KPPTextBoxPravoobladateli.Text = (selected.KPP).ToString();
                PhoneTextBoxPravoobladateli.Text = (selected.Phone).ToString();
                EmailTextBoxPravoobladateli.Text = selected.Email;
                AddresTextBoxPravoobladateli.Text = selected.Addres;
                IdTypeOfPravoobladateliTextBoxPravoobladateli.Text = selected.IdTypeOfPravoobladateli.ToString();
            }
        }

 
        private void TypeOfPravoobladateliDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypeOfPravoobladateliDataGrid.SelectedItem is TypeOfPravoobladateli selected)
            {
                NameTextBoxTypeOfPravoobladateli.Text = selected.Name;
            }
        }
    }
}