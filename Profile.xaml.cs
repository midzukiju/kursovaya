using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using kurs.Data;
using kurs.Helpers;

namespace kurs
{
    public partial class Profile : Window
    {
        private kurs.Models.Client _currentUser;

        public Profile()
        {
            InitializeComponent();
            Loaded += Profile_Loaded;
        }

        private async void Profile_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadUserProfile();
        }

        private async Task LoadUserProfile()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    // Если это админ - показываем заглушку
                    if (CurrentUser.Role == "admin")
                    {
                        UserNameText.Text = "Администратор";
                        EmailText.Text = "admin@musubi.ru";
                        PhoneText.Text = "-";
                        AvatarText.Text = "A";
                        EditButton.Visibility = Visibility.Collapsed;
                        return;
                    }

                    // Загружаем данные текущего пользователя из БД
                    string login = CurrentUser.Login;

                    _currentUser = await db.Clients
                        .FirstOrDefaultAsync(c => c.Phone == login || c.Email == login);

                    if (_currentUser != null)
                    {
                        // Формируем полное имя
                        string fullName = $"{_currentUser.LastName} {_currentUser.FirstName} {_currentUser.Patronymic}".Trim();

                        // Заполняем поля просмотра
                        UserNameText.Text = string.IsNullOrWhiteSpace(fullName) ? "Без имени" : fullName;
                        EmailText.Text = string.IsNullOrWhiteSpace(_currentUser.Email) ? "Не указан" : _currentUser.Email;
                        PhoneText.Text = string.IsNullOrWhiteSpace(_currentUser.Phone) ? "Не указан" : _currentUser.Phone;

                        // Заполняем поля редактирования
                        UserNameBox.Text = fullName;
                        EmailBox.Text = _currentUser.Email;
                        PhoneBox.Text = _currentUser.Phone;

                        // Первая буква имени для аватара
                        if (!string.IsNullOrEmpty(_currentUser.FirstName))
                        {
                            AvatarText.Text = _currentUser.FirstName.Substring(0, 1).ToUpper();
                        }
                        else if (!string.IsNullOrEmpty(_currentUser.LastName))
                        {
                            AvatarText.Text = _currentUser.LastName.Substring(0, 1).ToUpper();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пользователь не найден в базе данных", "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки профиля: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditProfile_Click(object sender, RoutedEventArgs e)
        {
            // Переключаем в режим редактирования
            ViewPanel.Visibility = Visibility.Collapsed;
            EditPanel.Visibility = Visibility.Visible;
            EditButton.Visibility = Visibility.Collapsed;
            ActionButtons.Visibility = Visibility.Visible;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем данные из TextBox'ов
                string fullName = UserNameBox.Text.Trim();
                string email = EmailBox.Text.Trim();
                string phone = PhoneBox.Text.Trim();

                // Проверка обязательных полей
                if (string.IsNullOrWhiteSpace(fullName))
                {
                    MessageBox.Show("Введите имя", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(phone))
                {
                    MessageBox.Show("Введите email или телефон", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Разделяем полное имя на части
                var nameParts = fullName.Split(' ');
                string lastName = nameParts.Length > 0 ? nameParts[0] : "";
                string firstName = nameParts.Length > 1 ? nameParts[1] : fullName;
                string patronymic = nameParts.Length > 2 ? nameParts[2] : "";

                using (var db = new AppDbContext())
                {
                    // Загружаем актуальную запись из БД
                    var client = await db.Clients.FindAsync(_currentUser.ClientId);

                    if (client != null)
                    {
                        // Обновляем данные
                        client.LastName = lastName;
                        client.FirstName = firstName;
                        client.Patronymic = patronymic;
                        client.Email = email;
                        client.Phone = phone;

                        await db.SaveChangesAsync();

                        // Обновляем локальные данные
                        _currentUser = client;

                        MessageBox.Show("Данные успешно сохранены!", "Успех",
                            MessageBoxButton.OK, MessageBoxImage.Information);

                        // Переключаем обратно в режим просмотра
                        SwitchToViewMode();

                        // Обновляем отображение
                        await LoadUserProfile();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Отменяем редактирование - просто переключаем режим
            SwitchToViewMode();
        }

        private void SwitchToViewMode()
        {
            ViewPanel.Visibility = Visibility.Visible;
            EditPanel.Visibility = Visibility.Collapsed;
            EditButton.Visibility = Visibility.Visible;
            ActionButtons.Visibility = Visibility.Collapsed;
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        private void CatalogButton_Click(object sender, RoutedEventArgs e)
        {
            Catalog catalog = new Catalog();
            catalog.Show();
            Close();
        }

        private void ServicesButton_Click(object sender, RoutedEventArgs e)
        {
            Service service = new Service();
            service.Show();
            Close();
        }

        private void ReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            Reviews reviews = new Reviews();
            reviews.Show();
            Close();
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            AboutUs about = new AboutUs();
            about.Show();
            Close();
        }

        private void Order1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Заказ оформлен");
        }

        private void Order2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Заказ оформлен");
        }

        private void Order3_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Заказ оформлен");
        }

        private void Delete1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Удалено из избранного");
        }

        private void Delete2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Удалено из избранного");
        }

        private void Delete3_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Удалено из избранного");
        }
    }
}