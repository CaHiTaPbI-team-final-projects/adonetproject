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
    public class GenresViewModel : INotifyPropertyChanged
    {
        private EDDM _EDDM;

        public ObservableCollection<Genre> Genres { get; set; }

        public GenresViewModel()
        {
            _EDDM = new EDDM();
            Genres = new ObservableCollection<Genre>();
            var GenresList = _EDDM.Genres.ToList();
            Genres.Add(new Genre() { Id = 999, Name = "Жанры" });
            foreach (Genre genre in GenresList)
            {
                Genres.Add(genre);
            }

        }



        private Genre _selectedGenre;
        public Genre SelectedGenre
        {
            get { return _selectedGenre; }
            set
            {
                _selectedGenre = value;
                

                OnPropertyChanged("SelectedGenre");
            }
        }


        private RelayCommand _addGenre;
        public RelayCommand AddGenre
        {
            get
            {
                return _addGenre ?? (_addGenre = new RelayCommand(obj =>
                {
                    Genre genre = new Genre()
                    {
                        Id = 0,
                        Name = "",
                        //Records = new List<Record>()
                        
                    };
                    Genres.Add(genre);
                    SelectedGenre = genre;
                }));
            }
        }

        private RelayCommand _updateGenre;
        public RelayCommand UpdateGenre
        {
            get
            {
                return _updateGenre ?? (_updateGenre = new RelayCommand(obj =>
                {
                    var newGenres = Genres.Where(u => u.Id == 0).ToList();
                    foreach (var gu in newGenres)
                        _EDDM.Genres.Add(new Genre() {  Name = gu.Name });
                    _EDDM.SaveChanges();
                    System.Windows.MessageBox.Show("New genre added succesfully");
                }));
            }
        }


        private RelayCommand _delGenre;
        public RelayCommand DelGenre
        {
            get
            {
                return _delGenre ?? (_delGenre = new RelayCommand(obj =>
                {
                    var deletedGenre = _EDDM.Genres.Where(c => c.Name == SelectedGenre.Name).FirstOrDefault();
                    if (deletedGenre != null)
                    {
                        _EDDM.Genres.Remove(deletedGenre);
                        _EDDM.SaveChanges();
                        System.Windows.MessageBox.Show("Genre вырезан succesfully");
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
