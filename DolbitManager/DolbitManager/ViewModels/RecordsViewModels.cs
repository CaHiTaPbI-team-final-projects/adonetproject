using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using DolbitManager.Models;
using DolbitManager.EF;
using DolbitManager.Commands;
namespace DolbitManager.ViewModels
{
    public class RecordsViewModels : INotifyPropertyChanged
    {
        private EDDM _EDDM;

        public ObservableCollection<Record> Records { get; set; }
        public RecordsViewModels()
        {
            _EDDM = new EDDM();
            Records = new ObservableCollection<Record>();
            var RecordsList = _EDDM.Records.ToList();
            foreach(Record record in RecordsList)
            {
                Records.Add(record);
            }
        }

        private Record _selectedRecord;
        public Record SelectedRecord
        {
            get { return _selectedRecord; }
            set
            {
                _selectedRecord = value;


                OnPropertyChanged("SelectedRecord");
            }
        }


        private RelayCommand _addRecord;
        public RelayCommand AddRecord
        {
            get
            {
                return _addRecord ?? (_addRecord = new RelayCommand(obj =>
                {
                    Record record = new Record()
                    {
                        Id = 0,
                        Name = "New record",
                        GroupId = 0,
                        ProducerId = 0,
                        TrackCount = 0,
                        GenreId = 0,
                        StorageId = 0,
                        RecordDate = new DateTime(1939, 09,1),
                        BasicPrice = 0,
                        SalePrice = 0,
                        //Sales = new List<Sale>()
                        
                    };
                    Records.Add(record);
                    SelectedRecord = record;
                }));
            }
        }

        private RelayCommand _updateRecord;
        public RelayCommand UpdateRecord
        {
            get
            {
                return _updateRecord ?? (_updateRecord = new RelayCommand(obj =>
                {
                    var newRecord = Records.Where(u => u.Id == 0).ToList();
                    foreach (var gu in newRecord)
                        _EDDM.Records.Add(new Record() { Name = gu.Name, GroupId = gu.GroupId, ProducerId = gu.ProducerId, TrackCount = gu.TrackCount, GenreId = gu.GenreId, StorageId = gu.StorageId, RecordDate = gu.RecordDate, 
                            BasicPrice =  gu.BasicPrice, SalePrice = gu.SalePrice });
                    _EDDM.SaveChanges();
                    System.Windows.MessageBox.Show("New Record added succesfully");
                }));
            }
        }


        private RelayCommand _delRecord;
        public RelayCommand DelRecord
        {
            get
            {
                return _delRecord ?? (_delRecord = new RelayCommand(obj =>
                {
                    var deletedRecord = _EDDM.Records.Where(c => c.Name == SelectedRecord.Name).FirstOrDefault();
                    if (deletedRecord != null)
                    {
                        Records.Remove(deletedRecord);
                        _EDDM.Records.Remove(deletedRecord);
                        _EDDM.SaveChanges();
                        System.Windows.MessageBox.Show("Record вырезан succesfully");
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
