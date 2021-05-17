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

namespace DolbitManager.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private EDDM dolBaza;

        public ObservableCollection<User> UsersList { get; set; }

        public UserViewModel()
        {
            dolBaza = new EDDM();
            UsersList = new ObservableCollection<User>();
            var UserListVar = dolBaza.Users.ToList();
            UsersList.Add(new User() { Id = -1, FirstName = "Пользователи", MoneySpent = 0, Password = "-", Phone = "0", SecondName = "-", ThirdName = "-", Username = "-" });
            foreach (User user in UserListVar) 
            {
                UsersList.Add(user);
            }
        }

        private User _selectedUser;
        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }

        private RelayCommand _addUser;
        public RelayCommand AddUser
        {
            get
            {
                return _addUser ?? (_addUser = new RelayCommand(obj =>
                {
                    User user = new User()
                    {
                        Id = 0,
                        FirstName = "",
                        SecondName = "",
                        ThirdName = "",
                        MoneySpent = 0,
                        Phone = "",
                        Username = "",
                        Password = ""
                    };
                    UsersList.Add(user);
                    SelectedUser = user;
                }));
            }
        }

        private RelayCommand _updateUser;
        public RelayCommand UpdateUser
        {
            get
            {
                return _updateUser ?? (_updateUser = new RelayCommand(obj =>
                {
                    var newUsers = UsersList.Where(u => u.Id == 0).ToList();
                    foreach (var nu in newUsers)
                        dolBaza.Users.Add(new User() { FirstName = nu.FirstName, SecondName=nu.SecondName, ThirdName = nu.ThirdName, MoneySpent = nu.MoneySpent, Phone = nu.Phone, Username = nu.Username, Password = nu.Password  });
                    dolBaza.SaveChanges();
                    MessageBox.Show("New users added succesfully");
                }));
            }
        }


        private RelayCommand _delUser;
        public RelayCommand DelUser
        {
            get
            {
                return _delUser ?? (_delUser = new RelayCommand(obj =>
                {
                    var deletedUser = dolBaza.Users.Where(c => c.Phone == SelectedUser.Phone).FirstOrDefault();
                    if (deletedUser != null)
                    {
                        dolBaza.Users.Remove(deletedUser);
                        dolBaza.SaveChanges();
                        MessageBox.Show("User вырезан succesfully");
                    }
                }));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
