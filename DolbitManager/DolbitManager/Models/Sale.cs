using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolbitManager.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }


        public int RecordId { get; set; }
        public virtual Record Record {get;set;}

        public DateTime Date { get; set; }

    }
}
