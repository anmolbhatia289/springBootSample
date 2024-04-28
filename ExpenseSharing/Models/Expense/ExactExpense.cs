using ExpenseSharing.Exceptions;

namespace ExpenseSharing.Models
{
    public class ExactExpense : Expense
    {
        public ExactExpense(string name, string description, Guid paidBy, float amount)
        {
            this.id = Guid.NewGuid();
            this.name = name;
            this.description = description;
            this.types = ExpenseType.EXACT;
            this.paidBy = paidBy;
            this.amount = amount;
            this.splitStrategy = new ExactSplitStrategy();
        }

        public override void validateExpense(List<SplitView> splits)
        {
            float currentAmount = 0;
            foreach (SplitView splitView in splits) 
            {
                var exactSplit = splitView as ExactSplitView;
                currentAmount += exactSplit.getAmount();
            }

            if (currentAmount != amount) throw new InvalidSplitException("Individual splits do not add up to total expense amount");
        }
    }
}
