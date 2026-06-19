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
    /// Логика взаимодействия для ProductDetails.xaml
    /// </summary>
    public partial class ProductDetails : Window
    {
        public ProductDetails()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Catalog catalog = new Catalog();
            catalog.Show();
            Close();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            RequestWindow requestWindow = new RequestWindow();
            requestWindow.Show();
            Close();
        }

        private void FavoriteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Товар добавлен в избранное");
        }
    }
}
