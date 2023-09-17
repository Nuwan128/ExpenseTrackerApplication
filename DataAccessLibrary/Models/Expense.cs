using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string ExpenseName { get; set; }
        public string Amount { get; set;}
        public DateTime ExpenseDate { get; set; }
        public int UserId { get; set; }

    }
}
