using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; } 
        public string Salt { get; set; }
        public List<Expense> Expenses { get; set; } = new List<Expense>();

    }
}
