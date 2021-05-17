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
using DolbitManager.ViewModels;
using DolbitManager.Models;
namespace DolbitManager.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        // MainWindow data
        public MainWindow _mainWindow;
        public User authUser = new User();
        public bool isAuthorised { get; set; }
        //
        private LoginWindowViewModel LWVM;


        public LoginWindow(MainWindow mainWindow)
        {
            
            InitializeComponent();
            isAuthorised = false;
            LWVM = new LoginWindowViewModel(this);
            this.DataContext = LWVM;
            
            
            _mainWindow = mainWindow;
        }

       

      

       

        private void PasswordLogBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordLogBox.Clear();
        }

        private void LoginBox_GotFocus(object sender, RoutedEventArgs e)
        {
            LoginBox.Clear();
        }

        private void Reg1_GotFocus(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource == email)
            {
                email.Clear();
            }
            else if (e.OriginalSource == Fname)
            {
                Fname.Clear();
            }
            else if (e.OriginalSource == Sname)
            {
                Sname.Clear();
            }
            else if (e.OriginalSource == Tname)
            {
                Tname.Clear();
            }
            else if (e.OriginalSource == Pnumber)
            {
                Pnumber.Clear();
            }
            else if (e.OriginalSource == LoginRegBox)
            {
                LoginRegBox.Clear();
            }
            else if (e.OriginalSource == PasswordRegBox)
            {
                PasswordRegBox.Clear();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            isAuthorised = LWVM.isAuthorized;
            authUser = LWVM.authUser;

            if (isAuthorised == true)
            {

            }
            else
            {
                _mainWindow.Close();
            }
        }


    }
}
