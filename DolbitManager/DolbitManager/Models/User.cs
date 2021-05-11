using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolbitManager.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }

        public string FirstName {get;set;}
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public decimal MoneySpent { get; set; }

        
        public virtual List<Sale> Sales { get; set; }
    }
}
