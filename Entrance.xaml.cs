using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.EntityFrameworkCore;
using kurs.Data;

namespace kurs
{
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

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text.Trim();
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Проверка администратора
            if (login == "admin" && password == "admin")
            {
                MessageBox.Show("Добро пожаловать, администратор!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                kurs.Helpers.CurrentUser.Login = login;
                kurs.Helpers.CurrentUser.Role = "admin";

                Administrator adminWindow = new Administrator();
                adminWindow.Show();
                this.Close();
                return;
            }

            // Проверка обычного пользователя по БД
            try
            {
                using (var db = new AppDbContext())
                {
                    // Ищем клиента по телефону или почте И паролю
                    var client = await db.Clients
                        .FirstOrDefaultAsync(c =>
                            ((isPhoneMode && c.Phone == login) ||
                             (!isPhoneMode && c.Email == login)) &&
                            c.Password == password);

                    if (client != null)
                    {
                        // Сохраняем данные пользователя
                        kurs.Helpers.CurrentUser.Login = login;
                        kurs.Helpers.CurrentUser.Role = "user";

                        MessageBox.Show($"Добро пожаловать, {client.FirstName}!",
                            "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                        Profile profile = new Profile();
                        profile.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль!\n\nДля входа как администратор используйте:\nЛогин: admin\nПароль: admin",
                            "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);

                        PasswordBox.Clear();
                        PasswordBox.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к БД: {ex.Message}", "Ошибка");
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