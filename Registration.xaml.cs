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

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string fullName = NameBox.Text.Trim();
            string contact = ContactBox.Text.Trim();
            string password = PasswordBox.Password;

            // Проверка заполнения
            if (string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(contact) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Проверка длины пароля
            if (password.Length < 4)
            {
                MessageBox.Show("Пароль должен быть не менее 4 символов!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (var db = new AppDbContext())
                {
                    // Разделяем полное имя на части
                    var nameParts = fullName.Split(' ');
                    string lastName = nameParts.Length > 0 ? nameParts[0] : "";
                    string firstName = nameParts.Length > 1 ? nameParts[1] : fullName;
                    string patronymic = nameParts.Length > 2 ? nameParts[2] : "";

                    // Проверяем, существует ли уже такой клиент
                    var existingClient = await db.Clients
                        .FirstOrDefaultAsync(c =>
                            (isPhoneMode && c.Phone == contact) ||
                            (!isPhoneMode && c.Email == contact));

                    if (existingClient != null)
                    {
                        MessageBox.Show("Клиент с таким контактом уже существует!\nВойдите в систему.",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Создаем нового клиента
                    var newClient = new kurs.Models.Client
                    {
                        LastName = lastName,
                        FirstName = firstName,
                        Patronymic = patronymic,
                        Phone = isPhoneMode ? contact : "",
                        Email = !isPhoneMode ? contact : "",
                        Password = password  // СОХРАНЯЕМ ПАРОЛЬ
                    };

                    db.Clients.Add(newClient);
                    await db.SaveChangesAsync();

                    // Сохраняем данные текущего пользователя
                    kurs.Helpers.CurrentUser.Login = contact;
                    kurs.Helpers.CurrentUser.Role = "user";

                    MessageBox.Show("Регистрация успешна! Добро пожаловать!",
                        "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Переходим в профиль
                    Profile profile = new Profile();
                    profile.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка регистрации: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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