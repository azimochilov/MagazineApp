using MagazineApp.Service.DTOs.Users;
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
namespace MagazineApp.Windows;

public partial class RegisterWindow : Window
{
    private readonly LoginWindow loginWindow;
    public RegisterWindow(LoginWindow loginWindow)
    {

        this.loginWindow = loginWindow;
    }
    public RegisterWindow()
    {
        InitializeComponent();        
    }
    private void LoginLink_Click(object sender, RoutedEventArgs e)
    {
        LoginWindow loginnWindow = new LoginWindow();
        loginnWindow.Show();
    }
}
