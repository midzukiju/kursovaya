using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using kurs.Data;

namespace kurs
{
    public partial class Administrator : Window
    {
        public Administrator()
        {
            InitializeComponent();
            Loaded += Administrator_Loaded;
        }

        private async void Administrator_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadClientsAsync();
        }

        #region КЛИЕНТЫ

        private async Task LoadClientsAsync()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var clients = await db.Clients
                        .Select(c => new
                        {
                            ID = c.ClientId,
                            Фамилия = c.LastName,
                            Имя = c.FirstName,
                            Отчество = c.Patronymic,
                            Телефон = c.Phone,
                            Email = c.Email
                        })
                        .ToListAsync();

                    DgClients.ItemsSource = clients;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки клиентов: {ex.Message}", "Ошибка");
            }
        }

        private async void BtnRefreshClients_Click(object sender, RoutedEventArgs e)
        {
            await LoadClientsAsync();
        }

        private async void BtnAddClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string lastName = Microsoft.VisualBasic.Interaction.InputBox("Введите фамилию:", "Добавление клиента", "");
                string firstName = Microsoft.VisualBasic.Interaction.InputBox("Введите имя:", "Добавление клиента", "");
                string patronymic = Microsoft.VisualBasic.Interaction.InputBox("Введите отчество:", "Добавление клиента", "");
                string phone = Microsoft.VisualBasic.Interaction.InputBox("Введите телефон:", "Добавление клиента", "");
                string email = Microsoft.VisualBasic.Interaction.InputBox("Введите email:", "Добавление клиента", "");

                if (string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(firstName))
                {
                    MessageBox.Show("Фамилия и имя обязательны!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                using (var db = new AppDbContext())
                {
                    db.Clients.Add(new kurs.Models.Client
                    {
                        LastName = lastName.Trim(),
                        FirstName = firstName.Trim(),
                        Patronymic = string.IsNullOrWhiteSpace(patronymic) ? "" : patronymic.Trim(),
                        Phone = string.IsNullOrWhiteSpace(phone) ? "" : phone.Trim(),
                        Email = string.IsNullOrWhiteSpace(email) ? "" : email.Trim()
                    });
                    await db.SaveChangesAsync();
                    MessageBox.Show("Клиент добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    await LoadClientsAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        private async void BtnDeleteClient_Click(object sender, RoutedEventArgs e)
        {
            if (DgClients.SelectedItem == null)
            {
                MessageBox.Show("Выберите клиента для удаления");
                return;
            }

            try
            {
                var selectedItem = DgClients.SelectedItem;
                var prop = selectedItem.GetType().GetProperty("ID");
                if (prop == null) return;
                int id = Convert.ToInt32(prop.GetValue(selectedItem));

                var result = MessageBox.Show($"Удалить клиента ID={id}?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result != MessageBoxResult.Yes) return;

                using (var db = new AppDbContext())
                {
                    var client = await db.Clients.FindAsync(id);
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
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        #endregion

        #region СОТРУДНИКИ

        private async Task LoadEmployeesAsync()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var employees = await db.Employees
                        .Select(e => new
                        {
                            ID = e.EmployeeId,
                            Фамилия = e.LastName,
                            Имя = e.FirstName,
                            Отчество = e.Patronymic,
                            Должность = e.Position,
                            Телефон = e.Phone,
                            Email = e.Email,
                            ДатаПриема = e.HireDate
                        })
                        .ToListAsync();

                    DgEmployees.ItemsSource = employees;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки сотрудников: {ex.Message}", "Ошибка");
            }
        }

        private async void BtnRefreshEmployees_Click(object sender, RoutedEventArgs e)
        {
            await LoadEmployeesAsync();
        }

        private async void BtnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string lastName = Microsoft.VisualBasic.Interaction.InputBox("Введите фамилию:", "Добавление сотрудника", "");
                string firstName = Microsoft.VisualBasic.Interaction.InputBox("Введите имя:", "Добавление сотрудника", "");
                string patronymic = Microsoft.VisualBasic.Interaction.InputBox("Введите отчество:", "Добавление сотрудника", "");
                string position = Microsoft.VisualBasic.Interaction.InputBox("Введите должность:", "Добавление сотрудника", "");
                string phone = Microsoft.VisualBasic.Interaction.InputBox("Введите телефон:", "Добавление сотрудника", "");
                string email = Microsoft.VisualBasic.Interaction.InputBox("Введите email:", "Добавление сотрудника", "");

                if (string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(position))
                {
                    MessageBox.Show("Фамилия и должность обязательны!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                using (var db = new AppDbContext())
                {
                    db.Employees.Add(new kurs.Models.Employee
                    {
                        LastName = lastName.Trim(),
                        FirstName = string.IsNullOrWhiteSpace(firstName) ? "" : firstName.Trim(),
                        Patronymic = string.IsNullOrWhiteSpace(patronymic) ? "" : patronymic.Trim(),
                        Position = position.Trim(),
                        Phone = string.IsNullOrWhiteSpace(phone) ? "" : phone.Trim(),
                        Email = string.IsNullOrWhiteSpace(email) ? "" : email.Trim(),
                        HireDate = DateTime.Now
                    });
                    await db.SaveChangesAsync();
                    MessageBox.Show("Сотрудник добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    await LoadEmployeesAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        private async void BtnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (DgEmployees.SelectedItem == null)
            {
                MessageBox.Show("Выберите сотрудника для удаления");
                return;
            }

            try
            {
                var selectedItem = DgEmployees.SelectedItem;
                var prop = selectedItem.GetType().GetProperty("ID");
                if (prop == null) return;
                int id = Convert.ToInt32(prop.GetValue(selectedItem));

                var result = MessageBox.Show($"Удалить сотрудника ID={id}?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result != MessageBoxResult.Yes) return;

                using (var db = new AppDbContext())
                {
                    var employee = await db.Employees.FindAsync(id);
                    if (employee != null)
                    {
                        db.Employees.Remove(employee);
                        await db.SaveChangesAsync();
                        MessageBox.Show("Сотрудник удален!");
                        await LoadEmployeesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        #endregion

        #region ЗАКАЗЫ

        private async Task LoadOrdersAsync()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var orders = await db.Orders
                        .Select(o => new
                        {
                            ID = o.OrderId,
                            КодКлиента = o.ClientId,
                            КодКаталога = o.CatalogId,
                            ДатаНачала = o.StartDate,
                            ДатаВыдачи = o.IssueDate,
                            КодСрочности = o.UrgencyId,
                            Стоимость = o.Cost,
                            Аванс = o.Advance
                        })
                        .ToListAsync();

                    DgOrders.ItemsSource = orders;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки заказов: {ex.Message}", "Ошибка");
            }
        }

        private async void BtnRefreshOrders_Click(object sender, RoutedEventArgs e)
        {
            await LoadOrdersAsync();
        }

        private async void BtnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string clientIdStr = Microsoft.VisualBasic.Interaction.InputBox("Введите ID клиента:", "Добавление заказа", "");
                if (!int.TryParse(clientIdStr, out int clientId))
                {
                    MessageBox.Show("Неверный ID клиента!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string catalogIdStr = Microsoft.VisualBasic.Interaction.InputBox("Введите ID каталога:", "Добавление заказа", "");
                if (!int.TryParse(catalogIdStr, out int catalogId))
                {
                    MessageBox.Show("Неверный ID каталога!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string costStr = Microsoft.VisualBasic.Interaction.InputBox("Введите стоимость:", "Добавление заказа", "0");
                if (!decimal.TryParse(costStr, out decimal cost))
                {
                    MessageBox.Show("Неверная стоимость!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string advanceStr = Microsoft.VisualBasic.Interaction.InputBox("Введите аванс:", "Добавление заказа", "0");
                if (!decimal.TryParse(advanceStr, out decimal advance))
                {
                    MessageBox.Show("Неверный аванс!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                using (var db = new AppDbContext())
                {
                    db.Orders.Add(new kurs.Models.Order
                    {
                        ClientId = clientId,
                        CatalogId = catalogId,
                        StartDate = DateTime.Now,
                        IssueDate = DateTime.Now.AddDays(7),
                        UrgencyId = 1,
                        Cost = cost,
                        Advance = advance
                    });
                    await db.SaveChangesAsync();
                    MessageBox.Show("Заказ добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    await LoadOrdersAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        private async void BtnDeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (DgOrders.SelectedItem == null)
            {
                MessageBox.Show("Выберите заказ для удаления");
                return;
            }

            try
            {
                var selectedItem = DgOrders.SelectedItem;
                var prop = selectedItem.GetType().GetProperty("ID");
                if (prop == null) return;
                int id = Convert.ToInt32(prop.GetValue(selectedItem));

                var result = MessageBox.Show($"Удалить заказ ID={id}?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result != MessageBoxResult.Yes) return;

                using (var db = new AppDbContext())
                {
                    var order = await db.Orders.FindAsync(id);
                    if (order != null)
                    {
                        db.Orders.Remove(order);
                        await db.SaveChangesAsync();
                        MessageBox.Show("Заказ удален!");
                        await LoadOrdersAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        #endregion

        #region ИНДИВИДУАЛЬНЫЕ ЗАКАЗЫ

        private async Task LoadIndOrdersAsync()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var orders = await db.IndividualOrders
                        .Select(o => new
                        {
                            ID = o.IndOrderId,
                            КодКлиента = o.ClientId,
                            КодМодели = o.ModelId,
                            КодМатериала = o.MaterialId,
                            КодЦвета = o.ColorId,
                            Размер = o.Size,
                            КодТепла = o.ThermalId,
                            ДатаНачала = o.StartDate,
                            ДатаПримерки = o.FitDate,
                            ДатаВыдачи = o.IssueDate,
                            КодСрочности = o.UrgencyId,
                            Стоимость = o.Cost,
                            Аванс = o.Advance
                        })
                        .ToListAsync();

                    DgIndOrders.ItemsSource = orders;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки индивидуальных заказов: {ex.Message}", "Ошибка");
            }
        }

        private async void BtnRefreshIndOrders_Click(object sender, RoutedEventArgs e)
        {
            await LoadIndOrdersAsync();
        }

        private async void BtnAddIndOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string clientIdStr = Microsoft.VisualBasic.Interaction.InputBox("Введите ID клиента:", "Добавление индивидуального заказа", "");
                if (!int.TryParse(clientIdStr, out int clientId))
                {
                    MessageBox.Show("Неверный ID клиента!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string modelIdStr = Microsoft.VisualBasic.Interaction.InputBox("Введите ID модели:", "Добавление индивидуального заказа", "");
                if (!int.TryParse(modelIdStr, out int modelId))
                {
                    MessageBox.Show("Неверный ID модели!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string materialIdStr = Microsoft.VisualBasic.Interaction.InputBox("Введите ID материала:", "Добавление индивидуального заказа", "");
                if (!int.TryParse(materialIdStr, out int materialId))
                {
                    MessageBox.Show("Неверный ID материала!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string colorIdStr = Microsoft.VisualBasic.Interaction.InputBox("Введите ID цвета:", "Добавление индивидуального заказа", "");
                if (!int.TryParse(colorIdStr, out int colorId))
                {
                    MessageBox.Show("Неверный ID цвета!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string size = Microsoft.VisualBasic.Interaction.InputBox("Введите размер:", "Добавление индивидуального заказа", "");

                string thermalIdStr = Microsoft.VisualBasic.Interaction.InputBox("Введите ID теплозащиты:", "Добавление индивидуального заказа", "1");
                if (!int.TryParse(thermalIdStr, out int thermalId)) thermalId = 1;

                string costStr = Microsoft.VisualBasic.Interaction.InputBox("Введите стоимость:", "Добавление индивидуального заказа", "0");
                if (!decimal.TryParse(costStr, out decimal cost))
                {
                    MessageBox.Show("Неверная стоимость!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string advanceStr = Microsoft.VisualBasic.Interaction.InputBox("Введите аванс:", "Добавление индивидуального заказа", "0");
                if (!decimal.TryParse(advanceStr, out decimal advance))
                {
                    MessageBox.Show("Неверный аванс!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string urgencyIdStr = Microsoft.VisualBasic.Interaction.InputBox("Введите ID срочности:", "Добавление индивидуального заказа", "1");
                if (!int.TryParse(urgencyIdStr, out int urgencyId)) urgencyId = 1;

                using (var db = new AppDbContext())
                {
                    db.IndividualOrders.Add(new kurs.Models.IndividualOrder
                    {
                        ClientId = clientId,
                        ModelId = modelId,
                        MaterialId = materialId,
                        ColorId = colorId,
                        Size = string.IsNullOrWhiteSpace(size) ? "" : size,
                        ThermalId = thermalId,
                        StartDate = DateTime.Now,
                        FitDate = DateTime.Now.AddDays(3),
                        IssueDate = DateTime.Now.AddDays(7),
                        UrgencyId = urgencyId,
                        Cost = cost,
                        Advance = advance
                    });
                    await db.SaveChangesAsync();
                    MessageBox.Show("Индивидуальный заказ добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    await LoadIndOrdersAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        private async void BtnDeleteIndOrder_Click(object sender, RoutedEventArgs e)
        {
            if (DgIndOrders.SelectedItem == null)
            {
                MessageBox.Show("Выберите индивидуальный заказ для удаления");
                return;
            }

            try
            {
                var selectedItem = DgIndOrders.SelectedItem;
                var prop = selectedItem.GetType().GetProperty("ID");
                if (prop == null) return;
                int id = Convert.ToInt32(prop.GetValue(selectedItem));

                var result = MessageBox.Show($"Удалить индивидуальный заказ ID={id}?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result != MessageBoxResult.Yes) return;

                using (var db = new AppDbContext())
                {
                    var order = await db.IndividualOrders.FindAsync(id);
                    if (order != null)
                    {
                        db.IndividualOrders.Remove(order);
                        await db.SaveChangesAsync();
                        MessageBox.Show("Индивидуальный заказ удален!");
                        await LoadIndOrdersAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        #endregion

        #region МАТЕРИАЛЫ

        private async Task LoadMaterialsAsync()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var materials = await db.Materials
                        .Select(m => new
                        {
                            ID = m.MaterialId,
                            Название = m.MaterialName
                        })
                        .ToListAsync();

                    DgMaterials.ItemsSource = materials;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки материалов: {ex.Message}", "Ошибка");
            }
        }

        private async void BtnRefreshMaterials_Click(object sender, RoutedEventArgs e)
        {
            await LoadMaterialsAsync();
        }

        private async void BtnAddMaterial_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string materialName = Microsoft.VisualBasic.Interaction.InputBox("Введите название материала:", "Добавление материала", "");
                if (string.IsNullOrWhiteSpace(materialName))
                {
                    MessageBox.Show("Название обязательно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                using (var db = new AppDbContext())
                {
                    db.Materials.Add(new kurs.Models.Material
                    {
                        MaterialName = materialName.Trim()
                    });
                    await db.SaveChangesAsync();
                    MessageBox.Show("Материал добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    await LoadMaterialsAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        private async void BtnDeleteMaterial_Click(object sender, RoutedEventArgs e)
        {
            if (DgMaterials.SelectedItem == null)
            {
                MessageBox.Show("Выберите материал для удаления");
                return;
            }

            try
            {
                var selectedItem = DgMaterials.SelectedItem;
                var prop = selectedItem.GetType().GetProperty("ID");
                if (prop == null) return;
                int id = Convert.ToInt32(prop.GetValue(selectedItem));

                var result = MessageBox.Show($"Удалить материал ID={id}?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result != MessageBoxResult.Yes) return;

                using (var db = new AppDbContext())
                {
                    var material = await db.Materials.FindAsync(id);
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
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        #endregion

        #region ПОСТАВКИ

        private async Task LoadSuppliesAsync()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var supplies = await db.Supplies
                        .Select(s => new
                        {
                            ID = s.SupplyId,
                            КодПоставщика = s.SupplierId,
                            КодМатериала = s.MaterialId,
                            ДатаПоставки = s.SupplyDate,
                            Количество = s.Quantity,
                            ЦенаЗаЕдиницу = s.UnitPrice
                        })
                        .ToListAsync();

                    DgSupplies.ItemsSource = supplies;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки поставок: {ex.Message}", "Ошибка");
            }
        }

        private async void BtnRefreshSupplies_Click(object sender, RoutedEventArgs e)
        {
            await LoadSuppliesAsync();
        }

        private async void BtnAddSupply_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string supplierIdStr = Microsoft.VisualBasic.Interaction.InputBox("Введите ID поставщика:", "Добавление поставки", "");
                if (!int.TryParse(supplierIdStr, out int supplierId))
                {
                    MessageBox.Show("Неверный ID поставщика!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string materialIdStr = Microsoft.VisualBasic.Interaction.InputBox("Введите ID материала:", "Добавление поставки", "");
                if (!int.TryParse(materialIdStr, out int materialId))
                {
                    MessageBox.Show("Неверный ID материала!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string quantityStr = Microsoft.VisualBasic.Interaction.InputBox("Введите количество:", "Добавление поставки", "1");
                if (!int.TryParse(quantityStr, out int quantity))
                {
                    MessageBox.Show("Неверное количество!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string priceStr = Microsoft.VisualBasic.Interaction.InputBox("Введите цену за единицу:", "Добавление поставки", "0");
                if (!decimal.TryParse(priceStr, out decimal unitPrice))
                {
                    MessageBox.Show("Неверная цена!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                using (var db = new AppDbContext())
                {
                    db.Supplies.Add(new kurs.Models.Supply
                    {
                        SupplierId = supplierId,
                        MaterialId = materialId,
                        SupplyDate = DateTime.Now,
                        Quantity = quantity,
                        UnitPrice = unitPrice
                    });
                    await db.SaveChangesAsync();
                    MessageBox.Show("Поставка добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    await LoadSuppliesAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        private async void BtnDeleteSupply_Click(object sender, RoutedEventArgs e)
        {
            if (DgSupplies.SelectedItem == null)
            {
                MessageBox.Show("Выберите поставку для удаления");
                return;
            }

            try
            {
                var selectedItem = DgSupplies.SelectedItem;
                var prop = selectedItem.GetType().GetProperty("ID");
                if (prop == null) return;
                int id = Convert.ToInt32(prop.GetValue(selectedItem));

                var result = MessageBox.Show($"Удалить поставку ID={id}?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result != MessageBoxResult.Yes) return;

                using (var db = new AppDbContext())
                {
                    var supply = await db.Supplies.FindAsync(id);
                    if (supply != null)
                    {
                        db.Supplies.Remove(supply);
                        await db.SaveChangesAsync();
                        MessageBox.Show("Поставка удалена!");
                        await LoadSuppliesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        #endregion
    }
}