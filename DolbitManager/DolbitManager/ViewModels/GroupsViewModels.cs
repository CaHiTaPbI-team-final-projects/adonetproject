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
    public class GroupsViewModels : INotifyPropertyChanged
    {
        private EDDM _EDDM;

        public ObservableCollection<Group> Groups { get; set; }

        public GroupsViewModels()
        {
            _EDDM = new EDDM();
            Groups = new ObservableCollection<Group>();
            var GroupsList = _EDDM.Groups.ToList();
            Groups.Add(new Group() { Id = -1, Name = "Группы" });
            foreach (Group group in GroupsList)
            {
                Groups.Add(group);
            }
        }



        private Group _selectedGroup;
        public Group SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                _selectedGroup = value;


                OnPropertyChanged("SelectedGroup");
            }
        }


        private RelayCommand _addGroup;
        public RelayCommand AddGroup
        {
            get
            {
                return _addGroup ?? (_addGroup = new RelayCommand(obj =>
                {
                    Group group = new Group()
                    {
                        Id = 0,
                        Name = "",
                        //Records = new List<Record>()

                    };
                    Groups.Add(group);
                    SelectedGroup = group;
                }));
            }
        }

        private RelayCommand _updateGroup;
        public RelayCommand UpdateGroup
        {
            get
            {
                return _updateGroup ?? (_updateGroup = new RelayCommand(obj =>
                {
                    var newGroup = Groups.Where(u => u.Id == 0).ToList();
                    foreach (var gu in newGroup)
                        _EDDM.Groups.Add(new Group() { Name = gu.Name });
                    _EDDM.SaveChanges();
                    System.Windows.MessageBox.Show("New Group added succesfully");
                }));
            }
        }


        private RelayCommand _delGroup;
        public RelayCommand DelGroup
        {
            get
            {
                return _delGroup ?? (_delGroup = new RelayCommand(obj =>
                {
                    var deletedGroup = _EDDM.Groups.Where(c => c.Name == SelectedGroup.Name).FirstOrDefault();
                    if (deletedGroup != null)
                    {
                        _EDDM.Groups.Remove(deletedGroup);
                        _EDDM.SaveChanges();
                        System.Windows.MessageBox.Show("Group вырезан succesfully");
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
