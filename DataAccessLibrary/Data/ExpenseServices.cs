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
        public bool CreateExpense(Expense expense)
        {
            // Check if an expense with the same attributes already exists
            var existingExpense = _dbContext.Expenses
                .FirstOrDefault(e =>
                    e.ExpenseName == expense.ExpenseName &&
                    e.ExpenseDate.Date == expense.ExpenseDate.Date &&
                    e.Amount == expense.Amount);

            if (existingExpense == null)
            {
                // If no matching expense exists, add the new expense
                _dbContext.Expenses.Add(expense);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                // If a matching expense exists, handle the duplicate gracefully
               return false;
                
            }
        }



        public Expense GetExpenseById(int id)
        {
            return _dbContext.Expenses.FirstOrDefault(e => e.Id == id);
        }
        public List<Expense> GetExpensesByUserId(int userId)
        {
            return _dbContext.Expenses.Where(e => e.UserId == userId).ToList();
        }
        public List<Expense> GetExpensesByUserName(string expenseName)
        {
            return _dbContext.Expenses.Where(e => e.ExpenseName == expenseName).ToList();
        }



        public IEnumerable<Expense> GetAllExpenses()
        {
            return _dbContext.Expenses.ToList();
        }

        public bool UpdateExpense(Expense updatedExpense)
        {
            // Retrieve the existing expense from the database based on its unique identifier (e.g., ExpenseId)
            var existingExpense = _dbContext.Expenses.FirstOrDefault(e => e.Id == updatedExpense.Id);

            if (existingExpense != null)
            {
                // Update the properties of the existing expense with the new values
                existingExpense.ExpenseName = updatedExpense.ExpenseName;
                existingExpense.Amount = updatedExpense.Amount;
                existingExpense.ExpenseDate = updatedExpense.ExpenseDate;

                // Save the changes to the database
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                // Handle the case where the expense to update does not exist
                return false;
                
            }
        }


        public bool DeleteExpense(int id)
        {
            
                var expense = _dbContext.Expenses.FirstOrDefault(e => e.Id == id);
                if (expense != null)
                {
                    _dbContext.Expenses.Remove(expense);
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            
           
            
        }


    }

}
