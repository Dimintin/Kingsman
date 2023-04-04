using Microsoft.Win32;
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
    /// Логика взаимодействия для AddServiceWindow.xaml
    /// </summary>
    public partial class AddServiceWindow : Window
    {
        private string pathImage = null;
        public AddServiceWindow()
        {
            InitializeComponent();

            CmbServiceType.ItemsSource = ClassHelper.EF.Context.ServiceType.ToList();
            CmbServiceType.DisplayMemberPath = "TypeName";
            CmbServiceType.SelectedItem = 0;
        }

        private void ImgAddServiceImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                ImgAddServiceImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));

                pathImage = openFileDialog.FileName;
            }
            ImgAddServiceImage.Width = BrdrAddNewService.Width;
            ImgAddServiceImage.Height = BrdrAddNewService.Height;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DB.Service addService = new DB.Service();

            addService.ServiceName = TbServiceName.Text;
            addService.Description = TbServiceDescription.Text;
            addService.Price = Convert.ToInt32(TbServicePrice.Text);
            addService.ServiceTypeId = (CmbServiceType.SelectedItem as DB.ServiceType).Id;
            if (pathImage != null)
            {
            }

            ClassHelper.EF.Context.Service.Add(addService);
            ClassHelper.EF.Context.SaveChanges();

            MessageBox.Show("Услуга успешно добавлена!", "Добавление услуги", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
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
    }
}
