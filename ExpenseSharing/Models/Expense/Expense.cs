namespace ExpenseSharing
{
    public abstract class Expense
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public ExpenseType types { get; set; }
        public List<SplitView> splits { get; set; }
        public Guid paidBy { get; set; }
        public SplitStrategy splitStrategy { get; set; }
        public float amount { get; set; }

        public abstract void validateExpense(List<SplitView> splits);

        public List<Split> getSplits(List<SplitView> splitViews)
        {
            return this.splitStrategy.getSplits(this, splitViews);
        }
    }
}
