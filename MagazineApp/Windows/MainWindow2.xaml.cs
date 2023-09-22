using MagazineApp.Service.Services;
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

namespace MagazineApp.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow2.xaml
    /// </summary>
    public partial class MainWindow2 : Window
    {
        private UserService userService;

        public MainWindow2()
        {
            InitializeComponent();
            userService = new UserService();
            Loaded += MainWindow2_Loaded; 
        }

        private async void MainWindow2_Loaded(object sender, RoutedEventArgs e)
        {            
            var users = await userService.RetrieveAllAsync();
            
            userListView.ItemsSource = users;
        }

        private void menuRegistration_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RegistrationWindow regWindow = new RegistrationWindow();
            regWindow.Show();
            this.Close();
        }

        private void menuStores_MouseUp(object sender, MouseButtonEventArgs e)
        {
            StoreWindow storeWindow = new StoreWindow();
            storeWindow.Show();
            this.Close();
        }
    }
}
