using MagazineApp.Service.DTOs.Stores;
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
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private StoreService storeService;
        public event EventHandler ItemAdded;

        public AddWindow()
        {
            InitializeComponent();
            storeService = new StoreService(); // Initialize your StoreService
        }

        private async void AddProduct(object sender, RoutedEventArgs e)
        {

            string storeName = tbName.Text;
            string storeDescription = new TextRange(tbDescription.Document.ContentStart, tbDescription.Document.ContentEnd).Text;

            var dtoStore = new StoreCreationDto()
            {
                Address = storeDescription,
                Name = storeName
            };

            await storeService.AddAsync(dtoStore);
            
            ItemAdded?.Invoke(this, EventArgs.Empty);
            StoreWindow storeWindow = new StoreWindow();
            storeWindow.Show();
            this.Close();
        }

    }
}
