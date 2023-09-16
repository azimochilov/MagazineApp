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

namespace MagazineApp.Windows;
public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
    }
    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        string username = UsernameTextBox.Text;
        string password = PasswordBox.Password;

        // TODO: Implement authentication logic here
        if (AuthenticateUser(username, password))
        {
            MessageBox.Show("Login successful!");
            // Proceed to the main application or navigate to another window
        }
        else
        {
            MessageBox.Show("Invalid credentials. Please try again.");
        }
    }

    private void RegisterButton_Click(object sender, RoutedEventArgs e)
    {
            showHideButton.Content = "Hide Password";
    }

    private void ShowPasswordButton_Click(object sender, RoutedEventArgs e)
    {
        // Toggle password visibility
        if (PasswordBox.Visibility == Visibility.Visible)
        {
            PasswordBox.Visibility = Visibility.Collapsed;
            ShowPasswordButton.Content = "Show Password";
        }
        else
        {
            PasswordBox.Visibility = Visibility.Visible;
            ShowPasswordButton.Content = "Hide Password";
        }
    }

    // TODO: Implement your authentication logic here
    private bool AuthenticateUser(string username, string password)
    {
        // Replace this with your actual authentication logic (e.g., check against a database)
        // Return true if authentication succeeds, false otherwise
        return username == "admin" && password == "password";
    }

}
