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
    /// <summary>
    /// Логика взаимодействия для Entrance.xaml
    /// </summary>
    public partial class Entrance : Window
    {
        private bool isPhoneMode = true;

        public Entrance()
        {
            InitializeComponent();
        }

        private void EmailButton_Click(object sender, RoutedEventArgs e)
        {
            isPhoneMode = false;

            EmailButton.Background = Brushes.White;
            PhoneButton.Background = new SolidColorBrush(Color.FromRgb(217, 217, 217));

            LoginPlaceholder.Text = "Введите почту";
        }

        private void PhoneButton_Click(object sender, RoutedEventArgs e)
        {
            isPhoneMode = true;

            PhoneButton.Background = Brushes.White;
            EmailButton.Background = new SolidColorBrush(Color.FromRgb(217, 217, 217));

            LoginPlaceholder.Text = "Введите номер телефона";
        }

        private void LoginBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoginPlaceholder.Visibility =
                string.IsNullOrEmpty(LoginBox.Text)
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

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text.Trim();
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Проверка администратора
            if (login == "admin" && password == "admin")
            {
                MessageBox.Show("Добро пожаловать, администратор!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Открываем главное окно (MainWindow)
                Administrator mainWindow = new Administrator();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                // Для обычных пользователей (если нужна проверка по БД)
                // Пока просто показываем ошибку
                MessageBox.Show("Неверный логин или пароль!\n\nДля входа как администратор используйте:\nЛогин: admin\nПароль: admin",
                    "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);

                // Очищаем поле пароля
                PasswordBox.Clear();
                PasswordBox.Focus();
            }
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
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