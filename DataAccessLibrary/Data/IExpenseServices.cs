using DataAccessLibrary.Models;

namespace DataAccessLibrary.Data
{
    public interface IExpenseServices
    {
     
        bool CreateExpense(Expense expense);
        bool DeleteExpense(int id);
        IEnumerable<Expense> GetAllExpenses();
        Expense GetExpenseById(int id);
        List<Expense> GetExpensesByUserId(int userId);
        bool UpdateExpense(Expense updatedExpense);
    }
}