using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using kurs.Data;

namespace kurs
{
    public partial class RequestWindow : Window
    {
        public RequestWindow()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Service service = new Service();
            service.Show();
            Close();
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем данные из полей
            string name = NameBox.Text.Trim();
            string phone = PhoneBox.Text.Trim();
            string email = EmailBox.Text.Trim();
            string comment = CommentBox.Text.Trim();

            // Проверка обязательных полей
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Введите имя!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(phone) && string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Введите телефон или email!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (var db = new AppDbContext())
                {
                    // Разделяем имя на части
                    var nameParts = name.Split(' ');
                    string lastName = nameParts.Length > 0 ? nameParts[0] : name;
                    string firstName = nameParts.Length > 1 ? nameParts[1] : "";
                    string patronymic = nameParts.Length > 2 ? nameParts[2] : "";

                    // Проверяем, существует ли уже такой клиент
                    var existingClient = await db.Clients
                        .FirstOrDefaultAsync(c =>
                            (!string.IsNullOrWhiteSpace(phone) && c.Phone == phone) ||
                            (!string.IsNullOrWhiteSpace(email) && c.Email == email));

                    if (existingClient != null)
                    {
                        // Обновляем комментарий существующего клиента
                        existingClient.Comment = string.IsNullOrWhiteSpace(comment)
                            ? existingClient.Comment
                            : (string.IsNullOrWhiteSpace(existingClient.Comment)
                                ? comment
                                : existingClient.Comment + "\n" + comment);

                        await db.SaveChangesAsync();

                        MessageBox.Show(
                            "Заявка успешно отправлена!\n\nВы уже зарегистрированы в системе.\nВаш комментарий добавлен.",
                            "Успех",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                    else
                    {
                        // Создаем нового клиента с заявкой
                        var newClient = new kurs.Models.Client
                        {
                            LastName = lastName,
                            FirstName = firstName,
                            Patronymic = patronymic,
                            Phone = string.IsNullOrWhiteSpace(phone) ? "" : phone,
                            Email = string.IsNullOrWhiteSpace(email) ? "" : email,
                            Password = "", // Пароль не задан
                            Comment = string.IsNullOrWhiteSpace(comment) ? "" : comment
                        };

                        db.Clients.Add(newClient);
                        await db.SaveChangesAsync();

                        MessageBox.Show(
                            "Заявка успешно отправлена!\n\nВы зарегистрированы в системе.\nДля входа используйте свой телефон или email.",
                            "Успех",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }

                    // Очищаем поля
                    NameBox.Text = "";
                    PhoneBox.Text = "";
                    EmailBox.Text = "";
                    CommentBox.Text = "";

                    // Возвращаемся на страницу услуг
                    Service service = new Service();
                    service.Show();
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка отправки заявки: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}