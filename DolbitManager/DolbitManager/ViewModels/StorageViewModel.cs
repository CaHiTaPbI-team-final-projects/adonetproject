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
    public class StorageViewModel : INotifyPropertyChanged
    {
        private EDDM dolBaza = new EDDM();

        public ObservableCollection<Storage> StorageList { get; set; } = new ObservableCollection<Storage>();

        public StorageViewModel()
        {
            var StorageListVar = dolBaza.Storages.ToList();
            StorageList.Add(new Storage() { Id = -1, Name = "Носители" });
            foreach (Storage storage in StorageListVar)
            {
                StorageList.Add(storage);
            }
        }

        private Storage _selectedStorage;
        public Storage SelectedStorage
        {
            get { return _selectedStorage; }
            set
            {
                _selectedStorage = value;
                OnPropertyChanged("SelectedStorage");
            }
        }

        private RelayCommand _addStorage;
        public RelayCommand AddStorage
        {
            get
            {
                return _addStorage ?? (_addStorage = new RelayCommand(obj =>
                {
                    Storage storage = new Storage()
                    {
                        Id = 0,
                        Name = ""
                    };
                    StorageList.Add(storage);
                    SelectedStorage = storage;
                }));
            }
        }

        private RelayCommand _updateStorage;
        public RelayCommand UpdateStorage
        {
            get
            {
                return _updateStorage ?? (_updateStorage = new RelayCommand(obj =>
                {
                    var newStorages = StorageList.Where(u => u.Id == 0).ToList();
                    foreach (var ns in newStorages)
                        dolBaza.Storages.Add(new Storage() { Name = ns.Name });
                    dolBaza.SaveChanges();
                    MessageBox.Show("New storage added succesfully");
                }));
            }
        }


        private RelayCommand _delStorage;
        public RelayCommand DelStorage
        {
            get
            {
                return _delStorage ?? (_delStorage = new RelayCommand(obj =>
                {
                    var deletedStorage = dolBaza.Storages.Where(c => c.Name == SelectedStorage.Name).FirstOrDefault();
                    if (deletedStorage != null)
                    {
                        dolBaza.Storages.Remove(deletedStorage);
                        dolBaza.SaveChanges();
                        MessageBox.Show("Storage вырезан succesfully");
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
