using DataAccessLibrary.Data;
using DataAccessLibrary.Models;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using User = DataAccessLibrary.Models.User;

namespace Expense_Tracker
{
    public partial class Dashboard : Form
    {
        private readonly IExpenseServices _expenseServices;
        private readonly User _user;
        private int expensId;

        public Dashboard(IExpenseServices expenseServices, User user)
        {
            InitializeComponent();
            _expenseServices = expenseServices;
            _user = user;
            expenseDataGridView.DataSource = _expenseServices.GetExpensesByUserId(user.Id);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            List<string> emptyFields = new List<string>();

            if (string.IsNullOrWhiteSpace(expenseTextBox.Text))
            {
                emptyFields.Add("Expense Name");
                expenseTextBox.Focus();
            }
            if (string.IsNullOrWhiteSpace(amountTextBox.Text))
            {
                emptyFields.Add("Amout");
                amountTextBox.Focus();
            }


            if (emptyFields.Count > 0)
            {
                string message = "Please enter the following fields:\n\n";
                message += string.Join("\n", emptyFields);
                MessageBox.Show(message, "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var expense = new Expense
            {
                ExpenseName = expenseTextBox.Text,
                Amount = amountTextBox.Text,
                ExpenseDate = expenceDateTimePicker.Value.Date,
                UserId = _user.Id
            };

            try
            {
                var createSuccess = _expenseServices.CreateExpense(expense);
                if (createSuccess)
                {
                    MessageBox.Show("Expense added successfuly!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    expenseDataGridView.DataSource = _expenseServices.GetExpensesByUserId(_user.Id);
                    expenseTextBox.Clear();
                    amountTextBox.Clear();
                }
                else
                {
                    MessageBox.Show("Created failed.an error during Creted.", "Create Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
             
            }
            catch (Exception ex)
            {

                MessageBox.Show($" failed. there was an error during adding espense.{ex}", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            List<string> emptyFields = new List<string>();
            if (string.IsNullOrWhiteSpace(expenseTextBox.Text))
            {
                emptyFields.Add("Expense Name");
                expenseTextBox.Focus();
            }
            if (string.IsNullOrWhiteSpace(amountTextBox.Text))
            {
                emptyFields.Add("Amout");
                amountTextBox.Focus();
            }



            if (emptyFields.Count > 0)
            {
                string message = "Please enter the following fields:\n\n";
                message += string.Join("\n", emptyFields);
                MessageBox.Show(message, "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var removeSuccess = _expenseServices.DeleteExpense(expensId); 
                if (removeSuccess)
                {
                    MessageBox.Show("Expense deleted successfuly!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    expenseDataGridView.DataSource = _expenseServices.GetExpensesByUserId(_user.Id);
                    expenseTextBox.Clear();
                    amountTextBox.Clear();
                }
                else
                {
                    MessageBox.Show("Remove failed.an error during remove.", "Remove Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}");
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            List<string> emptyFields = new List<string>();
            if (string.IsNullOrWhiteSpace(expenseTextBox.Text))
            {
                emptyFields.Add("Expense Name");
                expenseTextBox.Focus();
            }
            if (string.IsNullOrWhiteSpace(amountTextBox.Text))
            {
                emptyFields.Add("Amout");
                amountTextBox.Focus();
            }



            if (emptyFields.Count > 0)
            {
                string message = "Please enter the following fields:\n\n";
                message += string.Join("\n", emptyFields);
                MessageBox.Show(message, "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            var expense = new Expense
            {
                Id = expensId,
                ExpenseName = expenseTextBox.Text,
                Amount = amountTextBox.Text,
                ExpenseDate = expenceDateTimePicker.Value.Date,
                UserId = _user.Id
            };
            try
            {
                var updateSuccess = _expenseServices.UpdateExpense(expense);
                if (updateSuccess)
                {
                    MessageBox.Show("Expense updated successfuly!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    expenseDataGridView.DataSource = _expenseServices.GetExpensesByUserId(_user.Id);
                    expenseTextBox.Clear();
                    amountTextBox.Clear();
                }
                else
                {
                MessageBox.Show("Update failed.an error during Update.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }



            }
            catch (Exception ex)
            {

                MessageBox.Show($" failed. there was an error during updating espense.{ex}", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void expenseDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var idCellValue = expenseDataGridView.Rows[e.RowIndex].Cells["Id"].Value.ToString();

            expensId = Convert.ToInt32(idCellValue);

            try
            {

                var expense = _expenseServices.GetExpenseById(expensId);
                expenseTextBox.Text = expense.ExpenseName;
                amountTextBox.Text = expense.Amount;
                expenceDateTimePicker.Value = expense.ExpenseDate;
            }
            catch (Exception ex)
            {

                MessageBox.Show($" failed. there was an error during  espense.{ex}", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }



        }

    
    }
}
