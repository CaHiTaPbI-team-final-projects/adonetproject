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
    public  class ProducersViewModels : INotifyPropertyChanged
    {
        private EDDM _EDDM;

        public ObservableCollection<Producer> Producers { get; set; }

        public ProducersViewModels()
        {
            _EDDM = new EDDM();
            Producers = new ObservableCollection<Producer>();
            var ProducersList = _EDDM.Producers.ToList();
            Producers.Add(new Producer() { Id = 999, Name = "Продюсеры" });
            foreach (Producer producer in ProducersList)
            {
                Producers.Add(producer);
            }

        }


        private Producer _selectedProducer;
        public Producer SelectedProducer
        {
            get { return _selectedProducer; }
            set
            {
                _selectedProducer = value;


                OnPropertyChanged("SelectedProducer");
            }
        }


        private RelayCommand _addProducer;
        public RelayCommand AddProducer
        {
            get
            {
                return _addProducer ?? (_addProducer = new RelayCommand(obj =>
                {
                    Producer producer = new Producer()
                    {
                        Id = 0,
                        Name = "",
                        //Records = new List<Record>();

                    };
                    Producers.Add(producer);
                    SelectedProducer = producer;
                }));
            }
        }

        private RelayCommand _updateProducer;
        public RelayCommand UpdateProducer
        {
            get
            {
                return _updateProducer ?? (_updateProducer = new RelayCommand(obj =>
                {
                    var newProducer = Producers.Where(u => u.Id == 0).ToList();
                    foreach (var gu in newProducer)
                        _EDDM.Producers.Add(new Producer() { Name = gu.Name });
                    _EDDM.SaveChanges();
                    System.Windows.MessageBox.Show("New Producer added succesfully");
                }));
            }
        }


        private RelayCommand _delProducer;
        public RelayCommand DelProducer
        {
            get
            {
                return _delProducer ?? (_delProducer = new RelayCommand(obj =>
                {
                    var deletedProducer = _EDDM.Producers.Where(c => c.Name == SelectedProducer.Name).FirstOrDefault();
                    if (deletedProducer != null)
                    {
                        _EDDM.Producers.Remove(deletedProducer);
                        _EDDM.SaveChanges();
                        System.Windows.MessageBox.Show("Producer вырезан succesfully");
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
