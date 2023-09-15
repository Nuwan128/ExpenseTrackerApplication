using DataAccessLibrary.Models;

namespace DataAccessLibrary.Data
{
    public interface IExpenseServices
    {
        void CreateExpense(Expense expense);
        void DeleteExpense(int id);
        IEnumerable<Expense> GetAllExpenses();
        Expense GetExpenseById(int id);
        void UpdateExpense(Expense expense);
    }
}