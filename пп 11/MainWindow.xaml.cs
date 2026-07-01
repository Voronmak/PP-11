using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
using IronWord.Models.Enums;
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

        private List<string> StatusList = new List<string>
        {
            "Свободен",
            "Занят",
            "В аренде"
        };

        private List<string> TypeOfGroundList = new List<string>
        {
            "Земли сельскохозяйственного назначения",
            "Земли населённых пунктов",
            "Земли промышленности, энергетики, транспорта и иного специального назначения",
            "Земли особо охраняемых территорий и объектов",
            "Земли лесного фонда",
            "Земли водного фонда",
            "Земли запаса"
        };

        private List<string> TypeOfObremeneniaList = new List<string>
        {
            "Ипотека",
            "Доверительное управление",
            "Наем"
        };


        private List<string> YstanovlFaceList = new List<string>
        {
            "Ипотека",
            "Аренда",
            "Рента"
        };
        private List<string> roles = new List<string>() {
            "Администратор",
            "Оператор"
        };

        public MainWindow(string role)
        {
            InitializeComponent();
            db = new ContextDB();
            LoadAllData();
            if (role == "Оператор")
            {
                MainTabControl.SelectedItem = PoluchitObremenenieTabItem;
                GroundPlaceTabItem.Visibility = Visibility.Collapsed;
                RightTabItem.Visibility = Visibility.Collapsed;
                UserTabItem.Visibility = Visibility.Collapsed;
                TypeOfRightsTabItem.Visibility = Visibility.Collapsed;
                PravoobladateliTabItem.Visibility = Visibility.Collapsed;
                TypeOfPravoobladateliTabItem.Visibility = Visibility.Collapsed;
            }
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
            RightDataGrid.ItemsSource = db.Rights.ToList();
            PravoobladateliDataGrid.ItemsSource = db.Pravoobladatelis.ToList();
            ObremeneneiaDataGrid.ItemsSource = db.Obremenenia.ToList();
            GroundPlaceDataGrid.ItemsSource = db.GroundPlaces.ToList();
        }

        private void LoadComboBoxes()
        {
            TypeOfGroundTextBoxGroundPlace.ItemsSource = TypeOfGroundList;
            StatusTextBoxGroundPlace.ItemsSource = StatusList;
            TypeOfObremeneniaComboBoxObremenenia.ItemsSource = TypeOfObremeneniaList;
            YstanovlFaceTextBoxObremenenia.ItemsSource = YstanovlFaceList;
            IdRoleTextBoxUser.ItemsSource = roles;
            FreeGroundPlaceComboBoxPoluchitObremenenie.ItemsSource = (from g in db.GroundPlaces
                                                                      where g.Status == "Свободен"
                                                                      select g.KadastrNumber).ToList();
            TypeOfObremeneniaComboBoxPoluchitObremenenie.ItemsSource = TypeOfObremeneniaList;
            TypeRightComboBoxPoluchitObremenenie.ItemsSource = (from e in db.TypeOfRight
                                                               select e.Name).ToList();
            PravoobladatelComboBoxPoluchitObremenenie.ItemsSource = (from e in db.Pravoobladatelis
                                                                    select e.Name).ToList();
            YstanovlFaceComboBoxPoluchitObremenenie.ItemsSource = YstanovlFaceTextBoxObremenenia.ItemsSource = YstanovlFaceList;

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

                    KadastrNumber = Convert.ToInt32(KadastrNumberTextBoxGroundPlace.Text),
                    Addres = AddresTextBoxGroundPlace.Text,
                    Square = Convert.ToDouble(SquareTextBoxGroundPlace.Text),
                    TypeOfGround = TypeOfGroundTextBoxGroundPlace.Text,
                    KadastrPrice = Convert.ToDouble(KadastrPriceTextBoxGroundPlace.Text),
                    Status = StatusTextBoxGroundPlace.Text

                };

                db.GroundPlaces.Add(groundPlace);
                db.SaveChanges();

                LoadAllData();
                ClearGround();
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
                    ClearGround() ;

                    MessageBox.Show("Земельный участок удален", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                catch (Exception ex)
                { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }

            }
        }

        private void ChangeButtonGroundPlace_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (GroundPlaceDataGrid.SelectedItem is not GroundPlace selected)
            {
                MessageBox.Show("Выберите земельный участок для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

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


                selected.KadastrNumber = Convert.ToInt32(KadastrNumberTextBoxGroundPlace.Text);
                    selected.Addres = AddresTextBoxGroundPlace.Text;
                    selected.Square = Convert.ToDouble(SquareTextBoxGroundPlace.Text);
                    selected.TypeOfGround = TypeOfGroundTextBoxGroundPlace.Text;
                    selected.KadastrPrice = Convert.ToDouble(KadastrPriceTextBoxGroundPlace.Text);
                    selected.Status = StatusTextBoxGroundPlace.Text;

                db.SaveChanges();

                LoadAllData();
                ClearGround();
                MessageBox.Show("Земельный участок изменен", "Успех", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            catch (Exception ex)
            { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
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
                    StatusOfRight = StatusOfRightTextBoxRights.IsChecked.Value,
                    IdGroundPlace = Convert.ToInt32(IdGroundPlaceTextBoxRights.Text),
                    IdPravoobladateli = Convert.ToInt32(IdPravoobladateliTextBoxRights.Text),
                    IdTypeOfRight = Convert.ToInt32(IdTypeOfRightTextBoxRights.Text)

                };

                db.Rights.Add(right);
                db.SaveChanges();
                var gra = (from q in db.GroundPlaces
                           where q.Id == Convert.ToInt32(IdGroundPlaceTextBoxRights.Text)
                           select q).FirstOrDefault();
                gra.Status = StatusList[1];
                db.SaveChanges();
                LoadAllData();
                ClearRight();
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
                    ClearRight();

                    MessageBox.Show("Право удалено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                catch (Exception ex)
                { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }

            }
        }

        private void EditButtonRight_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (RightDataGrid.SelectedItem is not Right selected)
            {
                MessageBox.Show("Выберите право для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

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

                selected.NumberOfRegistration = Convert.ToInt32(NumberOfRegistrationTextBoxRights.Text);
                selected.DateOfRegistration = Convert.ToDateTime(DateOfRegistrationTextBoxRights.Text);
                selected.DocumentOsnovanie = DocumentOsnovanieTextBoxRights.Text;
                selected.StatusOfRight = StatusOfRightTextBoxRights.IsChecked.Value;
                selected.IdGroundPlace = Convert.ToInt32(IdGroundPlaceTextBoxRights.Text);
                selected.IdPravoobladateli = Convert.ToInt32(IdPravoobladateliTextBoxRights.Text);
                selected.IdTypeOfRight = Convert.ToInt32(IdTypeOfRightTextBoxRights.Text);

                db.SaveChanges();

                LoadAllData();
                ClearRight();
                MessageBox.Show("Право изменено", "Успех", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            catch (Exception ex)
            { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        #endregion

        #region ground
        private void RefreshButtonGroundPlace_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion



        #region user
        private void AddButtonUser_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string error = "";
            if (string.IsNullOrEmpty(LoginTextBoxUser.Text))
            {
                error += "Введите логин\n";
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

                    Name = LoginTextBoxUser.Text,
                    Password = PasswordPasswordBoxUser.Text,
                    Status = Convert.ToBoolean(StatusCheckBoxUser),
                    DateCreate = Convert.ToDateTime(DataCreateTextBoxUser.Text),
                    Role = IdRoleTextBoxUser.Text
                };

                db.Users.Add(user);
                db.SaveChanges();

                LoadAllData();
                ClearUser();
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
                    ClearUser();

                    MessageBox.Show("Пользователь удален", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                catch (Exception ex)
                { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }

            }
        }

        private void EditButtonUser_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (UserDataGrid.SelectedItem is not User selected)
            {
                MessageBox.Show("Выберите пользователя для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string error = "";
            if (string.IsNullOrEmpty(LoginTextBoxUser.Text))
            {
                error += "Введите логин\n";
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
                selected.Name = LoginTextBoxUser.Text;
                selected.Password = PasswordPasswordBoxUser.Text;
                selected.Status = Convert.ToBoolean(StatusCheckBoxUser);
                selected.DateCreate = Convert.ToDateTime(DataCreateTextBoxUser.Text);
                selected.Role =IdRoleTextBoxUser.Text;

                db.SaveChanges();

                LoadAllData();
                ClearUser();
                MessageBox.Show("Пользователь изменен", "Успех", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            catch (Exception ex)
            { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        #endregion

        #region typeofright
        private void EditButtonTypeOfRight_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (TypeOfRightDataGrid.SelectedItem is not TypeOfRight selected)
            {
                MessageBox.Show("Выберите вид права для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string error = "";
            if (string.IsNullOrEmpty(NameTextBoxTypeOfRight.Text))
            {
                error += "Введите наименование вида права\n";
            }
            if (string.IsNullOrEmpty(CodeNameTextBoxTypeOfRight.Text))
            {
                error += "Введите кодовое обозначение вида\n";
            }

            if (error != "")
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {

                selected.Name = NameTextBoxTypeOfRight.Text;
                selected.CodeName = Convert.ToInt32(CodeNameTextBoxTypeOfRight.Text);
                selected.Dicribe = DiscribeTextBoxTypeOfRight.Text;

                db.SaveChanges();

                LoadAllData();
                ClearTypeOfRight();
                MessageBox.Show("Вид права изменен", "Успех", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            catch (Exception ex)
            { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
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
                    ClearTypeOfRight();

                    MessageBox.Show("Вид права удален", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                catch (Exception ex)
                { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }

            }
        }

        private void AddButtonTypeOfRight_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string error = "";
            if (string.IsNullOrEmpty(NameTextBoxTypeOfRight.Text))
            {
                error += "Введите наименование вида права\n";
            }
            if (string.IsNullOrEmpty(CodeNameTextBoxTypeOfRight.Text))
            {
                error += "Введите кодовое обозначение вида\n";
            }

            if (error != "")
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                string name = NameTextBoxTypeOfRight.Text;
                string dicribe = DiscribeTextBoxTypeOfRight.Text;
                var typeOfRights = new TypeOfRight(name, Convert.ToInt32(CodeNameTextBoxTypeOfRight.Text), dicribe);

                db.TypeOfRight.Add(typeOfRights);
                db.SaveChanges();

                LoadAllData();
                ClearTypeOfRight();
                MessageBox.Show("Вид права добавлен", "Успех", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            catch (Exception ex)
            { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        #endregion

        #region obremenenia
        private void EditButtonObremenenia_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ObremeneneiaDataGrid.SelectedItem is not Obremenenia selected)
            {
                MessageBox.Show("Выберите обременение для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string error = "";
            if (string.IsNullOrEmpty(TypeOfObremeneniaComboBoxObremenenia.Text))
            {
                error += "Введите тип обременения\n";
            }
            if (string.IsNullOrEmpty(NumberOfRegistrationBoxObremenenia.Text))
            {
                error += "Введите номер обременения\n";
            }
            if (string.IsNullOrEmpty(DataOfRegistrationDatePickerObremenenia.Text))
            {
                error += "Введите дату обременения\n";
            }
            if (string.IsNullOrEmpty(YstanovlFaceTextBoxObremenenia.Text))
            {
                error += "Введите установленное лицо обременения\n";
            }
            if (string.IsNullOrEmpty(PravoTextBoxObremenenia.Text))
            {
                error += "Введите право обременения\n";
            }


            if (error != "")
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {

                selected.TypeOfObremenenia = TypeOfObremeneniaComboBoxObremenenia.Text;
                    selected.NumberOfRegistration = Convert.ToInt32(NumberOfRegistrationBoxObremenenia.Text);
                selected.DataOfRegistration = Convert.ToDateTime(DataOfRegistrationDatePickerObremenenia.Text);
                    selected.YstanovlFace = YstanovlFaceTextBoxObremenenia.Text;
                    selected.Discribe = DiscribeTextBoxObremenenia.Text;
                    selected.IdRight = Convert.ToInt32(PravoTextBoxObremenenia.Text);

                db.SaveChanges();

                LoadAllData();
                ClearObremenenia();
                MessageBox.Show("Обременение изменено", "Успех", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            catch (Exception ex)
            { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
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
                    ClearObremenenia();

                    MessageBox.Show("Обременение удалено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                catch (Exception ex)
                { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }

            }
        }

        private void AddButtonObremenenia_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string error = "";
            if (string.IsNullOrEmpty(TypeOfObremeneniaComboBoxObremenenia.Text))
            {
                error += "Введите тип обременения\n";
            }
            if (string.IsNullOrEmpty(NumberOfRegistrationBoxObremenenia.Text))
            {
                error += "Введите номер обременения\n";
            }
            if (string.IsNullOrEmpty(DataOfRegistrationDatePickerObremenenia.Text))
            {
                error += "Введите дату обременения\n";
            }
            if (string.IsNullOrEmpty(YstanovlFaceTextBoxObremenenia.Text))
            {
                error += "Введите установленное лицо обременения\n";
            }
            if (string.IsNullOrEmpty(PravoTextBoxObremenenia.Text))
            {
                error += "Введите право обременения\n";
            }


            if (error != "")
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var obremenenia = new Obremenenia
                {

                    TypeOfObremenenia = TypeOfObremeneniaComboBoxObremenenia.Text,
                    NumberOfRegistration = Convert.ToInt32(NumberOfRegistrationBoxObremenenia.Text),
                    DataOfRegistration = Convert.ToDateTime(DataOfRegistrationDatePickerObremenenia.Text),
                    YstanovlFace = YstanovlFaceTextBoxObremenenia.Text,
                    Discribe = DiscribeTextBoxObremenenia.Text,
                    IdRight = Convert.ToInt32(PravoTextBoxObremenenia.Text)
                };

                db.Obremenenia.Add(obremenenia);
                db.SaveChanges();
                ClearObremenenia();
                LoadAllData();
                MessageBox.Show("Обременение добавлено", "Успех", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            catch (Exception ex)
            { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        
        #endregion

        #region pravoobladateli
        private void EditButtonPravoobladateli_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (PravoobladateliDataGrid.SelectedItem is not Pravoobladateli selected)
            {
                MessageBox.Show("Выберите правообладтеля для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string error = "";
            if (string.IsNullOrEmpty(NameTextBoxPravoobladateli.Text))
            {
                error += "Введите ФИО правообладателя\n";
            }
            if (string.IsNullOrEmpty(INNTextBoxPravoobladateli.Text))
            {
                error += "Введите ИНН правообладателя\n";
            }
            if (string.IsNullOrEmpty(ORGNTextBoxPravoobladateli.Text))
            {
                error += "Введите ОГРН правообладателя\n";
            }
            if (string.IsNullOrEmpty(KPPTextBoxPravoobladateli.Text))
            {
                error += "Введите КПП правообладателя\n";
            }
            if (string.IsNullOrEmpty(PhoneTextBoxPravoobladateli.Text))
            {
                error += "Введите телефон правообладателя\n";
            }
            if (string.IsNullOrEmpty(EmailTextBoxPravoobladateli.Text))
            {
                error += "Введите электронную почту правообладателя\n";
            }
            if (string.IsNullOrEmpty(AddresTextBoxPravoobladateli.Text))
            {
                error += "Введите адрес регистрации правообладателя\n";
            }
            if (string.IsNullOrEmpty(IdTypeOfPravoobladateliTextBoxPravoobladateli.Text))
            {
                error += "Введите тип правообладателей\n";
            }


            if (error != "")
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {

                selected.Name = NameTextBoxPravoobladateli.Text;
                selected.INN = Convert.ToUInt32(INNTextBoxPravoobladateli.Text);
                selected.ORGN = Convert.ToUInt32(ORGNTextBoxPravoobladateli.Text);
                selected.KPP = Convert.ToUInt32(KPPTextBoxPravoobladateli.Text);
                selected.Phone = Convert.ToInt32(PhoneTextBoxPravoobladateli.Text);
                selected.Email = EmailTextBoxPravoobladateli.Text;
                selected.Addres = AddresTextBoxPravoobladateli.Text;
                selected.IdTypeOfPravoobladateli = Convert.ToInt32(IdTypeOfPravoobladateliTextBoxPravoobladateli.Text);

                db.SaveChanges();

                LoadAllData();
                ClearPravoobladateli();
                MessageBox.Show("Правообладатель изменен", "Успех", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            catch (Exception ex)
            { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
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
                    ClearPravoobladateli();

                    MessageBox.Show("Правообладатель удален", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                catch (Exception ex)
                { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }

            }
        }

        private void AddButtonPravoobladateli_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            string error = "";
            if (string.IsNullOrEmpty(NameTextBoxPravoobladateli.Text))
            {
                error += "Введите ФИО правообладателя\n";
            }
            if (string.IsNullOrEmpty(INNTextBoxPravoobladateli.Text))
            {
                error += "Введите ИНН правообладателя\n";
            }
            if (string.IsNullOrEmpty(ORGNTextBoxPravoobladateli.Text))
            {
                error += "Введите ОГРН правообладателя\n";
            }
            if (string.IsNullOrEmpty(KPPTextBoxPravoobladateli.Text))
            {
                error += "Введите КПП правообладателя\n";
            }
            if (string.IsNullOrEmpty(PhoneTextBoxPravoobladateli.Text))
            {
                error += "Введите телефон правообладателя\n";
            }
            if (string.IsNullOrEmpty(EmailTextBoxPravoobladateli.Text))
            {
                error += "Введите электронную почту правообладателя\n";
            }
            if (string.IsNullOrEmpty(AddresTextBoxPravoobladateli.Text))
            {
                error += "Введите адрес регистрации правообладателя\n";
            }
            if (string.IsNullOrEmpty(IdTypeOfPravoobladateliTextBoxPravoobladateli.Text))
            {
                error += "Введите тип правообладателей\n";
            }


            if (error != "")
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var pravoobladateli = new Pravoobladateli
                {

                    Name = NameTextBoxPravoobladateli.Text,
                    INN = Convert.ToUInt32(INNTextBoxPravoobladateli.Text),
                    ORGN = Convert.ToUInt32(ORGNTextBoxPravoobladateli.Text),
                    KPP = Convert.ToUInt32(KPPTextBoxPravoobladateli.Text),
                    Phone = Convert.ToInt32(PhoneTextBoxPravoobladateli.Text),
                    Email = EmailTextBoxPravoobladateli.Text,
                    Addres = AddresTextBoxPravoobladateli.Text,
                    IdTypeOfPravoobladateli = Convert.ToInt32(IdTypeOfPravoobladateliTextBoxPravoobladateli.Text)
                };

                db.Pravoobladatelis.Add(pravoobladateli);
                db.SaveChanges();

                LoadAllData();
                ClearPravoobladateli();
                MessageBox.Show("Правообладатель добавлен", "Успех", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }


        }
        #endregion

        #region typeofpravoobladateli
        private void EditButtonTypeOfPravoobladateli_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (TypeOfPravoobladateliDataGrid.SelectedItem is not TypeOfPravoobladateli selected)
            {
                MessageBox.Show("Выберите тип правообладтеля для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string error = "";
            if (string.IsNullOrEmpty(NameTextBoxTypeOfPravoobladateli.Text))
            {
                error += "Введите наименование типа правообладателя\n";
            }

            if (error != "")
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {

                selected.Name = NameTextBoxTypeOfPravoobladateli.Text;

                db.SaveChanges();

                LoadAllData();
                ClearTypeOfPravoobladateli();
                MessageBox.Show("Тип правообладателя добавлен", "Успех", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            catch (Exception ex)
            { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
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
                    ClearTypeOfPravoobladateli();

                    MessageBox.Show("Тип правообладателя удален", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                catch (Exception ex)
                { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }

            }
        }

        private void AddButtonTypeOfPravoobladateli_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string error = "";
            if (string.IsNullOrEmpty(NameTextBoxTypeOfPravoobladateli.Text))
            {
                error += "Введите наименование типа правообладателя\n";
            }

            if (error != "")
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var typeOfPravoobladateli = new TypeOfPravoobladateli
                {

                    Name = NameTextBoxTypeOfPravoobladateli.Text
                };

                db.TypeOfPravoobladatelis.Add(typeOfPravoobladateli);
                db.SaveChanges();

                LoadAllData();
                ClearTypeOfPravoobladateli();
                MessageBox.Show("Тип правообладателя добавлен", "Успех", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            catch (Exception ex)
            { MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        #endregion

        private void SformirovatObremenenieButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            var lastObremenenie = db.Obremenenia
    .OrderByDescending(o => o.Id)
    .FirstOrDefault();

            if (lastObremenenie == null)
            {
                MessageBox.Show("Нет существующих обременений", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Документ Word (*.docx)|*.docx";
            saveFileDialog.FileName = "МойДокумент.docx";



            var prava = db.Rights.FirstOrDefault(p => lastObremenenie.IdRight == p.Id);
            string statusText = prava.StatusOfRight ? "Активно" : "Неактивно";
            var pravoobl = db.Pravoobladatelis
                .FirstOrDefault(pr => prava.IdPravoobladateli == pr.Id);

            var ground = db.GroundPlaces
                .FirstOrDefault(g => prava.IdGroundPlace == g.Id);

            if (saveFileDialog.ShowDialog() == true)
            {
                var doc = new WordDocument();
                var section = doc.Sections[0];


                section.PageSetup.SetTopMargin(2, MeasurementUnit.Centimeter);
                section.PageSetup.SetBottomMargin(2, MeasurementUnit.Centimeter);
                section.PageSetup.SetLeftMargin(3, MeasurementUnit.Centimeter);
                section.PageSetup.SetRightMargin(1.5, MeasurementUnit.Centimeter);


                section.PageSetup.Orientation = PageOrientation.Portrait;

                var lines = new string[] { 
                    $"Правообладатель: {pravoobl.Name},{pravoobl.ORGN}, {pravoobl.KPP}",
                    $"\rИНН: {pravoobl.INN}",
                    $"\rОГРН: {pravoobl.ORGN}",
                    $"\rКПП: {pravoobl.KPP}",
                    $"\rЮр-Адрес: {pravoobl.Addres}",
                    $"Документ-основание: {prava.DocumentOsnovanie}",
                    $"Номер и дата регистрации: {lastObremenenie.NumberOfRegistration}, {lastObremenenie.DataOfRegistration}", 
                    $"Статус права: {statusText}", 
                    $"Выдано: Министерство сельского хозяйства Оренбургской области", 
                    $"Что выдано: {ground.KadastrNumber}" , 
                    $"Когда выдано: {prava.DateOfRegistration}",
                    "", 
                    "Подпись",
                    "________________________",
                    "",
                    "Дата",
                    "____________"};
                var title = new string[]
                {
                    "Обременение",
                    ""
                };
                foreach (string t in title)
                {

                    var paragraph = new Paragraph();
                    paragraph.Alignment = IronSoftware.Abstractions.Word.TextAlignment.Center;
                    var textRun = new IronWord.Models.Run(new TextContent(t));
                    textRun.Style = new TextStyle()
                    {
                        FontSize = 16,
                        Color = Color.Black,
                        TextFont = new Font() { FontFamily = "Times New Roman" },
                        IsBold = true,
                    };

                    paragraph.AddChild(textRun);
                    section.AddParagraph(paragraph);
                }
                foreach (string line in lines)
                {

                    var paragraph = new Paragraph();
                    var textRun = new IronWord.Models.Run(new TextContent(line));
                    textRun.Style = new TextStyle()
                    {
                        FontSize = 14,
                        Color = Color.Black,
                        TextFont = new Font() { FontFamily = "Times New Roman" }
                    };

                    paragraph.AddChild(textRun);
                    section.AddParagraph(paragraph);
                }

                doc.SaveAs(saveFileDialog.FileName);
                MessageBox.Show("Документ Word успешно сохранен");
            }
        }

        private void OformitPravoButtonType_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string error = "";
            if (FreeGroundPlaceComboBoxPoluchitObremenenie.Text == "") error += "Выберите свободный участок\n";
            if (TypeOfObremeneniaComboBoxPoluchitObremenenie.Text == "") error += "Выберите тип обремениения\n";
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
                int idground = (from q in db.GroundPlaces
                                where q.KadastrNumber == Convert.ToInt32(FreeGroundPlaceComboBoxPoluchitObremenenie.Text)
                                select q.Id).FirstOrDefault();


                int idpravoobl = (from p in db.Pravoobladatelis
                                  where p.Name == PravoobladatelComboBoxPoluchitObremenenie.Text
                                  select p.Id).FirstOrDefault();

                int idtype = (from t in db.TypeOfRight
                              where t.Name == TypeRightComboBoxPoluchitObremenenie.Text
                              select t.Id).FirstOrDefault();


                if (idground == 0 || idtype == 0)
                {
                    MessageBox.Show("Не удалось найти указанные данные в базе.");
                    return;
                }

                // Создаем запись
                Right right = new Right(
                    lastRegistrationNumber2++,
                    DateTime.Now,
                    "Разрешение на использование земель",
                    true,
                    idground,
                    idpravoobl,
                    idtype
                );
                db.Rights.Add(right);
                db.SaveChanges();
                int idright = Convert.ToInt32(right.Id);
                Obremenenia obremenenia = new Obremenenia(TypeOfObremeneniaComboBoxPoluchitObremenenie.Text, lastRegistrationNumber3, DateTime.Now, DiscribeTextBoxPoluchitObremenenie.Text, YstanovlFaceComboBoxPoluchitObremenenie.Text, idright);
                db.Obremenenia.Add(obremenenia);

                var gra = (from q in db.GroundPlaces
                           where q.KadastrNumber == Convert.ToInt32(FreeGroundPlaceComboBoxPoluchitObremenenie.Text)
                           select q).FirstOrDefault();
                gra.Status = StatusList[1]; 

                db.SaveChanges();
                LoadAllData();
            }
            catch(Exception ex) {  MessageBox.Show(ex.Message); return;}
        
        }
        #region Clear
        private void ClearGround()
        {
            KadastrNumberTextBoxGroundPlace.Text = null;
            AddresTextBoxGroundPlace.Text = null;
            SquareTextBoxGroundPlace.Text = null;
            TypeOfGroundTextBoxGroundPlace.SelectedItem = null;
            KadastrPriceTextBoxGroundPlace.Text = null;
            StatusTextBoxGroundPlace.SelectedItem = null;
        }

        private void ClearRight()
        {
            NumberOfRegistrationTextBoxRights.Text = null;
            DateOfRegistrationTextBoxRights.SelectedDate =  null;
            DocumentOsnovanieTextBoxRights.Text = null;
            StatusOfRightTextBoxRights.IsChecked = null;
            IdGroundPlaceTextBoxRights.Text = null;
            IdPravoobladateliTextBoxRights.Text = null;
            IdTypeOfRightTextBoxRights.Text = null;
        }



        private void ClearUser()
        {
            LoginTextBoxUser.Text = null;
            PasswordPasswordBoxUser.Text = null;
            StatusCheckBoxUser.IsChecked = null;
            DataCreateTextBoxUser.SelectedDate = null;
            IdRoleTextBoxUser.Text = null;
        }

        private void ClearTypeOfRight()
        {
            NameTextBoxTypeOfRight.Text = null;
            CodeNameTextBoxTypeOfRight.Text = null;
            DiscribeTextBoxTypeOfRight.Text = null;
        }

        private void ClearObremenenia()
        {
            TypeOfObremeneniaComboBoxObremenenia.SelectedItem = null;
            NumberOfRegistrationBoxObremenenia.Text = null;
            DataOfRegistrationDatePickerObremenenia.SelectedDate = null;
            DiscribeTextBoxObremenenia.Text = null;
            YstanovlFaceTextBoxObremenenia.Text = null;
            PravoTextBoxObremenenia.Text = null;
        }

        private void ClearPravoobladateli()
        {
            NameTextBoxPravoobladateli.Text = null;
            INNTextBoxPravoobladateli.Text = null;
            ORGNTextBoxPravoobladateli.Text = null;
            KPPTextBoxPravoobladateli.Text = null;
            PhoneTextBoxPravoobladateli.Text = null;
            EmailTextBoxPravoobladateli.Text = null;
            AddresTextBoxPravoobladateli.Text = null;
            IdTypeOfPravoobladateliTextBoxPravoobladateli.Text = null;
        }

        private void ClearTypeOfPravoobladateli()
        {
            NameTextBoxTypeOfPravoobladateli.Text = null;
        }
        #endregion

        #region selectionChange
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

 



        private void UserDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserDataGrid.SelectedItem is User selected)
            {
                LoginTextBoxUser.Text = selected.Name;
                PasswordPasswordBoxUser.Text = selected.Password;
                StatusCheckBoxUser.IsChecked = selected.Status;
                DataCreateTextBoxUser.SelectedDate = selected.DateCreate;
                IdRoleTextBoxUser.Text = selected.Role.ToString();
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
        #endregion

        private void ToDocObremBurron_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (ObremeneneiaDataGrid.SelectedItem is not Obremenenia selected)
            {
                MessageBox.Show("Выберите обременение для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Документ Word (*.docx)|*.docx";
            saveFileDialog.FileName = "МойДокумент.docx";



            var prava = db.Rights.FirstOrDefault(p => selected.IdRight == p.Id);
            string statusText = prava.StatusOfRight ? "Активно" : "Неактивно";
            var pravoobl = db.Pravoobladatelis
                .FirstOrDefault(pr => prava.IdPravoobladateli == pr.Id);

            var ground = db.GroundPlaces
                .FirstOrDefault(g => prava.IdGroundPlace == g.Id);

            if (saveFileDialog.ShowDialog() == true)
            {
                var doc = new WordDocument();
                var section = doc.Sections[0];


                section.PageSetup.SetTopMargin(2, MeasurementUnit.Centimeter);
                section.PageSetup.SetBottomMargin(2, MeasurementUnit.Centimeter);
                section.PageSetup.SetLeftMargin(3, MeasurementUnit.Centimeter);
                section.PageSetup.SetRightMargin(1.5, MeasurementUnit.Centimeter);


                section.PageSetup.Orientation = PageOrientation.Portrait;

                var lines = new string[] {
                    $"Правообладатель: {pravoobl.Name},{pravoobl.ORGN}, {pravoobl.KPP}",
                    $"\rИНН: {pravoobl.INN}",
                    $"\rОГРН: {pravoobl.ORGN}",
                    $"\rКПП: {pravoobl.KPP}",
                    $"\rЮр-Адрес: {pravoobl.Addres}",
                    $"Документ-основание: {prava.DocumentOsnovanie}",
                    $"Номер и дата регистрации: {selected.NumberOfRegistration}, {selected.DataOfRegistration}",
                    $"Статус права: {statusText}",
                    $"Выдано: Министерство сельского хозяйства Оренбургской области",
                    $"Что выдано: {ground.KadastrNumber}" ,
                    $"Когда выдано: {prava.DateOfRegistration}",
                    "",
                    "Подпись",
                    "________________________",
                    "",
                    "Дата",
                    "____________"};
                var title = new string[]
                {
                    "Обременение",
                    ""
                };
                foreach (string t in title)
                {

                    var paragraph = new Paragraph();
                    paragraph.Alignment = IronSoftware.Abstractions.Word.TextAlignment.Center;
                    var textRun = new IronWord.Models.Run(new TextContent(t));
                    textRun.Style = new TextStyle()
                    {
                        FontSize = 16,
                        Color = Color.Black,
                        TextFont = new Font() { FontFamily = "Times New Roman" },
                        IsBold = true,
                    };

                    paragraph.AddChild(textRun);
                    section.AddParagraph(paragraph);
                }
                foreach (string line in lines)
                {

                    var paragraph = new Paragraph();
                    var textRun = new IronWord.Models.Run(new TextContent(line));
                    textRun.Style = new TextStyle()
                    {
                        FontSize = 14,
                        Color = Color.Black,
                        TextFont = new Font() { FontFamily = "Times New Roman" }
                    };

                    paragraph.AddChild(textRun);
                    section.AddParagraph(paragraph);
                }

                doc.SaveAs(saveFileDialog.FileName);
                MessageBox.Show("Документ Word успешно сохранен");
            }
        }
    }
}