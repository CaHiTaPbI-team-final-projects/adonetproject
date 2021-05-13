using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using DolbitManager.Models;
using DolbitManager.ViewModels;
using DolbitManager.EF;

namespace DolbitManager.ViewModels
{
    public class AppViewModel
    {
        public GenresViewModel genvm { get; set; } = new GenresViewModel();
        public GroupsViewModels grovm { get; set; } = new GroupsViewModels();
        public ProducersViewModels provm { get; set; } = new ProducersViewModels();
        public StorageViewModel storvm { get; set; } = new StorageViewModel();

        public RecordsViewModels recordvm { get; set; } = new RecordsViewModels();
        public UserViewModel uservm { get; set; } = new UserViewModel();
        public SaleViewModel salevm { get; set; } = new SaleViewModel();

    }
}
