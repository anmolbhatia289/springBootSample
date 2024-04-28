using ExpenseSharing.Exceptions;
using ExpenseSharing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing
{
    public static class ExpenseFactory
    {
        public static Expense getExpense(ExpenseView expenseView)
        {
            Expense expense = null;
            switch (expenseView.type)
            {
                case ExpenseType.EQUAL:
                    expense = new EqualExpense(expenseView.Name, expenseView.description, expenseView.paidBy, expenseView.amount);
                    break;
                case ExpenseType.PERCENT:
                    expense = new PercentageExpense(expenseView.Name, expenseView.description, expenseView.paidBy, expenseView.amount);
                    break;
                case ExpenseType.EXACT:
                    expense = new ExactExpense(expenseView.Name, expenseView.description, expenseView.paidBy, expenseView.amount);
                    break;
                default:
                    throw new InvalidExpenseType();
            }

            return expense;
        }
    }
}
