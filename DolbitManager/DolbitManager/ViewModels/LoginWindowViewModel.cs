using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using DolbitManager.Commands;
using DolbitManager.Models;
using DolbitManager.Views;
using DolbitManager.EF;
using System.Windows;
using DolbitManager.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;
using System.Security.Cryptography;
namespace DolbitManager.ViewModels
{
    class LoginWindowViewModel : INotifyPropertyChanged
    {
        //login values
        private string _login;
        private string _password;

        LoginWindow _loginWindow;

        //register values
        private string _loginReg;
        private string _passwordReg;
        private string _emailReg;
        private string _fnameReg;
        private string _snameReg;
        private string _tnameReg;
        private string _phoneReg;

        private UserViewModel uvm = new UserViewModel();

        // Data for main window
        public bool isAuthorized { get; set; }
        public User authUser = new User();
        //

        public ICommand Authorize { get; set; }
        public ICommand Register { get; set; }

        

        public LoginWindowViewModel(LoginWindow loginWindow)
        {
            isAuthorized = false;
            Authorize = new RelayCommand(ForceAuthorize, canExecuteMethod);
            Register = new RelayCommand(ForceRegister, canExecuteMethod);
            _loginWindow = loginWindow;
        }

        public static string GetHash(string input)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] bs = Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            StringBuilder s = new StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();

        }


        public string Login
        {
            get {return _login;}
            set
            { 
                _login = value;
                OnPropertyChanged("Login");
            }
        }


        public string LoginReg
        {
            get { return _loginReg; }
            set
            {
                _loginReg = value;
                OnPropertyChanged("LoginReg");
            }
        }

        public string EmailReg
        {
            get { return _emailReg; }
            set
            {
                _emailReg = value;
                OnPropertyChanged("EmailReg");
            }
        }

        public string FnameReg
        {
            get { return _fnameReg; }
            set
            {
                _fnameReg = value;
                OnPropertyChanged("FnameReg");
            }
        }

        public string SnameReg
        {
            get { return _snameReg; }
            set
            {
                _snameReg = value;
                OnPropertyChanged("SnameReg");
            }
        }

        public string TnameReg
        {
            get { return _tnameReg; }
            set
            {
                _tnameReg = value;
                OnPropertyChanged("TnameReg");
            }
        }

        public string PhoneReg
        {
            get { return _phoneReg; }
            set
            {
                _phoneReg = value;
                OnPropertyChanged("PhoneReg");
            }
        }

        void ForceRegister(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            _passwordReg = passwordBox.Password;
            _passwordReg = GetHash(_passwordReg);
            User NewUser = new User()
            {
                Id = 0,
                FirstName = _fnameReg,
                SecondName = _snameReg,
                ThirdName = _tnameReg,
                Phone = _phoneReg,
                Password = _passwordReg,
                Username = _loginReg,
                MoneySpent = 0,

            };
            uvm.UsersList.Add(NewUser);
            var obj = new object();
            uvm.UpdateUser.Execute(obj);


        }

        void ForceAuthorize(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            _password = passwordBox.Password;
            _password = GetHash(_password);
            authUser = uvm.UsersList.Where(c => c.Username == _login).FirstOrDefault();
            if( authUser != null && authUser.Password == _password )
            {
                isAuthorized = true;
                _loginWindow.Close();
            }
            else
            {
                MessageBox.Show("Authorize Failed!\nIncorrect login or password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private RelayCommand signInButton_Click;
        public RelayCommand SignInButton_Click
        {
            get
            {
                return signInButton_Click ??
                    (signInButton_Click = new RelayCommand(obj =>
                    {
                        _loginWindow.Reg1.Visibility = Visibility.Hidden;
                        _loginWindow.Log1.Visibility = Visibility.Visible;
                        _loginWindow.Log2.Visibility = Visibility.Visible;

                    }));
            }
        }

        private RelayCommand signUpButton_Click;
        public RelayCommand SignUpButton_Click
        {
            get
            {
                return signUpButton_Click ??
                    (signUpButton_Click = new RelayCommand(obj =>
                    {
                        _loginWindow.Reg1.Visibility = Visibility.Visible;
                        _loginWindow.Log1.Visibility = Visibility.Hidden;
                        _loginWindow.Log2.Visibility = Visibility.Hidden;

                    }));
            }
        }



   

        private bool canExecuteMethod(object parameter)
        {
            return true;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
