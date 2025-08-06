namespace ExpenseSharing.Managers
{
    public class ExpenseRepository
    {
        private Dictionary<Guid, Expense> _expenses;
        public static ExpenseRepository _instance;
        private ExpenseRepository()
        {
            _expenses = new Dictionary<Guid, Expense>();
        }

        public static ExpenseRepository getInstance()
        {
            if (_instance == null)
            {
                _instance = new ExpenseRepository();
            }

            return _instance;
        }

        public void addExpense(Expense expense) 
        {
            _expenses.Add(expense.id, expense);
        }

        public void removeExpense(Guid id) 
        {
            _expenses.Remove(id);
        }

        public Expense getExpense(Guid id) 
        {
            return _expenses[id];
        }
    }
}
