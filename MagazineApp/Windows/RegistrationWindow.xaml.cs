using MagazineApp.Data.IRepositories;
using MagazineApp.Domain.Entities;
using MagazineApp.Service.DTOs.Users;
using MagazineApp.Service.Interfaces;
using MagazineApp.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class RegistrationWindow : Window
    {
        private readonly UserService userService;        
        MainWindow mainWindow = new MainWindow();
        public RegistrationWindow()
        {
            InitializeComponent();
            userService = new UserService();
        }
        
        private void LoginLink_Click(object sender, RoutedEventArgs e)
        {            
            mainWindow.Show();

            this.Close();
        }

        private void TogglePasswordVisibility_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Visibility == Visibility.Visible)
            {
                PasswordBox.Visibility = Visibility.Collapsed;
                PasswordTextBox.Visibility = Visibility.Visible;
                PasswordTextBox.Text = PasswordBox.Password;
            }
            else
            {
                PasswordBox.Visibility = Visibility.Visible;
                PasswordTextBox.Visibility = Visibility.Collapsed;
                PasswordBox.Password = PasswordTextBox.Text;
            }
        }


        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userDto = new UserCreationDto
                {
                    FirstName = FirstNameTextBox.Text,
                    LastName = LastNameTextBox.Text,
                    PhoneNumber = PhoneNumberTextBox.Text,
                    Email = EmailTextBox.Text,
                    Password = PasswordBox.Password,
                };

                string passwordPattern = @"^(?=.*[A-Z])(?=.*\d).{8,}$";
                string username = userDto.Email; 

                if (!Regex.IsMatch(userDto.Password, passwordPattern))
                {
                    MessageBox.Show("Password must be at least 8 characters long and contain at least one uppercase letter and one digit.");
                    return;
                }

                if (username.Length < 8)
                {
                    MessageBox.Show("Username must be at least 8 characters long.");
                    return;
                }

                var registeredUser = await this.userService.AddAsync(userDto);

                MessageBox.Show($"User {registeredUser.FirstName} registered successfully!");
                MainWindow2 mainWindow2 = new MainWindow2();

                mainWindow2.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error registering user: {ex.Message}");
            }
        }


    }

}

