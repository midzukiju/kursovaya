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
using Microsoft.EntityFrameworkCore;
using kurs.Data;
using kurs.Helpers;
using kurs.ViewModels;

// Псевдонимы для разрешения конфликтов имён с WPF
using MaterialEntity = kurs.Models.Material;
using ClientEntity = kurs.Models.Client;
using EmployeeEntity = kurs.Models.Employee;
using OrderEntity = kurs.Models.Order;

namespace kurs
{
    public partial class Administrator : Window
    {
        public Administrator()
        {
            InitializeComponent();
            Loaded += Administrator_Loaded;
        }

        private async void TestConnection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var count = await db.Clients.CountAsync();
                    MessageBox.Show($"Подключение успешно! В базе {count} клиентов.", "Тест");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения:\n{ex.Message}", "Ошибка");
            }
        }

        private async void Administrator_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadClientsAsync();
        }

        #region Клиенты

        private async Task LoadClientsAsync()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var clients = await db.Clients
                        .Select(c => new ClientViewModel
                        {
                            Код = c.ClientId,
                            Фамилия = c.LastName,
                            Имя = c.FirstName,
                            Отчество = c.Patronymic,
                            Телефон = c.Phone,
                            Почта = c.Email
                        })
                        .ToListAsync();

                    DgClients.ItemsSource = clients;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки клиентов:\n{ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void BtnRefreshClients_Click(object sender, RoutedEventArgs e)
        {
            await LoadClientsAsync();
        }

        private async void BtnAddClient_Click(object sender, RoutedEventArgs e)
        {
            var lastName = Microsoft.VisualBasic.Interaction.InputBox("Введите фамилию:", "Добавление клиента", "");
            if (string.IsNullOrWhiteSpace(lastName)) return;

            try
            {
                using (var db = new AppDbContext())
                {
                    var newClient = new ClientEntity
                    {
                        LastName = lastName,
                        FirstName = "Имя",
                        Patronymic = "Отчество",
                        Phone = "000",
                        Email = "email@example.com"
                    };
                    db.Clients.Add(newClient);
                    await db.SaveChangesAsync();
                    MessageBox.Show("Клиент добавлен!");
                    await LoadClientsAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления:\n{ex.Message}", "Ошибка");
            }
        }

        private async void BtnEditClient_Click(object sender, RoutedEventArgs e)
        {
            if (DgClients.SelectedItem == null)
            {
                MessageBox.Show("Выберите клиента для редактирования");
                return;
            }

            var selectedClient = (ClientViewModel)DgClients.SelectedItem;
            int clientId = selectedClient.Код;

            var newName = Microsoft.VisualBasic.Interaction.InputBox("Введите новое имя:", "Редактирование", "");
            if (string.IsNullOrWhiteSpace(newName)) return;

            try
            {
                using (var db = new AppDbContext())
                {
                    var client = await db.Clients.FindAsync(clientId);
                    if (client != null)
                    {
                        client.FirstName = newName;
                        await db.SaveChangesAsync();
                        MessageBox.Show("Данные обновлены!");
                        await LoadClientsAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка редактирования:\n{ex.Message}", "Ошибка");
            }
        }

        private async void BtnDeleteClient_Click(object sender, RoutedEventArgs e)
        {
            if (DgClients.SelectedItem == null)
            {
                MessageBox.Show("Выберите клиента для удаления");
                return;
            }

            var result = MessageBox.Show("Удалить выбранного клиента?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            var selectedClient = (ClientViewModel)DgClients.SelectedItem;
            int clientId = selectedClient.Код;

            try
            {
                using (var db = new AppDbContext())
                {
                    var client = await db.Clients.FindAsync(clientId);
                    if (client != null)
                    {
                        db.Clients.Remove(client);
                        await db.SaveChangesAsync();
                        MessageBox.Show("Клиент удален!");
                        await LoadClientsAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления:\n{ex.Message}", "Ошибка");
            }
        }

        #endregion

        #region Сотрудники

        private async Task LoadEmployeesAsync()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var employees = await db.Employees
                        .Select(emp => new EmployeeViewModel
                        {
                            Код = emp.EmployeeId,
                            Фамилия = emp.LastName,
                            Имя = emp.FirstName,
                            Отчество = emp.Patronymic,
                            Должность = emp.Position,
                            Телефон = emp.Phone,
                            Email = emp.Email,
                            Дата_приема = emp.HireDate
                        })
                        .ToListAsync();

                    DgEmployees.ItemsSource = employees;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки сотрудников:\n{ex.Message}", "Ошибка");
            }
        }

        private async void BtnRefreshEmployees_Click(object sender, RoutedEventArgs e)
        {
            await LoadEmployeesAsync();
        }

        private void BtnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Здесь будет форма добавления сотрудника");
        }

        private void BtnEditEmployee_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Здесь будет форма редактирования сотрудника");
        }

        private void BtnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Здесь будет удаление сотрудника");
        }

        #endregion

        #region Заказы

        private async Task LoadOrdersAsync()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var orders = await db.Orders
                        .Select(o => new OrderViewModel
                        {
                            Код = o.OrderId,
                            Код_клиента = o.ClientId,
                            Код_каталога = o.CatalogId,
                            Дата_начала = o.StartDate,
                            Дата_выдачи = o.IssueDate,
                            Код_срочности = o.UrgencyId,
                            Стоимость = o.Cost,
                            Аванс = o.Advance
                        })
                        .ToListAsync();

                    DgOrders.ItemsSource = orders;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки заказов:\n{ex.Message}", "Ошибка");
            }
        }

        private async void BtnRefreshOrders_Click(object sender, RoutedEventArgs e)
        {
            await LoadOrdersAsync();
        }

        #endregion

        #region Материалы

        private async Task LoadMaterialsAsync()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var materials = await db.Materials
                        .Select(m => new MaterialViewModel
                        {
                            Код = m.MaterialId,
                            Название = m.MaterialName
                        })
                        .ToListAsync();

                    DgMaterials.ItemsSource = materials;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки материалов:\n{ex.Message}", "Ошибка");
            }
        }

        private async void BtnRefreshMaterials_Click(object sender, RoutedEventArgs e)
        {
            await LoadMaterialsAsync();
        }

        private async void BtnAddMaterial_Click(object sender, RoutedEventArgs e)
        {
            var materialName = Microsoft.VisualBasic.Interaction.InputBox("Введите название материала:", "Добавление", "");
            if (string.IsNullOrWhiteSpace(materialName)) return;

            try
            {
                using (var db = new AppDbContext())
                {
                    var newMaterial = new MaterialEntity { MaterialName = materialName };
                    db.Materials.Add(newMaterial);
                    await db.SaveChangesAsync();
                    MessageBox.Show("Материал добавлен!");
                    await LoadMaterialsAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка:\n{ex.Message}", "Ошибка");
            }
        }

        private async void BtnDeleteMaterial_Click(object sender, RoutedEventArgs e)
        {
            if (DgMaterials.SelectedItem == null)
            {
                MessageBox.Show("Выберите материал для удаления");
                return;
            }

            var result = MessageBox.Show("Удалить выбранный материал?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            var selectedMaterial = (MaterialViewModel)DgMaterials.SelectedItem;
            int materialId = selectedMaterial.Код;

            try
            {
                using (var db = new AppDbContext())
                {
                    var material = await db.Materials.FindAsync(materialId);
                    if (material != null)
                    {
                        db.Materials.Remove(material);
                        await db.SaveChangesAsync();
                        MessageBox.Show("Материал удален!");
                        await LoadMaterialsAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка:\n{ex.Message}", "Ошибка");
            }
        }

        #endregion
    }
}