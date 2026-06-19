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
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        public Profile()
        {
            InitializeComponent();
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

        private void EditProfile_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Редактирование профиля");
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
