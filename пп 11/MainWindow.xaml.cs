using System.ComponentModel.DataAnnotations.Schema;
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
using Grpc.Core;
using IronWord;
using IronWord.Models;
using Microsoft.Win32;
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
            TypeOfRightDataGrid.ItemsSource = db.TypeOfRight.ToList();
            TypeOfPravoobladateliDataGrid.ItemsSource = db.TypeOfPravoobladatelis.ToList();
            RoleDataGrid.ItemsSource = db.Roles.ToList();
            RightDataGrid.ItemsSource = db.Rights.ToList();
            PravoobladateliDataGrid.ItemsSource = db.Pravoobladatelis.ToList();
            ObremeneneiaDataGrid.ItemsSource = db.Obremenenia.ToList();
            GroundPlaceDataGrid.ItemsSource = db.GroundPlaces.ToList();
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
            if (string.IsNullOrEmpty(KadastrNumberTextBoxGroundPlace.Text))
            {
                error += "Введите кадастровый номер земельного участка\n";
            }
            if (string.IsNullOrEmpty(AddresTextBoxGroundPlace.Text))
            {
                error += "Введите адрес земельного участка\n";
            }
            if (string.IsNullOrEmpty(SquareTextBoxGroundPlace.Text))
            {
                error += "Введите площадь земельного участка\n";
            }
            if (string.IsNullOrEmpty(TypeOfGroundTextBoxGroundPlace.Text))
            {
                error += "Введите категорию земли земельного участка\n";
            }
            if (string.IsNullOrEmpty(KadastrPriceTextBoxGroundPlace.Text))
            {
                error += "Введите кадастровую стоимость земельного участка\n";
            }
            if (string.IsNullOrEmpty(StatusTextBoxGroundPlace.Text))
            {
                error += "Введите статус земельного участка\n";
            }

            if (error != "")
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var groundPlace = new GroundPlace
                {

                    KadastrNumber = Convert.ToInt32(KadastrNumberTextBoxGroundPlace),
                    Addres = AddresTextBoxGroundPlace.Text,
                    Square = Convert.ToDouble(SquareTextBoxGroundPlace),
                    TypeOfGround = TypeOfGroundTextBoxGroundPlace.Text,
                    KadastrPrice = Convert.ToDouble(KadastrPriceTextBoxGroundPlace),
                    Status = StatusTextBoxGroundPlace.Text

                };

                db.GroundPlaces.Add(groundPlace);
                db.SaveChanges();

                LoadAllData();
                MessageBox.Show("Земельный участок добавлен", "Успех", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            catch (Exception ex)
            { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void DeleteButtonGroundPlace_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (GroundPlaceDataGrid.SelectedItem is not GroundPlace selected)
            {
                MessageBox.Show("Выберите земельный участок для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show($"Удалить земельный участок '{selected.KadastrNumber}'?\n", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);


            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    db.GroundPlaces.Remove(selected);
                    db.SaveChanges();

                    LoadAllData();


                    MessageBox.Show("Земельный участок удален", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                catch (Exception ex)
                { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }

            }
        }

        private void ChangeButtonGroundPlace_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion

        #region right
        private void AddButtonRight_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string error = "";
            if (string.IsNullOrEmpty(NumberOfRegistrationTextBoxRights.Text))
            {
                error += "Введите номер регистрации права\n";
            }
            if (string.IsNullOrEmpty(DateOfRegistrationTextBoxRights.Text))
            {
                error += "Введите дату регистрации права\n";
            }
            if (string.IsNullOrEmpty(DocumentOsnovanieTextBoxRights.Text))
            {
                error += "Введите документ основание права\n";
            }
            if (string.IsNullOrEmpty(IdGroundPlaceTextBoxRights.Text))
            {
                error += "Введите Id земельного участка\n";
            }
            if (string.IsNullOrEmpty(IdPravoobladateliTextBoxRights.Text))
            {
                error += "Введите Id правообладателя\n";
            }
            if (string.IsNullOrEmpty(IdTypeOfRightTextBoxRights.Text))
            {
                error += "Введите Id вида\n";
            }

            if (error != "")
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var right = new Right
                {

                    NumberOfRegistration = Convert.ToInt32(NumberOfRegistrationTextBoxRights.Text),
                    DateOfRegistration = Convert.ToDateTime(DateOfRegistrationTextBoxRights.Text),
                    DocumentOsnovanie = DocumentOsnovanieTextBoxRights.Text,
                    StatusOfRight = Convert.ToBoolean(StatusOfRightTextBoxRights),
                    IdGroundPlace = Convert.ToInt32(IdGroundPlaceTextBoxRights.Text),
                    IdPravoobladateli = Convert.ToInt32(IdPravoobladateliTextBoxRights.Text),
                    IdTypeOfRight = Convert.ToInt32(IdTypeOfRightTextBoxRights)

                };

                db.Rights.Add(right);
                db.SaveChanges();

                LoadAllData();
                MessageBox.Show("Право добавлено", "Успех", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            catch (Exception ex)
            { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void DeleteButtonRight_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (RightDataGrid.SelectedItem is not Right selected)
            {
                MessageBox.Show("Выберите право для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show($"Удалить право '{selected.NumberOfRegistration}'?\n", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);


            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    db.Rights.Remove(selected);
                    db.SaveChanges();

                    LoadAllData();


                    MessageBox.Show("Право удалено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                catch (Exception ex)
                { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }

            }
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
            string error = "";
            if (string.IsNullOrEmpty(NameTextBoxRole.Text))
            {
                error += "Введите наименование роли\n";
            }

            if (error != "")
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var role = new Role
                {

                    Name = NameTextBoxRole.Text,
                    Discribe = DiscribeTextBoxRole.Text

                };

                db.Roles.Add(role);
                db.SaveChanges();

                LoadAllData();
                MessageBox.Show("Роль добавлена", "Успех", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            catch (Exception ex)
            { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void DeleteButtonRole_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (RoleDataGrid.SelectedItem is not Role selected)
            {
                MessageBox.Show("Выберите роль для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show($"Удалить роль '{selected.Name}'?\n", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);


            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    db.Roles.Remove(selected);
                    db.SaveChanges();

                    LoadAllData();


                    MessageBox.Show("Роль удалена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                catch (Exception ex)
                { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }

            }
        }

        private void EditButtonRole_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion

        #region user
        private void AddButtonUser_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string error = "";
            if (string.IsNullOrEmpty(LoginTextBoxUser.Text))
            {
                error += "Введите логинn";
            }
            if (string.IsNullOrEmpty(PasswordPasswordBoxUser.Text))
            {
                error += "Введите пароль\n";
            }
            if (string.IsNullOrEmpty(DataCreateTextBoxUser.Text))
            {
                error += "Введите дату создания\n";
            }
            if (string.IsNullOrEmpty(IdRoleTextBoxUser.Text))
            {
                error += "Введите Id роли\n";
            }

            if (error != "")
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var user = new User
                {

                    Name = NameTextBoxRole.Text,
                    Password = DiscribeTextBoxRole.Text,
                    Status = Convert.ToBoolean(StatusCheckBoxUser),
                    DateCreate = Convert.ToDateTime(DataCreateTextBoxUser),
                    IdRole = Convert.ToInt32(IdRoleTextBoxUser)
                };

                db.Users.Add(user);
                db.SaveChanges();

                LoadAllData();
                MessageBox.Show("Пользователь добавлен", "Успех", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            catch (Exception ex)
            { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void DeleteButtonUser_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (UserDataGrid.SelectedItem is not User selected)
            {
                MessageBox.Show("Выберите пользователя для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show($"Удалить пользователя '{selected.Name}'?\n", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);


            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    db.Users.Remove(selected);
                    db.SaveChanges();

                    LoadAllData();


                    MessageBox.Show("Пользователь удален", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                catch (Exception ex)
                { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }

            }
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
            if (TypeOfRightDataGrid.SelectedItem is not TypeOfRight selected)
            {
                MessageBox.Show("Выберите вид права для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show($"Удалить вид права '{selected.Name}'?\n", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);


            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    db.TypeOfRight.Remove(selected);
                    db.SaveChanges();

                    LoadAllData();


                    MessageBox.Show("Вид права удален", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                catch (Exception ex)
                { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }

            }
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
            if (ObremeneneiaDataGrid.SelectedItem is not Obremenenia selected)
            {
                MessageBox.Show("Выберите обременение для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show($"Удалить обременение '{selected.TypeOfObremenenia}'?\n", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);


            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    db.Obremenenia.Remove(selected);
                    db.SaveChanges();

                    LoadAllData();


                    MessageBox.Show("Обременение удалено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                catch (Exception ex)
                { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }

            }
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
            if (PravoobladateliDataGrid.SelectedItem is not Pravoobladateli selected)
            {
                MessageBox.Show("Выберите правообладателя для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show($"Удалить правообладателя '{selected.Name}'?\n", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);


            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    db.Pravoobladatelis.Remove(selected);
                    db.SaveChanges();

                    LoadAllData();


                    MessageBox.Show("Правообладатель удален", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                catch (Exception ex)
                { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }

            }
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
            if (TypeOfPravoobladateliDataGrid.SelectedItem is not TypeOfPravoobladateli selected)
            {
                MessageBox.Show("Выберите тип правообладателя для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show($"Удалить тип правообладателя '{selected.Name}'?\n", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);


            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    db.TypeOfPravoobladatelis.Remove(selected);
                    db.SaveChanges();

                    LoadAllData();


                    MessageBox.Show("Тип правообладателя удален", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                catch (Exception ex)
                { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }

            }
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