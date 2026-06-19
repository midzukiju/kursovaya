using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace kurs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void CatalogButton_Click(object sender, RoutedEventArgs e)
        {
            // Открытие окна каталога
            Catalog catalog = new Catalog();
            catalog.Show();
            this.Close();
        }

        private void ServicesButton_Click(object sender, RoutedEventArgs e)
        {
            // Уже открыта страница услуг
            Service service = new Service();
            service.Show();
            this.Close();
        }

        private void ReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            // Открытие окна отзывов
            Reviews reviews = new Reviews();
            reviews.Show();
            this.Close();
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            // Открытие окна "О нас"
            AboutUs aboutUs = new AboutUs();
            aboutUs.Show();
            this.Close();
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            // Открытие окна авторизации/профиля
            Registration registration = new Registration();
            registration.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RequestWindow requestWindow = new RequestWindow();
            requestWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RequestWindow requestWindow = new RequestWindow();
            requestWindow.Show();
            this.Close();
        }
    }
}