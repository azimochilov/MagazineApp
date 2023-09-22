using MagazineApp.Service.DTOs.Stores;
using MagazineApp.Service.Exceptions;
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
    public partial class UpdateWindow : Window
    {
        public event EventHandler ItemUpdated;

        private StoreResultDto selectedStore;
        private StoreService storeService;

        public UpdateWindow()
        {
            InitializeComponent();
            storeService = new StoreService(); 
        }

        public UpdateWindow(StoreResultDto store)
        {
            InitializeComponent();
            selectedStore = store;
            storeService = new StoreService();

            tbName.Text = selectedStore.Name;
            tbAddress.Document.Blocks.Clear();
            tbAddress.Document.Blocks.Add(new Paragraph(new Run(selectedStore.Address)));
        }

        private async void UpdateStore_Click(object sender, RoutedEventArgs e)
        {
            string newName = tbName.Text;
            string newAddress = new TextRange(tbAddress.Document.ContentStart, tbAddress.Document.ContentEnd).Text;

            if (long.TryParse(tbId.Text, out long Id))
            {                
                var updateDto = new StoreUpdateDto
                {
                    Id = Id, 
                    Name = newName,
                    Address = newAddress
                };

                try
                {                    
                    var updatedStore = await storeService.ModifyAsync(updateDto);
                    
                    ItemUpdated?.Invoke(this, EventArgs.Empty);

                    StoreWindow storeWindow = new StoreWindow();
                    storeWindow.Show();
                    this.Close();
                }
                catch (MagazineException ex)
                {
                    
                    MessageBox.Show($"Error: {ex.Message}", "Update Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid Id. Please enter a valid numeric Id.", "Update Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void CancelUpdate_Click(object sender, RoutedEventArgs e)
        {
            StoreWindow storeWindow = new StoreWindow();
            storeWindow.Show();
            this.Close();
        }

    }


}
