using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolbitManager.Models
{
    public class Storage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Record> Records { get; set; }
    }
}
