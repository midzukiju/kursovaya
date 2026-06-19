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
    /// Логика взаимодействия для Catalog.xaml
    /// </summary>
    public partial class Catalog : Window
    {
        public Catalog()
        {
            InitializeComponent();
        }

        private void Product_Click(object sender, RoutedEventArgs e)
        {
            ProductDetails details = new ProductDetails();
            details.Show();
            Close();
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
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

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            profile.Show();
            Close();
        }
        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text == "Поиск...")
            {
                SearchBox.Text = "";
                SearchBox.Foreground = Brushes.Black;
            }
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchBox.Text))
            {
                SearchBox.Text = "Поиск...";
                SearchBox.Foreground = Brushes.Gray;
            }
        }
    }
}
