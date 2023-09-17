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

namespace MagazineApp.Windows;
public partial class LoginWindow : Window
{


    private readonly RegisterWindow registrationWindow;
    public LoginWindow()
    {
        InitializeComponent();
        
    }
    public LoginWindow(RegisterWindow registrationWindow)
    {
        
        this.registrationWindow = registrationWindow;
    }
    private void ShowPasswordButton_Click(object sender, RoutedEventArgs e)
    {
        // Toggle between PasswordBox and TextBox visibility
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
        }
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        // Implement your login logic here
        string username = LoginTextBox.Text;
        string password = PasswordBox.Password;

        // Check if "Remember Me" is checked
        bool rememberMe = RememberMeCheckBox.IsChecked ?? false;

        // Implement authentication logic and handle the result
        // You can display messages or navigate to another window as needed
    }

    private void RegisterLink_Click(object sender, RoutedEventArgs e)
    {

        RegisterWindow registerWindow = new RegisterWindow();
        registerWindow.Show();


    }

}
