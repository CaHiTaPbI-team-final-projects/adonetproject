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
    class SaleViewModel : INotifyPropertyChanged
    {
        private EDDM dolBaza = new EDDM();

        public List<Sale> SaleList { get; set; } = new List<Sale>();

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
                    Sale Sale = new Sale()
                    {
                        Id = 0,
                        RecordId = 0,
                        UserId = 0,
                        Date = new DateTime(2012,12,12),
                    };
                    SaleList.Add(Sale);
                    SelectedSale = Sale;
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
                    var newSales = SaleList.Where(u => u.Id == 0).ToList();
                    foreach (var ns in newSales)
                        dolBaza.Sales.Add(new Sale() { RecordId = ns.RecordId, UserId = ns.UserId, Date = ns.Date });
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