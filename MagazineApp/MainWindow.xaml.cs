using MagazineApp.Data.Contexts;
using MagazineApp.Service.Helpers;
using MagazineApp.Windows;
using Microsoft.EntityFrameworkCore;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MagazineApp
{
    public partial class MainWindow : Window
    {

        
        public MainWindow()
        {            
            InitializeComponent();
        }        

        private void ShowPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Visibility == Visibility.Visible)
            {
                PasswordBox.Visibility = Visibility.Hidden;
                PasswordTextBox.Visibility = Visibility.Visible;
                PasswordTextBox.Text = PasswordBox.Password;
            }
            else
            {
                PasswordBox.Visibility = Visibility.Visible;
                PasswordTextBox.Visibility = Visibility.Hidden;
                PasswordBox.Password = PasswordTextBox.Text;
            }
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {            
            string username = LoginTextBox.Text;
            string password = PasswordBox.Password;
            
            bool rememberMe = RememberMeCheckBox.IsChecked == true;

            string email = username;
            AppDbContext appDbContext = new AppDbContext();
            var user = await appDbContext.Users.FirstOrDefaultAsync(x =>
                x.Email.ToLower() == email.ToLower());
            if (user is null)
            {
                MessageBox.Show("Email is not correct!");
                return;
            }
            var result = PasswordHelper.Verify(password, user.Password, user.Salt);
            if (result is true)
            {                
                MainWindow2 mainWindow2 = new MainWindow2();

                mainWindow2.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Password is not correct!");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }


        }

        private void RegisterLink_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();

            this.Close();
        }
    }
}
