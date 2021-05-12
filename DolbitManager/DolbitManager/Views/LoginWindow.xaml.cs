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

namespace DolbitManager.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
       // MainWindow _mainWindow;

        public LoginWindow()
        {
            InitializeComponent();
            //_mainWindow = mainWindow;
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            Reg1.Visibility = Visibility.Hidden;
            Log1.Visibility = Visibility.Visible;
            Log2.Visibility = Visibility.Visible;
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            Reg1.Visibility = Visibility.Visible;
            Log1.Visibility = Visibility.Hidden;
            Log2.Visibility = Visibility.Hidden;
        }

      

        private void Reg1_GotFocus(object sender, RoutedEventArgs e)
        {
            //Registration
            if(e.OriginalSource == email)
            {
                email.Clear();
            }
            else if(e.OriginalSource == Fname)
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

        private void PasswordLogBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordLogBox.Clear();
        }

        private void LoginBox_GotFocus(object sender, RoutedEventArgs e)
        {
            LoginBox.Clear();
        }


    }
}
