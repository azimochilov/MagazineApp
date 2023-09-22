using MagazineApp.Service.DTOs.Stores;
using MagazineApp.Service.Exceptions;
using MagazineApp.Service.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MagazineApp.Windows
{
    /// <summary>
    /// Interaction logic for StoreWindow.xaml
    /// </summary>
    public partial class StoreWindow : Window
    {
        private StoreService storeService;

        public StoreWindow()
        {
            InitializeComponent();
            storeService = new StoreService();
            storeListView.SelectionChanged += storeListView_SelectionChanged;
            
            var addWindow = new AddWindow();
            addWindow.ItemAdded += AddWindow_ItemAdded;
        }
        private void menuStoresAdd(object sender, MouseButtonEventArgs e)
        {
            AddWindow addWindow = new AddWindow();

            addWindow.Show();
            this.Close();
        }

        private void AddWindow_ItemAdded(object sender, EventArgs e)
        {
            DisplayAllStores();
        }
        private void backToMenu(object sender, MouseButtonEventArgs e)
        {
            MainWindow2 mainWindow = new MainWindow2();

            mainWindow.Show();
            this.Close();
        }
        private async void storeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var stores = await storeService.RetrieveAllAsync();
             
                ObservableCollection<StoreResultDto> storeList = new ObservableCollection<StoreResultDto>(stores);
                storeListView.ItemsSource = storeList;
            }
            catch (Exception ex)
            {         
                MessageBox.Show($"Error: {ex.Message}");
            }
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DisplayAllStores();
        }

        private async void DisplayAllStores()
        {
            try
            {
                var stores = await storeService.RetrieveAllAsync();
             
                ObservableCollection<StoreResultDto> storeList = new ObservableCollection<StoreResultDto>(stores);
                storeListView.ItemsSource = storeList;
            }
            catch (Exception ex)
            {             
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void UpdateStore_Click(object sender, RoutedEventArgs e)
        {
            UpdateWindow updateWindow = new UpdateWindow();

            updateWindow.Show();
            this.Close();
        }

        private async void DeleteStore_Click(object sender, RoutedEventArgs e)
        {            
            if (e.OriginalSource is FrameworkElement source && source.DataContext is StoreResultDto storeToDelete)
            {
                try
                {         
                    bool deleted = await storeService.RemoveAsync(storeToDelete.Id);

                    if (deleted)
                    {                 
                        var storeList = (ObservableCollection<StoreResultDto>)storeListView.ItemsSource;
                        storeList.Remove(storeToDelete);
                    }
                }
                catch (MagazineException ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Unable to determine which store to delete.", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}








