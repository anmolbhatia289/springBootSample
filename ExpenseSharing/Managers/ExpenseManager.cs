using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing.Managers
{
    public class ExpenseManager
    {
        public static ExpenseManager _instance;
        private ExpenseRepository _repository;
        private Dictionary<Guid, Dictionary<Guid, float>> balanceSheet;
        private ExpenseManager()
        {
            _repository = ExpenseRepository.getInstance();
            balanceSheet = new Dictionary<Guid, Dictionary<Guid, float>>();
        }

        public static ExpenseManager getInstance()
        {
            if (_instance == null)
            {
                _instance = new ExpenseManager();
            }

            return _instance;
        }

        public void addExpense(ExpenseView expenseView) 
        {
            var expense = ExpenseFactory.getExpense(expenseView);
            expense.validateExpense(expenseView.splits);
            _repository.addExpense(expense);
            List<Split> splits = expense.getSplits(expenseView.splits);
            foreach (Split split in splits) 
            {
                createNewEntryIfNotExists(split.paidBy, split.paidTo);
                createNewEntryIfNotExists(split.paidTo, split.paidBy);
                balanceSheet[split.paidBy][split.paidTo] += split.amount;
                balanceSheet[split.paidTo][split.paidBy] += -1 * split.amount;
            }
        }

        public float getBalance(Guid user1, Guid user2)
        {
            createNewEntryIfNotExists(user1, user2);
            return balanceSheet[user1][user2];
        }

        private void createNewEntryIfNotExists(Guid user1, Guid user2) 
        {
            if (!balanceSheet.ContainsKey(user1))
            {
                balanceSheet[user1] = new Dictionary<Guid, float>();
            }

            if (!balanceSheet[user1].ContainsKey(user2))
            {
                balanceSheet[user1].Add(user2, 0);
            }
        }
    }
}
