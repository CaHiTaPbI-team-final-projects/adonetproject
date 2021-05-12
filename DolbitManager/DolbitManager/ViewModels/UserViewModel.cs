﻿using System;
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

namespace DolbitManager.ViewModel
{
    class UserViewModel : INotifyPropertyChanged
    {
        private EDDM dolBaza = new EDDM();

        public List<User> UsersList { get; set; } = new List<User>();

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