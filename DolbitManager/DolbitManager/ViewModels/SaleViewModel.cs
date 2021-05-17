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
    public class SaleViewModel : INotifyPropertyChanged
    {
        private EDDM dolBaza = new EDDM();

        private RecordsViewModels recVM = new RecordsViewModels();
        private UserViewModel userVM = new UserViewModel();

        public ObservableCollection<Sale> SaleList { get; set; } = new ObservableCollection<Sale>();

        public SaleViewModel()
        {
            var SaleListVar = dolBaza.Sales.ToList();
            foreach (Sale sale in SaleListVar)
            {
                SaleList.Add(sale);
            }
        }

        private Sale _selectedSale;
        public Sale SelectedSale
        {
            get { return _selectedSale; }
            set
            {
                _selectedSale = value;
                OnPropertyChanged("SelectedSale");
            }
        }

        private RelayCommand _addSale;
        public RelayCommand AddSale
        {
            get
            {
                return _addSale ?? (_addSale = new RelayCommand(obj =>
                {
                    Sale sale = new Sale()
                    {
                        Id = 0,
                        RecordId = 1,
                        UserId = 1,
                        Date = new DateTime(2012,12,12),
                    };
                    SaleList.Add(sale);
                    SelectedSale = sale;
                }));
            }
        }

        private RelayCommand _updateSale;
        public RelayCommand UpdateSale
        {
            get
            {
                return _updateSale ?? (_updateSale = new RelayCommand(obj =>
                {
                    MainWindow wd = obj as MainWindow;
                    var newSales = SaleList.Where(u => u.Id == 0).ToList();
                    foreach (var ns in newSales)
                        dolBaza.Sales.Add(new Sale() { RecordId =  wd.avm.recordvm.SelectedRecord.Id, UserId = wd.avm.uservm.SelectedUser.Id, Date = DateTime.Now });
                    dolBaza.SaveChanges();
                    MessageBox.Show("New Sale added succesfully");
                }));
            }
        }


        private RelayCommand _delSale;
        public RelayCommand DelSale
        {
            get
            {
                return _delSale ?? (_delSale = new RelayCommand(obj =>
                {
                    var deletedSale = dolBaza.Sales.Where(c => c.RecordId == SelectedSale.RecordId && c.UserId == SelectedSale.UserId).FirstOrDefault();
                    if (deletedSale != null)
                    {
                        dolBaza.Sales.Remove(deletedSale);
                        dolBaza.SaveChanges();
                        MessageBox.Show("Sale вырезан succesfully");
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
