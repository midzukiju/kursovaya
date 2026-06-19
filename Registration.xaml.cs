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

namespace kurs
{
    public partial class Registration : Window
    {
        private bool isPhoneMode = true;

        public Registration()
        {
            InitializeComponent();
        }

        private void EmailButton_Click(object sender, RoutedEventArgs e)
        {
            isPhoneMode = false;

            EmailButton.Background = Brushes.White;
            PhoneButton.Background = new SolidColorBrush(Color.FromRgb(217, 217, 217));

            ContactPlaceholder.Text = "Введите почту";
        }

        private void PhoneButton_Click(object sender, RoutedEventArgs e)
        {
            isPhoneMode = true;

            PhoneButton.Background = Brushes.White;
            EmailButton.Background = new SolidColorBrush(Color.FromRgb(217, 217, 217));

            ContactPlaceholder.Text = "Введите номер телефона";
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            NamePlaceholder.Visibility =
                string.IsNullOrEmpty(NameBox.Text)
                ? Visibility.Visible
                : Visibility.Hidden;
        }

        private void ContactBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ContactPlaceholder.Visibility =
                string.IsNullOrEmpty(ContactBox.Text)
                ? Visibility.Visible
                : Visibility.Hidden;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordPlaceholder.Visibility =
                string.IsNullOrEmpty(PasswordBox.Password)
                ? Visibility.Visible
                : Visibility.Hidden;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            profile.Show();
            this.Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Entrance entrance = new Entrance();
            entrance.Show();
            Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
