using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Data
{
    public class ExpenseServices : IExpenseServices
    {
        private readonly ExpenseDBContext _dbContext;

        public ExpenseServices(ExpenseDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateExpense(Expense expense)
        {
            _dbContext.Expenses.Add(expense);
            _dbContext.SaveChanges();
        }

        public Expense GetExpenseById(int id)
        {
            return _dbContext.Expenses.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Expense> GetAllExpenses()
        {
            return _dbContext.Expenses.ToList();
        }

        public void UpdateExpense(Expense expense)
        {
            _dbContext.Expenses.Update(expense);
            _dbContext.SaveChanges();
        }

        public void DeleteExpense(int id)
        {
            var expense = _dbContext.Expenses.FirstOrDefault(e => e.Id == id);
            if (expense != null)
            {
                _dbContext.Expenses.Remove(expense);
                _dbContext.SaveChanges();
            }
        }


    }

}
