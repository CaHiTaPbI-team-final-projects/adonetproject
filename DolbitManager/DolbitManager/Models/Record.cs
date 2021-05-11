using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolbitManager.Models
{
    public class Record
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        public int ProducerId { get; set; }
        public virtual Producer Producer { get; set; }
        public int TrackCount { get; set; }
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
        public int StorageId { get; set; }
        public virtual Storage Storage { get; set; }
        public DateTime RecordDate {get;set;}
        public decimal BasicPrice { get; set; }
        public int SalePrice { get; set; }

        public virtual List<Sale> Sales { get; set; }
    }
}
