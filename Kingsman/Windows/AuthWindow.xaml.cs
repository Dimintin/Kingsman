using Kingsman.ClassHelper;
using Kingsman.Windows.Client;
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

namespace Kingsman.Windows.Employee
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void BtnSighIn_Click(object sender, RoutedEventArgs e)
        {
            var userAuth = ClassHelper.EF.Context.Employee.ToList().
                Where(i => i.Login == TbLogin.Text && i.Password == PbPassword.Text).FirstOrDefault();
            if (userAuth != null) {
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();
            }
            else {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            
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

        private void TbRegister_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            registrationWindow.ShowDialog();
            this.Close();
        }
    }
}
