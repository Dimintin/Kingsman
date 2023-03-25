using Kingsman.Windows.Employee;
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

namespace Kingsman.Windows.Client
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();

            CmbGender.ItemsSource = ClassHelper.EF.Context.Gender.ToList();
            CmbGender.DisplayMemberPath = "GenderName";
            CmbGender.SelectedIndex = 0;
        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text.Length == 0)
            {
                (sender as TextBox).Foreground = Brushes.DarkGray;
                (sender as TextBox).Text = (string)(sender as TextBox).Tag;
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text.Length != 0 && (sender as TextBox).Text == (string)(sender as TextBox).Tag)
            {
                (sender as TextBox).Foreground = Brushes.Black;
                (sender as TextBox).Text = "";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TbFirstName.Text) || string.IsNullOrEmpty(TbLastName.Text) || string.IsNullOrEmpty(TbPhone.Text) ||
                string.IsNullOrEmpty(TbEmail.Text) || string.IsNullOrEmpty(TbNewPassword.Text) || string.IsNullOrEmpty(TbNewLogin.Text))
            {
                MessageBox.Show("Заполните все необходимые поля!");
                return;
            }

            DB.Client newClient = new DB.Client();
            newClient.FirstName = TbFirstName.Text;
            newClient.LastName = TbLastName.Text;
            if (TbPatronymic.Text != string.Empty)
            {
                newClient.Patronymic = TbPatronymic.Text;
            }
            newClient.Phone = TbPhone.Text;
            newClient.Email = TbEmail.Text;
            newClient.Login = TbNewLogin.Text;
            newClient.Password = TbNewPassword.Text;

            ClassHelper.EF.Context.Client.Add(newClient);

            ClassHelper.EF.Context.SaveChanges();

            MessageBox.Show("Пользователь упспешно добавлен!", "Новый пользователь", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void TbBack_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            this.Close();
            authWindow.Show();
        }
    }
}
